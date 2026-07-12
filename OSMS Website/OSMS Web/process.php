<?php
// Database connection
$servername = "localhost";
$username = "root"; // Update with your DB username
$password = ""; // Update with your DB password
$dbname = "vision_gate"; // Replace with your database name

$conn = mysqli_connect($servername, $username, $password, $dbname);

// Check connection
if (!$conn) {
    die("Connection Error!");
}

// Check which form was submitted
if ($_SERVER["REQUEST_METHOD"] === "POST") {
    if (isset($_POST['examType'])) {
        // Handle "Book an Exam" form
        $examType = $_POST['examType'];
        $fullName = $_POST['examName'];
        $email = $_POST['examEmail'];
        $preferredDate = $_POST['examDate'];

        // Insert data into the exam_bookings table
        $sql = "INSERT INTO exam_bookings (exam_type, full_name, email, preferred_date) 
                VALUES ('$examType', '$fullName', '$email', '$preferredDate')";

        if (mysqli_query($conn, $sql)) {
            echo "Exam Booking Successful! 🎉";
        } else {
            echo "Error: " . $sql . "<br>" . mysqli_error($conn);
        }

    } elseif (isset($_POST['channelDoctor'])) {
        // Handle "Request Channeling" form
        $fullName = $_POST['channelName'];
        $doctor = $_POST['channelDoctor'];
        $preferredDate = $_POST['channelDate'];
        $paymentAmount = $_POST['channelAmount'];

        // Insert data into the channeling_requests table
        $sql = "INSERT INTO channeling_requests (full_name, doctor, preferred_date, payment_amount) 
                VALUES ('$fullName', '$doctor', '$preferredDate', '$paymentAmount')";

        if (mysqli_query($conn, $sql)) {
            echo "Channeling Request Submitted Successfully! 🎉";
        } else {
            echo "Error: " . $sql . "<br>" . mysqli_error($conn);
        }
    }
}

// Close the database connection
mysqli_close($conn);
?>
