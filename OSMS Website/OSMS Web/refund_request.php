<?php
// Database connection
$servername = "localhost";
$username = "root"; // Update with your DB username
$password = ""; // Update with your DB password
$dbname = "vision_gate"; // Your database name

$conn = mysqli_connect($servername, $username, $password, $dbname);

// Check connection
if (!$conn) {
    die("Connection Error: " . mysqli_connect_error());
}

// Process the form submission
if ($_SERVER["REQUEST_METHOD"] === "POST") {
    // Fetch form data
    $order_id = $_POST['orderId'];
    $full_name = $_POST['fullName'];
    $email = $_POST['email'];
    $phone_number = $_POST['phone'];
    $refund_reason = $_POST['reason'];
    $additional_details = $_POST['details'] ?? '';

    // Validate required fields
    if (empty($order_id) || empty($full_name) || empty($email) || empty($phone_number) || empty($refund_reason)) {
        echo "Error: All required fields must be filled!";
        exit;
    }

    // Prepare SQL query
    $sql = "INSERT INTO refund_requests (order_id, full_name, email, phone_number, refund_reason, additional_details)
            VALUES ('$order_id', '$full_name', '$email', '$phone_number', '$refund_reason', '$additional_details')";

    // Execute the query
    if (mysqli_query($conn, $sql)) {
        echo "Refund request submitted successfully! 🎉";
    } else {
        echo "Error: " . $sql . "<br>" . mysqli_error($conn);
    }
}

// Close the connection
mysqli_close($conn);
?>
