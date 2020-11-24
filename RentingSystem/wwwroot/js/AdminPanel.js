$(document).ready(function () {
    const logged = (typeof $.cookie('Identity.Cookie') !== 'undefined') && (sessionStorage.getItem("email") !== "");

    if (logged) {
        $.ajax({
            url: Users,
            method: "GET",
            contentType: ContentType,
        }).done(function (data) {
            $('#User-table').dataTable({
                aaData: data,
                columns: [
                    { data: "UserId" },
                    { data: "Name" },
                    { data: "Surname" },
                    { data: "Email" },
                    {
                        data: null,
                        render:
                            function (data, type, full, meta) {
                                return `
                                    <select id="roles">
                                      <option value="1">User</option>
                                      <option value="2">Customer</option>
                                      <option value="3">Worker</option>
                                      <option value="4" selected>Customer</option>
                                    </select>
                                    <button type="button" class="btn btn-danger ml-2">Save</button>`;
                            }
                    }
                ]
            });
        });

        $('#User-table tbody').on('click', 'button', function () {
            const row = $(this).closest('tr').find('td');

            const userId = parseInt(row.eq(0).text()),
            const roleId = parseInt(row.eq(4).text())

            $.ajax({
                type: "PATCH",
                url: ChangeUserRole + `/${userId}/${roleId}`,
                contentType: ContentType,
                success: function () {
                    location.reload();
                },
                error: function () {
                    alert('Error in Operation');
                }
            });
        });
    }
});