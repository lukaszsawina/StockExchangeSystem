<?php
function GetAPI($url, $data)
{
    $arrContextOptions= [
        'ssl' => [
            'cafile' => '/path/to/bundle/cacert.pem',
            "verify_peer"=>false,
            "verify_peer_name"=>false,
        ],
    ];

    if ($data)
        $url = sprintf("%s/%s", $url, $data);
    // Read JSON file
    $response = file_get_contents($url, false, stream_context_create($arrContextOptions));

    // Decode JSON data into PHP array
    $response_data = json_decode($response);

    return $response_data;
}

$mail = $_POST['email'];
$api_url = 'https://localhost:7070/api/Account/email/'.$mail;
$response = GetAPI($api_url,false);

session_start();
$_SESSION["id"] = $response->id;

$api_url = 'https://localhost:7070/api/Admin/isAdmin/'.$response->id;
$response = GetAPI($api_url,false);

$_SESSION["admin"] = $response;

header("Location: index.php");
?>