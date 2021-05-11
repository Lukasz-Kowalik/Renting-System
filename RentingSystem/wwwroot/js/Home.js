$(document).ready(function () {
    const logged = (typeof $.cookie('Identity.Cookie') !== 'undefined') && (sessionStorage.getItem("email") !== "");
    $.ajax({
        url: Items,
        method: "GET",
        contentType: ContentType
    }).done(function (data) {
        $('#Item-table').dataTable({
            aaData: data,
            columns: [
                { data: "itemId" },
                { data: "name" },
                { data: "quantity" },
                { data: "category" },
                {
                    data: null,
                    render:
                        function (data, type, full, meta) {
                            return `

<input type="number" value="1" min="1" max="${data.quantity}"/>
<button type="button" class="btn btn-primary ml-2" >Add</button>

<button type="button" class="btn btn-light ml-2" disabled>
<a href="${Host}Items/${data.itemId}">
Details</a></button>
`;
                        }
                }
            ],
            dom: 'lBfrtip',
            buttons: [
                'copy', 'csv', 'pdf', 'print'
            ]
        });
    });

    if (logged) {
        $('#Item-table tbody').on('click', 'button', function () {
            const row = $(this).closest('tr').find('td');
            const currentQuantity = parseInt(row.eq(2).text());
            let quantity = row.last().find("input").val();

            if (quantity <= 0) {
                quantity = 1;
            }
            else if (quantity >= currentQuantity) {
                quantity = currentQuantity;
            } else {
                quantity = parseInt(quantity);
            }

            const item = {
                itemId: parseInt(row.eq(0).text()),
                email: sessionStorage.getItem('email'),
                quantity: quantity
            };
            if (currentQuantity > 0) {
                $.ajax({
                    type: "POST",
                    url: AddToCart,
                    contentType: ContentType,
                    data: JSON.stringify(item),
                    success: function () {
                        row.eq(2).html((currentQuantity - quantity).toString());
                    },
                    error: function () {
                        alert('Error in Operation');
                    }
                });
            }
        });
    };
});