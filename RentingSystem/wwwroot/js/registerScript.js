$(document).ready(function () {
    const x = $("#registerForm").serialize();
    $("#registerForm").submit(function () {
    const jqxhr = $.post('localhost:8000/CreateUser', $('#registerForm').serialize())
        .success(
            function() {
                const loc = jqxhr.getResponseHeader('Location');
                console.log(jqxhr);
                console.log(loc);
            }).error(function() {
            $('#message').html("Error posting the update.");
        });
    return false;
});
});
