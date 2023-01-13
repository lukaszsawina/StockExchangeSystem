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
                                    <td><a class="btn btn-sm btn-primary" onclick="delete_user(<?php echo $c->id; ?>)">Delete</a></td>
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
                    <div class="row g-4">
                        <div class="col-sm-12 col-md-6 col-xl-6 bg-secondary ">
                            <div class="">
                                <h6 class="mb-0">New element on web</h6>
                            </div>
                            <div class="">
                                <form id="form" action="addElement.php" method="post" class="bg-secondary rounded h-100 p-4">
                                        <div class="form-floating mb-3">
                                            <select class="form-select" name="type" id="type"
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

                                    <button type="submit" onclick="signupInfo()"class="btn shadow-none btn-primary py-3 w-100 mb-4" >Add new element</button>
                                </form>
                                <div id="existInfo" class="d-none align-items-center justify-content-center flex-wrap">
                                    <h1>Already exist!</h1>
                                </div>
                                <div id="access" class="alert alert-warning fade collapse" role="alert">
                                <strong>Remember!</strong> this option takes some time.
                                <button type="button"  class="close shadow-none" onclick="hideAllert()">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-6 col-xl-6  bg-secondary">
                            <div class="">
                                <h6 class="mb-0">New admin</h6>
                            </div>
                            <form method="post" id="adminform" class="bg-secondary rounded  p-4">
                                <div class="form-floating mb-3">
                                    <input type="text" class="form-control" id="fName" placeholder="jhondoe" name="firstName">
                                    <label for="floatingText">First name</label>
                                </div>
                                <div class="form-floating mb-3">
                                    <input type="text" class="form-control" id="lName" placeholder="jhondoe" name="lastName">
                                    <label for="floatingText">Last name</label>
                                </div>
                                <div class="form-floating mb-3">
                                    <input type="email" class="form-control" id="eMail" placeholder="name@example.com" name="email">
                                    <label for="floatingInput">Email address</label>
                                </div>
                                <div class="form-floating mb-3">
                                    <input type="phone" class="form-control" id="phone" placeholder="123123123" name="phone">
                                    <label for="floatingInput">Phone</label>
                                </div>
                                <div class="form-floating mb-4">
                                    <input type="password" class="form-control" id="pass" placeholder="Password" name="password">
                                    <label for="floatingPassword">Password</label>
                                </div>
                                <div id="successInfo" class="d-none align-items-center justify-content-between flex-wrap">
                                    <h1>Success</h1>
                                </div>
                                <div id="existAdminInfo" class="d-none align-items-center justify-content-between flex-wrap">
                                    <h1>User already exist</h1>
                                </div>
                                <button id="subBtn" type="submit" class="btn shadow-none btn-primary py-3 w-100 mb-4">Add new Admin</button>
                            </form>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Table Start -->
            <div class="container-fluid pt-4 px-4">
                <div class="row g-4">
                    <div class="col-12">
                        <div class="bg-secondary rounded h-100 p-4">
                            <h6 class="mb-4">Daily info from past year</h6>
                            <div class="table-responsive overflow-auto logs-dropdown">
                                <table id="dtDynamicVerticalScrollExample" class="table mb-2">
                                    <thead>
                                        <tr>
                                            <th scope="col">ID</th>
                                            <th scope="col">Message</th>
                                            <th scope="col">Level</th>
                                            <th scope="col">Time</th>
                                        </tr>
                                    </thead>
                                    <tbody id="tableBody" class="">
                                    <?php
                                        $serverName = "(localdb)\MSSQLLocalDB";
                                        $connectionInfo = array( "Database"=>"StockSystemDB", "UID"=>"", "PWD"=>"");

                                        $conn = sqlsrv_connect($serverName, $connectionInfo);

                                        $sql = "SELECT * FROM logs WHERE Level != 'Warning' ORDER BY Id DESC ";
                                        $stmt = sqlsrv_query( $conn, $sql );
                                        if( $stmt === false) {
                                            die( print_r( sqlsrv_errors(), true) );
                                        }

                                        while( $row = sqlsrv_fetch_array( $stmt, SQLSRV_FETCH_ASSOC) ) {
                                            $Date = $row['TimeStamp']->format('Y-m-d H:m');
                                            ?>
                                                <tr>
                                                    <th scope="row"><?php echo $row['Id'];?></th>
                                                    <td><?php echo  $row['Message'];?></td>
                                                    <td><?php echo  $row['Level'];?></td>
                                                    <td><?php echo $Date;?></td>
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


    <script>
        var form=document.getElementById('form')
        var adminform=document.getElementById('adminform')

        function signupInfo()
    {
        $('#access').show();
        document.getElementById("access").classList.add("show");
    }

    function hideAllert()
    {//hide
     $('#access').hide();
    }

        function delete_user(user)
        {
        fetch('https://localhost:7070/api/User/'+user, {
        method: 'DELETE',
        headers: {
            'Content-type': 'application/json;',
        }
        })
        .then(function(response){ 
            })
            .then(function(data)
            {
                location.reload();
            }) 
        }

        form.addEventListener('submit', function(e){
        e.preventDefault()
            document.getElementById("existInfo").classList.add("d-none");
            document.getElementById("existInfo").classList.remove("d-flex");

            if(!ExistAPI()){
                form.submit();
            }
            else{

                document.getElementById("existInfo").classList.remove("d-none");
                document.getElementById("existInfo").classList.add("d-flex");
            }
        });


        function ExistAPI()
        {
        var s = document.getElementById('symbol').value;
        var t = document.getElementById('type').value;

        var exist;
        var link = "https://localhost:7070/api/"+t+"/Exist/"+s;
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



        adminform.addEventListener('submit', function(e){
        e.preventDefault()
            document.getElementById("existAdminInfo").classList.add("d-none");
            document.getElementById("existAdminInfo").classList.remove("d-flex");
            document.getElementById("successInfo").classList.add("d-none");
            document.getElementById("successInfo").classList.remove("d-flex");

            if(!UserExistAPI()){
                PostUserAPI();
            }
            else{
                document.getElementById("existAdminInfo").classList.remove("d-none");
                document.getElementById("existAdminInfo").classList.add("d-flex");
            }
        });

        function UserExistAPI()
        {
        var m = document.getElementById('eMail').value.split("@");

        var exist;
        var link = "https://localhost:7070/api/Account/exist/"+m[0]+"%40"+m[1];
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
            fetch('https://localhost:7070/api/Admin', {
        method: 'POST',
        body: JSON.stringify({
                email: document.getElementById('eMail').value,
                password: document.getElementById('pass').value,
                createTime: new Date(),
                firstName: document.getElementById('fName').value,
                phone: document.getElementById('phone').value,
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
                document.getElementById("existAdminInfo").classList.add("d-none");
                document.getElementById("existAdminInfo").classList.remove("d-flex");
                document.getElementById("successInfo").classList.remove("d-none");
                document.getElementById("successInfo").classList.add("d-flex");
                adminform.reset();

            })
        }

    </script>

<?php
include_once 'parts/footer.php';
?>