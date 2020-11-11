$(document).ready(function () {
    const logged = (typeof $.cookie('Identity.Cookie') !== 'undefined') && (sessionStorage.getItem("email") !== "");

    if (logged) {
        $.ajax({
            url: Cart,
            method: "GET",
            contentType: ContentType,
            data: USER_EMAIL
        }).done(function (data) {
            $('#Cart-table').dataTable({
                aaData: data,
                columns: [
                    { data: "itemId" },
                    { data: "name" },
                    { data: "quantity" },
                    {
                        data: null,
                        render:
                            function (data, type, full, meta) {
                                return `<input type="number" value="1" min="1" max="${data.quantity}"/>
                                    <button type="button" class="btn btn-danger ml-2">Remove</button>`;
                            }
                    }
                ]
            });
        });

        $('#Cart-table tbody').on('click', 'button', function () {
            const row = $(this).closest('tr').find('td');
            const currentQuantity = parseInt(row.eq(2).text());
            let quantity = row.eq(3).find("input").val();
            if (quantity <= 0) {
                quantity = 1;
            }
            else if (quantity > currentQuantity) {
                quantity = currentQuantity;
            }
            else {
                quantity = parseInt(quantity);
            }

            const item = {
                itemId: parseInt(row.eq(0).text()),
                email: sessionStorage.getItem('email'),
                quantity: quantity
            };

            if (currentQuantity > 0) {
                $.ajax({
                    type: "DELETE",
                    url: RemoveFromCart,
                    contentType: ContentType,
                    data: JSON.stringify(item),
                    success: function () {
                        row.eq(2).html((currentQuantity - quantity).toString());
                        const newQuantity = parseInt(row.eq(2).text());
                        if (newQuantity === 0) {
                            row.remove();
                        }
                    },
                    error: function () {
                        console.log('Error in Operation');
                    }
                });
            }
        });

        $('#rent-btn').click(() => {
            console.log(
                RentItems + "?email=" + sessionStorage.getItem('email'));
            $.ajax({
                type: "POST",
                url: RentItems + "?email=" + sessionStorage.getItem('email'),
                contentType: ContentType,
                success: function () {
                    $('#Cart-table tbody').empty();
                },
                error: function () {
                    console.log('Error in Operation');
                }
            });
            console.log("work");
        });
    }
});