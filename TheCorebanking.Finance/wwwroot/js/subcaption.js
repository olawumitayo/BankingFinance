var url_path = window.location.pathname;
if (url_path.charAt(url_path.length - 1) == '/') {
    url_path = url_path.slice(0, url_path.length - 1);
}

$(document).ready(function ($) {
    $("#AccountSubName").mouseout(function (event) {
        debugger
        var subname = $('#AccountSubName').val();
        if (subname == '') {
            $.notify({ icon: "add_alert", message: 'Your subcaption name is empty' }, { type: 'danger', timer: 1000 });
            $(this).css('border-color', 'red');
            $('#AccountSubName').focus();
            return false;
        } else {
            $(this).css('border-color', 'green');
        }

    });
});
$(document).ready(function ($) {
    $("#ddlStatuss").mouseout(function (event) {
        debugger
        var status = $('#ddlStatuss').val().trigger('change');
        if (status == '') {
            $.notify({ icon: "add_alert", message: 'Your account status is empty' }, { type: 'danger', timer: 1000 });
            $(this).css('border-color', 'red');
            $('#ddlStatuss').focus();
            return false;
        } else {
            $(this).css('border-color', 'green');
        }

    });
});
$(document).ready(function ($) {
    $("#ddlCurrencys").mouseout(function (event) {
        debugger
        var currency = $('#ddlCurrencys').val();
        if (currency == '') {
            $.notify({ icon: "add_alert", message: 'Your account currency is empty' }, { type: 'danger', timer: 1000 });
            $(this).css('border-color', 'red');
            $('#ddlCurrencys').focus();
            return false;
        } else {
            $(this).css('border-color', 'green');
        }

    });
});
function subFormatter(value, row, index) {
    return [
        '<div class="btn-group">' + '<a style="color:white"  class="edit btn btn-sm btn-info"  title="Edit subCaption">'
        + '<i class="fas fa-edit"></i>' +
        '<a style="color:white"  title="Remove SubCaption" class="remove btn btn-sm btn-danger">'
        +'<i class="fas fa-trash"></i></a>'+
        '</a> '
    ].join('');
}

window.subEvents = {
    'click .edit': function (e, value, row, index) {
       
        if (row.state = true) {
          
            var data = JSON.stringify(row);
            $('#IdUpdate').val(row.id);
            $('#AccountSubNameUpdate').val(row.accountSubName);
            $('#ddlStatussUpdate').val(row.accountStatus).trigger('change');
            $('#ddlCurrencysUpdate').val(row.currency).trigger('change');           
            $('#UpdateSub').modal('show'); 
            $('#btnSubUpdate').html('  <i class="now-ui-icons ui-1_check"></i> Update Record');
          
        }
    },
    'click .remove': function (e, value, row, index) {
        info = JSON.stringify(row);
        console.log(info);       
        debugger
        $('#ID').val(row.id);
        $.ajax({
            url: '../caption/RemoveSub',
            type: 'POST',
            data: { ID: row.id},
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
                        $('#subTable').
                            bootstrapTable(
                            'refresh', { url: '../caption/listSubCaption' });

                        //return false;
                    }
                    else {
                        swal("Sub Caption", "You cancelled add Caption.", "error");
                    }
                     $('#subTable').
                            bootstrapTable(
                         'refresh', { url: '../caption/listSubCaption' });
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

function updateSub() {
    debugger  
    var subInformation = {
        Id: $('#IdUpdate').val(),
        AccountSubName: $('#AccountSubNameUpdate').val(),
        AccountStatus: $('#ddlStatussUpdate').val(),
        Currency: $('#ddlCurrencysUpdate').val(),

    }
    //var Id = $('#Id').val();
    //var Description = $('#AccountSubName').val();

    $("input[type=submit]").attr("disabled", "disabled"); 
    $('#frmUpdatesub').validate({
     
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
                text: "Sub Caption will be updated!",
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
                    $("#btnSubUpdate").attr("disabled", "disabled");
                    debugger
                    $.ajax({
                        url: '../caption/UpdateSub',
                        type: 'POST',
                        data: subInformation,
                        dataType: 'json',
                        cache: false,                                            
                        success: function (result) {
                            
                            if (result.toString != '' && result != null) {
                                swal({ title: 'Update Caption', text: 'Caption updated successfully!', type: 'success' }).then(function () { window.location.reload(true); });
                                $('#UpdateSub').modal('hide'); 
                                $('#subTable').
                                    bootstrapTable(
                                    'refresh', { url: '../caption/listSubCaption' });

                                $("#btnSubUpdate").removeAttr("disabled");
                            }
                            else {
                                swal({ title: 'Update Caption', text: 'Something went wrong: </br>' + result.toString(), type: 'error' }).then(function () { clear(); });
                                $("#btnSubUpdate").removeAttr("disabled");
                            }
                        },
                        error: function (e) {
                            swal({ title: 'Update Caption', text: 'Caption update encountered an error', type: 'error' }).then(function () { clear(); });
                            $("#btnSubUpdate").removeAttr("disabled");
                        }
                    });
                }
            });
        }

    },
        function (dismiss) {
            swal('Update Caption', 'You cancelled caption update.', 'error');
            $("#btnSubUpdate").removeAttr("disabled");
        });

}



