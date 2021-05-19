$(document).ready(function () {
    const logged = (typeof $.cookie('Identity.Cookie') !== 'undefined') && (sessionStorage.getItem("email") !== "");
    //First load
    let isFullTable = true;
    
    if (isFullTable) {
        $.ajax({
            url: Items,
            method: "GET",
            contentType: ContentType
        }).done(function (data) {
            $('#Item-table').DataTable({
                aaData: data,
                columns: [
                    { data: "itemId" },
                    { data: "name" },
                    { data: "quantity" },
                    { data: "category" },
                    {
                        data: null,
                        render:
                            function (data, type, full, meta) {
                                let result = '';
                                if (logged)
                                    result += `
                                <input type="number" value="1" min="1" max="${data.quantity}"/>
                                <button type="button" class="btn btn-primary ml-2" >Add</button>`
                                result += `
                                <a href="${Host}Items/${data.itemId}" class="btn btn-light ml-2">
                                Details</a>`
                                return result;
                            }
                    }
                ],
                dom: 'lBfrtip',
                buttons: [
                    'copy', 'csv', 'pdf', 'print'
                ]
            });
        });
    }
    $.ajax({
        url: Category,
        method: "GET",
        contentType: ContentType
    }).done(function (data) {
        for (var i = data.length; i > 0; i--) {
            const d = `  <div>
                                <input type="checkbox" id="${data[i - 1].id}"  value="${data[i - 1].id}">
                                <label for="${data[i - 1].name}">${data[i - 1].name}</label>
                            </div>`
            $("form").prepend(d);
        }
    });

    //After submitting
    $("form").submit(function (event) {
        event.preventDefault();
        isFullTable = false;

        const values = [];
        $("input[type=checkbox]:checked").val(function (index, value) {
            values.push(parseInt(value));
            return value;
        });

        $.ajax({
            url: GetSortedList,
            type: 'POST',
            dataType: "json",
            traditional: true,
            data: { ids: values }
        }).done(function (data) {
           
            $('#Item-table').DataTable().clear().draw();
            $('#Item-table').DataTable().rows.add(data);
            $('#Item-table').DataTable().columns.adjust().draw();
            
        });
    });

    if (logged) {
        $('#Item-table tbody').on('click', 'button', function () {
            const row = $(this).closest('tr').find('td');
            const currentQuantity = parseInt(row.eq(2).text());
            let quantity = row.last().find("input").val();

            if (quantity <= 0) {
                quantity = 1;
            }
            else if (quantity >= currentQuantity) {
                quantity = currentQuantity;
            } else {
                quantity = parseInt(quantity);
            }

            const item = {
                itemId: parseInt(row.eq(0).text()),
                email: sessionStorage.getItem('email'),
                quantity: quantity
            };
            if (currentQuantity > 0) {
                $.ajax({
                    type: "POST",
                    url: AddToCart,
                    contentType: ContentType,
                    data: JSON.stringify(item),
                    success: function () {
                        row.eq(2).html((currentQuantity - quantity).toString());
                    },
                    error: function () {
                        alert('Error in Operation');
                    }
                });
            }
        });
    };
});