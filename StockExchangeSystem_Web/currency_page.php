<?php
include_once 'parts/header.php';

$currency = $_GET['c'];
$api_url = 'https://localhost:7070/api/Currency/'.$currency ;

$response = GetAPI($api_url,false);


$times = explode("T", $response->metaData->lastRefreshed);
?>
            <!-- Description Start -->
            <div class="container-fluid pt-4 px-4">
                <div class="row g-4">
                    <div class="col-sm-12 col-xl-12">
                        <div class="bg-secondary rounded d-flex align-items-center p-4">
                            <a href="krypto.php"><i class="fa  fa-arrow-left fa-3x text-primary"></i></a>
                            <div class="ms-3">
                                <h1 class="text-body mb-2"><?php echo $response->metaData->information;?></h1>
                                <h1 class="text-body mb-2">Symbol: <?php echo $response->metaData->fromSymbol;?></h1>
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
                                        foreach ($response->ohlcData as &$c)
                                        {
                                            $ohlcTime = explode("T", $c->time);
                                            ?>
                                            <th scope="row"><?php echo $ohlcTime[0];?></th>
                                            <td><?php echo number_format($c->open,2, ',', ' ');?></td>
                                            <td><?php echo number_format($c->high,2, ',', ' ');?></td>
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
    document.getElementById("navwaluty").classList.add('active');

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

    var xValues = [];
    var chartType = false;
    var currentCrypto = "<?php echo $currency;?>";

    var xValues = [];
    var chartType = false;

    $( document ).ready(function() {
            document.getElementById("Daily").classList.add("active");
            myChart3.data.datasets[0].data = getCurrency(chartType, currentCrypto);
            myChart3.data.labels = xValues;
            myChart3.update();
            $('#dtDynamicVerticalScrollExample').DataTable({
                "scrollY": "60vh",
                "scrollCollapse": true,
            });
            $('.dataTables_length').addClass('bs-select');

        })

    
    var data = [];
    var label = currentCrypto;

    //Change Chart Type
    function changeChartType(type)
    {
        
        document.getElementById("Daily").classList.remove("active");
        document.getElementById("Weekly").classList.remove("active");
        document.getElementById("Monthly").classList.remove("active");

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
        myChart3.data.datasets[0].data = getCurrency(chartType, currentCrypto);
        myChart3.data.labels = xValues;
        myChart3.update();
    }


    //Get currency data with offset type
    function getCurrency(type, symbol)
    {
        if(!type)
            var link = "https://localhost:7070/api/Currency/"+symbol;
        else if(type == "Predict")
        {
            console.log(type == "Predict")
            var link = "https://localhost:7070/api/Currency/predict/"+symbol;
        }
        else
            var link = "https://localhost:7070/api/Currency/"+type+"/"+symbol;
        var data = [];
        
        $.ajax({
                url: link,
                type: 'GET',
                async: false,
                dataType: 'json',
                cors: false ,
                contentType:'application/json',
                success: function (APIdata){
                    label =  APIdata["metaData"]["fromSymbol"];
                    xValues = [];
                    if(type =="Predict")
                    for(let i = 0; i < 21;i++)
                        {
                            var time = APIdata["ohlcData"][APIdata["ohlcData"].length-21+i]["time"].split('T');
                            xValues.push(time[0]);
                            data.push(APIdata["ohlcData"][APIdata["ohlcData"].length-21+i]["closeUSD"]);
                        }
                    else
                    {
                        for(let i = 0; i < APIdata["ohlcData"].length;i++)
                        {
                            var time = APIdata["ohlcData"][i]["time"].split('T');
                            xValues.push(time[0]);
                            data.push(APIdata["ohlcData"][i]["closeUSD"]);
                        }
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
            