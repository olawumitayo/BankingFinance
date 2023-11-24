var url_path = window.location.pathname;
if (url_path.charAt(url_path.length - 1) == '/') {
    url_path = url_path.slice(0, url_path.length - 1);
}

function defaultFormatter(value, row, index) {
    return [
        '<a style="color:white"  class="edit btn btn-sm btn-info mr-2"  title="Edit default">'
        + '<i class="fas fa-edit"></i>' +
        '<a style="color:white"  title="Remove default" class="remove btn btn-sm btn-danger">'
        + '<i class="fas fa-trash"></i></a>' +
        '</a> '
    ].join('');
}

window.defaultEvents = {
    'click .edit': function (e, value, row, index) {
      
        if (row.state = true) {

            var data = JSON.stringify(row);
            $('#DfId').val(row.dfId);
            $('#ddlAccountName').val(row.dfDescription).trigger('change');
            $('#ddlAcctName').val(row.accountName).trigger('change');
            $('#ddlAcctNumber').val(row.accountId).trigger('change');   
            $('#ddlAccountType').val(row.dfDescription).trigger('change');         
           
            $('#ddlFinance :selected').text(row.financePnc);
            
            $('#ddlTeller :selected').text(row.tellerPnc);
            $('#AddNewDefault').modal('show');            
            $('#btnDefaultUpdate').html('  <i class="now-ui-icons ui-1_check"></i> Update Record');
            $('#btnDefaultUpdate').show();
            $('#btnDefault').hide();
        }
    },
    'click .remove': function (e, value, row, index) {
        info = JSON.stringify(row);
        console.log(info);  
        debugger
        var ID = $('#DfId').val(row.dfId);
        $.ajax({
            url:"../chart/RemoveDefault",
            type: 'POST',
            data: ID,
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
                        $('#defaultTable').
                            bootstrapTable(
                            'refresh', { url: 'default/listdefault' });

                        //return false;
                    }
                    else {
                        swal("Default", "You cancelled delete default.", "error");
                    }
                    $('#defaultTable').
                        bootstrapTable(
                        'refresh', { url:"../chart/listdefault"});
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
    $('#btnDefaultUpdate').show();
    $('#btnDefault').hide();
    $('#btnDefaultUpdate').on('click', function () {
        updateDefault();
      
    });

});
$(document).ready(function ($) {
    $('#btnDefaultUpdate').hide();
    $('#btnDefault').show();
    $('#btnDefault').on('click', function () {
        debugger
        addDefault();
       

    });

});
$(document).ready(function ($) {
    $('#btnUploadBank').on('click', function () {
        UploadBank();
    });
});
function UploadBank() {
    $.ajax({
        url:"../chart/BankUpload",
        type: 'POST',
        success: function (result) {
            if (result.toString != '' && result != null) {               

                $('#defaultTable').
                    bootstrapTable(
                    'refresh', { url:"../chart/listdefault" });
                swal({ title: 'Upload Bank', text: 'Bank uploaded successfully!', type: 'success' }).then(function () { clear(); });


            }
            else {
                swal({ title: 'Upload GL', text: 'Something went wrong: </br>' + result.toString(), type: 'error' }).then(function () { clear(); });

            }

        },

    });

}

function addDefault() {
    debugger

 

    $('#btnDefaultUpdate').hide();
    $("input[type=submit]").attr("disabled", "disabled");
    $('#frmdefault').validate({
        messages: {
            AccountName: { required: "Account Name is required" },
            AccountId: { required: "Account Number is required" },
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
                text: "Default of account will be added!",
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
                    $("#btnDefault").attr("disabled", "disabled");

                    debugger

                    var reg = {
                        DfId: $('#DfId').val(),
                        DfDescription: $('#ddlAccountName').val(), 
                        AccountName: $('#ddlAcctName').val(),
                        AccountId: $('#ddlAcctNumber').val(),
                        DfDescription: $('#ddlAccountType').val(),                   
                        FinancePnc: $('#ddlFinance').val(),                      
                        TellerPnc: $('#ddlTeller').val()
                    }
           
                    $.ajax({
                        url:"../chart/AddDefault",
                        type: 'POST',
                        data: reg,
                        dataType: "json",                     
                        success: function (result) {
                            if (result.toString != '' && result != null) {
                                swal({ title: 'Add Default', text: 'Default added successfully!', type: 'success' }).then(function () { window.location.reload(true); });
                                $('#AddNewDefault').modal('hide');
                                $('#defaultTable').
                                    bootstrapTable(
                                    'refresh', { url:"../chart/listdefault" });

                                $("#btnDefault").removeAttr("disabled");
                            }
                            else {
                                swal({ title: 'Add Default', text: 'Something went wrong: </br>' + result.toString(), type: 'error' }).then(function () { clear(); });
                                $("#btnDefault").removeAttr("disabled");
                            }
                        },
                        error: function (e) {
                            swal({ title: 'Add Default', text: 'Adding default encountered an error', type: 'error' }).then(function () { clear(); });
                            $("#btnDefault").removeAttr("disabled");
                        }
                    });
                }
            });
        }

    },
        function (dismiss) {
            swal('Add Default', 'You cancelled add default.', 'error');
            $("#btnDefault").removeAttr("disabled");
        });

}
function reloadpage() {
    location.reload();
}

