<?php
session_start();

// Sample product list
$products = [
    ['name' => 'Product 1', 'price' => 50, 'image' => 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR7vmx4uXjJTnkZJPRg_7PaqPdWcQSKxl2zsg&s'],
    ['name' => 'Product 2', 'price' => 75, 'image' => 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTYOf7lHvzGewMxvNLlzCxMzHxYVoYhdcy2wQ&s'],
    ['name' => 'Product 3', 'price' => 100, 'image' => 'https://www.adorama.com/alc/wp-content/uploads/2021/05/bird-wings-flying-feature.gif']
];

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    // Handle adding to cart
    $productIndex = $_POST['productIndex'];
    $color = $_POST['color'];
    $size = $_POST['size'];
    $quantity = $_POST['quantity'];

    $product = $products[$productIndex];

    $cartItem = [
        'name' => $product['name'],
        'price' => $product['price'],
        'color' => $color,
        'size' => $size,
        'quantity' => $quantity
    ];

    // Add to session cart (simulating cart storage)
    $_SESSION['cart'][] = $cartItem;

    // Redirect to avoid form resubmission
    header('Location: ' . $_SERVER['PHP_SELF']);
    exit();
}
?>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Product Catalog</title>
    <style>
        /* Add your previous styles here */
    </style>
</head>
<body>
    <header>Product Catalog</header>
    <div class="catalog-container">
        <?php foreach ($products as $index => $product): ?>
            <div class="product-item">
                <img src="<?= $product['image'] ?>" alt="<?= $product['name'] ?>">
                <h3><?= $product['name'] ?></h3>
                <p>Price: Rs. <?= $product['price'] ?></p>
                
                <!-- Customize & Add to Cart Form -->
                <form action="" method="POST">
                    <input type="hidden" name="productIndex" value="<?= $index ?>">

                    <label for="color">Select Color</label>
                    <select name="color" required>
                        <option value="">Choose a color</option>
                        <option value="Red">Red</option>
                        <option value="Blue">Blue</option>
                        <option value="Green">Green</option>
                        <option value="Black">Black</option>
                    </select>

                    <label for="size">Select Size</label>
                    <select name="size" required>
                        <option value="">Choose a size</option>
                        <option value="Small">Small</option>
                        <option value="Medium">Medium</option>
                        <option value="Large">Large</option>
                    </select>

                    <label for="quantity">Quantity</label>
                    <input type="number" name="quantity" value="1" min="1" required>

                    <button type="submit">Add to Cart</button>
                </form>
            </div>
        <?php endforeach; ?>
    </div>

    <!-- Link to cart page -->
    <a href="cart.php" class="view-cart-btn">Go to Cart</a>
</body>
</html>
