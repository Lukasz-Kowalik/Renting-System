$(document).ready(function () {
    $("form").submit(function (event) {
        event.preventDefault();
        const formData = {
            name: $("#name").val(),
        };
        
        $.ajax({
            type: "POST",
            url: Category,
            data: formData,
            cache: false,
            dataType: "json"
        }).done(window.location.href = Host + "Category");

    });
});