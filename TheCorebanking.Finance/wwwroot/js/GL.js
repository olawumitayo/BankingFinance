var url_path = window.location.pathname;
if (url_path.charAt(url_path.length - 1) == '/') {
    url_path = url_path.slice(0, url_path.length - 1);
}
function glFormatter(value, row, index) {
    return [
        '<div class="btn-group">' + '<a style="color:white"  class="edit btn btn-sm btn-info"  title="Edit GL">'
        + '<i class="fas fa-edit">&nbsp;</i>' +
        '<a style="color:white" title="Remove GL" class="remove btn btn-sm btn-danger">'
        + '<i class="fas fa-trash"></i></a>' +
        '</a> '
    ].join('');
}

window.glEvents = {
    'click .edit': function (e, value, row, index) {
        if (row.state = true) {
            var data = JSON.stringify(row);     

            $('#Id').val(row.id);
            $('#ddlGL').val(row.bankId).trigger('change'); 
            $('#ddlBank').val(row.accountId);            
            $('#ddlBank').val(row.accountId);
            $('#ddlBranchs').val(row.branchName).trigger('change'); 
            $('#ContactName').val(row.contactName);
            $('#AccNo').val(row.accNo);
            $('#ddlName').val(row.bankName).trigger('change');
            $('#ContactPhoneNo').val(row.contactPhoneNo);           
            $('#ContactEmail').val(row.contactEmail); 
            $('#ContactAddress').val(row.contactAddress);        
            $('#AddNewGL').modal('show');
            $('#btnGLUpdate').html('<i class="now-ui-icons ui-1_check"></i> Update Record');
            $('#btnGL').hide();
            $('#btnGLUpdate').show();
        }
    },
    'click .remove': function (e, value, row, index) {
        info = JSON.stringify(row);
        console.log(info);
    
        debugger
        $('#ID').val(row.id);
        $.ajax({
            url: '../chart/RemoveGL',
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
                        $('#glTable').
                            bootstrapTable(
                                'refresh', { url: '../chart/listGL' });

                        //return false;
                    }
                    else {
                        swal("GL Mapping", "You cancelled add category.", "error");
                    }
                    $('#glTable').
                        bootstrapTable(
                            'refresh', { url: '../chart/listGL' });
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

function updateGL() {
    debugger
    var reg=  {
        Id: $('#Id').val(),
        BankId: $('#ddlGL').val(),
        BankName: $('#ddlName').val(),
        BranchName: $('#ddlBranchs').val(),
        ContactName: $('#ContactName').val(),
        ContactPhoneNo: $('#ContactPhoneNo').val(),
        ContactEmail: $('#ContactEmail').val(),
        ContactAddress: $('#ContactAddress').val(),
        AccNo: $('#AccNo').val(),
        AccountId: $('#ddlBank').val()
    }
  
    $("input[type=submit]").attr("disabled", "disabled");

    $('#frmgl').validate({

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
                text: "GL mapping will be updated!",
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
                    $("#btnGLUpdate").attr("disabled", "disabled");
                    debugger
                    $.ajax({
                        url: '../chart/UpdateGL',
                        type: 'POST',
                        data: reg,
                        dataType: "json",
                        //headers: {
                        //    'VerificationToken': forgeryId
                        //},
                        success: function (result) {

                            if (result.toString != '' && result != null) {
                                swal({ title: 'Update GL', text: 'GL mapping updated successfully!', type: 'success' }).then(function () { window.location.reload(true); });
                                $('#AddNewGL').modal('hide');
                                $('#glTable').
                                    bootstrapTable(
                                        'refresh', { url: '../chart/listgl' });

                                $("#btnGLUpdate").removeAttr("disabled");
                            }
                            else {
                                swal({ title: 'Update GL Mapping', text: 'Something went wrong: </br>' + result.toString(), type: 'error' }).then(function () { clear(); });
                                $("#btnGLUpdate").removeAttr("disabled");
                            }
                        },
                        error: function (e) {
                            swal({ title: 'Update GL Mapping', text: 'GL mapping update encountered an error', type: 'error' }).then(function () { clear(); });
                            $("#btnGLUpdate").removeAttr("disabled");
                        }
                    });
                }
            });
        }

    },
        function (dismiss) {
            swal('Update GL Mapping', 'You cancelled gl update.', 'error');
            $("#btnGLUpdate").removeAttr("disabled");
        });

}



