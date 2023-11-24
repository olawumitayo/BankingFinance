var url_path = window.location.pathname;
if (url_path.charAt(url_path.length - 1) == '/') {
    url_path = url_path.slice(0, url_path.length - 1);
}

//Validation
$(document).ready(function ($) {
    $("#Description").mouseout(function (event) {
        debugger
        var descr = $('#Description').val();
        if (descr == '') {
            $.notify({ icon: "add_alert", message: 'Your description is empty' }, { type: 'danger', timer: 1000 });
            $(this).css('border-color', 'red');
            $('#Description').focus();
            return false;
        } else {
            $(this).css('border-color', 'green');
        }

    });
    $('#creategrouptitle').html("ADD NEW GROUP");
    $('#updategrouptitle').hide();
});
$(document).ready(function ($) {
    $("#ddlActive").mouseout(function (event) {
        debugger
        var status = $('#ddlActive').val();
        if (status == '') {
            $.notify({ icon: "add_alert", message: 'Your status is empty' }, { type: 'danger', timer: 1000 });
            $(this).css('border-color', 'red');
            $('#ddlActive').focus();
            return false;
        } else {
            $(this).css('border-color', 'green');
        }

    });
});


function groupFormatter(value, row, index) {
    return [
       
        '<div class="btn-group">' + '<a style="color:white" class="edit btn btn-sm  btn-info"  title="Edit Group">'
        + '<i class="fas fa-edit"></i>' +
        '<a style="color:white"  title="Remove company" class="remove btn btn-sm btn-danger">'
        + '<i class="fas fa-trash"></i></a>' +
        '</a> ' +'</div>'
        //'<a class="print btn btn-sm btn-priamry" target="_blank" href="../FinanceSetup/PrintPdf?id=' + row.id + '" title="print report">',
        //'<i class="now-ui-icons files_paper "></i></a>'
    ].join('');
}

window.groupEvents = {
    'click .edit': function (e, value, row, index) {
        if (row.state = true) {
            var data = JSON.stringify(row);
            $('#Ids').val(row.id);
            $('#Descriptions').val(row.description);
            //$('#ddlActive').val(row.active); 
            $('#ddlActives :selected').text(row.active);
            $('#UpdateGroup').modal('show');
            $('#btnGroupUpdate').html('  <i class="now-ui-icons ui-1_check"></i> Update Record');
            $('#btnGroupUpdate').show();
          
            
        }
    },
    'click .remove': function (e, value, row, index) {
        info = JSON.stringify(row);
        console.log(info);

        debugger
        $('#ID').val(row.id);
        $.ajax({
            url: url_path +'/financesetup/RemoveGroup',
            type: 'POST',
            data: { ID: row.id },
            success: function (data) {
                swal({
                    title: "Are you sure?",
                    text: "You are about to delete this record!",
                    type: "warning",
                    showCancelButton: true,
                    confirmButtonColor: "#ff9800",
                    confirmButtonText: "Yes, proceed",
                    cancelButtonText: "No, cancel!",
                    showLoaderOnConfirm: true,
                    preConfirm: function () {
                        return new Promise(function (resolve) {
                            setTimeout(function () {
                                resolve();
                            }, 4000);
                        });
                    }
                }).then(function (isConfirm) {
                    if (isConfirm) {



                        swal("Deleted succesfully");
                        //alert('Deleted succesfully');
                        $('#groupTable').
                            bootstrapTable(
                                'refresh', { url: url_path +'/financesetup/listgroup' });

                        //return false;
                    }
                    else {
                        swal("Group", "You cancelled add group.", "error");
                    }
                    $('#groupTable').
                        bootstrapTable(
                        'refresh', { url: url_path +'/financesetup/listgroup' });
                });
                return

            },

            error: function (e) {
                //alert("An exception occured!");
                swal("An exception occured!");
            }
        });
    }

};
$(document).ready(function ($) {
    //$('#btnGroupUpdate').show();
    //$('#btnGroup').hide();
    $('#btnGroupUpdate').on('click', function () {
        debugger
        updateGroup();
    });

});


$(document).ready(function ($) {

    $('#btnUploadGroup').on('click', function () {
        uploadGroup()
    });
});

function uploadGroup() {
    $.ajax({
        type: 'POST',
        url: url_path +'/financesetup/GroupUpload',        
        success: function (result) {
            debugger
            
            if (result.toString != '' && result != null) {              

                $('#groupTable').
                    bootstrapTable(
                    'refresh', { url: url_path +'/financesetup/listgroup' });
                swal({ title: 'Upload Group', text: 'Group uploaded successfully!', type: 'success' }).then(function () { clear(); });

            }
            else {
                swal({ title: 'Upload Group', text: 'Something went wrong: </br>' + result.toString(), type: 'error' }).then(function () { clear(); });

            }
        },
    })

}
function selectgroup() {

$('#groupTable tbody tr').each(function () {
    $description = $(this).find('td:eq(1)').text();
    if ($description == $('#Description').val()) {
        alert("You have same data")
    }
    });
}

