
var ctx = document.getElementById("myChart").getContext("2d");
var myChart = new Chart(ctx, {});

$(document).ready(function () {

    $.ajax({
        type: "GET",
        url: "/Dashboard/GetDashboardDatas",
        success: data => {



            $(".violators").html(data.ViolatorCount);
            $(".accidents").html(data.AccidentCount);
            $(".amount").html(data.TotalPaidAmount);
            $(".approved").html(data.ApproveContestCount);
            $(".pending").html(data.PendingContestCount);
            $(".rejected").html(data.RejectedContestCount);

        }
    })

    getChartData("Accident");

  
});


$(".data-chart-select").on("change", function () {
   getChartData($(this).val())
})


function getChartData(typeOfData) {
    $.ajax({
        type: "GET",
        contentType: "application/x-www-form-urlencoded",
        url: "/Dashboard/GetDatasMonthByType",
        data: {type:typeOfData},
        success: data => {
            console.log(data)
            myChart.destroy();
             myChart = new Chart(ctx, {
                type: "bar",
                data: {
                    labels: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"],
                    datasets: [
                        {
                            label: "Months",
                            data: [data.Jan, data.Feb, data.Mar, data.Apr, data.May, data.Jun, data.Jul, data.Aug, data.Sep, data.Oct, data.Nov, data.Dec],
                            backgroundColor: [],
                            borderColor: [
                                "rgba(3, 80, 111, 1)",
                                "rgba(3, 80, 111, 1)",
                                "rgba(3, 80, 111, 1)",
                                "rgba(3, 80, 111, 1)",
                                "rgba(3, 80, 111, 1)",
                                "rgba(3, 80, 111, 1)",
                                "rgba(3, 80, 111, 1)",
                                "rgba(3, 80, 111, 1)",
                                "rgba(3, 80, 111, 1)",
                                "rgba(3, 80, 111, 1)",
                                "rgba(3, 80, 111, 1)",
                                "rgba(3, 80, 111, 1)",
                            ],
                            borderWidth: 1,
                        },
                    ],
                },
                options: {
                    scales: {
                        y: {
                            beginAtZero: true,
                        },
                    },
                },
            });

            
        }
    })
}


