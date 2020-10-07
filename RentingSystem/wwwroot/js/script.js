 const Host = "http://localhost:8000/";
 const Items = Host + "Items";
 const RentedItems = Host + "RentedItems";
 const Rents = Host + "Rents";
 const User = Host + "Users";

$(document).ready(function () {
    $('#Item-table').DataTable({
        ajax: { Items }
    });
    let zmienna = 5;
    for (var i = 0; i < zmienna; i++) {
        console.log(i);
    }
});