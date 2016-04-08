using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Xml;

namespace ServiceLib
{
    public class DataCommonConfig
    {
        private  Dictionary<string,string> configSqlList = null;
        private  string[] filesList ;

        public Dictionary<string,string> ConfigSqlList{get { return configSqlList; }} 
        public DataCommonConfig()
        {
            configSqlList=new Dictionary<string, string>();
            LoadSqlConfig();
        }
        //读取全部配置的sql
        private void LoadSqlConfig()
        {
            filesList = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory+@"bin\Data");
            GetConfigSqlList();
        }
        //生成数据字典
        private void GetConfigSqlList()
        {
            foreach (var config in filesList)
            {
                GetKeyAndSql(config);
            }
        }
        //根据配置文件获取key和sql
        private void GetKeyAndSql(string fileName)
        {
            var loadXml = new XmlDocument();
            loadXml.Load(fileName);

            //var nodeList=loadXml.GetElementsByTagName("Sql");
            var nodeList = loadXml.SelectNodes("SqlList/Sql");
            foreach (var ele in nodeList)
            {
                var node = ele as XmlElement;
                if(node ==null) throw new Exception("定位节点失败");
                var key =node.GetAttribute("Name");
                var sql = node.InnerText;
                InsertKV(key,sql);
            }
            /*database.config */
            nodeList= loadXml.SelectNodes("DatabaseList/Database");
            if(nodeList == null) throw new Exception("数据库节点配置问题");
            foreach (var ele in nodeList)
            {
                var node = ele as XmlElement;
                var innerXml=node.InnerXml;
                if (node == null) throw new Exception("定位节点失败");
                var key = node.GetAttribute("Name");
                var sql = node.InnerText;
                InsertKV(key,sql);
            }
        }

        private void InsertKV(string key, string val)
        {
            if (configSqlList.ContainsKey(key)) 
                throw new Exception(key + "重复键");
            configSqlList.Add(key, val);
        }
    }

    /*使用列子
     * public class MyClass
    {
        [DataMapping("ParentId", "ParentId", DbType.Int32)]
        public int ParentId { get; set; }
        [DataMapping("Id", "ID", DbType.Int32)]
        public int Id { get; set; }
        [DataMapping("CName", "CName", DbType.String)]
        public string CName { get; set; }
    }
     * */

    public class DataMappingAttribute : Attribute
    {
        private string localName, dbName;
        private string type;
        public DataMappingAttribute(string a, string b, DbType type)
        {
            this.localName = a;
            this.dbName = b;
            this.type = "System." + type.ToString();
        }

        public string LocalName
        {
            get { return localName; }
        }
        public string DbName { get { return dbName; } }
        public string DataType { get { return type; } }
    }

    public class DataCommand
    {
        private static string connStr = "";
        private static DataCommonConfig xmlConfig = null;

        public DataCommand()
        {
            xmlConfig=new DataCommonConfig();
            connStr = xmlConfig.ConfigSqlList[((KEYSSET) 1).ToString()].Trim();
        }
        public List<T> Exe<T>(string cmdStr, IEnumerable<Tuple<string,string>> param)
        {
            var resList = new List<T>();
            /*参数处理*/
            cmdStr = xmlConfig.ConfigSqlList[cmdStr];
            if(param!=null)
            cmdStr = param.Aggregate(cmdStr, (current, p) => current.Replace(p.Item1, p.Item2));
           
            var dt = new DataTable();
            /*数据库字段与Datatable绑定*/
            var type = typeof (T);
            var fieldList = type.GetProperties();
            foreach (var filed in fieldList)
            {
                var attributes = filed.GetCustomAttributes(typeof (DataMappingAttribute), false);
                var ab = (DataMappingAttribute) attributes[0];
                dt.Columns.Add(ab.DbName, Type.GetType(ab.DataType));
            }
            
            using (var connection = new SqlConnection(connStr))
            {
                connection.Open();
                var cmd = new SqlCommand {Connection = connection};
                cmd.CommandText = cmdStr;
                var dataAdapter = new SqlDataAdapter(cmd);
                dataAdapter.Fill(dt);
                connection.Close();
            }

            foreach (DataRow row in dt.Rows)
            {
                var objT = Activator.CreateInstance<T>();
                foreach (var filed in fieldList)
                {
                    var attributes = filed.GetCustomAttributes(typeof (DataMappingAttribute), false);
                    var tempName = "";
                    foreach (var attr in attributes)
                    {
                        var attri = (DataMappingAttribute) attr;
                        if (attri.LocalName.Equals(filed.Name))
                            tempName = attri.DbName;
                    }

                    if (!filed.CanWrite || string.IsNullOrWhiteSpace(tempName)) continue;
                    filed.SetValue(objT, row[tempName] == DBNull.Value ? -1m : row[tempName], null);
                }
                resList.Add(objT);
            }
            return resList;
        }
    }
    /*存储预定义的关键字*/
    internal enum KEYSSET
    {
        Database_DBRW=1
    }
}
