$(document).ready(function () {
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
                            return `<button type="button" class="btn btn-danger">Remove</button>`;
                        }
                }
            ]
        });
    });

    $('#Cart-table tbody').on('click', 'button', function () {
        const row = $(this).closest('tr').find('td');
        const item = {
            itemId: parseInt(row.eq(0).text()),
            email: sessionStorage.getItem("email")
        };
        const quantity = parseInt(row.eq(2).text());
        if (quantity > 0) {
            $.ajax({
                type: "DELETE",
                url: RemoveFromCart,
                contentType: ContentType,
                data: JSON.stringify(item),
                success: function () {
                    row.eq(2).html((quantity + 1).toString());
                },
                error: function () {
                    console.log('Error in Operation');
                }
            });
        }
    });


    $("#rent-btn").click(() => {
        alert("klik");
    });
});