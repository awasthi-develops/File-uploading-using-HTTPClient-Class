<?php

$conn = mysqli_connect("localhost","root","","as_login");

$name = $_POST['name'];
$email = $_POST['email'];
$file_name = $_POST['file_name'];

$stmt = $conn->prepare("Insert into upload_application(name,email,file_name) values (?,?,?)");
	$stmt->bind_param("sss",$name,$email,$file_name);
	$stmt->execute();

$target_path = dirname(__FILE__).'/FileUploadApp/';
if($_FILES['file']['name'])
{
	$target_path = $target_path.basename($_FILES['file']['name']);
	
	try{
		if(!move_uploaded_file($_FILES['file']['tmp_name'],$target_path))
		{
			echo "file uploading failed";
		}
		else{
			echo "file uploaded successfully";
		}
	}catch(Exception $e){
		 echo "error,try again";
	}
	
}


?>