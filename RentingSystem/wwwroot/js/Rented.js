$(document).ready(function () {
    const logged = (typeof $.cookie('Identity.Cookie') !== 'undefined') && (sessionStorage.getItem("email") !== "");
    if (logged) {

        $.ajax({
            url: GetRentedItems,
            method: "GET",
            contentType: ContentType,
            data: USER_EMAIL
        }).done(function (data) {
            $('#Rented-table').dataTable({
                aaData: data,
                columns: [
                    { data: "rentId"},
                    { data: "itemId" },
                    { data: "name" },
                    { data: "quantity" },
                    { data: "rentTime" },
                    { data: "whenShouldBeReturned" },
                    { data: "rentReturnTime"},
                    {
                        data: null,
                        render:
                            function (data, type, full, meta) {
                              return `<button type="button" class="btn btn-primary">Return Items</button>`;
                            }
                    }
                ]
            });

        });


    }
});