<?php
// Database connection
$servername = "localhost";
$username = "root"; // Update with your DB username
$password = ""; // Update with your DB password
$dbname = "contact_form_db"; // Replace with your database name

$conn = mysqli_connect($servername, $username, $password, $dbname);

// Check connection
if (!$conn) {
    die("Connection Error!");
}

echo "Connected to the database successfully!<br>";

if ($_SERVER["REQUEST_METHOD"] === "POST") {
    // Get form inputs
    $name = $_POST['Name'];
    $email = $_POST['Email'];
    $contact = $_POST['Contact'];
    $address = $_POST['Address'];

    // Validate input
    if (empty($name) || empty($email) || empty($contact) || empty($address)) {
        echo "All fields are required.";
        exit;
    }

    // Prepare and execute SQL statement
    $sql = "INSERT INTO contact_info (name, email, contact, address) 
            VALUES ('$name', '$email', '$contact', '$address')";

    if (mysqli_query($conn, $sql)) {
        echo "Thank you! Your information has been submitted.";
    } else {
        echo "Error: " . $sql . "<br>" . mysqli_error($conn);
    }
}

// Close the database connection
mysqli_close($conn);
?>
