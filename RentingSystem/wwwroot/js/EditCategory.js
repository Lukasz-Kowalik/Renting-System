$(document).ready(function () {
    const pathname = window.location.pathname;
    const categoryId = pathname.charAt(pathname.length - 1);
    $.ajax({
        url: Category + `/${categoryId}`,
        method: "GET",
        contentType: ContentType,   
    }).done (function (data) {
        $("#name").val(data.name);
    });

    $("form").submit(function (event) {
        event.preventDefault();
        const data = {
            id: categoryId,
            name: $("#name").val(),
        };

        $.ajax({
            type: "Patch",
            url: Category,
            data: data,
            cache: false,
            dataType: "json",
            success: () => window.location.href = Host + "Category",
            error: ()=> alert("Bad data")
        });
    });
});