$(document).ready(function ($) {
    $('#btnGLUpdate').hide();
    $('#btnGL').show();
    $('#btnGL').on('click', function () {
        debugger
        addGL();

    });

});
function addGL() {
    debugger

    $("input[type=submit]").attr("disabled", "disabled");
    $('#frmgl').validate({

        messages: {
            //BankId: {required:"Select bank is required"},
            BankName: { required: "Select bank is required"},
            BranchName: { required: "Branch name is required" },
            ContactName: { required: "Contact name is required" },
            ContactPhoneNo: { required: "Contact phone no is required" },
            ContactEmail: { required: "Contact email is required" },
            ContactAddress: { required: "Contact address is required" },
            AccNo: { required: "Account number is required" },
            AccountId: { required: "Account name is required" }

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
                text: "GL mapping will be added!",
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
                    $("#btnGL").attr("disabled", "disabled");

                    debugger            
                    var reg = {
                        Id: $('#Id').val(),
                        BankId: $('#ddlGL').val(),
                        BankName: $('#ddlName').val(),
                        BranchName: $('#ddlBranchs').val(),
                        ContactName: $('#ContactName').val(),
                        ContactPhoneNo: $('#ContactPhoneNo').val(),
                        ContactEmail: $('#ContactEmail').val(),
                        ContactAddress: $('#ContactAddress').val(),
                        AccNo: $('#AccNo').val(),
                        AccountId: $('#ddlBank').val()
                    }    
                    $.ajax({
                        url: '../chart/AddGL',
                        type: 'POST',
                        data: reg,
                        dataType: "json",                    
                        success: function (result) {
                            
                            if (result.toString != '' && result != null) {
                                swal({ title: 'Add GL', text: 'GL added successfully!', type: 'success' }).then(function () { window.location.reload(true); });
                                $('#AddNewGL').modal('hide');
                                $('#glTable').
                                    bootstrapTable(
                                        'refresh', { url: '../chart/listgl' });

                                $("#btnGL").removeAttr("disabled");
                            }
                            else {
                                swal({ title: 'Add GL', text: 'Something went wrong: </br>' + result.toString(), type: 'error' }).then(function () { clear(); });
                                $("#btnGL").removeAttr("disabled");
                            }
                        },
                        error: function (e) {
                            swal({ title: 'Add GL', text: 'Adding gl encountered an error', type: 'error' }).then(function () { clear(); });
                            $("#btnGL").removeAttr("disabled");
                        }
                    });
                }
            });
        }

    },
        function (dismiss) {
            swal('Add GL', 'You cancelled add gl.', 'error');
            $("#btnGL").removeAttr("disabled");
        });

}
$(document).ready(function ($) {
    debugger
    //for dropdown list control for Comapny (id=ddlCompany)starts here

    $("#ddlGroup").select2({
        theme: "bootstrap4",
        placeholder: "Loading...",

    });

    $.ajax({
        url: "FinanceSetup/loadgroup",
    }).then(function (response) {
        debugger
        $("#ddlGroup").select2({
            theme: "bootstrap4",
            // placeholder: "Select Company...",  
            width: '100%',
            data: response.results
        });
    });
});
$(document).ready(function ($) {
    debugger


    $("#ddlGL").select2({
        theme: "bootstrap4",
        placeholder: "Loading...",

    });
    $.ajax({
        url: "Chart/loadChart",
    }).then(function (response) {
        debugger
        $("#ddlGL").select2({
            theme: "bootstrap4",
            // placeholder: "Select Company...",  
            width: '100%',
            data: response.results
        });
    });

    $("#ddlGL").on("select2:select", function (e) {
        debugger
        $("#ddlBank").val(e.params.data.acctid);
        $("#ddlName").val(e.params.data.text);
       

     });
});
$(document).ready(function ($) {

    $('#btnUploadGL').on('click', function () {
        uploadGL();
    })
});

function uploadGL() {
    $.ajax({
        type: 'POST',
        url: '../chart/GLUpload',
        success: function (result) {

            if (result.toString != '' && result != null) {              

                $('#glTable').
                    bootstrapTable(
                    'refresh', { url: '../chart/listGL' });
                swal({ title: 'Upload Chart', text: 'GL uploaded successfully!', type: 'success' }).then(function () { window.location.reload(true); });

            }
            else {
                swal({ title: 'Upload GL', text: 'Something went wrong: </br>' + result.toString(), type: 'error' }).then(function () { clear(); });

            }
        },
    })

}


$(document).ready(function () {

    $("#ddlCompanys").select2({
        theme: "bootstrap4",
        placeholder: "Loading..."
    });
    $.ajax({
        url: "Chart/loadCompany",
    }).then(function (response) {

        $("#ddlCompanys").select2({
            theme: "bootstrap4",
            placeholder: "Select Company",
            data: response.results
        });
    });


    $('#ddlCompanys').on('change', function () {
        debugger
        $("#ddlBranchs").select2({
            theme: "bootstrap4",
            placeholder: "Loading..."
        });
        $.ajax({
            url: "Chart/loadBranch",
            data: { CoyId: $('#ddlCompanys').val() },
            type: "POST"
        }).then(function (response) {
            $("#ddlBranchs").empty().trigger('change');
            $("#ddlBranchs").select2({
                theme: "bootstrap4",
                placeholder: "Select Branch",
                data: response.results
            });
        });
    });


});
$(document).ready(function ($) {
  
    $('#btnGLUpdate').on('click', function () {
        debugger
        updateGL();
    });

});
function reloadpage() {
    location.reload();
}

function clear() {
    $('#Id').val('');
    $('#Active').val('');   
    $('#AccountGroupId').val('');
    $('#Descriptions').val('');
   
}