$(document).ready(function ($) {
  
    $('#btnSub').on('click', function () {
        debugger      
       
            addSub();   
 
    });

});
$(document).ready(function ($) {
    $('#btnUploadSub').on('click', function () {
        UploadSub();
    });
});
function UploadSub() {
    $.ajax({
        url: '../caption/SubUpload',
        type: 'POST',
        success: function (result) {
            if (result.toString != '' && result != null) {
                
                $('#subTable').
                    bootstrapTable(
                    'refresh', { url: '../caption/listSubCaption' });
                swal({ title: 'Upload Sub', text: 'Sub caption uploaded successfully!', type: 'success' }).then(function () { clear(); });


            }
            else {
                swal({ title: 'Upload sub caption', text: 'Something went wrong: </br>' + result.toString(), type: 'error' }).then(function () { clear(); });

            }

        },

    });

}

function addSub() {
    debugger
   
    $("input[type=submit]").attr("disabled", "disabled");
    $('#frmsub').validate({
        messages: {
            AccountSubName: { required: "Description is required" },
            AccountStatus: { required: "Account status is required" },
            Currency: { required: "Currency is required" }       
            
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
                text: "Caption will be added!",
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
                    $("#btnSub").attr("disabled", "disabled");

                    debugger
                    var subcaption_data = {
                        id: $('#Id').val(),
                        accountSubName: $('#AccountSubName').val(),
                        accountStatus: $('#ddlStatuss').val(),
                        Currency: $('#ddlCurrencys').val()

                    }
                        
                    $.ajax({
                        url: '../caption/AddSub',
                        type: 'POST',
                        data: subcaption_data,
                        dataType: "json",
                        success: function (result) {
                            
                            if (result.toString != '' && result != null) {
                                swal({ title: 'Add Caption', text: 'Caption added successfully!', type: 'success' }).then(function () { window.location.reload(true); });
                                $('#AddNewSub').modal('hide'); 
                                $('#subTable').
                                    bootstrapTable(
                                    'refresh', { url: '../caption/listSubCaption' });

                                $("#btnSub").removeAttr("disabled");
                            }
                            else {
                                swal({ title: 'Add Caption', text: 'Something went wrong: </br>' + result.toString(), type: 'error' }).then(function () { clear(); });
                                $("#btnSub").removeAttr("disabled");
                            }
                        },
                        error: function (e) {
                            swal({ title: 'Add Caption', text: 'Adding caption encountered an error', type: 'error' }).then(function () { clear(); });
                            $("#btnSub").removeAttr("disabled");
                        }
                    });
                }
            });
        }

    },
        function (dismiss) {
            swal('Add Caption', 'You cancelled add caption.', 'error');
            $("#btnSub").removeAttr("disabled");
        });

}

$(document).ready(function ($) {
   
    $('#btnSubUpdate').on('click', function () {
        debugger
        updateSub();
    });

});
function reloadpage() {
    location.reload();
}

function clear() {
    $('#AccountSubName').val('');

}
$(document).ready(function ($) {
    debugger
    //for dropdown list control for Comapny (id=ddlCompany)starts here

    $("#ddlCurrencys").select2({
        theme: "bootstrap4",
        placeholder: "Loading...",

    });

    $.ajax({
        url: "Caption/loadCurrency",
    }).then(function (response) {
        debugger
        $("#ddlCurrencys").select2({
            theme: "bootstrap4",
            // placeholder: "Select Company...",  
            width: '100%',
            data: response.results
        });
    });

    $("#ddlCurrencysUpdate").select2({
        theme: "bootstrap4",
        placeholder: "Loading...",

    });

    $.ajax({
        url: "Caption/loadCurrency",
    }).then(function (response) {
        debugger
        $("#ddlCurrencysUpdate").select2({
            theme: "bootstrap4",
            // placeholder: "Select Company...",  
            width: '100%',
            data: response.results
        });
    });
});
$(document).ready(function ($) {
    debugger
    //for dropdown list control for Comapny (id=ddlCompany)starts here

    $("#ddlStatuss").select2({
        theme: "bootstrap4",
        placeholder: "Loading...",

    });

    $.ajax({
        url: "Caption/loadStatus",
    }).then(function (response) {
        debugger
        $("#ddlStatuss").select2({
            theme: "bootstrap4",
            // placeholder: "Select Company...",  
            width: '100%',
            data: response.results
        });
    });

    $("#ddlStatussUpdate").select2({
        theme: "bootstrap4",
        placeholder: "Loading...",

    });

    $.ajax({
        url: "Caption/loadStatus",
    }).then(function (response) {
        debugger
        $("#ddlStatussUpdate").select2({
            theme: "bootstrap4",
            // placeholder: "Select Company...",  
            width: '100%',
            data: response.results
        });
    });
});
//$('#currencyTable').on('expand-row.bs.table', function (e, index, row, $detail) {
//    $detail.html('Loading request...');

//    var htmlData = '';
//    var header = '<div>';
//    var footer = '</div>';
//    htmlData = htmlData + header;

//    debugger

//    var html =
//        '<h8>' +
//        '<p style="text-align:left">' +
   
//        '<strong> Currency Code:</strong> &nbsp' + row.currCode + '' + '<p>' +
//        '<strong> Currency Name:</strong> &nbsp' + row.currName + '' + '<p>' +
//        '<strong> Currency Symbol:</strong> &nbsp' + row.currSymbol + '' + '<p>' +
//        '<strong> Exchange Rate:</strong> &nbsp' + row.exchangeRate + '' + '<p>' +
//        '<strong> Country Code:</strong> &nbsp' + row.countryCode + '';
      
//    htmlData = htmlData + html;
//    htmlData = htmlData + footer;
//    $detail.html(htmlData);
//});



