$(function() {
    var cookie = getCookie('admin');

    if (cookie == null) {
    } //location.href = "../login.htm";
    else {

    }
    $('#loginOut').live('click', function() {
        delCookie('admin');
    });

    var dates = $("#startDate,#endDate");
    dates.datepicker({
        onSelect: function(selectedDate) {
            var option = this.id == "startDate" ? "minDate" : "maxDate";
            dates.not(this).datepicker("option", option, selectedDate);
        },
        changeMonth: true,
        changeYear: true,

    });

    $('#loginOut').live('click', function() {
        delCookie('admin');
    });

    /*修改数据保存*/
    $('.btModif').live('click', function() {
        var timeType = $("#timeType").val();
        var timeId = $(this).attr('timeId');
        var devId = $(this).attr('devId');
        var val = $(this).parent('td').prev('td').html();
        var data = { TimeType: timeType, DevNum: devId, TimeId: timeId, Val: val };
        var dataStr = JSON.stringify(data);
        $.ajax({
            url: page1.url().UpdateInsertEnergyData,
            type: "POST",
            dataType: "json",
            contentType: 'application/json',
            data: dataStr, // { "Inputs":  dataStr},
            success: function(data, textStatus) {
                var obj = JSON.parse(data);
                if (obj.Exception.Success) {
                    alert("保存数据成功");
                    $('.btModif').css('display', 'none');
                } else {
                    alert("保存数据失败");
                }
            },
            error: function(data, status, e) {
                alert("错误信息：" + e);
            }
        });

        queryData.Page.CurrPage = 1;
        page1.QueryEnergyData(1);
    });

    $(".editeTd").live("dblclick", function() {
        oldVal = $(this).html(); //获取旧数据
        objTd = this;
        ShowDiv();
    });
    $(".editeTd1").live("dblclick", function() {
        oldVal = $(this).html(); //获取旧数据
        objTd = this;
        ShowDiv();
    });

    $("#firstPage").click(function() {
        queryData.Page.CurrPage = 1;
        page1.QueryEnergyData(1);
    });
    //
    $("#previousPage").click(function() {
        var pPage = $("#currPage").text();
        if (pPage <= 1) {
            alert("已经是首页");
            return;
        }
        queryData.Page.CurrPage = pPage - 1;
        page1.QueryEnergyData(queryData.Page.CurrPage);
    });
    $("#nextPage").click(function() {
        var pPage = parseInt($("#currPage").text());
        var totalNum = $("#totalPage").text();
        if (pPage >= totalNum) {
            alert("已经是尾页");
            return;
        }

        queryData.Page.CurrPage = pPage + 1;
        page1.QueryEnergyData(queryData.Page.CurrPage);

    });


    $("#lastPage").click(function() {
        var totalNum = $("#totalPage").text();
        queryData.Page.CurrPage = totalNum;
        page1.QueryEnergyData(totalNum);
    });
    /*跳转指定页面*/
    $("#GetSpeciPage").click(function() {
        var speciNum = $("#numPage").val();
        if (speciNum === "" || speciNum == null) {
            layer.msg('页码未填写！');
            return;
        }
        speciNum = $.trim(speciNum);
        var pPage = parseInt(speciNum);
        var totalNum = $("#totalPage").text();
        if (pPage > totalNum) {
            alert("页码错误");
            return;
        }
        if (pPage <= 0) {
            alert("页码错误");
            return;
        }
        queryData.Page.CurrPage = pPage;
        page1.QueryEnergyData(queryData.Page.CurrPage);
    });
    /*导出excel数据*/
    $('#btExport').click(function() {
        page1.ExcelExport();
    });

    /*绑定设备*/
    page1.initDevList();

    $("#btQuery").click(function() {
        if ($("#startDate").val().length === 0 || $("#endDate").val().length === 0) {
            layer.msg('日期未选择');
            return;
        }
        page1.QueryEnergyData(1);
    });

    $("#jumpChart").click(function() {
        window.open("html/chart.htm?key=" + iputStrGlobal, "_blank");
    });
});//end-load

function Page1() {

}

