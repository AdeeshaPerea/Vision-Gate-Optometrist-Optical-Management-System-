<?php
// Database connection
$servername = "localhost";
$username = "root"; // Replace with your database username
$password = ""; // Replace with your database password
$dbname = "vision_gate"; // Your database name

$conn = mysqli_connect($servername, $username, $password, $dbname);

// Check connection
if (!$conn) {
    die("Connection Error: " . mysqli_connect_error());
}

// Handle form submission
if ($_SERVER["REQUEST_METHOD"] === "POST") {
    $name = $_POST['name'];
    $feedback = $_POST['feedback'];

    // Validate input
    if (!empty($name) && !empty($feedback)) {
        $sql = "INSERT INTO feedback (name, feedback) VALUES ('$name', '$feedback')";

        if (mysqli_query($conn, $sql)) {
            echo "Feedback submitted successfully!";
        } else {
            echo "Error: " . $sql . "<br>" . mysqli_error($conn);
        }
    } else {
        echo "Error: Name and Feedback are required!";
    }

    mysqli_close($conn);
    exit;
}

// Retrieve all feedback
$sql = "SELECT * FROM feedback ORDER BY submitted_at DESC";
$result = mysqli_query($conn, $sql);

$feedbackData = [];
if ($result && mysqli_num_rows($result) > 0) {
    while ($row = mysqli_fetch_assoc($result)) {
        $feedbackData[] = $row;
    }
}

mysqli_close($conn);
?>
