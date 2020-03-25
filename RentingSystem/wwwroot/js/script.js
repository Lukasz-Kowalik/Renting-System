//$(document).ready(function () {
//    function SendFormPost(webSubpage, form) {
//        $(form).submit(function (event) {
//            const data = $(form).serialize();
//            $.ajax({
//                type: 'POST',
//                url: 'http://localhost:8000/' + webSubpage,
//                contentType: "application/json; charset=utf-8",
//                dataType: "json",
//                data: JSON.stringify(data),
//                success: function (msg) {
//                    console.log(msg);
//                }
//            });
//        });
//    }

//    SendFormPost('#registerForm', 'CreateUser');
//});
//window.onload = function() {
//  $(document).ready(function(e) {
//    $('#loginBtn').on('click', validate);
//  });
//};

//function validate() {
//  const password = $('#password').val();
//  const email = $('#email').val();
//}
//function showIfIsValidate(object) {
//  $(object).addClass('is-valid');
//}
//function showIfIsNotValidate(object) {
//  $(object).addClass('is-invalid');
//}
//function validateEmail(email) {
//  // const re = /^(([^<>()[]\\.,;:s@"]+(.[^<>()[]\\.,;:s@"]+)*)|(".+"))@(([[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}])|(([a-zA-Z-0-9]+.)+[a-zA-Z]{2,}))$/;
//  const regex = new RegExp(
//    '/^(([^<>()[]\\.,;:s@"]+(.[^<>()[]\\.,;:s@"]+)*)|(".+"))@(([[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}.[0-9]{1,3}])|(([a-zA-Z-0-9]+.)+[a-zA-Z]{2,}))$/'
//  );
//  return regex.test(email);
//}

//function isMediumPassword(pass) {
//  const mediumRegex = new RegExp(
//    '^(((?=.*[a-z])(?=.*[A-Z]))|((?=.*[a-z])(?=.*[0-9]))|((?=.*[A-Z])(?=.*[0-9])))(?=.{6,})'
//  );
//  return mediumRegex.test(pass);
//}