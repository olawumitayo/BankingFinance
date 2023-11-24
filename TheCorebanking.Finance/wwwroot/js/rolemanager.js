var $usertable = $('#userAssignTable'), $roletable = $('#roleAssignTable'), $button = $('#btnMapRole');


$(function () {
    $button.click(function () {
        debugger
        var userData = $usertable.bootstrapTable('getAllSelections');
        var roleData = $roletable.bootstrapTable('getAllSelections');
        $.each(userData, function (index, userItemData) {
            debugger
            $.each(roleData, function (index, roleItemData) {
                debugger
                $.ajax({
                    url: '../Administration/AddUserRole',
                    type: 'POST',
                    data: { RoleName: roleItemData.name, Username: userItemData.userName},
                    success: function (data) {

                        debugger
                        //$table.bootstrapTable('refresh', { url: '../Administration/' });
                    },
                    error: function (e) {
                        alert("An exception occured!");
                    }
                });

            });

        });
       
        alert('Selected transactions posted successfully!');
    });
});