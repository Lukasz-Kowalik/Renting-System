$(document).ready(function () {
    const logged = (typeof $.cookie('Identity.Cookie') !== 'undefined') && (sessionStorage.getItem("email") !== "");
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
                                Edit</a>`;
                        }
                }
            ],
            dom: 'lBfrtip',
            buttons: [
                'copy', 'csv', 'pdf', 'print'
            ]
        });
    });


});