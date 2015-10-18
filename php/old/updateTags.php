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

$tag = urldecode($_POST['tag']);
$id = urldecode($_POST['id']);

echo "id: " . $id . " tag: " . $tag;

$writeQuery = "INSERT INTO `findr`.`tags` (`tag`, `id`) VALUES ('$tag', '$id');";
$write = mysqli_query($connection,$writeQuery);

if(!$write){
	echo "Write failed!<br> y";
}

//mysqli_close($connection);

?>

</body>

</html>


