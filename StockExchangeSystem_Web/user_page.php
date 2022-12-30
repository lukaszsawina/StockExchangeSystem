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
                                <h6 class="mb-0">View your profile and your virtual investments.</h6>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Sale & Revenue End -->

            <!-- User information Start -->
            <div class="container-fluid pt-4 px-4">
                <div class="row g-4">
                    <div class="col-sm-12 col-xl-12">
                        <div class="bg-secondary rounded d-flex align-items-center justify-content-between p-4">
                            <div id="userInfo"  class="ms-3">
                                <?php
                                    $Time = explode("T", $response->createTime);
                                ?>
                                <h3 class="text-body mb-2">User ID: <?php echo $response->id; ?></h4>
                                <h3 class="text-body mb-2">First Name: <?php echo $response->firstName; ?></h4>
                                <h3 class="text-body mb-2">Last Name: <?php echo $response->lastName; ?></h4>
                                <h3 class="text-body mb-2">Email: <?php echo $response->email; ?></h4>
                                <h3 class="text-body mb-2">Create Time: <?php echo $Time[0]; ?></h4>
                            </div>
                            <div id="userEdit" class="d-none bg-secondary rounded h-100 p-4">
                                <form id="form" method="post">
                                    <div class="mb-3">
                                        <label for="exampleInputPassword1" class="form-label">First name</label>
                                        <input type="text" id="fName" class="form-control" id="exampleInputPassword1" name="fname" value="<?php echo $response->firstName;?>" require>
                                    </div><div class="mb-3">
                                        <label for="exampleInputPassword1" class="form-label">LastName</label>
                                        <input type="text" id="lName" class="form-control" id="exampleInputPassword1" name="lname" value="<?php echo $response->lastName;?>" require>
                                    </div>
                                    <div class="mb-3">
                                        <label for="exampleInputEmail1" class="form-label">Email address</label>
                                        <input type="email" id="eMail" class="form-control" id="exampleInputEmail1" name="email" value="<?php echo $response->email;?>" require
                                            aria-describedby="emailHelp">
                                        <div id="emailHelp" class="form-text">We'll never share your email with anyone else.
                                        </div>
                                    </div>
                                    <button type="submit" class="btn btn-primary">Save changes</button>
                                </form>
                            </div>

                            <button type="button" onclick="showEdit()" class="btn btn-square btn-outline-primary m-2 shadow-none"><i class="fa fa-edit"></i></button>
                        </div>
                    </div>
                </div>
            </div>
            <!-- User information End -->

            <!-- Sale & Revenue Start -->
            <div class="container-fluid pt-4 px-4">
                <div class="row g-4">
                    <div class="col-sm-6 col-xl-3">
                        <div class="bg-secondary rounded d-flex align-items-center justify-content-between p-4">
                            <i class="fa fa-chart-line fa-3x text-primary"></i> <!--ikona-->
                            <div class="ms-3">
                                <p class="mb-2">Today Sale</p>
                                <h6 class="mb-0">$1234</h6>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6 col-xl-3">
                        <div class="bg-secondary rounded d-flex align-items-center justify-content-between p-4">
                            <i class="fa fa-chart-bar fa-3x text-primary"></i>
                            <div class="ms-3">
                                <p class="mb-2">Total Sale</p>
                                <h6 class="mb-0">$1234</h6>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6 col-xl-3">
                        <div class="bg-secondary rounded d-flex align-items-center justify-content-between p-4">
                            <i class="fa fa-chart-area fa-3x text-primary"></i>
                            <div class="ms-3">
                                <p class="mb-2">Today Revenue</p>
                                <h6 class="mb-0">$1234</h6>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6 col-xl-3">
                        <div class="bg-secondary rounded d-flex align-items-center justify-content-between p-4">
                            <i class="fa fa-chart-pie fa-3x text-primary"></i>
                            <div class="ms-3">
                                <p class="mb-2">Total Revenue</p>
                                <h6 class="mb-0">$1234</h6>
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
                        <h6 class="mb-0">Recent Salse</h6>
                        <a href="">Show All</a>
                    </div>
                    <div class="table-responsive">
                        <table class="table text-start align-middle table-bordered table-hover mb-0">
                            <thead>
                                <tr class="text-white">
                                    <th scope="col"><input class="form-check-input" type="checkbox"></th>
                                    <th scope="col">Date</th>
                                    <th scope="col">Invoice</th>
                                    <th scope="col">Customer</th>
                                    <th scope="col">Amount</th>
                                    <th scope="col">Status</th>
                                    <th scope="col">Action</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td><input class="form-check-input" type="checkbox"></td>
                                    <td>01 Jan 2045</td>
                                    <td>INV-0123</td>
                                    <td>Jhon Doe</td>
                                    <td>$123</td>
                                    <td>Paid</td>
                                    <td><a class="btn btn-sm btn-primary" href="">Detail</a></td>
                                </tr>
                                <tr>
                                    <td><input class="form-check-input" type="checkbox"></td>
                                    <td>01 Jan 2045</td>
                                    <td>INV-0123</td>
                                    <td>Jhon Doe</td>
                                    <td>$123</td>
                                    <td>Paid</td>
                                    <td><a class="btn btn-sm btn-primary" href="">Detail</a></td>
                                </tr>
                                <tr>
                                    <td><input class="form-check-input" type="checkbox"></td>
                                    <td>01 Jan 2045</td>
                                    <td>INV-0123</td>
                                    <td>Jhon Doe</td>
                                    <td>$123</td>
                                    <td>Paid</td>
                                    <td><a class="btn btn-sm btn-primary" href="">Detail</a></td>
                                </tr>
                                <tr>
                                    <td><input class="form-check-input" type="checkbox"></td>
                                    <td>01 Jan 2045</td>
                                    <td>INV-0123</td>
                                    <td>Jhon Doe</td>
                                    <td>$123</td>
                                    <td>Paid</td>
                                    <td><a class="btn btn-sm btn-primary" href="">Detail</a></td>
                                </tr>
                                <tr>
                                    <td><input class="form-check-input" type="checkbox"></td>
                                    <td>01 Jan 2045</td>
                                    <td>INV-0123</td>
                                    <td>Jhon Doe</td>
                                    <td>$123</td>
                                    <td>Paid</td>
                                    <td><a class="btn btn-sm btn-primary" href="">Detail</a></td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
            <!-- Recent Sales End -->

    <script>  
        document.getElementById("navindex").classList.add('active'); 
        
        var show = false;

        function showEdit()
        {
            show = !show;

            if(show)
            {
                document.getElementById("userInfo").classList.add("d-none");
                document.getElementById("userEdit").classList.remove("d-none");
                document.getElementById("userEdit").classList.add("d-flex");
            }
        }

        var form=document.getElementById('form');


        form.addEventListener('submit', function(e){
        e.preventDefault()

            
            if(!UserExistAPI()){
                PostUserAPI();
            }
            else{
                
                document.getElementById('subBtn').setAttribute('disabled', '');
                document.getElementById("existInfo").classList.remove("d-none");
                document.getElementById("existInfo").classList.add("d-flex");
            }
        });

        function UserExistAPI()
        {
        var m = <?php echo $response->id;?>;
        var exist;
        var link = "https://localhost:7070/api/Account/exist/"+m;
        $.ajax({
                url: link,
                type: 'GET',
                async: false,
                dataType: 'json',
                cors: false ,
                contentType:'application/json',
                success: function (APIdata){
                    exist = APIdata;
                }
                
            });
            return exist;
        }


        function PostUserAPI()
        {
            fetch('https://localhost:7070/api/User/<?php echo $response->id;?>', {
        method: 'PUT',
        body: JSON.stringify({
                id: <?php echo $response->id;?>,
                email: document.getElementById('eMail').value,
                password: "<?php echo $response->password;?>",
                createTime: "<?php echo $response->createTime;?>",
                firstName: document.getElementById('fName').value,
                lastName: document.getElementById('lName').value
        }),
        headers: {
            'Content-type': 'application/json;',
        }
        })
        .then(function(response){ 
            })
            .then(function(data)
            {
                window.location.href = 'user_page.php?u=<?php echo $response->id;?>';

            }) 
        }
                
    </script>

<?php
include_once 'parts/footer.php';
?>