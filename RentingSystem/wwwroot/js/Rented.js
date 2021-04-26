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
                    { data: "rentId" },
                    { data: "itemId" },
                    { data: "name" },
                    { data: "quantity" },
                    { data: "category"},
                    { data: "rentTime" },
                    { data: "whenShouldBeReturned" },
                    { data: "rentReturnTime" },
                    
                ]
            });
        });

    }
});