function clear() {
  
    $('#DfId').val('');
    $('#AccountId').val('');
    $('#ddlAccountName').val('');
}

function updateDefault() {
    debugger
   
    $("input[type=submit]").attr("disabled", "disabled");
    $('#frmdefault').validate({
        
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
                text: "Default will be updated!",
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
                    $("#btnDefaultUpdate").attr("disabled", "disabled");
                    debugger
                    var reg={
                        DfId: $('#DfId').val(),
                        DfDescription: $('#ddlAccountName').val(), AccountId: $('#ddlAcctNumber').val(),
                        AccountName: $('#ddlAcctName').val(),
                        DfDescription: $('#ddlAccountType').val(),                     
                        FinancePnc: $('#ddlFinance').val(),                     
                        TellerPnc: $('#ddlTeller').val()
                    }
                   
                    $.ajax({
                        url:"../chart/UpdateDefault",
                        type: 'POST',
                        data: reg,
                        dataType: "json",
                        //headers: {
                        //    'VerificationToken': forgeryId
                        //},
                        success: function (result) {
                           
                            if (result.toString != '' && result != null) {
                                swal({ title: 'Update Default', text: 'Default updated successfully!', type: 'success' }).then(function () { window.location.reload(true); });
                                $('#AddNewDefault').modal('hide');
                                $('#defaultTable').
                                    bootstrapTable(
                                    'refresh', { url:"../chart/listdefault" });

                                $("#btnDefaultUpdate").removeAttr("disabled");
                            }
                            else {
                                swal({ title: 'Update Default', text: 'Something went wrong: </br>' + result.toString(), type: 'error' }).then(function () { clear(); });
                                $("#btnDefaultUpdate").removeAttr("disabled");
                            }
                        },
                        error: function (e) {
                            swal({ title: 'Update Default', text: 'Update Default encountered an error', type: 'error' }).then(function () { clear(); });
                            $("#btnDefaultUpdate").removeAttr("disabled");
                        }
                    });
                }
            });
        }

    },
        function (dismiss) {
            swal('Update Default', 'You cancelled Default updation.', 'error');
            $("#btnDefaultUpdate").removeAttr("disabled");
        });

}
//$(document).ready(function ($) {
//    debugger
//    //for dropdown list control for Comapny (id=ddlCompany)starts here

//    $("#ddlCompany").select2({
//        theme: "bootstrap4",
//        placeholder: "Loading...",

//    });

//    $.ajax({
//        url: "Chart/loadCompany",
//    }).then(function (response) {
//        debugger
//        $("#ddlCompany").select2({
//            theme: "bootstrap4",
//            // placeholder: "Select Company...",  
//            width: '100%',
//            data: response.results
//        });
//        });

//});
//$(document).ready(function ($) {
//    debugger
//    //for dropdown list control for Comapny (id=ddlCompany)starts here

//    $("#ddlBranch").select2({
//        theme: "bootstrap4",
//        placeholder: "Loading...",

//    });

