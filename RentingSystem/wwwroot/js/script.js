 const Host = "http://localhost:8000/";
const Items = Host + "Items/getList";
 const RentedItems = Host + "RentedItems";
 const Rents = Host + "Rents";
 const User = Host + "Users";

$(document).ready(function () {
  $.ajax({
    'url': Items,
    'method': "GET",
    'contentType': 'application/json'
  }).done(function (data) {
      $('#Item-table').dataTable({
      "aaData": data,
      "columns": [
          { "data": "itemId" },
          { "data": "name" },
          { "data": "quantity" }
      
      ]
    })
  })


//  $('#Item-table').DataTable({
//    ajax: { Items }
//    console.log(ajax);
//});



});