function updateGroup() {
    debugger
    var reg = {
        Id : $('#Ids').val(),
        Description : $('#Descriptions').val(),
        Active : $('#ddlActives').val()
    }
    //var json_data = {};
    //json_data.Id=$('#Id').val();
    //json_data.Description = $('#Description').val();
    //json_data.Active = $('#Active').val();  
    $("input[type=submit]").attr("disabled", "disabled");

    $('#frmGroups').validate({
  
        errorPlacement: function (error, element) {
            $.notify({
                icon: "now-ui-icons travel_info",
                message: error.text(),
            }, {
                    type: 'danger',
                    placement: {
                        from: 'top',
                        align: 'right'
                    }
                });
        },
        submitHandler: function (form) {
            swal({
                title: "Are you sure?",
                text: "Group will be updated!",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#ff9800",
                confirmButtonText: "Yes, continue",
                cancelButtonText: "No, stop!",
                showLoaderOnConfirm: true,
                preConfirm: function () {
                    return new Promise(function (resolve) {
                        setTimeout(function () {
                            resolve();
                        }, 4000);
                    });
                }
            }).then(function (isConfirm) {
                if (isConfirm) {
                    $("#btnGroupUpdate").attr("disabled", "disabled");
                    debugger
                    $.ajax({
                        url: url_path +'/financesetup/UpdateGroup',
                        type: 'POST',
                        data: reg,
                        dataType: "json",
                        //headers: {
                        //    'VerificationToken': forgeryId
                        //},
                        success: function (result) {

                            if (result.toString != '' && result != null) {
                                swal({ title: 'Update Group', text: 'Group updated successfully!', type: 'success' }).then(function () { window.location.reload(true); });
                                $('#UpdateGroup').modal('hide');
                                $('#groupTable').
                                    bootstrapTable(
                                        'refresh', { url: url_path +'/financesetup/listgroup' });

                                $("#btnGroupUpdate").removeAttr("disabled");
                            }
                            else {
                                swal({ title: 'Update Group', text: 'Something went wrong: </br>' + result.toString(), type: 'error' }).then(function () { clear(); });
                                $("#btnGroupUpdate").removeAttr("disabled");
                            }
                        },
                        error: function (e) {
                            swal({ title: 'Update Group', text: 'Group update encountered an error', type: 'error' }).then(function () { clear(); });
                            $("#btnGroupUpdate").removeAttr("disabled");
                        }
                    });
                }
            });
        }

    },
        function (dismiss) {
            swal('Update Group', 'You cancelled group update.', 'error');
            $("#btnGroupUpdate").removeAttr("disabled");
        });

}



$(document).ready(function ($) {
    $('#btnGroupUpdate').hide();
    $('#btnGroup').show();
    $('#btnGroup').on('click', function () {
        debugger
        addGroup();
        
    });

});
function addGroup() {
    debugger
    $('#btnGroupUpdate').hide();

    $("input[type=submit]").attr("disabled", "disabled");    

    $('#frmGroup').validate({
        messages: {
            
            Description: { required: "Description is required" },
            lActive: { required: "Status is required" }
       
        },

        errorPlacement: function (error, element) {
            $.notify({
                icon: "now-ui-icons travel_info",
                message: error.text(),
            }, {
                    type: 'danger',
                    placement: {
                        from: 'top',
                        align: 'right'
                    }
                });
        },
        submitHandler: function (form) {
            swal({
                title: "Are you sure?",
                text: "Group will be added!",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#ff9800",
                confirmButtonText: "Yes, continue",
                cancelButtonText: "No, stop!",
                showLoaderOnConfirm: true,
                preConfirm: function () {
                    return new Promise(function (resolve) {
                        setTimeout(function () {
                            resolve();
                        }, 4000);
                    });
                }
            }).then(function (isConfirm) {
                if (isConfirm) {
                    $("#btnGroup").attr("disabled", "disabled");
                    debugger  
                    var reg = {
                        Id: $('#Id').val(),
                        Description : $('#Description').val(),
                        Active : $('#ddlActive').val()
                    }
                    //var json_data = {};
                    //json_data.Id = $('#Id').val();
                    //json_data.Description = $('#Description').val();
                    //json_data.Active = $('#Active').val();  
                    $.ajax({
                        url: url_path +'/financesetup/AddGroup',
                        type: 'POST',
                        data: reg,
                        dataType: "json",
                        //headers: {
                        //    'VerificationToken': forgeryId
                        //},
                        success: function (result) {
                           
                            if (result.toString != '' && result != null) {
                                swal({ title: 'Add Group', text: 'Group added successfully!', type: 'success' }).then(function () { window.location.reload(true); });
                                $('#AddNewGroup').modal('hide');
                                $('#groupTable').
                                    bootstrapTable(
                                        'refresh', { url: url_path +'/financesetup/listgroup' });

                                $("#btnGroup").removeAttr("disabled");
                            }
                            else {
                                swal({ title: 'Add Group', text: 'Something went wrong: </br>' + result.toString(), type: 'error' }).then(function () { clear(); });
                                $("#btnGroup").removeAttr("disabled");
                            }
                        },
                        error: function (e) {
                            swal({ title: 'Add Group', text: 'Adding group encountered an error', type: 'error' }).then(function () { clear(); });
                            $("#btnGroup").removeAttr("disabled");
                        }
                    });
                }
            });
        }

    },
        function (dismiss) {
            swal('Add Group', 'You cancelled add group.', 'error');
            $("#btnGroup").removeAttr("disabled");
        });

}


function reloadpage() {
    location.reload();
}

function clear() {
    $('#Id').val('');
    $('#Description').val('');
    $('#Active').val('');
    
  
}


