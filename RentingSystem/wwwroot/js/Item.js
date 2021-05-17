$(document).ready(function () {
    const logged = (typeof $.cookie('Identity.Cookie') !== 'undefined') && (sessionStorage.getItem("email") !== "");

    const pathname = window.location.pathname;
    const id = pathname.charAt(pathname.length - 1);
    if (logged) {
        const item = {
            itemId: parseInt(id),
            email: sessionStorage.getItem('email'),
            quantity: 1
        };
        $.ajax({
            type: "POST",
            url: AddToCart,
            contentType: ContentType,
            data: JSON.stringify(item),
            success: function () {
            },
            error: function () {
                alert('Error in Operation');
            }
        });
    }
}