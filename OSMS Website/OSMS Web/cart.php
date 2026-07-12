<?php
session_start();

if (empty($_SESSION['cart'])) {
    $message = "Your cart is empty.";
} else {
    $cartItems = $_SESSION['cart'];
    $message = "";
}

?>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Your Cart</title>
    <style>
        /* Add your previous styles here */
    </style>
</head>
<body>
    <header>Your Cart</header>
    
    <div class="cart-container">
        <?php if ($message): ?>
            <p><?= $message ?></p>
        <?php else: ?>
            <table>
                <thead>
                    <tr>
                        <th>Product</th>
                        <th>Color</th>
                        <th>Size</th>
                        <th>Quantity</th>
                        <th>Price</th>
                    </tr>
                </thead>
                <tbody>
                    <?php foreach ($cartItems as $item): ?>
                        <tr>
                            <td><?= $item['name'] ?></td>
                            <td><?= $item['color'] ?></td>
                            <td><?= $item['size'] ?></td>
                            <td><?= $item['quantity'] ?></td>
                            <td>Rs. <?= $item['price'] * $item['quantity'] ?></td>
                        </tr>
                    <?php endforeach; ?>
                </tbody>
            </table>
            <a href="checkout.php" class="checkout-btn">Proceed to Checkout</a>
        <?php endif; ?>
    </div>
</body>
</html>
