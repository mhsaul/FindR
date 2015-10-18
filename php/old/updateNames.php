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

$name = urldecode($_POST['name']);
$id = urldecode($_POST['id']);

echo "id: " . $id . " name: " . $name;

$writeQuery = "INSERT INTO `findr`.`name` (`name`, `id`) VALUES ('$name', '$id');";
$write = mysqli_query($connection,$writeQuery);

if(!$write){
	echo "Write failed!<br> y";
}

//mysqli_close($connection);

?>

</body>

</html>


