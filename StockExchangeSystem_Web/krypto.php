<?php
include_once 'parts/header.php';
$api_url = 'https://localhost:7070/api/Crypto';

?>


            <!-- Description Start -->
            <div class="container-fluid pt-4 px-4">
                <div class="row g-4">
                    <div class="col-sm-12 col-xl-12">
                        <div class="bg-secondary rounded d-flex align-items-center p-4">
                            <i class="fa-brands fa-bitcoin fa-3x text-primary" onclick="userAction()"></i>
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
                                            <td><img class="coin-logo" src="https://s2.coinmarketcap.com/static/img/coins/64x64/1.png" loading="lazy" alt="BTC logo"></td>
                                            <td><?php echo $c->name;?></td>
                                            <td id="symbol"><?php echo $c->symbol;?></td>
                                            <td><?php echo number_format($c->value,2, ',', ' ');?></td>
                                            <td><?php echo number_format($c->volume,2, ',', ' ');?></td>
                                            <td><?php echo number_format($c->changeDay,2, ',', ' ')."%";?></td>
                                            <td><?php echo number_format($c->changeWeek,2, ',', ' ')."%";?></td>
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
                            <h6 class="mb-4">Single Line Chart</h6>
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
    function subtractMonths(date, months) {
        date.setMonth(date.getMonth() - months);
        return date;
    }
    //Days to table
    function subtractDays(date, day) {
        date.setDate(date.getDate() - 1)
        return date;
    }
    var today = new Date(); // yyyy-mm-dd

    var xValues = [];

    for(let i = 0; i < 365; i++)
    {
        var days = subtractDays(today, 1);
        var days = days.toLocaleDateString();
        xValues.push(days);
    }

    var xValues = xValues.reverse();
    var data = [];
    var label = "Brak";
    // Comparison Line Chart
    var count = 0
    var queue = []
    let collection = document.querySelectorAll(".kryptocheck");
    
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
    for(let i=0; i<3; i++){
        if(collection[i].checked == true && ilosc<2){
            myChart2.data.datasets[ilosc].data = getCryptoMonthly(coins[i].id);
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


    function getCryptoMonthly(symbol)
    {
        var data = [];
        $.ajax({
                url: "https://localhost:7070/api/Crypto/"+symbol,
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

    // Single Line Chart
    let coins = document.getElementsByClassName('krypto')
    for(let i=0; i<coins.length; i++){
        coins[i].onclick= () =>{
            
            
            myChart3.data.datasets[0].data = getCryptoMonthly(coins[i].id);
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
                    backgroundColor: "rgba(235, 22, 22, .7)",
                    fill: true
                },
                {
                    label: label,
                    data: data[1],
                    backgroundColor: "rgba(235, 22, 22, .5)",
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
            