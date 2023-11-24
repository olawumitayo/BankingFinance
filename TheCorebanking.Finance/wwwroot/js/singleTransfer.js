var url_path = window.location.pathname;
if (url_path.charAt(url_path.length - 1) == '/') {
    url_path = url_path.slice(0, url_path.length - 1);
}
$("#Amount").blur(function () {
    //var amtValue = $.trim(("#Amount").val);Transactiondate
    var amtValue = $.trim($("#Amount").val());
    var lastvalueofAmtString = amtValue.substr(amtValue.length - 1);
    var realAmount;

    switch (lastvalueofAmtString.toLowerCase()) {
        case 't':
            realAmount = $.trim(amtValue.replace(lastvalueofAmtString, '')) * 1000;
            break;
        case 'm':
            realAmount = $.trim(amtValue.replace(lastvalueofAmtString, '')) * 1000000;
            break;
        case 'b':
            realAmount = $.trim(amtValue.replace(lastvalueofAmtString, '')) * 1000000000;
            break;
        default:
            
    }


    // alert(formatnumber(realAmount));
    $("#Amount").val(formatnumber(realAmount))
});
function formatnumber(value) {
    return parseFloat(value.toString().replace(/,/g, "")).toFixed(2).toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}

//$("#ddlCustomerAccount").on('change', function () {
   
//    $("#ddlAcct").val(null).trigger("change");

//})

