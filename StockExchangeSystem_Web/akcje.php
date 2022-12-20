<?php
include_once 'parts/header.php';
?>

 <!-- Description Start -->
 <div class="container-fluid pt-4 px-4">
                <div class="row g-4">
                    <div class="col-sm-12 col-xl-12">
                        <div class="bg-secondary rounded d-flex align-items-center p-4">
                            <i class="fa-solid fa-arrow-trend-up fa-3x text-primary"></i>
                            <div class="ms-3">
                                <h1 class="text-body mb-2">Akcje</p>
                                <h6 class="mb-0">Inwestycje w akcje uznaje się za inwestycje stosunkowo bezpieczne i mało ryzykowne w porównaniu z inwestycjami w inne aktywa.</h6>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Description End -->

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
                                <th scope="col">Kap.</th>
                                <th scope="col">Wol. (24h)</th>
                                <th scope="col">Cał. wol.</th>
                                <th scope="col">Zm. (24h)</th>
                                <th scope="col">Zm. (7d)</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr id="BTC" class="krypto">
                                <td><input id="BTCchk" class="kryptocheck" type="checkbox"></td>
                                <th scope="row">1</th>
                                <td><img class="coin-logo" src="https://s2.coinmarketcap.com/static/img/coins/64x64/1.png" loading="lazy" alt="BTC logo"></td>
                                <td>Bitcoin</td>
                                <td>BTC</td>
                                <td>20.859,4</td>
                                <td>400,47B $</td>
                                <td>52,58B $</td>
                                <td>57,42%</td>
                                <td>-1,62%</td>
                                <td>+2,29%</td>
                            </tr>
                            <tr id="ETH" class="krypto">
                                <td><input id="ETHchk" class="kryptocheck" type="checkbox"></td>
                                <th scope="row">2</th>
                                <td><img class="coin-logo" src="https://s2.coinmarketcap.com/static/img/coins/64x64/1027.png" loading="lazy" alt="ETH logo"></td>
                                <td>Etherum</td>
                                <td>ETH</td>
                                <td>20.859,4</td>
                                <td>400,47B $</td>
                                <td>52,58B $</td>
                                <td>57,42%</td>
                                <td>-1,62%</td>
                                <td>+2,29%</td>
                            </tr>
                            <tr id="USDT" class="krypto">
                                <td><input id="USDTchk" class="kryptocheck" type="checkbox"></td>
                                <th scope="row">3</th>
                                <td><img class="coin-logo" src="https://s2.coinmarketcap.com/static/img/coins/64x64/825.png" loading="lazy" alt="USDT logo"></td>
                                <td>Tether</td>
                                <td>USDT</td>
                                <td>20.859,4</td>
                                <td>400,47B $</td>
                                <td>52,58B $</td>
                                <td>57,42%</td>
                                <td>-1,62%</td>
                                <td>+2,29%</td>
                            </tr>
                            <tr id="USDC" class="krypto">
                                <td><input id="USDCchk" class="kryptocheck" type="checkbox"></td>
                                <th scope="row">4</th>
                                <td><img class="coin-logo" src="https://s2.coinmarketcap.com/static/img/coins/64x64/3408.png" loading="lazy" alt="USDC logo"></td>
                                <td>USD Coin</td>
                                <td>USDC</td>
                                <td>20.859,4</td>
                                <td>400,47B $</td>
                                <td>52,58B $</td>
                                <td>57,42%</td>
                                <td>-1,62%</td>
                                <td>+2,29%</td>
                            </tr>
                            <tr id="BNB" class="krypto">
                                <td><input class="kryptocheck" type="checkbox"></td>
                                <th scope="row">5</th>
                                <td><img class="coin-logo" src="https://s2.coinmarketcap.com/static/img/coins/64x64/1839.png" loading="lazy" alt="BNB logo"></td>
                                <td>BNB</td>
                                <td>BNB</td>
                                <td>20.859,4</td>
                                <td>400,47B $</td>
                                <td>52,58B $</td>
                                <td>57,42%</td>
                                <td>-1,62%</td>
                                <td>+2,29%</td>
                            </tr>
                            <tr id="BUSD" class="krypto">
                                <td><input class="kryptocheck" type="checkbox"></td>
                                <th scope="row">6</th>
                                <td><img class="coin-logo" src="https://s2.coinmarketcap.com/static/img/coins/64x64/4687.png" loading="lazy" alt="BUSD logo"></td>
                                <td>Binance USD</td>
                                <td>BUSD</td>
                                <td>20.859,4</td>
                                <td>400,47B $</td>
                                <td>52,58B $</td>
                                <td>57,42%</td>
                                <td>-1,62%</td>
                                <td>+2,29%</td>
                            </tr>
                            <tr id="XRP" class="krypto">
                                <td><input class="kryptocheck" type="checkbox"></td>
                                <th scope="row">7</th>
                                <td><img class="coin-logo" src="https://s2.coinmarketcap.com/static/img/coins/64x64/52.png" loading="lazy" alt="XRP logo"></td>
                                <td>XRP</td>
                                <td>XRP</td>
                                <td>20.859,4</td>
                                <td>400,47B $</td>
                                <td>52,58B $</td>
                                <td>57,42%</td>
                                <td>-1,62%</td>
                                <td>+2,29%</td>
                            </tr>
                            <tr id="DOGE" class="krypto">
                                <td><input class="kryptocheck" type="checkbox"></td>
                                <th scope="row">8</th>
                                <td><img class="coin-logo" src="https://s2.coinmarketcap.com/static/img/coins/64x64/2010.png" loading="lazy" alt="ADA logo"></td>
                                <td>Dogecoin</td>
                                <td>DOGE</td>
                                <td>20.859,4</td>
                                <td>400,47B $</td>
                                <td>52,58B $</td>
                                <td>57,42%</td>
                                <td>-1,62%</td>
                                <td>+2,29%</td>
                            </tr>
                            <tr id="ADA" class="krypto">
                                <td><input class="kryptocheck" type="checkbox"></td>
                                <th scope="row">9</th>
                                <td><img class="coin-logo" src="https://s2.coinmarketcap.com/static/img/coins/64x64/74.png" loading="lazy" alt="DOGE logo"></td>
                                <td>Cardano</td>
                                <td>ADA</td>
                                <td>20.859,4</td>
                                <td>400,47B $</td>
                                <td>52,58B $</td>
                                <td>57,42%</td>
                                <td>-1,62%</td>
                                <td>+2,29%</td>
                            </tr>
                            <tr id="MATIC" class="krypto">
                                <td><input class="kryptocheck" type="checkbox"></td>
                                <th scope="row">10</th>
                                <td><img class="coin-logo" src="https://s2.coinmarketcap.com/static/img/coins/64x64/3890.png" loading="lazy" alt="MATIC logo"></td>
                                <td>Polygon</td>
                                <td>MATIC</td>
                                <td>20.859,4</td>
                                <td>400,47B $</td>
                                <td>52,58B $</td>
                                <td>57,42%</td>
                                <td>-1,62%</td>
                                <td>+2,29%</td>
                            </tr>
                            <tr id="DOT" class="krypto">
                                <td><input class="kryptocheck" type="checkbox"></td>
                                <th scope="row">11</th>
                                <td><img class="coin-logo" src="https://s2.coinmarketcap.com/static/img/coins/64x64/6636.png" loading="lazy" alt="DOT logo"></td>
                                <td>Polkadot</td>
                                <td>DOT</td>
                                <td>20.859,4</td>
                                <td>400,47B $</td>
                                <td>52,58B $</td>
                                <td>57,42%</td>
                                <td>-1,62%</td>
                                <td>+2,29%</td>
                            </tr>
                            <tr id="DAI" class="krypto">
                                <td><input class="kryptocheck" type="checkbox"></td>
                                <th scope="row">12</th>
                                <td><img class="coin-logo" src="https://s2.coinmarketcap.com/static/img/coins/64x64/4943.png" loading="lazy" alt="DAI logo"></td>
                                <td>Dai</td>
                                <td>DAI</td>
                                <td>20.859,4</td>
                                <td>400,47B $</td>
                                <td>52,58B $</td>
                                <td>57,42%</td>
                                <td>-1,62%</td>
                                <td>+2,29%</td>
                            </tr>
                            <tr id="SHIB" class="krypto">
                                <td><input class="kryptocheck" type="checkbox"></td>
                                <th scope="row">13</th>
                                <td><img class="coin-logo" src="https://s2.coinmarketcap.com/static/img/coins/64x64/5994.png" loading="lazy" alt="SHIB logo"></td>
                                <td>Shiba Inu</td>
                                <td>SHIB</td>
                                <td>20.859,4</td>
                                <td>400,47B $</td>
                                <td>52,58B $</td>
                                <td>57,42%</td>
                                <td>-1,62%</td>
                                <td>+2,29%</td>
                            </tr>
                            <tr id="SOL" class="krypto">
                                <td><input class="kryptocheck" type="checkbox"></td>
                                <th scope="row">14</th>
                                <td><img class="coin-logo" src="https://s2.coinmarketcap.com/static/img/coins/64x64/5426.png" loading="lazy" alt="SOL logo"></td>
                                <td>Solana</td>
                                <td>SOL</td>
                                <td>20.859,4</td>
                                <td>400,47B $</td>
                                <td>52,58B $</td>
                                <td>57,42%</td>
                                <td>-1,62%</td>
                                <td>+2,29%</td>
                            </tr>
                            <tr id="TRX" class="krypto">
                                <td><input class="kryptocheck" type="checkbox"></td>
                                <th scope="row">15</th>
                                <td><img class="coin-logo" src="https://s2.coinmarketcap.com/static/img/coins/64x64/1958.png" loading="lazy" alt="TRX logo"></td>
                                <td>TRON</td>
                                <td>TRX</td>
                                <td>20.859,4</td>
                                <td>400,47B $</td>
                                <td>52,58B $</td>
                                <td>57,42%</td>
                                <td>-1,62%</td>
                                <td>+2,29%</td>
                            </tr>
                            
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
document.getElementById("navakcje").classList.add('active');
    // Chart Global Color
    Chart.defaults.color = "#6C7293";
    Chart.defaults.borderColor = "#000000";

    var xValues = [10 + " Dni ",60 + " Dni ",70,80,90,100,110,120,130,140,169];
    var yValues = [7,8,8,9,9,9,10,11,14,14,15.5];
    var yValues2 = [10,9,8,7,7,6,8,12,12,13,12,11];

    var BTCdata = [10,11,12,9,7,8,8,11,11,13,13,14];
    var ETHdata = [9,8,7,5,5,7,8,7,9,10,10,7];
    var USDTdata = [4,5,6,2,5,4,3,6,6,5,7,6];
    var USDCdata = [2,3,4,3,3,4,3,3,4,4,4,5];

    // Arrays with crypto data sorted
    var data = [BTCdata, ETHdata, USDTdata, USDCdata]
    var label = ['Bitcoin', 'Etherum', 'Tether', 'USD Coin']


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
    for(let i=0; i<4; i++){
        if(collection[i].checked == true && ilosc<2){
            console.log("sheesh")
            myChart2.data.datasets[ilosc].data = data[i]
            myChart2.data.datasets[ilosc].label = label[i]
            myChart2.update()
            ilosc++
            a=i;
        }
    }
    console.log(ilosc)
    console.log(a)
    if(ilosc==1 && a!=0)
        {
            myChart2.data.datasets[1].data = data[0]
            myChart2.data.datasets[1].label = label[0]
        }
    if(ilosc==1 && a==0)
        {
            myChart2.data.datasets[1].data = data[1]
            myChart2.data.datasets[1].label = label[1]
        }
            myChart2.update()
        }
        
    // Single Line Chart

    let coins = document.getElementsByClassName('krypto')
    for(let i=0; i<4; i++){
        coins[i].onclick = () =>{
            myChart3.data.datasets[0].data = data[i]
            myChart3.data.datasets[0].label = label[i]
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
                label: label[0],
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
                    label: label[0],
                    data: data[0],
                    backgroundColor: "rgba(235, 22, 22, .7)",
                    fill: true
                },
                {
                    label: label[1],
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