<?php
session_start();

if (empty($_SESSION['cart'])) {
    $message = "Your cart is empty.";
} else {
    $cartItems = $_SESSION['cart'];
    $totalPrice = array_sum(array_map(function($item) {
        return $item['price'] * $item['quantity'];
    }, $cartItems));
}

?>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Checkout</title>
    <style>
        /* Add your previous styles here */
    </style>
</head>
<body>
    <header>Checkout</header>

    <div class="checkout-container">
        <?php if ($message): ?>
            <p><?= $message ?></p>
        <?php else: ?>
            <h3>Your Order Summary</h3>
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
            <h3>Total Price: Rs. <?= $totalPrice ?></h3>
            <!-- You can integrate a payment gateway here for processing payments -->
            <form action="process-payment.php" method="POST">
                <button type="submit">Proceed to Payment</button>
            </form>
        <?php endif; ?>
    </div>
</body>
</html>
