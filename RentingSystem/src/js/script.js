//-----------------API CALLS---------------
const ContentType = "application/json; charset=utf-8";
const APIHost = "http://localhost:8000/";
const Header = "Access-Control - Allow - Origin: "
const Items = APIHost + "Items/getList";
const RentedItems = APIHost + "RentedItems";
const Rents = APIHost + "Rents";
const User = APIHost + "Users";
const Item = APIHost + "Items";
const AddToCart = APIHost + "Cart/Add";

$(document).ready(function () {
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
                {
                    data: null,
                    render:
                        function (data, type, full, meta) {
                            return `<button type="button" class="btn btn-primary">
                        Add</button>`;
                        }
                }
            ],
            buttons: [
                'copy', 'csv', 'excel', 'pdf', 'print'
            ]
        });
    });

    $('#Item-table tbody').on('click', 'button', function () {
        const row = $(this).closest('tr').find('td');
        const item = {
            itemId: parseInt(row.eq(0).text()),
            name: row.eq(1).text(),
            quantity: 1
        };
        const quantity = parseInt(row.eq(2).text());

        $.ajax({
            type: "POST",
            url: AddToCart,
            contentType: ContentType,
            data: JSON.stringify(item),
            dataType: "json",
            success: function (data, textStatus, xhr) {
                row.eq(2).html((quantity - 1).toString());
            },
            error: function (xhr, textStatus, errorThrown) {
                console.log('Error in Operation');
            }
        });
    });

    //$('#Cart-table').DataTable({
    //    data: userCart,
    //    column:
    //        [
    //            { title: "id" },
    //            { title: "Name" },
    //            { title: "Quantity" }
    //        ]
    //});
});