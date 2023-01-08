<?php
include_once 'parts/header.php';

$krypto = $_GET['c'];
$api_url = 'https://localhost:7070/api/Crypto/'.$krypto ;

$response = GetAPI($api_url,false);


$times = explode("T", $response->metaData->lastRefreshed);
// "information": "Daily Prices and Volumes for Digital Currency",
//     "dcCode": "BTC",
//     "dcName": "Bitcoin",
//     "marketCode": "USD",
//     "marketName": "United States Dollar",
//     "lastRefreshed": "2022-12-25T00:00:00",
//     "timeZone": "UTC"
?>
            <!-- Description Start -->
            <div class="container-fluid pt-4 px-4">
                <div class="row g-4">
                    <div class="col-sm-12 col-xl-12">
                        <div class="bg-secondary rounded d-flex align-items-center p-4">
                            <a href="krypto.php"><i class="fa  fa-arrow-left fa-3x text-primary"></i></a>
                            <div class="ms-3">
                                <h1 class="text-body mb-2"><?php echo $response->metaData->dcName;?> Symbol: <?php echo $response->metaData->dcCode;?></h1>
                                <h6 class="mb-2">Market Code: <?php echo $response->metaData->marketCode;?> Market Name: <?php echo $response->metaData->marketName;?></h6>
                                <h6 class="mb-2">Last Time Refreshed: <?php echo $times[0];?>  Time Zone: <?php echo $response->metaData->timeZone;?></h6>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Description End -->

            <?php
                $response = GetAPI($api_url,false);
            ?>


            <!-- Table Start -->
            <div class="container-fluid pt-4 px-4">
                <div class="row g-4">
                    <div class="col-12">
                        <div class="bg-secondary rounded h-100 p-4">
                            <h6 class="mb-4">Daily info from past year</h6>
                            <div class="table-responsive">
                                <table id="dtDynamicVerticalScrollExample" class="table mb-2">
                                    <thead>
                                        <tr>
                                            <th scope="col">Time</th>
                                            <th scope="col">Open</th>
                                            <th scope="col">High</th>
                                            <th scope="col">Low</th>
                                            <th scope="col">Close</th>
                                        </tr>
                                    </thead>
                                    <tbody id="tableBody">
                                        <?php
                                        foreach ($response->ohlcvCryptoData as &$c)
                                        {
                                            $ohlcTime = explode("T", $c->time);
                                            ?>
                                            <th scope="row"><?php echo $ohlcTime[0];?></th>
                                            <td><?php echo number_format($c->openUSD,2, ',', ' ');?></td>
                                            <td><?php echo number_format($c->highUSD,2, ',', ' ');?></td>
                                            <td><?php echo number_format($c->lowUSD,2, ',', ' ');?></td>
                                            <td><?php echo number_format($c->closeUSD,2, ',', ' ');?></td>
                                        </tr>           
                                            <?php
                                        } 
                                        ?>                     
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Table End -->

            <!-- Chart Start -->
            <div class="container-fluid pt-4 px-4">
                <div class="row g-4">
                    <div class="col-sm-12 col-xl-12">
                        <div id="chart1"class="bg-secondary rounded h-100 p-4">
                            <div class="ChartTop mb-4">
                                <text>Single Line Chart</text>
                                <div class="btn-group">
                                    <button type="button" class="btn btn-primary shadow-none" onclick="changeChartType('D')" id="Daily">Daily</button>
                                    <button type="button" class="btn btn-primary shadow-none" onclick="changeChartType('W')" id="Weekly">Weekly</button>
                                    <button type="button" class="btn btn-primary shadow-none" onclick="changeChartType('M')" id="Monthly">Monthly</button>
                                    <?php
                                    if(isset($_SESSION["id"]))
                                    {
                                    ?>
                                    <button type="button" class="btn btn-primary shadow-none" onclick="changeChartType('P')" id="Predict">Predict</button>
                                        <?php
                                        }
                                        else
                                        {
                                            ?>
                                    <button type="button" class="btn btn-primary shadow-none" onclick="signupInfo()" id="Predict">Predict</button>

                                            <?php
                                        }
                                        ?>
                                </div>
                            </div>
                            <div id="access" class="alert alert-warning alert-danger fade collapse" role="alert">
                            <strong>Sorry!</strong> Option only for registered users. <a href="signup.php">Sign up</a> to get more access 
                            <button type="button"  class="close shadow-none" onclick="hideAllert()">
                                <span aria-hidden="true">&times;</span>
                            </button>
                            </div>
                            <canvas id="liniowy"></canvas>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Chart End -->

            <script>
    document.getElementById("navkrypto").classList.add('active');

    function signupInfo()
    {
        $('#access').show();
        document.getElementById("access").classList.add("show");
    }

    function hideAllert()
    {//hide
     $('#access').hide();
    }

    // Chart Global Color
    Chart.defaults.color = "#6C7293";
    Chart.defaults.borderColor = "#000000";

    //Months to table 
    function subtractMonths(date) {
        date.setMonth(date.getMonth() - 1);
        return date;
    }
    //Weeks to table
    function subtractWeek(date) {
        date.setDate(date.getDate() - 7)
        return date;
    }
    //Days to table
    function subtractDays(date, day) {
        date.setDate(date.getDate() - 1)
        return date;
    }
    function subtractPrediction(date) {
        date.setDate(date.getDate() - 1)
        return date;
    }

    var xValues = [];
    var chartType = false;
    var currentCrypto = "<?php echo $krypto;?>";


    $( document ).ready(function() {
            document.getElementById("Daily").classList.add("active");
            setChartxValues();
            $('#dtDynamicVerticalScrollExample').DataTable({
                "scrollY": "60vh",
                "scrollCollapse": true,
            });
            $('.dataTables_length').addClass('bs-select');

        })

    function setChartxValues()
    {
        var today = new Date(); // yyyy-mm-dd

        var xValues = [];
        var num = 0;
        if(chartType == false)
            num = 365;
        else if(chartType == "Weekly")
            num = 52;
        else if(chartType == "Predict")
            num = 14;
        else 
            num = 12;
        var days = today.toLocaleDateString();
        xValues.push(days);
        for(let i = 0; i < num; i++)
        {
            switch (chartType) {
                case false:
                    var days = subtractDays(today);
                    break;
                case "Weekly":
                    var days = subtractWeek(today);
                    break;
                case "Monthly":
                    var days = subtractMonths(today);
                    break;
                case "Predict":
                    var days = subtractDays(today);
                    break;
                default:
                    break;
            }
        }
        xValues = xValues.reverse();
        myChart3.data.labels = xValues;
        myChart3.data.datasets[0].data = getCrypto(chartType, currentCrypto);

        myChart3.update();
    }
    

    var data = [];
    var label = currentCrypto;

    //Change Chart Type
    function changeChartType(type)
    {
        
        document.getElementById("Daily").classList.remove("active");
        document.getElementById("Weekly").classList.remove("active");
        document.getElementById("Monthly").classList.remove("active");
        document.getElementById("Predict").classList.remove("active");

        
        switch (type) {
            case "D":
            {
                document.getElementById("Daily").classList.add("active");
                chartType = false;
            }
                break;
            case "W":
            {
                document.getElementById("Weekly").classList.add("active");
                chartType = "Weekly";
            }
                break;
            case "M":
            {
                document.getElementById("Monthly").classList.add("active");
                chartType = "Monthly";
            }
                break;
            case "P":
            {
                document.getElementById("Predict").classList.add("active");
                chartType = "Predict";
            }
                break;
        
            default:
                break;
        }
        setChartxValues();

        myChart3.data.datasets[0].data = getCrypto(chartType, currentCrypto);
        myChart3.data.labels = xValues;
        myChart3.update()
    }


    //Get crypto data with offset type
    function getCrypto(type, symbol)
    {
        if(!type)
            var link = "https://localhost:7070/api/Crypto/"+symbol;
        else if(type == "Predict")
        {
            console.log(type == "Predict")
            var link = "https://localhost:7070/api/Crypto/predict/"+symbol;
        }
        else
            var link = "https://localhost:7070/api/Crypto/"+type+"/"+symbol;
        var data = [];
        $.ajax({
                url: link,
                type: 'GET',
                async: false,
                dataType: 'json',
                cors: false ,
                contentType:'application/json',
                success: function (APIdata){
                    xValues = [];
                    label =  APIdata["metaData"]["dcName"];
                    if(type =="Predict")
                    for(let i = 0; i < 21;i++)
                        {
                            var time = APIdata["ohlcvCryptoData"][APIdata["ohlcvCryptoData"].length-21+i]["time"].split('T');
                            xValues.push(time[0]);
                            data.push(APIdata["ohlcvCryptoData"][APIdata["ohlcvCryptoData"].length-21+i]["closeUSD"]);
                        }
                    else
                        for(let i = 0; i < APIdata["ohlcvCryptoData"].length;i++)
                        {
                            var time = APIdata["ohlcvCryptoData"][i]["time"].split('T');
                            xValues.push(time[0]);
                            data.push(APIdata["ohlcvCryptoData"][i]["closeUSD"]);
                        }
                }
                
            });

        return data;
    }
    
    // Single Line Chart
    var ctx3 = $("#liniowy").get(0).getContext("2d");
    var myChart3 = new Chart(ctx3, {
        type: 'line',
        data: {
            labels: xValues,
            datasets: [{
                label: label,
                data: data[0],
                backgroundColor: "rgba(235, 22, 22, .7)",
            }]
        },
        options: {
            responsive: true,
        }
    });
    </script>

    <?php
    include_once 'parts/footer.php';
    ?>
            