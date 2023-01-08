<?php
function my_utf8_encode(array $in): array
{
    foreach ($in as $key => $record) {
        if (is_array($record)) {
            $in[$key] = my_utf8_encode($record);
        } else {
            $in[$key] = utf8_encode($record);
        }
    }

    return $in;
}

$url = "https://localhost:7070/api/".$_POST["type"];
$data = array('Symbol' => $_POST['symbol']);

    $data = my_utf8_encode($data);
    $postdata = json_encode($data);


$options = array(
    'http' => array(
        'header'  => "Content-type: application/json\r\n",
        'method'  => 'POST',
        'content' => $postdata
    ),
    "ssl" => [
      "verify_peer"=>false,
      "verify_peer_name"=>false,
  ]
);

$context  = stream_context_create($options);
$result = file_get_contents($url, false, $context);

if($result)
{
    header("Location: admin_page.php");
}
else
{
    header("Location: 404_newElement.php");
}

?>