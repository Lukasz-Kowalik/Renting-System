$(document).ready(function () {
    const pathname = window.location.pathname;
    const Id = pathname.charAt(pathname.length - 1);
    $(".btn-back").attr("href", Host + `Items/${Id}`);
    $.ajax({
        url: Category,
        method: "GET",
        contentType: ContentType,
    })
        .done(function (data) {
            for (var i = data.length; i > 0; i--) {
                const d = `<option id="${data[i - 1].id}"  value="${data[i - 1].id}">${data[i - 1].name}</option>`
                $("#category").prepend(d);
            }
        });
    $.ajax({
        url: Item + "/" + Id,
        method: "GET",
        contentType: ContentType,
    })
        .done(function (data) {
            $("#url").val(data.imageUrl),
                $("#name").val(data.name),
                $("#category").val(data.category.id).change(),
                $("#max-quantity").val(data.maxQuantity),
                $("#documentation").val(data.url),
                $("#description").val(data.description)
        })


    $("form").submit(function (event) {
        event.preventDefault();
        const formData = {
            itemId: Id,
            imageUrl: $("#url").val(),
            name: $("#name").val(),
            categoryId: $("#category").val(),
            maxQuantity: $("#max-quantity").val(),
            url: $("#documentation").val(),
            description: $("#description").val(),
        };

        $.ajax({
            type: "PUT",
            url: Item,
            data: formData,
            cache: false,
            dataType: "json",
            error: function () {
                alert("Error");
            }
        }).done(window.location.href = Host + "Items/" + Id);
    });
});