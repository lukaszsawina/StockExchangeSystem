<?php
include_once 'parts/header.php';


?>


            <!-- Sale & Revenue Start -->
            <div class="container-fluid pt-4 px-4">
                <div class="row g-4">
                    <div class="col-sm-12 col-xl-12">
                        <div class="bg-secondary rounded d-flex align-items-center p-4">
                            <i class="fa-solid fa-money-bill-trend-up fa-3x text-primary"></i>
                            <div class="ms-3">
                                <h1 class="text-body mb-2">Welcome to DarkStock</p>
                                <h6 class="mb-0">See what's happening on the stock market. Check out the interesting assets we offer.</h6>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Sale & Revenue End -->



            <!-- Recent Sales Start -->
            <div class="container-fluid pt-4 px-4">
                <div class="bg-secondary text-center rounded p-4">
                    <div class="d-flex align-items-center justify-content-between mb-4">
                        <h4 class="mb-0">Our investment proposals</h4>
                    </div>

                    <?php
                        if(isset($_SESSION["id"]))
                        {
                    ?>

                    <div class="d-flex align-items-center justify-content-between mb-4">
                        <h1 class="text-body mb-0">Crypto</h1>
                    </div>
                    <div class="table-responsive">

                    <?php
                        $api_url = 'https://localhost:7070/api/Crypto/proposition';

                        $response = GetAPI($api_url,false);
                        if(count($response) > 0)
                        {
                    ?>
                        <table class="table text-start align-middle table-bordered table-hover mb-4">
                            <thead>
                                <tr class="text-white">
                                    <th scope="col"> </th>
                                    <th scope="col">Symbol</th>
                                    <th scope="col">Value(USD)</th>
                                    <th scope="col">Change (24h)</th>
                                    <th scope="col">Change (7d)</th>
                                    <th scope="col">Info.</th>
                                </tr>
                            </thead>
                            <tbody>

                                    <?php
                                        $i = 1;
                                        foreach ($response as &$c)
                                        {
                                            ?>
                                        <tr id="<?php echo $c->symbol;?>" class="krypto">
                                            <th scope="row"><?php echo $i;?></th>
                                            <td class="symbol"><?php echo $c->symbol;?></td>
                                            <td><?php echo number_format($c->value,2, '.', ',');?></td>
                                            <td><?php echo number_format($c->changeDay,2, '.', ',')."%";?></td>
                                            <td><?php echo number_format($c->changeWeek,2, '.', ',')."%";?></td>
                                            <td><a href="crypto_page.php?c=<?php echo $c->symbol;?>" type="button" class="btn btn-primary shadow-none">More</a></td>
                                        </tr>           
                                            <?php
                                            $i++;
                                        } ?>
               
                            </tbody>
                        </table>
                        <?php
                            }
                            else
                            {
                                ?>
                            <div class="d-flex align-items-center justify-content-between mb-1">
                                <h3 class=" text-body mb-0">Sorry!</h3>
                            </div>
                            <div class="d-flex align-items-center justify-content-between mb-4">
                                <h4 class="mb-0">Nothing interesting so far</h4>
                            </div>
                            <?php
                            }
                            ?> 
                        <div class="d-flex align-items-center justify-content-between mb-4">
                        <h1 class="text-body mb-0">Currencies</h1>
                    </div>
                    <div class="table-responsive">

                    <?php
                        $api_url = 'https://localhost:7070/api/Currency/proposition';

                        $response = GetAPI($api_url,false);
                        if(count($response) > 0)
                        {
                    ?>
                        <table class="table text-start align-middle table-bordered table-hover mb-4">
                            <thead>
                                <tr class="text-white">
                                    <th scope="col"> </th>
                                    <th scope="col">Symbol</th>
                                    <th scope="col">Value(USD)</th>
                                    <th scope="col">Change (7d)</th>
                                    <th scope="col">Change (30d)</th>
                                    <th scope="col">Info.</th>
                                </tr>
                            </thead>
                            <tbody>

                                    <?php
                                        
                                        $i = 1;
                                        foreach ($response as &$c)
                                        {
                                            ?>
                                       <tr id="<?php echo $c->symbol;?>" class="waluty">
                                            <th scope="row"><?php echo $i;?></th>
                                            <td><?php echo $c->symbol;?></td>
                                            <td><?php echo number_format($c->inUSD,2, ',', ' ');?></td>
                                            <td><?php echo number_format($c->weekChange,2, ',', ' ')."%";?></td>
                                            <td><?php echo number_format($c->monthChange,2, ',', ' ')."%";?></td>
                                            <td><a href="currency_page.php?c=<?php echo $c->symbol;?>" type="button" class="btn btn-primary shadow-none">More</a></td>
                                        </tr>        
                                            <?php
                                            $i++;
                                        } 
                                        ?>                
                            </tbody>
                        </table>
                        <?php
                            }
                            else
                            {
                                ?>
                            <div class="d-flex align-items-center justify-content-between mb-1">
                                <h3 class=" text-body mb-0">Sorry!</h3>
                            </div>
                            <div class="d-flex align-items-center justify-content-between mb-4">
                                <h4 class="mb-0">Nothing interesting so far</h4>
                            </div>
                            <?php
                            }
                        ?> 
                        <div class="d-flex align-items-center justify-content-between mb-4">
                        <h1 class="text-body mb-0">Stocks</h1>
                    </div>
                    <div class="table-responsive">

                    <?php
                        $api_url = 'https://localhost:7070/api/Stock/proposition';

                        $response = GetAPI($api_url,false);
                        if(count($response) > 0)
                        {
                    ?>
                        <table class="table text-start align-middle table-bordered table-hover mb-4">
                            <thead>
                                <tr class="text-white">
                                    <th scope="col"> </th>
                                    <th scope="col">Symbol</th>
                                    <th scope="col">Value(USD)</th>
                                    <th scope="col">Change (7d)</th>
                                    <th scope="col">Change (30d)</th>
                                    <th scope="col">Info.</th>
                                </tr>
                            </thead>
                            <tbody>

                                    <?php
                                        
                                        $i = 1;
                                        foreach ($response as &$c)
                                        {
                                            ?>
                                        <tr id="<?php echo $c->symbol;?>" class="krypto">
                                            <th scope="row"><?php echo $i;?></th>
                                            <td><?php echo $c->symbol;?></td>
                                            <td><?php echo number_format($c->value,2, ',', ' ');?></td>
                                            <td><?php echo number_format($c->changeWeek,2, ',', ' ')."%";?></td>
                                            <td><?php echo number_format($c->changeMonth,2, ',', ' ')."%";?></td>
                                            <td><a href="stock_page.php?c=<?php echo $c->symbol;?>" type="button" class="btn btn-primary shadow-none">More</a></td>
                                        </tr>           
                                            <?php
                                            $i++;
                                        } 
                                        ?>                
                            </tbody>
                        </table>
                        <?php
                            }
                            else
                            {
                                ?>
                            <div class="d-flex align-items-center justify-content-between mb-1">
                                <h3 class=" text-body mb-0">Sorry!</h3>
                            </div>
                            <div class="d-flex align-items-center justify-content-between mb-4">
                                <h4 class="mb-0">Nothing interesting so far</h4>
                            </div>
                            <?php
                            }
                        ?> 
                    </div>
                    <?php
                        }
                        else
                        {
                            ?>
                            <div class="d-flex align-items-center justify-content-between mb-4">
                                <h1 class=" text-body mb-0">Sorry!</h1>
                            </div>
                            <div class="d-flex align-items-center justify-content-between mb-4">
                                <h4 class="mb-0">Please <a href="signup.php">sign up</a> or if you already are <a href="signup.php">sign in</a> to view this section</h4>
                            </div>
                            

                            <?php
                        }
                    ?>
                </div>
            </div>
            <!-- Recent Sales End -->



            <script>  document.getElementById("navindex").classList.add('active'); </script>

<?php
include_once 'parts/footer.php';
?>