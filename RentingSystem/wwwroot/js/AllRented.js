$(document).ready(function () {
    const logged = (typeof $.cookie('Identity.Cookie') !== 'undefined') && (sessionStorage.getItem("email") !== "");
    if (logged) {
        $.ajax({
            url: GetAllRentedItems,
            method: "GET",
            contentType: ContentType,
        }).done(function (data) {
            $('#Rented-table').dataTable({
                order: [],
                aaData: data,
                columns: [
                    { data: "rentId" },
                    { data: "itemId" },
                    { data: "email" },
                    { data: "name" },
                    { data: "quantity" },
                    { data: "category" },
                    {
                        render: function (data) {
                            return moment(data).format('DD/MM/YYYY HH:mm');
                        }
                    },
                    {
                        render: function (data) {
                            return moment(data).format('DD/MM/YYYY HH:mm');
                        }
                    },
                    {
                        render: function (data) {
                            return moment(data).format('DD/MM/YYYY HH:mm');
                        }
                    },
                    {
                        data: null,
                        render:
                            function (data, type, full, meta) {
                                return `${data.isReturned ?
                                    `<h4><span class="badge badge - secondary">Returned</span></h4>` :
                                    `<button type="button" class="btn btn-primary ">Return Items</button>`}`;
                            }
                    }
                ]
            });
        });

        $('#Rented-table tbody').on('click', 'button', function () {
            const row = $(this).closest('tr').find('td');
            const item = {
                rentId: parseInt(row.eq(0).text()),
                itemId: parseInt(row.eq(1).text())
            };

            $.ajax({
                type: "PATCH",
                url: ReturnItems,
                contentType: ContentType,
                data: JSON.stringify(item),
                success: function () {
                    location.reload();
                },
                error: function () {
                    alert('Error in Operation');
                }
            });
        });
    }
});