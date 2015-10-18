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

$rating = urldecode($_POST['rating']);
$id = urldecode($_POST['id']);

echo "id: " . $id . " rating: " . $rating;

$writeQuery = "INSERT INTO `findr`.`ratings` (`rating`, `id`) VALUES ('$rating', '$id');";
$write = mysqli_query($connection,$writeQuery);

if(!$write){
	echo "Write failed!<br> y";
}

//mysqli_close($connection);

?>

</body>

</html>


