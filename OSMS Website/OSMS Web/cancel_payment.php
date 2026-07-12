<?php
// Database connection
$servername = "localhost";
$username = "root"; // Replace with your database username
$password = ""; // Replace with your database password
$dbname = "payment_management"; // Your database name

$conn = mysqli_connect($servername, $username, $password, $dbname);

// Check connection
if (!$conn) {
    die("Connection Error: " . mysqli_connect_error());
}

// Handle form submission
if ($_SERVER["REQUEST_METHOD"] === "POST") {
    // Get form data
    $full_name = $_POST['name'];
    $email = $_POST['email'];
    $payment_id = $_POST['paymentID'];
    $cancel_reason = $_POST['cancelReason'];

    // Validate required fields
    if (empty($full_name) || empty($email) || empty($payment_id) || empty($cancel_reason)) {
        echo "Error: All fields are required!";
        exit;
    }

    // Prepare and execute SQL query
    $sql = "INSERT INTO payment_cancellations (full_name, email, payment_id, cancel_reason)
            VALUES ('$full_name', '$email', '$payment_id', '$cancel_reason')";

    if (mysqli_query($conn, $sql)) {
        echo "Your cancellation request has been submitted successfully!";
    } else {
        echo "Error: " . $sql . "<br>" . mysqli_error($conn);
    }
}

// Close the connection
mysqli_close($conn);
?>
