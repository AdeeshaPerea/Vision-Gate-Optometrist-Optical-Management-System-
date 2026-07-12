<?php
// WebPageConnection.php (Database Connection)
$servername = "localhost";
$username = "root"; // Update with your database username if necessary
$password = ""; // Update with your database password if necessary
$dbname = "webpagedatabase"; // Your database name

$conn = mysqli_connect($servername, $username, $password, $dbname);

// Check if connection was successful
if (!$conn) {
    die("Connection Error!"); // If connection fails, display error message
}
echo "Connected, Let's Gooo!!!";

// Fetch the form data from POST request
$name = $_POST["txtName"];
$email = $_POST["txtEmail"];
$phone = $_POST["txtPhone"];
$address = $_POST["txtAddress"];

// SQL query to insert the data into the webpagetable
$sql = "INSERT INTO webpagetable (name, email, phone, address)
        VALUES ('$name', '$email', '$phone', '$address')";

// Output the SQL query for debugging purposes
var_dump($sql);

// Execute the SQL query
if (mysqli_query($conn, $sql)) {
    // Success: Record inserted
    echo "Not Impressed, But Fine..."; // Success message
} else {
    // Error: Something went wrong with the SQL query
    echo "Error: Worthless as Expected " . $sql . "<br>" . mysqli_error($conn); // Display the error message
}

// Close the database connection
mysqli_close($conn);
?>
