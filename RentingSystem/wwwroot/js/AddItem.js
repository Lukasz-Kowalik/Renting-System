$(document).ready(function () {
    $.ajax({
        url: Category,
        method: "GET",
        contentType: ContentType
    }).done(function (data) {
        for (var i = data.length; i > 0; i--) {
            const d = `<option id="${data[i - 1].id}"  value="${data[i - 1].id}">${data[i - 1].name}</option>`
            $("#category").prepend(d);
        }
    });

    $("form").submit(function (event) {
        event.preventDefault();
        const formData = {
            ImageUrl: $("#url").val(),
            Name: $("#name").val(),
            CategoryId: $("#category").val(),
            MaxQuantity: $("#max-quantity").val(),
            Url: $("#documentation").val(),
            Description: $("#description").val()
        };
        $.ajax({
            type: "POST",
            url: Item,
            data: formData,
            dataType: "json",
            error: function () {
                alert("Error");
            }
        }).done(
           window.location.href = Host
        );
    });
});