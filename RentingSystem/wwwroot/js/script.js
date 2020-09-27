/*
 *Created by £ukasz Kowalik
 *
 */

import { Items } from "./Constants/Routs";
$(document).ready(function () {
    $('#Item-table').DataTable({
        ajax: Items
    });
});