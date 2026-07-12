<?php
// Database connection
$servername = "localhost";
$username = "root";
$password = "";
$dbname = "product_catalog";

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);

// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

// Fetch products from the database
$sql = "SELECT * FROM products";
$result = $conn->query($sql);

if ($result->num_rows > 0) {
    while ($row = $result->fetch_assoc()) {
        // Ensure all required fields are non-empty
        $image = !empty($row["image"]) ? htmlspecialchars($row["image"]) : 'placeholder.jpg'; // Use placeholder if no image
        $name = htmlspecialchars($row["name"]);
        $price = htmlspecialchars($row["price"]);
        $id = htmlspecialchars($row["id"]);

        echo '
            <div class="product-item">
                <img src="' . $image . '" alt="' . $name . '">
                <h3>' . $name . '</h3>
                <p>Price: Rs. ' . $price . '</p>
                <button onclick="openPopup(' . $id . ')">Customize & Add to Cart</button>
            </div>
        ';
    }
} else {
    echo "<p>No products available.</p>";
}

$conn->close();
?>
