"use strict";var _Routs=require("./Constants/Routs");$(document).ready((function(){$("#Item-table").DataTable({ajax:_Routs.Items})}));
"use strict";Object.defineProperty(exports,"__esModule",{value:!0}),exports.User=exports.Rents=exports.RentedItems=exports.Items=exports.Host=void 0;var Host="http://localhost:8000/";exports.Host=Host;var Items=Host+"Items";exports.Items=Items;var RentedItems=Host+"RentedItems";exports.RentedItems=RentedItems;var Rents=Host+"Rents";exports.Rents=Rents;var User=Host+"Users";exports.User=User;