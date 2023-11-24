var url_path = window.location.pathname;
if (url_path.charAt(url_path.length - 1) == '/') {
    url_path = url_path.slice(0, url_path.length - 1);
}
var transferAmounts;
var transferBalances;
var transferAmount;
var transferBalance;
var Account;
var balAccount;
var availbalance;
var valueDr;
var totals;
var totalDebit;
var balAccounts;
var availbalances;
var transactions;
var transferBal;
var availableBalances;
var TblCreditFundTransfer;
var TblDebitFundTransfer;
var $pendingtable = $('#multipleTable');
var batchName;
var pendingmultiple;
var total = [];
var Amount = [];
var value = [];
var stotal; var sessionId;
var $multipleTransfertable = $('#tempTable');


$(document).ready(function ($) {
    $("#Transactiondate").datetimepicker({
        format: "DD MMMM, YYYY",
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
    //$('#multipleTable').hide('true');
});
function dateFormatter(date) {
    return moment(date).format("DD MMMM, YYYY");
}
function multipleFormatter(value, row, index) {
    return [

        '<a style="color:white"  class="edit btn btn-sm btn-info mr-2"  title="Edit Amount" >'
        + '<i class="fas fa-edit"></i>' + "</a>" +
        '<a style="color:white"  title="Remove Beneficiary" class="remove btn btn-sm btn-danger">'
        + '<i class="fas fa-trash"></i></a>' +
        '</a> '
    ].join('');
}
window.multipleEvents = {
    'click .edit': function (e, value, row, index) {
        var form = $("#multipleTransfer");
        form.trigger("reset");
        if (row.state === true) {
            var data = JSON.stringify(row);
            form.find("[name=Id]").val(row.id);
            $('#Id').val(row.id);
            $('#sessionId').val(row.reference);
            $('#AmountsUpCr').val(row.amount);
            $('#acctNo').val(row.accountNo);
            $('#amountUpdateModal').modal('show');
            $('#btnBranchUpdate').html('  <i class="now-ui-icons ui-1_check"></i> Update Record');
            $('#btnMultipleUpdate').show();


        }
    },
    'click .remove': function (e, value, row, index) {
        info = JSON.stringify(row);
        console.log(info);

        debugger
        $('#ID').val(row.Id);
        swal({
            title: "Are you sure?",
            text: "The Transactions will be deleted!",
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

                $.ajax({
                    url: url_path + '/RemoveTransfer',
                    type: 'POST',
                    data: { ID: row.id },
                    success: function (data) {
                        swal("Deleted succesfully");
                        //alert('Deleted succesfully');
                        $('#tempTable').bootstrapTable('refresh', {
                            silent: true
                        });
                        return true;

                    },

                    error: function (e) {
                        //alert("An exception occured!");
                        swal("An exception occured!");
                    }
                });
            }
        });
    }
};
function multipleAllFormatter(value, row, index) {
    return [

        '<a style="color:white"  class="edit btn btn-sm btn-info mr-2"  title="Edit Amount" >'
        + '<i class="fas fa-edit"></i>' + "</a>" +
        '<a style="color:white"  title="Remove Beneficiary" class="remove btn btn-sm btn-danger">'
        + '<i class="fas fa-trash"></i></a>' +
        '</a> '
    ].join('');
}
window.multipleAllEvents = {
    'click .edit': function (e, value, row, index) {
        var form = $("#multipleTransfer");
        form.trigger("reset");
        if (row.state === true) {
            var data = JSON.stringify(row);
            form.find("[name=Id]").val(row.id);
            $('#Id').val(row.id);
            $('#sessionId').val(row.reference);
            $('#AmountsUpCr').val(row.amount);
            $('#acctNo').val(row.accountNo);
            $('#amountUpdateModal').modal('show');
            $('#btnBranchUpdate').html('  <i class="now-ui-icons ui-1_check"></i> Update Record');
            $('#btnMultipleUpdate').show();


        }
    },
    'click .remove': function (e, value, row, index) {
        info = JSON.stringify(row);
        console.log(info);

        debugger
        $('#ID').val(row.Id);
        swal({
            title: "Are you sure?",
            text: "The Transactions will be deleted!",
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

                $.ajax({
                    url: '/fundtransfer/RemoveTransfer',
                    type: 'POST',
                    data: { ID: row.id },
                    success: function (data) {
                        swal("Deleted succesfully");
                        //alert('Deleted succesfully');
                        $('#tempTable').bootstrapTable('refresh', {
                            silent: true
                        });
                        return true;

                    },

                    error: function (e) {
                        //alert("An exception occured!");
                        swal("An exception occured!");
                    }
                });
            }
        });
    }
};
//update
$('#updateBtn').on('click', function () {

    updateMultiple();
});
function updateMultiple() {
    var json_data =
    {
        Id: $('#Id').val(),
        Reference: $('#sessionId').val(),
        Amount: $('#AmountsUpCr').val(),
        acctNo: $('#acctNo').val()
    };

    sessionId = $('#sessionId').val();
    $.ajax("../fundtransfer/UpdateMultiple", {
        data: JSON.stringify(json_data),
        method: "POST",
        contentType: "application/json",
        success: function (result) {

            if (result.toString !== '' && result !== null) {
                swal({ title: 'Update Account', text: 'Account updated successfully!', type: 'success' }).then(function () { window.location.reload(true); });
                $("#amountUpdateModal").modal("hide");
                $('#tempTable').bootstrapTable('refresh', {
                    silent: true
                });

            }
            else {
                swal({ title: 'Update Account', text: 'Something went wrong: </br>' + result.toString(), type: 'error' }).then(function () { clear(); });
                $("#updateBtn").removeAttr("disabled");
            }
        },
        error: function (e) {

            swal({ title: 'Initiate new transfer', text: 'Transfer encountered an error', type: 'error' }).then(function () { clear(); });

        }

    }).then(function (response) {
        function transferSuccess() {
            swal({
                title: "Transfer to customer",
                text: "Transaction initiated successfully!",
                type: 'success',
                allowOutsideClick: false,
                allowEscapeKey: false
            }).then(function () {
                clear();
                $('#tempTable').bootstrapTable('refresh', {
                    silent: true
                });
                $('#wizardModal').modal('show');
                //$("#wizardComponent .btn-finish").attr("enabled", "true");
            });
        }

    }, function (error) {
        swal({
            title: 'Transfer to customer',
            text: 'Transaction encountered an error',
            type: 'error',
            allowOutsideClick: false,
            allowEscapeKey: false
        }).then(function () {
            $("#wizardComponent .btn-finish").attr("enabled", "true");
        });
    });


}
$(document).ready(function (e) {

    var sbalance = '';
    var sresult = '';
    debugger
    $("#ddlOperationType").select2({
        theme: "bootstrap4",
        placeholder: "Loading..."
    });
    $.ajax({
        url: "../fundtransfer/OperationTypeList"
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
            placeholder: "Loading..."
        });
        $.ajax({
            url: "../fundtransfer/loadCustomerAccount",
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
            availbalance = datas.availablebalance;
            $("#operationid").select2({
                theme: "bootstrap4",
                placeholder: "Loading..."
            });
            $.ajax({
                url: "../fundtransfer/loadProduct",
                data: { id: $('#ddlCustomerAccount').val() },
                type: "POST",
                cache: false
            }).then(function (response) {
                $("#operationid").empty().trigger('change');
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
                url: "../fundtransfer/loadTransactionType",

            }).then(function (response) {
                $("#TransactionType").empty().trigger('change');
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
            $.ajax("../fundtransfer/loadCustomerAccountCr", {
                data: { ID: $('#ddlOperationType').val(), acctName: $('#ddlCustomerAccount').val() },
                type: "POST",
            }).then(function (response) {               
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
                balAccount = datas.availablebalance;
                $("#ProductCr").select2({
                    theme: "bootstrap4",
                    placeholder: "Loading..."
                });
                $.ajax({
                    url: "../fundtransfer/loadProductCr",
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
                var value = $.trim($('#AmountCr').val()).replace(/,/g, "");
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
                        // $('#AmountCr').val(transferAmount);
                    }
                }
            });

            $("#Bank").select2({
                theme: "bootstrap4",
                placeholder: "Loading...",

            });
            $.ajax({
                url: "../fundtransfer/loadBank",
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

//Functions
function TotalAmount() {

    var tabl = $("#tempTable").bootstrapTable("getSelections");
    var sumVal = 0;
    for (var i = 0; i < tabl.length; i++) {
        sumVal = sumVal + parseFloat(tabl[i].amount);
    }
    alert(sumVal);
    $('#TotalCredit').val(sumVal);
    var tAmount = $('#Amount').val();
    $('#TotalDebit').val(tAmount);
    $('#TotalCredit').attr('disabled', 'disabled');
    $('#TotalDebit').attr('disabled', 'disabled');
    return sumVal;

}

$('#tempTable').on('change', function () {

    TotalAmount();
});

//Transfer All
$(document).ready(function ($) {
    $('#btnAddAll').on('click', function () {

        addMultiple();
    });
});

function addMultiple() {
    debugger
    var result = $('#TotalDebit');
    $("input[type=submit]").attr("disabled", "disabled");
    if (result) {
        swal({
            title: "Are you sure?",
            text: "Multiple Transactions will be sent!",
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
                MultipleData = $multipleTransfertable.bootstrapTable('getAllSelections');
                $.each(MultipleData, function (index, multipleTransferItemData) {
                    MultipleData[multipleTransferItemData.reference] = multipleTransferItemData.value;
                });
                var MultiData = $multipleTransfertable.bootstrapTable('getAllSelections');
                var totalResult = {
                    TotalDebit: $('#TotalDebit').val(),
                    TotalCredit: $('#TotalCredit').val(),
                    ChargeStamp: $('#stampduty').prop("checked"),
                    
                };
                if (parseInt(totalResult.TotalDebit) == parseInt(totalResult.TotalCredit)) {
                   
                        $.ajax({
                            dataType: 'json',
                            url: '/fundtransfer/MultipleTransferAll',
                            type: 'POST',
                            data: { 'transfers': MultipleData, 'totalResult': totalResult },
                            //contentType: "application/json",
                            success: function (result) {
                                debugger
                                if (result.message !== " ") {
                                    swal({ title: 'Initiate new transfer', text: 'Something went wrong: ' + result.message.toString(), type: 'error' }).then(function () {  });

                                    $('#tempTable').bootstrapTable('refresh', {
                                        silent: true
                                    });

                                }
                                else {
                                    swal({ title: 'Initiate new transfer', text: 'New transaction added successfully!', type: 'success' }).then(function () { });

                                    $('#tempTable').bootstrapTable('refresh', {
                                        silent: true
                                    });
                                    $('#multipleTransfer').trigger("reset");
                                }
                            }
                            //error: function (e) {
                            //    swal({ title: 'Initiate new transfer', text: 'Transfer encountered an error', type: 'error' }).then(function () {});

                            //}
                        });
                 
                } else {
                    alert("Credit must not be greater than debit");
                }
            }

        }),

            function (dismiss) {
                swal({
                    title: 'Transfer to customer',
                    text: 'Transaction encountered an error',
                    type: 'error',
                    allowOutsideClick: false,
                    allowEscapeKey: false
                }).then(function () {
                    $("#wizardComponent .btn-finish").attr("enabled", "true");
                });

            }
    }
}

//For one transfer
$(document).ready(function ($) {

    $('#btnAddFinish').on('click', function () {
        debugger
        addNewTransfer();
    });

});
function addNewTransfer() {
    debugger

    $("input[type=submit]").attr("disabled", "disabled");
    $('#multipleTransfer').validate({
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
                    var transactionModel = {};
                    var transModel = {
                        AccountDr: $('#Accountnumber').val(),
                        AccountCr: $('#AccountNoCr').val(),
                        Amount: $('#Amount').val(),
                        TransactionType: $('#TransactionType').val(),
                        NarrationDr: $('#NarrationDr').val(),
                        NarrationCr: $('#NarrationCr').val(),
                        PostDate: $('#Transactiondate').val(),
                        BatchName: $('#BatchName').val(),
                        availablebalance: $('#availablebalance').val(),
                        Accountname: $('#Accountname').val(),
                        AccountNo: $('#Accountnumber').val(),
                        AmountCr: $('#AmountCr').val(),
                        ValueDate: $('#Transactiondate').val(),
                        availablebalanceCr: $('#BalanceCr').val(),
                        transTypeCr: $('#transTypeCr').val(),
                        ProductsCr: $('#ProductCr').val(),
                        TotalDebit: $('#TotalDebit').val(),
                        TotalCredit: $('#TotalCredit').val()


                    };
                    debugger
                    if (parseInt(transModel.AmountCr) <= parseInt(transModel.TotalDebit)) {
                        $.ajax({
                            url: "../fundtransfer/TempTransfer",
                            type: 'POST',
                            data: {
                                AccountDr: $('#Accountnumber').val(),
                                AccountCr: $('#AccountNoCr').val(),
                                Amount: $('#Amount').val(),
                                TransactionType: $('#TransactionType').val(),
                                NarrationDr: $('#NarrationDr').val(),
                                NarrationCr: $('#NarrationCr').val(),
                                PostDate: $('#Transactiondate').val(),
                                BatchName: $('#BatchName').val(),
                                availablebalance: $('#availablebalance').val(),
                                Accountname: $('#Accountname').val(),
                                AccountNo: $('#Accountnumber').val(),
                                AmountCr: $('#AmountCr').val(),
                                ValueDate: $('#Transactiondate').val(),
                                availablebalanceCr: $('#BalanceCr').val(),
                                transTypeCr: $('#transTypeCr').val(),
                                ProductsCr: $('#ProductCr').val(),
                                TotalDebit: $('#TotalDebit').val(),
                                TotalCredit: $('#TotalCredit').val(),
                                OperationType: $('#ddlOperationType').val()
                            },
                            //contentType: "application/json",
                            success: function (result) {
                                debugger
                                if (result.message !== " ") {
                                    swal({ title: 'Initiate new transfer', text: 'Something went wrong: ' + result.message.toString(), type: 'error' }).then(function () { clear(); });

                                    $('#tempTable').bootstrapTable('refresh', {
                                        silent: true
                                    });

                                }
                                else {
                                    swal({ title: 'Initiate new transfer', text: 'New transaction added successfully!', type: 'success' }).then(function () { });

                                    $('#tempTable').bootstrapTable('refresh', {
                                        silent: true
                                    });
                                    var tDebit = $('#TotalDebit').val();
                                    tDebit = parseInt(transModel.TotalDebit) - parseInt(transModel.AmountCr);

                                    $("#TotalDebit").val(tDebit);

                                }
                            },
                            error: function (e) {
                                swal({ title: 'Initiate new transfer', text: 'Transfer encountered an error', type: 'error' }).then(function () { clear(); });

                            }
                        });
                    } else {
                        alert("Credit must not be greater than debit");
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

$('#Amount').on("mouseout", function () {
    debugger
    var form = $("#TotalDebit").val();

    var value = $.trim($('#Amount').val()).replace(/,/g, "");
    var svalue = $.trim($('#availablebalance').val()).replace(/,/g, "");
    var operationTYpe = $('#ddlOperationType').val();
    if ($.isNumeric(value)) {
        if (operationTYpe === "3" || operationTYpe === "4") {
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
        $("#TotalDebit").val(value);
    }
});
$('#AmountCr').on('mouseout', function () {
    var value = $.trim($('#AmountCr').val()).replace(/,/g, "");
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
            // $('#AmountCr').val(transferAmount);
        }
    }
});
//Validation
function initFormValidations() {
    // defaults
    jQuery.validator.setDefaults({
        onfocusout: false,
        onkeyup: false,
        onclick: false,
        normalizer: function (value) {
            // Trim the value of every element
            // before validation
            return $.trim(value);
        },
        errorPlacement: function (error, element) {
            $.notify({
                icon: "now-ui-icons travel_info",
                message: error.text()
            }, {
                type: "danger",
                placement: {
                    from: "top",
                    align: "right"
                }
            });
        }
    });
    $("#multipleTransfer")
        .each(function () {
            $(this).validate({

                messages: {
                    Accountnumber: {
                        required: "Account number is required"

                    },
                    AccountNoCr: {
                        required: "Beneficiary No is required"
                    },
                    ddlCustomerAccount: {
                        required: "Account number is required"

                    },
                    Amount: { required: "Debit Amount is required" },
                    operationid: { required: "Product is required" },
                    TransactionsType: { required: "Transaction Type is required" },
                    NarrationDr: { required: "Narration Dr is required" },
                    NarrationCr: { required: "Narration Cr is required" },
                    Transactiondate: { required: "Value date is required" },
                    availablebalance: { required: "available balance is required" },
                    Accountname: { required: "Account name is required" },
                    AmountCr: { required: "Amount Cr is required" },
                    ddlOperationType: { required: "Transaction Type is required" },
                    TotalDebit: { required: "Total Debit is required" }
                }
            });
        });



}
//Existing Batch
//Multiple Approval
$(document).ready(function ($) {
    debugger
    $("#ddlBatch").select2({
        theme: "bootstrap4",
        placeholder: "Loading...",
    });
    $.ajax({
        url: "../fundtransfer/loadBatchRef",
    }).then(function (response) {
        debugger
        $("#ddlBatch").select2({
            theme: "bootstrap4",
            placeholder: "Select Batch Reference...",
            width: '100%',
            data: response.results
        });
    });

    $('#ddlBatch').on('change', function () {
        debugger
        $('#tempTable').hide('true');
        $.ajax({
            url: "../fundtransfer/listFundMultipFund",
            data: { Id: $('#ddlBatch').val() },
            type: "POST",
            datatype: "application/json",
            success: function (response) {

                //console.log(response);
                debugger;

                var $table = $('#multipleTable');
                $table.bootstrapTable("destroy");

                $table.bootstrapTable({
                    data: response,

                    columns: [
                        {
                            field: 'state',
                            checkbox: true

                        }, {
                            field: 'accountNo',
                            title: 'AccountCr No'
                        }
                        , {
                            field: 'accountNameCr',
                            title: 'AccountCr Name'
                        }
                        , {
                            field: 'amount',
                            title: 'Amount'
                        }
                        , {
                            field: 'accountNoDr',
                            title: 'AccountDr No '
                        },
                        {
                            field: 'transCode',
                            title: 'Trans. Code'
                        },
                        {
                            field: 'dateCreated',
                            title: 'Date Posted',
                            formatter: "dateFormatter"
                        },
                        {
                            field: '',
                            events: 'multipleAllEvents',
                            formatter: 'multipleAllFormatter'
                        }


                    ]
                });

            },
        })

    });

});