//    $.ajax({
//        url: "Chart/loadBranch",
//    }).then(function (response) {
//        debugger
//        $("#ddlBranch").select2({
//            theme: "bootstrap4",
//            // placeholder: "Select Company...",  
//            width: '100%',
//            data: response.results
//        });
//    });
//});
//$(document).ready(function ($) {
//    debugger
//    //for dropdown list control for Comapny (id=ddlCompany)starts here

//    $("#ddlAccountName").select2({
//        theme: "bootstrap4",
//        placeholder: "Loading...",

//    });

//    $.ajax({
//        url: "Chart/loadChart",
//    }).then(function (response) {
//        debugger
//        $("#ddlAccountName").select2({
//            theme: "bootstrap4",
//            // placeholder: "Select Company...",  
//            width: '100%',
//            data: response.results
//        });
//    });
//});
//$(document).ready(function ($) {
//    debugger
//    //for dropdown list control for Comapny (id=ddlCompany)starts here

//    $("#ddlGroup").select2({
//        theme: "bootstrap4",
//        placeholder: "Loading...",

//    });

//    $.ajax({
//        url: "Chart/loadAccountGroup",
//    }).then(function (response) {
//        debugger
//        $("#ddlGroup").select2({
//            theme: "bootstrap4",
//            // placeholder: "Select Company...",  
//            width: '100%',
//            data: response.results
//        });
//    });
//});
//$(document).ready(function ($) {
//    debugger
//    //for dropdown list control for Comapny (id=ddlCompany)starts here

//    $("#ddlCurrency").select2({
//        theme: "bootstrap4",
//        placeholder: "Loading...",

//    });

//    $.ajax({
//        url: "Chart/loadCurrency",
//    }).then(function (response) {
//        debugger
//        $("#ddlCurrency").select2({
//            theme: "bootstrap4",
//            // placeholder: "Select Company...",  
//            width: '100%',
//            data: response.results
//        });
//    });
//});
//$(document).ready(function ($) {
//    debugger
//    //for dropdown list control for Comapny (id=ddlCompany)starts here

//    $("#ddlAccountCategory").select2({
//        theme: "bootstrap4",
//        placeholder: "Loading...",

//    });

//    $.ajax({
//        url: "Chart/loadCategory",
//    }).then(function (response) {
//        debugger
//        $("#ddlAccountCategory").select2({
//            theme: "bootstrap4",
//            // placeholder: "Select Company...",  
//            width: '100%',
//            data: response.results
//        });
//    });
//});
//$(document).ready(function ($) {
//    debugger
//    //for dropdown list control for Comapny (id=ddlCompany)starts here

//    $("#ddlCost").select2({
//        theme: "bootstrap4",
//        placeholder: "Loading...",

//    });

//    $.ajax({
//        url: "Chart/loadCost",
//    }).then(function (response) {
//        debugger
//        $("#ddlCost").select2({
//            theme: "bootstrap4",
//            // placeholder: "Select Company...",  
//            width: '100%',
//            data: response.results
//        });
//    });
//});
//$(document).ready(function ($) {
//    debugger
//    //for dropdown list control for Comapny (id=ddlCompany)starts here

//    $("#ddlStatus").select2({
//        theme: "bootstrap4",
//        placeholder: "Loading...",

//    });

//    $.ajax({
//        url: "Chart/loadStatus",
//    }).then(function (response) {
//        debugger
//        $("#ddlStatus").select2({
//            theme: "bootstrap4",
//            // placeholder: "Select Company...",  
//            width: '100%',
//            data: response.results
//        });
//    });
//});



$(document).ready(function ($) {
    debugger
  

    $("#ddlAccountName").select2({
        theme: "bootstrap4",
        placeholder: "Loading...",

    });
    $.ajax({
        url: "Chart/loadVwDefault",
    }).then(function (response) {
        debugger
        $("#ddlAccountName").select2({
            theme: "bootstrap4",
            // placeholder: "Select Company...",  
            width: '100%',
            data: response.results
        });
    });

    $("#ddlAccountName").on("select2:select", function (e) {
        debugger
        $("#ddlAcctNumber").val(e.params.data.acctid);
        $("#ddlAcctName").val(e.params.data.text);
        $("#ddlAccountType").val(e.params.data.typ);
        $("#ddlAccountCate").val(e.params.data.cate);
       
    });
});