Page1.prototype = {
  
    url: function () {
        return {
            GetEnerPageData: "../Service/ServiceTest.svc/GetJson",
            GetDeviceList: "../Service/ReportDataView.svc/GetDeviceList",
            GetEnergyDataList: "../Service/EnergyDataService.svc/GetEnergyDataList",
            GetPageInfo: "../Service/EnergyDataService.svc/GetPageInfo",
            UpdateInsertEnergyData: "../Service/EnergyDataService.svc/UpdateInsertEnergyData",
            ExportExcel: "../Service/EnergyDataService.svc/ExportExcel"
        };
    },
    initDevList: function getDevList() {
        $.ajax({
            url: this.url().GetDeviceList,
            type: "POST",
            dataType: "json",
            contentType:'application/json',
            //data: //{ "Inputs": null },
            success: function (data, textStatus) {
                var obj;
                if (typeof (JSON) == 'undefined')
                    obj = eval("("+data.d+")");
                else obj= JSON.parse(data);
                new Page1().SettingIntelliData(obj);
            },
            error: function (data, status, e) {
                alert("错误信息：" + e);
            }
        });
    },
    SettingIntelliData: function (json) {
        oo.Data = [];
        for (var i = 0; i < json.DevList.length; i++) {
            var obj = json.DevList[i];
            oo.Data.push(obj.Cname + "@" + obj.DevNum);
        }
        oo.Create(document.getElementById('abc'));   
    },
    GetQuerJson: function (currNum) {
        var devNameId = $("#abc").val();
        
        devNameId = $.trim(devNameId);
        var arr = devNameId.split('@');
        var devNum = arr[1];
        var cName = arr[0];
        var timeType = $("#timeType").val();
        var startTime = $("#startDate").val();
        var endTime = $("#endDate").val();
      
        var energyType = $("#energyType").val();
        var currPage = currNum;
        var num = $("#num").val();
        
        queryData.DevNum = devNum;
        queryData.Cname = cName;
        queryData.TimeType = timeType;
        queryData.StartTime = startTime;
        queryData.EndTime = endTime;
        queryData.EnergyType = energyType;
        queryData.Page.CurrPage = currPage;
        queryData.Page.NumAPage = num;
      
    },
    GetQueryExport:function() {
        var devNameId = $("#abc").val();
        devNameId = $.trim(devNameId);
        var arr = devNameId.split('@');
        var devNum = arr[1];
        var cName = arr[0];
        var timeType = $("#timeType").val();
        var startTime = $("#startDate").val();
        var endTime = $("#endDate").val();
        var energyType = $("#energyType").val();
        dataStr = {
            DevNum:devNum, "Cname":cName, TimeType:timeType, "StartTime":startTime,
            "EndTime":endTime, EnergyType:energyType
        };
    },
    ExcelExport: function () {
        new Page1().GetQueryExport();
        var iputStr;
        if (typeof (JSON) == 'undefined')
            iputStr = eval("(" + dataStr + ")");
        else
            iputStr = JSON.stringify(dataStr);
        
        $.ajax({
            url: this.url().ExportExcel,
            type: "POST",
            dataType: "json",
            contentType: "application/x-www-form-urlencoded; charset=utf-8",
            data: { "Inputs": iputStr },
            success: function (data, textStatus) {
                var obj = JSON.parse(data);
                if (obj.Exception.Success) {
                    window.open(obj.Exception.Exsg);
                } else {
                    layer.msg("导出错误");
                }
            },
            error: function (data, status, e) {
                alert("错误信息：" + e);
            }
        });
    },
    /*查询能耗数据*/
    QueryEnergyData: function (currNum) {
        new Page1().GetQuerJson(currNum);

        var iputStr;
        if (typeof (JSON) == 'undefined')
            iputStr = eval("(" + queryData + ")");
        else
            iputStr = JSON.stringify(queryData);
        iputStrGlobal = iputStr;
        $.ajax({
            url: this.url().GetEnergyDataList,
            type: "POST",
            beforeSend: function () { layer.load(30); },
            complete:function () { layer.closeAll(); },
            dataType: "json",
            contentType: 'application/json',
            data: iputStr,//{ "Inputs": iputStr },
            success: function (data, textStatus) {
                var obj = JSON.parse(data);
                if (obj.Exception.Success) {
                    $("#currPage").html();
                    $("#currPage").html(currNum);
                   
                    new Page1().ShowDataList(obj);
                }
                
            },
            error: function (data, status, e) {
                alert("错误信息80：" + e);
            }
        });

        $.ajax({
            url: this.url().GetPageInfo,
            type: "POST",
            dataType: "json",
            contentType: "application/json",
            data: iputStr,//{ "Inputs": iputStr },
            success: function (data, textStatus) {
                var obj = JSON.parse(data);
                if (obj.Exception.Success) {
                    $("#totalPage").html(obj.Page.Num);
                    
                }
                else {
                    alert(obj.Exception.Exsg);
                }
            },
            error: function (data, status, e) {
                alert("错误信息90：" + e);
            }
        });
    },
    ShowDataList: function (json) {
        if (json.DevList == null || json.DevList.length == 0) {
            alert("无数据");
            return;
        }
        var $tbody = $("#tpl");
        $tbody.empty();
        var items = template('dataItem', json);
        $tbody.html(items);
    }
};

function ShowDiv() {
    /*弹出图层*/
    pageii = $.layer({
        type: 1,
        title: "修改数据",
        area: ['auto', 'auto'],
        border: [10, 0.3, "#000"], //去掉默认边框
        shade: [0.5], //去掉遮罩
        closeBtn: [0, false], //去掉默认关闭按钮
        shift: null, //从左动画弹出
        page: {
            html: '<div style="width:220px; height:80px; padding:20px; border:1px solid #ccc; background-color:#eee;">' +
                '<input type="text" id="txtModifyVal" style="width:90px;"/><button id="pagebtn" class="btns" onclick="">确定</button></div>'
        }
    });
}

$("#pagebtn").live('click', function () {
    newVal = $('#txtModifyVal').val();
    newVal = $.trim(newVal);
    var oldInt = parseFloat(oldVal);
    var newInt = parseFloat(newVal);
    
    if (!(oldInt == newInt || newVal =="")) {
        oldInt = newInt;
        $(objTd).html(oldInt);
        $(objTd).next('td').children('input').css('display', 'inline');
    }
    layer.close(pageii); 
});

var page1 = new Page1();
var oo = new mSift('oo', 10);
var queryData={"DevNum":"","Cname":"","TimeType":"","StartTime":"","EndTime":"","EnergyType":"",
                "Page":{"CurrPage":"","NumAPage":""}};
var pageii;
var oldVal, newVal;
var objTd;
var dataStr;
var iputStrGlobal;