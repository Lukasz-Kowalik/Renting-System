$(document).ready(function () {
    const logged = (typeof $.cookie('Identity.Cookie') !== 'undefined') && (sessionStorage.getItem("email") !== "");
    //First load
 
                if (logged) {

        $("#display").hover(
            function () {
                    $.ajax({
                        url: Cart,
                        method: "GET",
                        contentType: ContentType,
                        data: USER_EMAIL
                    }).done(function (data) {
                        for (var i = 0; i < data.length; i++) {
                            $("#Cart-Info-Table").append(`<tr class="row-subCart">
                                <td class="text-justify">${data[i].name}</td>
                                <td class="text-center">${data[i].quantity}</td>
                                </tr>`)
                        }
                    });
                $(".Info").removeClass("d-none");
            },
            function () {
                $(".Info").addClass("d-none");
                $(".row-subCart").remove();

            }
        )
                }
});