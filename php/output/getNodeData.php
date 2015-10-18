
<?php 

//connect to sqlServer
$connection = mysqli_connect('127.0.0.1',"root",""); //change ip to suit needs
if(!$connection){
	die("Connection to mysql failed " . mysqli_connect_error()); //this breaks flow
}
//echo "Connected successfully to mysql! <br>";

//connect to specific database
$database = mysqli_select_db($connection,"findR");
if(!$database){
	die("Can't connect to database!");
}

//fields
$curLatitude = urldecode($_POST['lat']);
$curLongitude = urldecode($_POST['long']);
$maxDistance = urldecode($_POST['distance']);

//get only rows which are within distance
$query = "SELECT id FROM `findr`.`locations` WHERE 
			(
				6371 * acos 
				(
				  cos ( radians('$curLatitude') )
				  * cos( radians( `lat` ) )
				  * cos( radians( `long` ) - radians('$curLongitude') )
				  + sin( radians('$curLatitude') )
				  * sin( radians( `lat` ) )
				)
			) < '$maxDistance'";
			
$write = mysqli_query($connection,$query);


//$arr = array();

//Andrew: my code is maintainable!
if(mysqli_num_rows($write) > 0){
	$row = mysqli_fetch_assoc($write);
	while($row){
		$curId = $row["id"];
		
		//from current id
		//get location data
		$curQuery = "SELECT `lat`, `long`, `type`, `name`, `details`, `id` FROM `findr`.`locations`WHERE id = '$curId'";
		$curWrite = mysqli_query($connection,$curQuery);
		$curRow = mysqli_fetch_assoc($curWrite);
		
		echo $curRow["id"] . " $$$$$ ";
		echo $curRow["lat"] . " $$$$$ ";
		echo $curRow["long"] . " $$$$$ ";
		echo $curRow["type"] . " $$$$$ ";
		echo $curRow["name"] . " $$$$$ ";
		echo $curRow["details"] . " $$$$$ ";
		
		//$arr["lat"] = $curRow["lat"];
		//$arr["long"] = $curRow["long"];
		//$arr["type"] = $curRow["type"];
		//$arr["name"] = $curRow["name"];
		//$arr["details"] = $curRow["details"];
		
		//$arr = array_merge($arr, $curRow);
		//echo json_encode($arr);
		
		//get image data
		$curQuery = "SELECT `url` FROM `findr`.`images`WHERE id = '$curId'";
		$curWrite = mysqli_query($connection,$curQuery);
		$curRow = mysqli_fetch_assoc($curWrite);
		echo $curRow["url"] . " $$$$$ ";
		//$arr["url"] = $curRow["url"];
		//$arr = array_merge($arr, $curRow);
		
		//get rating data
		$curQuery = "SELECT `rating` FROM `findr`.`ratings`WHERE id = '$curId'";
		$curWrite = mysqli_query($connection,$curQuery);
		
		$numRatings = 0;
		$totalRatings = 0;
		if(mysqli_num_rows($curWrite) > 0){
			$curRow = mysqli_fetch_assoc($curWrite);
			while($curRow){
				$numRatings ++;
				$totalRatings += $curRow["rating"];
				$curRow = mysqli_fetch_assoc($curWrite);
			}
		}
		if($numRatings == 0){//fencepost case
			echo "0";
			//$arr["rating"] = 0;
		}
		else{
			echo ($totalRatings / $numRatings);
			//$arr["rating"] = ($totalRatings / $numRatings);
		}
		echo " $$$$$ ";
		
		//get comment data
		$curQuery = "SELECT `comment`, `upvotes` FROM `findr`.`comments`WHERE id = '$curId'";
		$curWrite = mysqli_query($connection,$curQuery);
		
		if(mysqli_num_rows($curWrite) > 0){
			$curRow = mysqli_fetch_assoc($curWrite);
			while($curRow){
				echo $curRow["comment"] . " ~~~~~ ";
				echo $curRow["upvotes"] . " ~~~~~ ";
				$curRow = mysqli_fetch_assoc($curWrite);
			}
		}
		echo " $$$$$ ";
		
		//get tag data
		$curQuery = "SELECT `tag`, `upvotes` FROM `findr`.`tags`WHERE id = '$curId'";
		$curWrite = mysqli_query($connection,$curQuery);
		
		if(mysqli_num_rows($curWrite) > 0){
			$curRow = mysqli_fetch_assoc($curWrite);
			while($curRow){
				echo $curRow["tag"] . " ~~~~~ ";
				echo $curRow["upvotes"] . " ~~~~~ ";
				//$arr["tag"] = $curRow["tag"];
				//$arr["tagUpvotes"] = $curRow["upvotes"];
				$curRow = mysqli_fetch_assoc($curWrite);
			}
		}
		echo " $$$$$ ";

		//echo json_encode($arr);
		
		$row = mysqli_fetch_assoc($write);
		echo "<br>";
	}
}

//do writing here

//mysqli_close($connection);

?>


