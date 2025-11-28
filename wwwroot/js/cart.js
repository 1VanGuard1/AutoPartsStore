function addToCart(productId) {
    fetch("/Cart/Add?productId=" + productId, {
        method: "POST"
    }).then(r => r.json())
        .then(_ => {
            alert("Товар добавлен в корзину!");
        });
}
