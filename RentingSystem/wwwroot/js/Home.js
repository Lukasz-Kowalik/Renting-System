$(document).ready(function () {
    const logged = (typeof $.cookie('Identity.Cookie') !== 'undefined') && (sessionStorage.getItem("email") !== "");
    $.ajax({
        url: Items,
        method: "GET",
        contentType: ContentType
    }).done(function (data) {
      const table=  $('#Item-table').dataTable({
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
            dom: 'lBfrtip',
            buttons: [
                'copy', 'csv', 'excel', 'pdf', 'print'
            ]
           
      });
      console.log(table);
    
    });
    
    if (logged) {
        $('#Item-table tbody').on('click', 'button', function () {
            const row = $(this).closest('tr').find('td');
            const item = {
                itemId: parseInt(row.eq(0).text()),
                email: sessionStorage.getItem("email")
            };
            const quantity = parseInt(row.eq(2).text());
            if (quantity > 0) {
                $.ajax({
                    type: "POST",
                    url: AddToCart,
                    contentType: ContentType,
                    data: JSON.stringify(item),
                    success: function () {
                        row.eq(2).html((quantity - 1).toString());
                    },
                    error: function () {
                        console.log('Error in Operation');
                    }
                });
            }
        });
    };
});