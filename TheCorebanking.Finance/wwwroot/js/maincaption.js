var url_path = window.location.pathname;
if (url_path.charAt(url_path.length - 1) == '/') {
    url_path = url_path.slice(0, url_path.length - 1);
}
function mainFormatter(value, row, index) {
    return [
        '<div class="btn-group">' + '<a style="color:white"  class="edit btn btn-sm btn-info"  title="Edit MainCaption">'
        + '<i class="fas fa-edit"></i>' +
        '<a style="color:white"  title="Remove MainCaption" class="remove btn btn-sm btn-danger">'
        +'<i class="fas fa-trash"></i></a>'+
        '</a> '
    ].join('');
}

window.mainEvents = {
    'click .edit': function (e, value, row, index) {
       
        if (row.state = true) {
            var data = JSON.stringify(row);
            $('#IdUpdate').val(row.id);
            $('#DescriptionUpdate').val(row.description);
            $('#ddlSubUpdate').val(row.subCaptionId).trigger('change');
            $('#ddlAccountCategoryUpdate').val(row.accountCategoryId).trigger('change');
            $('#ddlActiveUpdate :selected').text(row.active);           
            $('#UpdateMain').modal('show'); 
            $('#btnMainUpdate').html('  <i class="now-ui-icons ui-1_check"></i> Update Record');
        }
    },
    'click .remove': function (e, value, row, index) {
        info = JSON.stringify(row);
        console.log(info);       
        debugger
        $('#ID').val(row.id);
        $.ajax({
            url: '../caption/RemoveMain',
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
                        $('#mainTable').
                            bootstrapTable(
                            'refresh', { url: '../caption/listmain' });

                        //return false;
                    }
                    else {
                        swal("Main Caption", "You cancelled add Caption.", "error");
                    }
                     $('#mainTable').
                            bootstrapTable(
                         'refresh', { url: '../caption/listmain' });
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
    $('#btnUploadMain').on('click', function () {
        UploadMain();
    });
});
function UploadMain() {
    $.ajax({
        url: '../caption/MainUpload',
        type: 'POST',
        success: function (result) {
            if (result.toString != '' && result != null) {               

                $('#mainTable').
                    bootstrapTable(
                    'refresh', { url: '../caption/listmain' });

                swal({ title: 'Upload Main', text: 'Main caption uploaded successfully!', type: 'success' }).then(function () { clear(); });
            }
            else {
                swal({ title: 'Upload main caption', text: 'Something went wrong: </br>' + result.toString(), type: 'error' }).then(function () { clear(); });

            }

        },

    });

}


function updateCaption() {
    debugger  
    var json_data = {};
    json_data.Id = $('#IdUpdate').val();
    json_data.Description = $('#DescriptionUpdate').val();
    json_data.subCaptionId = $('#ddlSubUpdate').val();
    json_data.accountCategoryId = $('#ddlAccountCategoryUpdate').val();
    json_data.Active = $('#ddlActiveUpdate').val();  

    $("input[type=submit]").attr("disabled", "disabled"); 
    $('#frmUpdateMain').validate({
     
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
                text: "Main caption will be updated!",
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
                    $("#btnMainUpdate").attr("disabled", "disabled");
                    debugger
                    $.ajax({
                        url: '../caption/UpdateMain',
                        type: 'POST',
                        data: json_data,
                        dataType: "json",
                        //headers: {
                        //    'VerificationToken': forgeryId
                        //},
                        success: function (result) {
                            
                            if (result.toString != '' && result != null) {
                                swal({ title: 'Update Caption', text: 'Caption updated successfully!', type: 'success' }).then(function () { window.location.reload(true); });
                                $('#UpdateMain').modal('hide'); 
                                $('#mainTable').
                                    bootstrapTable(
                                    'refresh', { url: '../caption/listmain' });

                                $("#btnMainUpdate").removeAttr("disabled");
                            }
                            else {
                                swal({ title: 'Update Caption', text: 'Something went wrong: </br>' + result.toString(), type: 'error' }).then(function () { clear(); });
                                $("#btnMainUpdate").removeAttr("disabled");
                            }
                        },
                        error: function (e) {
                            swal({ title: 'Update Caption', text: 'Caption update encountered an error', type: 'error' }).then(function () { clear(); });
                            $("#btnMainUpdate").removeAttr("disabled");
                        }
                    });
                }
            });
        }

    },
        function (dismiss) {
            swal('Update Caption', 'You cancelled caption update.', 'error');
            $("#btnMainUpdate").removeAttr("disabled");
        });

}



