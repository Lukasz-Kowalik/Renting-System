$(document).ready(function () {
    const logged = (typeof $.cookie('Identity.Cookie') !== 'undefined') && (sessionStorage.getItem("email") !== "");
    let table;
    if (logged) {
        $.ajax({
            url: GetRentedItems,
            method: "GET",
            contentType: ContentType,
            data: USER_EMAIL
        }).done(function (data) {
        table=    $('#Rented-table').dataTable({
                responsive: true,               
                order: [[0,"desc"]],
                aaData: data,
                columns: [
                    { data: "rentId" },
                    { data: "itemId" },
                    { data: "name" },
                    { data: "quantity" },
                    { data: "category" },             
                    {
                         data:"rentTime",
                        render: function (data, type, row) {                           
                            return moment(row["rentTime"]).format('DD/MM/YYYY HH:mm');;
                        }
                    },  {
                        data: "whenShouldBeReturned",
                        render: function (data, type, row) {
                            return moment(row["whenShouldBeReturned"]).format('DD/MM/YYYY HH:mm');

                        }

                        
                    },  {
                        data: "rentReturnTime",
                        render: function (data, type, row) {
                            const temp = row["rentReturnTime"];
                            return temp === null ? "" : moment(temp).format('DD/MM/YYYY HH:mm');
                        }
                        
                    }               
                    ]
            });
        });
    }

});