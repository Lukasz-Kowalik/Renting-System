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
                order: [[0,"desc"]],
                aaData: data,
                columns: [
                    { data: "rentId" },
                    { data: "itemId" },
                    { data: "name" },
                    { data: "quantity" },
                    { data: "category" },             
                    {
                        render: function (data, type, row) {
                            return moment(row["rentTime"]).format('DD/MM/YYYY HH:mm');
                        }
                    },
                    {
                        render: function (data, type, row) {
                            return moment(row["whenShouldBeReturned"]).format('DD/MM/YYYY HH:mm');

                        }
                    },
                    {
                        render: function (data, type, row) {
                            const temp = row["rentReturnTime"];
                            return temp === null ? "" : moment(temp).format('DD/MM/YYYY HH:mm');
                        }
                    },
                                  ]
            });
        });
    }
});