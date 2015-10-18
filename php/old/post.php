<html>

<?php 

//connect to sqlServer
$connection = mysqli_connect('127.0.0.1',"root",""); //change ip to suit needs
if(!$connection){
	die("Connection to mysql failed " . mysqli_connect_error()); //this breaks flow
}
echo "Connected successfully to mysql! <br>";

//connect to specific database
$database = mysqli_select_db($connection,"findR");
if(!$database){
	die("Can't connect to database!");
}

$lat = urldecode($_POST['lat']);
$long = urldecode($_POST['long']);
$type = urldecode($_POST['type']);
//$id = urldecode($_POST['id']); get unique address
//figure out unique id
$id = 0;
$idQuery = "SELECT id FROM `findr`.`locations` WHERE Id = '$id'"; //queries how many people have id
$idCheck = mysqli_query($connection,$idQuery);
while(mysqli_num_rows($idCheck) > 0){ //if id exists
	$id++; 
	$idQuery = "SELECT id FROM `findr`.`locations` WHERE Id = '$id'";//move to next avaliable one and check again!
	$idCheck = mysqli_query($connection,$idQuery);
}

echo "Recieved lat: " . $lat . " long: " . $long . " type: " . $type . " id: " . $id . "<br>"; 

$writeQuery = "INSERT INTO `findr`.`locations` (`lat`, `long`, `type`, `id`) VALUES ('$lat', '$long', '$type', '$id')";
$write = mysqli_query($connection,$writeQuery);

if(!$write){
	echo "Write failed!<br> y";
}

//mysqli_close($connection);

?>

</body>

</html>