$(document).ready(function ($) {
 
    $('#btnMain').on('click', function () {
        debugger    
       
            addCaption();   
 
    });

});
function addCaption() {
    debugger

    $("input[type=submit]").attr("disabled", "disabled");
    $('#frmMain').validate({
        messages: {
            Description: { required: "Description is required" },
            SubCaptionId: { required: "Sub caption is required" },
            AccountCategoryId: { required: "Category is required" },
            Active: { required: "Active is required" }
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
                    $("#btnMain").attr("disabled", "disabled");

                    debugger
                    var json_data = {};
                    json_data.Id = $('#Id').val();
                    json_data.Description = $('#Description').val();
                    json_data.subCaptionId = $('#ddlSub').val();
                    json_data.accountCategoryId = $('#ddlAccountCategory').val();
                    json_data.Active = $('#ddlActive').val(); 
                    if ($('#ddlActive').val() == 'Active') {

                        json_data.Active  = 1;
                    }
                    else
                    {
                        json_data.Active  = 0;
                    }
                    $.ajax({
                        url: '../caption/AddMain',
                        type: 'POST',
                        data: json_data,
                        dataType: "json",
                        //headers: {
                        //    'VerificationToken': forgeryId
                        //},
                        success: function (result) {
                            
                            if (result.toString != '' && result != null) {
                                swal({ title: 'Add Caption', text: 'Caption added successfully!', type: 'success' }).then(function () { window.location.reload(true); });
                                $('#AddNewMain').modal('hide'); 
                                $('#mainTable').
                                    bootstrapTable(
                                    'refresh', { url: '../caption/listmain' });

                                $("#btnMain").removeAttr("disabled");
                            }
                            else {
                                swal({ title: 'Add Caption', text: 'Something went wrong: </br>' + result.toString(), type: 'error' }).then(function () { clear(); });
                                $("#btnMain").removeAttr("disabled");
                            }
                        },
                        error: function (e) {
                            swal({ title: 'Add Caption', text: 'Adding caption encountered an error', type: 'error' }).then(function () { clear(); });
                            $("#btnCaption").removeAttr("disabled");
                        }
                    });
                }
            });
        }

    },
        function (dismiss) {
            swal('Add Caption', 'You cancelled add caption.', 'error');
            $("#btnMain").removeAttr("disabled");
        });

}

$(document).ready(function ($) {
    
    $('#btnMainUpdate').on('click', function () {
        debugger
        updateCaption();
    });

});
function reloadpage() {
    location.reload();
}

function clear() {
    $('#CurrCode').val('');
    $('#CurrName').val('');
    $('#CurrSymbol').val('');
    $('#ExchangeRate').val('');
    $('#CountryCode').val('');
}
$(document).ready(function ($) {
    debugger
    //for dropdown list control for Comapny (id=ddlCompany)starts here

    $("#ddlSub").select2({
        theme: "bootstrap4",
        placeholder: "Loading...",

    });

    $.ajax({
        url: "Caption/loadSUb",
    }).then(function (response) {
        debugger
        $("#ddlSub").select2({
            theme: "bootstrap4",
            // placeholder: "Select Company...",  
            width: '100%',
            data: response.results
        });
    });

    $("#ddlSubUpdate").select2({
        theme: "bootstrap4",
        placeholder: "Loading...",

    });

    $.ajax({
        url: "Caption/loadSUb",
    }).then(function (response) {
        debugger
        $("#ddlSubUpdate").select2({
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

    $("#ddlAccountCategory").select2({
        theme: "bootstrap4",
        placeholder: "Loading...",

    });

    $.ajax({
        url: "Caption/loadCategory",
    }).then(function (response) {
        debugger
        $("#ddlAccountCategory").select2({
            theme: "bootstrap4",
            // placeholder: "Select Company...",  
            width: '100%',
            data: response.results
        });
    });

    $("#ddlAccountCategoryUpdate").select2({
        theme: "bootstrap4",
        placeholder: "Loading...",

    });

    $.ajax({
        url: "Caption/loadCategory",
    }).then(function (response) {
        debugger
        $("#ddlAccountCategoryUpdate").select2({
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



