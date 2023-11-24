var url_path = window.location.pathname;
if (url_path.charAt(url_path.length - 1) == '/') {
    url_path = url_path.slice(0, url_path.length - 1);
}

var accountId;
function bulkFormatter(value, row, index) {
    return [
        //'<a style="color:white"  class="edits btn btn-sm btn-warning mr-2"  title="Edit">'
        //+ '<i class="now-ui-icons ui-2_settings-90"></i>' +
        '<a style="color:white"  title="Remove chart" class="remove btn btn-sm btn-danger">'
        + '<i class="fas fa-trash"></i></a>' +
        '</a> '
    ].join('');
}
window.bulkEvents = {
   
    'click .remove': function (e, value, row, index) {
        info = JSON.stringify(row);
        console.log(info);

        debugger
        accountId= $('#ID').val(row.Id);
        $.ajax({
            url: '/fundtransfer/RemoveBulk',
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
                        $('#batchTable').
                            bootstrapTable(
                                'refresh', { url: '/fundtransfer/listbatchupload' });

                        //return false;
                    }
                    else {
                        swal("Batch Upload", "You cancelled delete upload.", "error");
                    }
                   
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
var $Transfertable = $('#transTable');
var $batchTables = $('#batchTable');
$(document).ready(function ($) {
    $("#Transactiondate").datetimepicker({
        format: "DD MMMM, YYYYY",
        icons: {
            time: "now-ui-icons tech_watch-time",
            date: "now-ui-icons ui-1_calendar-60",
            up: "fa fa-chevron-up",
            down: "fa fa-chevron-down",
            previous: "now-ui-icons arrows-1_minimal-left",
            next: "now-ui-icons arrows-1_minimal-right",
            today: "fa fa-screenshot",
            clear: "fa fa-trash",
            close: "fa fa-remove"
        }
    });
});
$(document).ready(function ($) {
    $('#btnUpload').on('click', function () {
        uploadTran();
    });
    debugger
    $('#btnUploadMultiple').on('click', function () {  
        debugger
        $.ajax({
            url: url_path+"/importBulkUpload",
            cache: false
        }).then(function (response) {

           alert("OK")

        });         

    });
});
$(document).ready(function ($) {

    $('#btnSave').on('click', function () {
        debugger
        addTransactions();
    });
    $('#btnSend').on('click', function () {
        debugger
        batchTransactions();
    });
    $('#btnDownload').on('click', function () {
        debugger
        $("input[type=submit]").attr("disabled", "disabled");
        
            $.ajax({
                type: 'POST',
                url: url_path + '/download',
                //cache: false,
                //dataType:'json',
                //contentType: false,
                //processData: false,
                //method: 'POST',
                success: function (result) {

                    //if (result.toString !== '' && result !== null) {
                    //    swal({ title: 'Download Template', text: 'Template downloaded successfully!', type: 'success' }).then(function () {  });

                    //    $('#transTable').
                    //        bootstrapTable(
                    //            'refresh', { url: 'listupload' });


                    //}
                    //else {
                    //    swal({ title: 'Download Template', text: 'Something went wrong: </br>' + result.toString(), type: 'error' }).then(function () { clear(); });

                    //}
                }
            });
        
    });
    
});
function uploadTran() {
    $.ajax({
        type: 'POST',
        url: url_path + '/ImportTransaction',
        success: function (result) {

            if (result.toString !== '' && result !== null) {
                swal({ title: 'Upload Transaction', text: 'Transaction uploaded successfully!', type: 'success' }).then(function () { window.location.reload(true); });

                $('#transTable').
                    bootstrapTable(
                        'refresh', { url: 'listupload' });


            }
            else {
                swal({ title: 'Upload Transaction', text: 'Something went wrong: </br>' + result.toString(), type: 'error' }).then(function () { clear(); });

            }
        }
    });

}


//For save transaction
function addTransactions() {

    SingleData = $Transfertable.bootstrapTable('getAllSelections');
    var stamp = $('#stampduty').val();   
    $("input[type=submit]").attr("disabled", "disabled");
    if (SingleData["length"] !== 0) {
        swal({
            title: "Are you sure?",
            text: "Transaction(s) will be sent!",
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
                debugger            
                if (SingleData["length"] !== 0) {
                                  
                            $.ajax({
                                url: url_path + '/uploadTransfer',
                                type: 'POST',
                                data: JSON.stringify(SingleData),
                                contentType: 'application/json',
                                success: function (result) {
                                    debugger
                                    if (result.message !== " ") {
                                        swal({ title: 'General Upload ', text: 'Something went wrong: ' + result.message.toString(), type: 'error' }).then(function () { window.location.reload(true); });

                                        $('#transTable').bootstrapTable('refresh', {
                                            silent: true
                                        });

                                    }
                                    else {
                                        swal({ title: 'General Upload ', text: 'General upload added successfully!', type: 'success' }).then(function () { });

                                        $('#transTable').bootstrapTable('refresh', {
                                            silent: true
                                        });

                                    }
                                },
                                error: function (e) {
                                    swal({ title: 'General Upload', text: 'General Upload encountered an error', type: 'error' }).then(function () { clear(); });

                                }
                            });                        
                   
                }
              
            }

        }),

            function (dismiss) {
                swal({
                    title: 'Transfer Reversal',
                    text: 'Reversal encountered an error',
                    type: 'error',
                    allowOutsideClick: false,
                    allowEscapeKey: false
                }).then(function () {
                    $("#wizardComponent .btn-finish").attr("enabled", "true");
                });

            }
    } else {
        swal("You have not selected any transaction(s)");
    }
}

//Bulk Upload

$(document).ready(function ($) {
    $("#ddlProcessType").select2({
        theme: "bootstrap4",
        placeholder: "Loading..."
    });
    $.ajax({
        url:  "/fundtransfer/PostTypeList",
        cache: false
    }).then(function (response) {

        $("#ddlProcessType").select2({
            theme: "bootstrap4",
            placeholder: "Select Process Type ",
            width: '100%',
            data: response.results
        });
    });
    $("#ddlLedgerAccount").select2({
        theme: "bootstrap4",
        placeholder: "Loading..."
    });
    $.ajax({
        url: "/fundtransfer/loadLedgerAccount",      
        cache: false
    }).then(function (response) {

        $("#ddlLedgerAccount").select2({
            theme: "bootstrap4",
            placeholder: "Select Account",
            width: '100%',
            data: response.results
        });

    });

});

$("#ddlLedgerAccount").on("select2:select", function (e) {
    debugger

    var datas = e.params.data;
   
    $("#Accountname").val(datas.id);
    
    //$('#ddlAcct').val(null).trigger('change.select2');
 
    //$("#operationid").select2({
    //    theme: "bootstrap4",
    //    placeholder: "Loading..."
    //});
   

});



function batchTransactions() {
    debugger
    batchData = $batchTables.bootstrapTable('getAllSelections');
    var dataResult =
    {
        Transactiondate: $('#Transactiondate').val(),
        BatchName: $('#BatchName').val(),
        type: $('#ddlProcessType').val(),
        acctNodr: $('#Accountname').val(),
        amountdr: $('#Amount').val(),
        Narration: $('#Narration').val(),
        TotalCredit: $('#TotalCredit').val()

    };
    //JSON.push(dataResult);
     
    //batchData = JSON.stringify({ 'batchData': batchData, 'dataResult': dataResult });
    var stamp = $('#Transactiondate').val();
    $("input[type=submit]").attr("disabled", "disabled");
   

    if (batchData["length"] === 0) {
        return $.notify(
            {
                icon: "now-ui-icons travel_info",
                message: "No table rows have been selected!"
            },
            {
                type: "danger",
                placement: {
                    from: "top",
                    align: "right"
                }
            }
        );
    }
    if ($('#Amount').val() === '' || $('#Amount').val() === 0) {
        return $.notify(
            {
                icon: "now-ui-icons travel_info",
                message: "Debit amount is missing"
            },
            {
                type: "danger",
                placement: {
                    from: "top",
                    align: "right"
                }
            }
        );
    }
    if ($('#Accountname').val() === '' || $('#Amount').val() === 0) {
        return $.notify(
            {
                icon: "now-ui-icons travel_info",
                message: "Account name is missing"
            },
            {
                type: "danger",
                placement: {
                    from: "top",
                    align: "right"
                }
            }
        );
    }
        swal({
            title: "Are you sure?",
            text: "Transaction(s) will be sent!",
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
                debugger
                if (batchData["length"] !== 0) {
                    $.ajax({
                        //contentType: 'application/json',
                        dataType: 'json',
                        url:'/fundtransfer/batchuploadTransfer',
                        type: 'POST',
                        data: { 'batchData': batchData, 'BatchTransfer': dataResult },                        
                        success: function (result) {
                            debugger
                            if (result.message !== " ") {
                                swal({ title: 'General Upload ', text: 'Something went wrong: ' + result.message.toString(), type: 'error' }).then(function () { window.location.reload(true); });

                                $('#batchTable').bootstrapTable('refresh', {
                                    silent: true
                                });

                            }
                            else {
                                swal({ title: 'General Upload ', text: 'General upload added successfully!', type: 'success' }).then(function () { });

                                $('#batchTable').bootstrapTable('refresh', {
                                    silent: true
                                });

                            }
                        },
                        error: function (e) {
                            swal({ title: 'General Upload', text: 'General Upload encountered an error', type: 'error' }).then(function () { clear(); });

                        }
                    });

                }

            }

        }),

            function (dismiss) {
                swal({
                    title: 'Transfer Reversal',
                    text: 'Reversal encountered an error',
                    type: 'error',
                    allowOutsideClick: false,
                    allowEscapeKey: false
                }).then(function () {
                    $("#wizardComponent .btn-finish").attr("enabled", "true");
                });

            }
    
}
$('#batchTable').on('change', function () {

    TotalAmount();
});
//Functions
function TotalAmount() {

    var tabl = $("#batchTable").bootstrapTable("getSelections");
    var sumVal = 0;
    for (var i = 0; i < tabl.length; i++) {
        sumVal = sumVal + parseFloat(tabl[i].amount);
    }
    alert(sumVal);
    $('#TotalCredit').val(sumVal);
    var tAmount = $('#Amount').val();
    //$('#TotalDebit').val(tAmount);
    $('#TotalCredit').attr('disabled', 'disabled');
    //$('#TotalDebit').attr('disabled', 'disabled');
    return sumVal;

}
$('#Amount').on("mouseout", function () {
    debugger
   
    var table = $("#batchTable").bootstrapTable("getSelections");
    var value = $.trim($('#Amount').val()).replace(/,/g, "");
    var svalue = $.trim($('#TotalCredit').val()).replace(/,/g, ""); 
    if (table['length'] !== 0) {
        if ($.isNumeric(value)) {
            if (Number(value) !== Number(svalue)) {
                this.value = '';
                $.notify(
                    {
                        icon: "now-ui-icons travel_info",
                        message: "<b>The sum of credit not equal to sum debit"
                    },
                    {
                        type: "danger",
                        placement: {
                            from: "top",
                            align: "right"
                        }
                    }
                );

            }
            // $("#TotalDebit").val('');
        }
        else {
            $('#Amount').val();

        }
    }
});
//

$('#BtnUploadTemp').on('click', function () {

    debugger
    tblStaffInformation();
})
function tblStaffInformation() {
    $("input[type=submit]").attr("disabled", "disabled");
    var Staffsignature = $("#doctemplate").get(0).files;
    debugger;
    if (!Staffsignature) {
        return;
    }
    var signatureimgData = new FormData();

    //for (var signatureCounter = 0; signatureCounter < Staffsignature.length; signatureCounter++) {
    //    signatureimgData.append("Staffsignature", staffsignature[signatureCounter])
    //};

    signatureimgData.append("Staffsignature", $("#doctemplate").get(0).files[0]);
    debugger;
    $.ajax(url_path + "/Import", {
        method: "POST",
        contentType: false,
        processData: false,
        data:  signatureimgData ,
        success: function (result) {
           
            if (result.toString !== '' && result !== null) {
                swal({ title: 'Upload Transaction', text: 'Transaction uploaded successfully!', type: 'success' }).then(function () { window.location.reload(true); });

                $('#batchTable').
                    bootstrapTable(
                        'refresh', { url: url_path + '/listbatchupload' });


            }
            else {
                swal({ title: 'Upload Transaction', text: 'Something went wrong: </br>' + result.toString(), type: 'error' }).then(function () { clear(); });

            }
        },
        error: function (e) {
            swal({ title: 'Signature ', text: 'Signature  encountered an error', type: 'error' }).then(function () {  });

        }
    });
}
