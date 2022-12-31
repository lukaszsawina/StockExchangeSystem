<?php
include_once 'parts/header.php';



?>


            <!-- Sale & Revenue Start -->
            <div class="container-fluid pt-4 px-4">
                <div class="row g-4">
                    <div class="col-sm-12 col-xl-12">
                        <div class="bg-secondary rounded d-flex align-items-center p-4">
                            <i class="fa fa-user fa-3x text-primary"></i>
                            <div class="ms-3">
                                <h1 class="text-body mb-2">Hello <?php echo $response->firstName." ".$response->lastName; ?></p>
                                <h6 class="mb-0">Now you can change webpage.</h6>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Sale & Revenue End -->

            <?php
                $api_url = 'https://localhost:7070/api/User';
                $response = GetAPI($api_url,false);
            
            
            ?>

            <!-- Users Start -->
            <div class="container-fluid pt-4 px-4">
                <div class="bg-secondary text-center rounded p-4">
                    <div class="d-flex align-items-center justify-content-between mb-4">
                        <h6 class="mb-0">Users</h6>
                    </div>
                    <div class="table-responsive">
                        <table class="table text-start align-middle table-bordered table-hover mb-0">
                            <thead>

                                <tr class="text-white">
                                    <th scope="col">ID</th>
                                    <th scope="col">Name</th>
                                    <th scope="col">Email</th>
                                    <th scope="col">Create Time</th>
                                    <th scope="col">Info</th>
                                    <th scope="col">Delete</th>
                                </tr>
                            </thead>
                            <tbody>
                                <?php                
                                    foreach ($response as &$c)
                                    {
                                        $Time = explode("T", $c->createTime);
                                ?>
                                <tr >
                                    <td><?php echo $c->id; ?></td>
                                    <td><?php echo $c->firstName." ".$c->lastName; ?></td>
                                    <td><?php echo $c->email; ?></td>
                                    <td><?php echo $Time[0]; ?></td>
                                    <td><a class="btn btn-sm btn-primary" href="user_page.php?u=<?php echo $c->id; ?>">Info</a></td>
                                    <td><a class="btn btn-sm btn-primary" href="delete_user.php?u=<?php echo $c->id; ?>">Delete</a></td>
                                </tr>
                                <?php
                                    } 
                                ?>  
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
            <!-- Users End -->

            <div class="container-fluid pt-4 px-4">
                <div class="bg-secondary text-center rounded p-4">
                    <div class="d-flex align-items-center justify-content-between mb-4">
                        <h6 class="mb-0">New element on web</h6>
                    </div>
                    <div class="col-sm-12 col-xl-6">
                        <form action="addElement.php" method="post" class="bg-secondary rounded h-100 p-4">
                                <div class="form-floating mb-3">
                                    <select class="form-select" name="type"
                                        aria-label="Floating label select example">
                                        <option selected>Open this select menu</option>
                                        <option value="Crypto">Crypto</option>
                                        <option value="Currency">Currency</option>
                                        <option value="Stock">Stock</option>
                                    </select>
                                    <label for="floatingSelect">Element Type</label>
                                </div>
                            <div class="form-floating mb-3">
                                <input type="text" class="form-control" id="symbol" name="symbol">
                                <label for="floatingInput">Symbol</label>
                            </div>
                            <button type="submit" class="btn btn-lg btn-primary m-2" >Add new element</button>
                        </form>
                    </div>
                </div>
            </div>


    <script>  

                
    </script>

<?php
include_once 'parts/footer.php';
?>