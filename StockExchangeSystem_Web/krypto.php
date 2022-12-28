<?php
include_once 'parts/header.php';
$api_url = 'https://localhost:7070/api/Crypto';

?>


            <!-- Description Start -->
            <div class="container-fluid pt-4 px-4">
                <div class="row g-4">
                    <div class="col-sm-12 col-xl-12">
                        <div class="bg-secondary rounded d-flex align-items-center p-4">
                            <i class="fa-brands fa-bitcoin fa-3x text-primary"></i>
                            <div class="ms-3">
                                <h1 class="text-body mb-2">Kryptowaluty</p>
                                <h6 class="mb-0">Inwestycje w kryptowaluty uznawane są za ryzykowne, ale jednocześnie dające szanse na osiągnięcie wręcz niewyobrażalnych zysków.</h6>
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
                            <h6 class="mb-4">Najpopularniejsze kryptowaluty</h6>
                            <div class="table-responsive">
                                <table id="example" class="table">
                                    <thead>
                                        <tr>
                                            <th scope="col"> </th>
                                            <th scope="col">#</th>
                                            <th scope="col">Logo</th>
                                            <th scope="col">Nazwa</th>
                                            <th scope="col">Symbol</th>
                                            <th scope="col">Cena(USD)</th>
                                            <th scope="col">Cał. wol.</th>
                                            <th scope="col">Zm. (24h)</th>
                                            <th scope="col">Zm. (7d)</th>
                                            <th scope="col">Info.</th>
                                        </tr>
                                    </thead>
                                    <tbody id="tableBody">
                                        
                                        <?php
                                        
                                        $i = 1;
                                        foreach ($response as &$c)
                                        {
                                            ?>
                                            <tr id="<?php echo $c->symbol;?>" class="krypto">
                                            <td><input id="<?php echo $c->symbol."chk";?>" class="kryptocheck" type="checkbox"></td>
                                            <th scope="row"><?php echo $i;?></th>
                                            <td><img class="coin-logo" src="https://cryptoicons.org/api/icon/<?php echo strtolower($c->symbol);?>/200" loading="lazy" alt="BTC logo"></td>
                                            <td><?php echo $c->name;?></td>
                                            <td id="symbol"><?php echo $c->symbol;?></td>
                                            <td><?php echo number_format($c->value,2, ',', ' ');?></td>
                                            <td><?php echo number_format($c->volume,2, ',', ' ');?></td>
                                            <td><?php echo number_format($c->changeDay,2, ',', ' ')."%";?></td>
                                            <td><?php echo number_format($c->changeWeek,2, ',', ' ')."%";?></td>
                                            <td><a href="strona_krypto.php?c=<?php echo $c->symbol;?>" type="button" class="btn btn-primary shadow-none">More</a></td>
                                        </tr>           
                                            <?php
                                            $i++;
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
                                </div>
                            </div>
                            
                            <canvas id="liniowy"></canvas>
                        </div>
                    </div>
                    <div class="col-sm-12 col-xl-12">
                        <div class="bg-secondary rounded h-100 p-4">
                            <h6 class="mb-4">Comparison Line Chart</h6>
                            <canvas id="porownanie"></canvas>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Chart End -->

            <script>
    document.getElementById("navkrypto").classList.add('active');

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

    var xValues = [];
    var chartType = false;

    $( document ).ready(function() {
            document.getElementById("Daily").classList.add("active");
            setChartxValues();
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
                default:
                    break;
            }
            
            var days = days.toLocaleDateString();
            xValues.push(days);
        }
        xValues = xValues.reverse();
        myChart3.data.labels = xValues;
        myChart2.data.labels = xValues;
        myChart3.update();
        myChart2.update();
    }
    

    var data = [];
    var label = "Brak";

    // Comparison Line Chart
    var count = 0
    var queue = []
    let collection = document.querySelectorAll(".kryptocheck");
    

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
        
            default:
                break;
        }
        setChartxValues();

        
        myChart3.data.datasets[0].data = getCrypto(chartType, currentyCrypto);
        myChart3.update()
    }

    // Handling checkbox click
    for (let i = 0; i < collection.length; i++) {
        collection[i].addEventListener('change', ()=>{
            if(collection[i].checked){
                count++

                // Changing chart data
                //whichChartChange(i, true)

                if(count >= 2){
                    checkBoxBLock(collection, true)
                }
                //myChart2.update()
            }
            else{
                count--
                queue.push(i)
                if(count < 2){
                    checkBoxBLock(collection, false)
                }
                //myChart2.update()
            }
            cmpChartUpdate();
        })
    }

    function checkBoxBLock(items, disable){
        for (let item of items) {
            if(disable){
                if(item.checked == false)
                {
                    item.disabled=true;
                }
            }
            else{
                if(item.disabled == true)
                {
                    item.disabled=false;
                }
            }
        }
    }

    function cmpChartUpdate(){
        let ilosc = 0;
        let a = 1;
    for(let i=0; i<collection.length; i++){
        if(collection[i].checked == true && ilosc<2){
            myChart2.data.datasets[ilosc].data = getCrypto(chartType, coins[i].id);
            myChart2.data.datasets[ilosc].label = label
            myChart2.update()
            ilosc++
            a=i;
        }
        else
        {
            myChart2.data.datasets[ilosc].data = []
            myChart2.data.datasets[ilosc].label = "Brak"
        }
    }

    if(ilosc==1 && a!=0)
        {
            myChart2.data.datasets[1].data = data[0]
            myChart2.data.datasets[1].label = "Brak"
        }
    if(ilosc==1 && a==0)
        {
            myChart2.data.datasets[1].data = data[1]
            myChart2.data.datasets[1].label = "Brak"
        }
            myChart2.update()
    }



    //Get crypto data with offset type
    function getCrypto(type, symbol)
    {
        if(!type)
            var link = "https://localhost:7070/api/Crypto/"+symbol;
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
                    label =  APIdata["metaData"]["dcName"];
                    for(let i = 0; i < APIdata["ohlcvCryptoData"].length;i++)
                    data.push(APIdata["ohlcvCryptoData"][i]["closeUSD"]);
                }
                
            });

        return data;
    }

    var currentCrytpo;
    // Single Line Chart
    let coins = document.getElementsByClassName('krypto')
    for(let i=0; i<coins.length; i++){
        coins[i].onclick= () =>{
            currentyCrypto = coins[i].id;
            
            myChart3.data.datasets[0].data = getCrypto(chartType, currentyCrypto);
            myChart3.data.datasets[0].label = label;
            myChart3.update()
            //cmpChartUpdate()
        }
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
    // Salse & Revenue Chart
    var ctx2 = $("#porownanie").get(0).getContext("2d");
    var myChart2 = new Chart(ctx2, {
        type: "line",
        data: {
            labels: xValues,
            datasets: [{
                    label: label,
                    data: data[0],
                    backgroundColor: "rgba(235, 22, 22, .5)",
                    fill: true
                },
                {
                    label: label,
                    data: data[1],
                    backgroundColor: "rgba(235, 22, 22, .7)",
                    fill: true
                }
            ]
            },
        options: {
            responsive: true
        }
    });
    </script>

    <?php
    include_once 'parts/footer.php';
    ?>
            