function Formatter(value, row, index) {
    return [
        '<div class="btn-group">' + '<a style="color:white"  class="approve btn btn-sm btn-info"  title="Approve">'
        + '<i class="fas fa-edit">&nbsp;</i>' +
        '<a style="color:white" title="Disapprove" class="remove btn btn-sm btn-danger">'
        + '<i class="fas fa-trash"></i></a>' +
        '</a> '
    ].join('');
}
window.glEvents = {
    'click .edit': function (e, value, row, index) {
       
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
        
    },
   
    'click .approve': function (e, value, row, index) {
        info = JSON.stringify(row);
        console.log(info);

        debugger
        $('#ID').val(row.id);
        
            swal({
                title: "Are you sure?",
                text: "You are about to approve transfer!",
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

                    TblSingleFundTransfer = {

                        AccountDr: $('#Accountnumber').val(row.AccountDr),
                        AccountCr: $('#AccountNoCr').val(row.AccountCr),
                        Amount: $('#AmountCr').val(row.Amount),
                        OperationType: $('#ddlOperationType').val(row.OperationType),
                        TransactionType: $('#TransactionType').val(row.TransactionType),
                        OperationId: $('#operationid').val(),
                        NarrationDr: $('#NarrationDr').val(),
                        NarrationCr: $('#NarrationCr').val(),
                        TransCode: $('#TransCode').val(),
                        PostDate: $('#Transactiondate').val(),
                        ChequeNo: $('#Cheque').val(),
                        availablebalance: $('#availablebalance').val()
                    };
                    debugger
                    $.ajax({
                        url: url_path +"/Approve",
                        type: "POST",
                        data: TblSingleFundTransfer,
                        success: function (result) {
                            debugger

                            if (result.message !== " ") {
                                swal({ title: 'Initiate new transfer', text: 'Something went wrong: ' + result.message.toString(), type: 'error' }).then(function () { window.location.reload(true); });
                                $("#btnSingleSave").removeAttr("disabled");
                                $("#btnSingleSend").removeAttr("disabled");
                            }
                            else {
                                swal({ title: 'Initiate new transfer', text: 'New transaction added successfully!', type: 'success' }).then(
                                    setTimeout(function () { window.location.replace(url_path +"/index"); }, 3000

                                    ));
                                $("#btnSingleSave").removeAttr("disabled");
                                $("#btnSingleSend").removeAttr("disabled");
                            }
                        },
                        error: function (e) {

                            swal({ title: 'Initiate new transfer', text: 'Transfer encountered an error', type: 'error' }).then(function () { clear(); });
                            $("#btnSingleSave").removeAttr("disabled");
                            $("#btnSingleSend").removeAttr("disabled");
                        }
                    });
                }
            });
        


        $.ajax({
            url: url_path + '/Approve',
            type: 'POST',
            data: { ID: row.id },
            success: function (data) {
                swal({
                    title: "Are you sure?",
                    text: "You are about to approve this transaction!",
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
                                'refresh', { url: '/chart/listGL' });

                        //return false;
                    }
                    else {
                        swal("GL Mapping", "You cancelled add category.", "error");
                    }
                    $('#glTable').
                        bootstrapTable(
                            'refresh', { url: '/chart/listGL' });
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

function dateFormatter(date) {
    return moment(date).format("DD MMMM, YYYY");
}
$(document).ready(function ($) {
     $("#Transactiondate").datetimepicker({
         format:"DD MMMM, YYYY", /*"Do MMM YYYY",*/
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
   // $('#totalAmount').val().toLocaleString(window.document.documentElement.lang);

});

var transferAmount;
var transferBalance;
var Account;
var balAccount;
var availbalance;
var transaction;
//$('#ddlOperationType').on('change', function (e) {

//    $("#ddlCustomerAccount").val('Select').trigger('change');
//    $("#ddlAcct").val(null).trigger("change");
//})
$(document).ready(function (e) {

    var  sbalance = '';
     var sresult = '';
        debugger
        $("#ddlOperationType").select2({
            theme: "bootstrap4",
            placeholder: "Loading..."
        });
        $.ajax({
            url: url_path +"/OperationTypeList" 
        }).then(function (response) {

            $("#ddlOperationType").select2({
                theme: "bootstrap4",
                placeholder: "Select Operation Type",
                width: '100%',
                data: response.results
            });
        });


    $('#ddlOperationType').on('change', function (e) {  
       
        $('#ddlCustomerAccount').empty().append(new Option()).trigger('change');
        $('#ddlAcct').empty().append(new Option()).trigger('change');
           
        $('#ddlOperations').val('');
        $("#Accountname").val('');
        $("#Accountnumber").val('');
        $('#availablebalance').val('');
        $('#Amount').val('');
        $('#AccountNoCr').val('');
        $('#AccountNameCr').val('');
        debugger
        $("#ddlCustomerAccount").select2({
            theme: "bootstrap4",
            allowClear: true,
            placeholder: "Loading..."
            
        });
       

        $.ajax({
            url: url_path +"/loadCustomerAccount",
            data: { ID: $('#ddlOperationType').val() },
            type: "POST",
            cache: false
        }).then(function (response) {       
            if (response.message == "Operation cannot be found,try another operation") {
                swal("Operation cannot be found,try another operation");
            }
            else {



                $("#ddlCustomerAccount").select2({
                    theme: "bootstrap4",
                    placeholder: "Select Account",
                    width: '100%',
                    data: response.results

                }

                );
            }
        });
        $("#ddlCustomerAccount").on("select2:select", function (e) {
            debugger
          
            var datas = e.params.data;
            $('#ddlOperations').val(datas.operationid);
            $("#Accountname").val(datas.id);
            $("#Accountnumber").val(datas.amount);
            $('#availablebalance').val(datas.availablebalance);
            $('#Amount').val('');
            $('#AccountNoCr').val('');
            $('#AccountNameCr').val('');
            $('#ddlAcct').val(null).trigger('change.select2');
            availbalance = datas.availablebalance;
            $("#operationid").select2({
                theme: "bootstrap4",
                placeholder: "Loading..."
            });
            $.ajax({
                url: url_path +"/loadProduct",
                data: { id: $('#ddlCustomerAccount').val() },
                type: "POST",
                cache:false
            }).then(function (response) {
                
                $("#operationid").select2({
                    theme: "bootstrap4",
                    placeholder: "Product Type",
                    width: '100%',
                    data: response.results
                });
            });
         
        });

        $("#ddlCustomerAccount").on("select2:select", function (e) {
            debugger

            $("#TransactionType").select2({
                theme: "bootstrap4",
                placeholder: "Loading..."
            });
            $.ajax({
                url: url_path +"/loadTransactionType",
                data: { id: $('#ddlCustomerAccount').val() },   
            }).then(function (response) {
               // $("#TransactionType").empty().trigger('change');  
                $("#TransactionType").select2({
                    theme: "bootstrap4",
                    placeholder: "Transaction Type",
                    width: '100%',
                    data: response.results
                });
            });

        });
       
       $("#ddlCustomerAccount").on("select2:select", function (e) {
        $("#ddlAcct").select2({
            theme: "bootstrap4",
            placeholder: "Loading...",

        });
           $.ajax(url_path +"/loadCustomerAccountCr" , {
               data: { ID: $('#ddlOperationType').val(), acctName: $('#ddlCustomerAccount').val()},            
               type: "POST",
           }).then(function (response) {               
           // $("#ddlAcct").empty().trigger('change');  
            $("#ddlAcct").select2({
                theme: "bootstrap4",
                placeholder: "Select Account...",  
                width: '100%',
                data: response.results
                
            });
               
            });
        $("#ddlAcct").on("select2:select", function (e) {
            debugger
            var datas = e.params.data;
            $("#AccountNameCr").val(datas.id);
            $("#AccountNoCr").val(datas.amount);            
            balAccount =datas.availablebalance;
            $("#ProductCr").select2({
                theme: "bootstrap4",
                placeholder: "Loading..."
            });
            $.ajax({
                url: url_path +"/loadProductCr",
                data: { id: $('#ddlAcct').val() },
                type: "POST"
            }).then(function (response) {
               // $("#ProductCr").empty().trigger('change');  
                $("#ProductCr").select2({
                    theme: "bootstrap4",
                    placeholder: "Product Type",
                    width: '100%',
                    data: response.results
                });
                });
            var form = $("#singleTransfer");
            var value = $.trim($('#Amount').val()).replace(/,/g, "");
            var svalue = balAccount;
            if ($.isNumeric(value)) {
                if (Number(value) <= 0) {
                    this.value = '';
                    $.notify(
                        {
                            icon: "now-ui-icons travel_info",
                            message: "<b>You cannot transfer ₦0 </b>"
                        },
                        {
                            type: "danger",
                            placement: {
                                from: "top",
                                align: "right"
                            }
                        }
                    );
                } else {
                    transferAmount = Number(value);
                    transferBalances = Number(svalue);
                    transferBalance = Number(transferBalances) + Number(transferAmount);
                    $('#BalanceCr').val(transferBalance);
                    $('#AmountCr').val(transferAmount);
                }
            }
           });

           $("#Bank").select2({
               theme: "bootstrap4",
               placeholder: "Loading...",

           });
           $.ajax({
               url: url_path +"/loadBank", 
           }).then(function (response) {
               debugger
               $("#Bank").select2({
                   theme: "bootstrap4",
                   // placeholder: "Select Company...",  
                   width: '100%',
                   data: response.results

               });

           });
    });
    });

   
});
//$('#ddlAcct').on("change", function () {
//    debugger
//    var form = $("#singleTransfer");  
//    var value = $.trim($('#Amount').val()).replace(/,/g, "");
//    var svalue = balAccount;
//    if ($.isNumeric(value)) {
//        if (Number(value) <= 0){
//            this.value = '';
//            $.notify(
//                {
//                    icon: "now-ui-icons travel_info",
//                    message: "<b>You cannot transfer ₦0 </b>"
//                },
//                {
//                    type: "danger",
//                    placement: {
//                        from: "top",
//                        align: "right"
//                    }
//                }
//            );
//        } else {
//            transferAmount = Number(value);
//            transferBalances = Number(svalue);
//            transferBalance = Number(transferBalances) + Number(transferAmount);
//            $('#BalanceCr').val(transferBalance);
//            $('#AmountCr').val(transferAmount);
//        }
//    }
//});

$('#Amount').on("mouseout", function () {
    debugger
    var form = $("#singleTransfer");

    var value = $.trim($('#Amount').val()).replace(/,/g, "");
    var svalue = $.trim(availbalance).replace(/,/g, "");
    var operationTYpe = $('#ddlOperationType').val();
    if ($.isNumeric(value)) {
        if (operationTYpe === "3" || operationTYpe === "4")
        {
            $('#ddlAcct').prop('disable', false);
        }
        else if (Number(value) > Number(svalue) || Number(value) <= 0) {
            this.value = '';
            $.notify(
                {
                    icon: "now-ui-icons travel_info",
                    message: "<b>You cannot transfer ₦0 </b>  OR </br> Your balance is low"
                },
                {
                    type: "danger",
                    placement: {
                        from: "top",
                        align: "right"
                    }
                }
            );
            $('#ddlAcct').prop('disable', true);
        } 
    }
});

$('#btnRefresh').on('click', function () {
    debugger
    //$('#ddlCustomerAccount').trigger('change');
    $('#ddlCustomerAccount').empty().append(new Option()).trigger('change');
    $('#ddlAcct').empty().append(new Option()).trigger('change');
});
//---------------------------------
$(document).ready(function ($) {

    $('#btnSingleSave').on('click', function () {
        debugger        
        addSingleTransfer();
    });

});
function addSingleTransfer() {
    debugger

    $("input[type=submit]").attr("disabled", "disabled");
    $('#singleTransfer').validate({
        rules: {
            Amount: {
                number: true
            }
        },
        messages: {

            ddlOperationType: { required: "Operation type is required" },
            ddlCustomerAccount: { required: "Customer Account. is required" },
            Accountname: { required: "Account name is required" },
            Accountnumber: { required: "Account No. is required" },
            OperationId: { required: "Product . is required" },
            Transactiondate: { required: "Transaction date is required" },
            TransactionType: { required: "Transaction type is required" },
            NarrationDr: { required: "Narration for debit is required" },
            ddlAcct: { required: "Account is required" },
            AccountNameCr: { required: "Account Name for credit is required" },
            AccountNoCr: { required: "Account No for credit is required" },
            BalanceCr: { required: "Beneficiary balance . is required" },
            ProductCr: { required: "Beneficiary product is required" },
            Amount: { required: "Originator amount is required" },
            AmountCr: { required: "Beneficiary amount is required" },
            NarrationCr: { required: " Beneficiary Narration is required" }         
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
                text: "You are about to initiate transfer!",
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
                   
                    TblSingleFundTransfer = {
                            
                            AccountDr: $('#Accountnumber').val(),
                            AccountCr: $('#AccountNoCr').val(),
                            Amount: $('#AmountCr').val(),
                            OperationType: $('#ddlOperationType').val(),
                            TransactionType: $('#TransactionType').val(),
                            OperationId: $('#operationid').val(),
                            NarrationDr: $('#NarrationDr').val(),
                            NarrationCr: $('#NarrationCr').val(),
                            TransCode: $('#TransCode').val(),
                            PostDate: $('#Transactiondate').val(),
                            ChequeNo: $('#Cheque').val(),
                            availablebalance: $('#availablebalance').val(),
                            ChargeStamp: $('#stampduty').prop("checked")
                    };
                    if (TblSingleFundTransfer.NarrationCr !== '' || TblSingleFundTransfer.NarrationDr !== '' || TblSingleFundTransfer.PostDate !== '') {
                        $.ajax({
                            url: url_path +"/Index",
                            type: "POST",
                            data: TblSingleFundTransfer,
                            success: function (result) {
                                debugger

                                if (result.message !== " ") {
                                    swal({ title: 'Initiate new transfer', text: 'Something went wrong: ' + result.message.toString(), type: 'error' }).then(function () { window.location.reload(true); });
                                    $("#btnSingleSave").removeAttr("disabled");
                                    $("#btnSingleSend").removeAttr("disabled");
                                }
                                else {
                                    swal({ title: 'Initiate new transfer', text: 'New transaction added successfully!', type: 'success' }).then(
                                        setTimeout(function () { window.location.replace("fundtransfer"); }, 3000

                                        ));
                                    $("#btnSingleSave").removeAttr("disabled");
                                    $("#btnSingleSend").removeAttr("disabled");
                                }
                            },
                            error: function (e) {

                                swal({ title: 'Initiate new transfer', text: 'Transfer encountered an error', type: 'error' }).then(function () { clear(); });
                                $("#btnSingleSave").removeAttr("disabled");
                                $("#btnSingleSend").removeAttr("disabled");
                            }
                        });
                    } else {
                        swal("Credit narration or debit narration is empty  or transaction date");
                    }
                }
            });
        }

    },

        function (dismiss) {
            swal('Initiate new transfer', 'You cancelled new transfer.', 'error');
            $("#btnSingleSave").removeAttr("disabled");
            $("#btnSingleSend").removeAttr("disabled");
        });

}
