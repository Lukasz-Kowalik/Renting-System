$(document).ready(function () {
    $('#loginBtn').click(() => {
        const InputData = {
            password1: $("#old").val(),
            password2: $("#new").val(),
            email: $("#email").val(),
        }
        console.log(InputData);
        $.ajax({
            type: "PATCH",
            url: ResetPassword,
            contentType: ContentType,
            data: JSON.stringify(InputData),
            success: function () {
                window.location.replace(Login);
            },
            error: function () {
                alert('Error in Operation');
            }
        });
    });
});