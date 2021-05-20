$(document).ready(function () {
    const logged = (typeof $.cookie('Identity.Cookie') !== 'undefined') && (sessionStorage.getItem("email") !== "");
    if (logged) {
        $.ajax({
            url: Category,
            method: "GET",
            contentType: ContentType
        }).done(function (data) {
            $('#Category-table').dataTable({
                aaData: data,
                columns: [
                    { data: "id" },
                    { data: "name" },

                    {
                        data: null,
                        render:
                            function (data, type, full, meta) {
                                return `<a href="${Host}Category/Edit/${data.id}"
                                class="btn btn-light ml-2">
                                Edit</a>
 <button type="button" class="btn btn-danger ml-2" >Remove</button>

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
        $('#Category-table tbody').on('click', 'button', function (e) {
           
            e.preventDefault();
            const row = $(this).closest('tr').find('td');
            const id = parseInt(row.eq(0).text());
           
                $.ajax({
                    type: "DELETE",
                    url: Category + "/" + id,
                    success: function () {
                        location.reload();
                    },
                    error: function () {
                        alert('Error in Operation');
                    }
                });
            }
        );
    }
});