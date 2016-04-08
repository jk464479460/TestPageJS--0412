$(function () {
    function getvalue(name) {
        var str = window.location.search; //location.search是从当前URL的?号开始的字符串 例如：http://www.51job.com/viewthread.jsp?tid=22720 它的search就是?
        tid = 22720;
        if (str.indexOf(name) != -1) {
            var pos_start = str.indexOf(name) + name.length + 1;
            var pos_end = str.indexOf("&", pos_start);
            if (pos_end == -1) {
                alert(str.substring(pos_start));
            } else {
                alert("对不起这个值不存在！");
            }
        }
    }

    getvalue("key");
    var xAxis = [],xData=[];
    $.ajax({
        url: "../Service/EnergyDataService.svc/GetChartData",
        type: "POST",
        dataType: "json",
        contentType: 'application/json',
        data:"",
        success: function (data, textStatus) {
            var obj;
            if (typeof (JSON) == 'undefined')
                obj = eval("(" + data+ ")");
            else obj = JSON.parse(data);
            console.log(obj);
            for (var i = 0; i < obj.length; i++) {
                xAxis.push(obj[i].Time);
                xData.push(obj[i].Val);
            }
        },
        error: function (data, status, e) {
            alert("错误信息：" + e);
        }
    });
    var options = {
        chart: {
            type: 'line'
        },
        title: {
            text: '用能走势图'
        },
        subtitle: {
            text: 'XXXXX'
        },
        xAxis: {
            categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec']
        },
        yAxis: {
            title: {
                text: '度(kWh)'
            }
        },
        tooltip: {
            enabled: true,
            formatter: function() {
                return '<b>' + this.series.name + '</b><br/>' + this.x + ': ' + this.y + 'kWh';
            }
        },
        plotOptions: {
            line: {
                dataLabels: {
                    enabled: true
                },
                enableMouseTracking: true
            }
        },
        series: [
            {
                name: 'XXXX',
                data: [7.0, 6.9, 9.5, 14.5, 18.4, 21.5, 25.2, 26.5, 23.3, 18.3, 13.9, 9.6]
            }
        ]
    };
    options.xAxis.categories = xAxis;
    options.series[0].data = xData;
    $('#container').highcharts(options);
});