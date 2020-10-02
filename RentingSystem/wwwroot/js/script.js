export const Host = "http://localhost:8000/";
export const Items = Host + "Items";
export const RentedItems = Host + "RentedItems";
export const Rents = Host + "Rents";
export const User = Host + "Users";

$(document).ready(function () {
    $('#Item-table').DataTable({
        ajax: Items
    });
    let zmienna = 5;
    for (var i = 0; i < zmienna; i++) {
        console.log(i);
    }
});