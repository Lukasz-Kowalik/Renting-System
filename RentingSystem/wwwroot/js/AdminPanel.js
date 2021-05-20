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
                    { data: "userId" },
                    { data: "firstName" },
                    { data: "lastName" },
                    { data: "email" }, {
                        data: null,
                        render:
                            function (data, type, full, meta) {
                                const time = data.maxReturnTimeInDays;
                                return `<input id="time-${data.userId}" typ="number" min="0" value="${time}">`;
                            }
                    },
                    {
                        data: null,
                        render:
                            function (data, type, full, meta) {
                                const id = data.role;
                                return `
                                    <select id="roles">
                                      <option value="1"${1 === id ? 'selected' : ""}>User</option>
                                      <option value="2"${2 === id ? 'selected' : ""}>Admin</option>
                                    </select>
                                    <button type="button" class="btn btn-primary ml-2">Save</button>`;
                            }
                    }
                ]
            });
        });

        $('#User-table tbody').on('click', 'button', function () {
            const row = $(this).closest('tr').find('td');

            const userId = parseInt(row.eq(0).text());
            const days = row.eq(4).find("input").val();
            const roleId = parseInt(row.eq(5).find("#roles option:selected").val());

            $.ajax({
                type: "PATCH",
                url: ChangeUserRole + `/${userId}/${roleId}/${days}`,
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