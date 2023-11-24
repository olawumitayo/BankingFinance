var url_path = window.location.pathname;
if (url_path.charAt(url_path.length - 1) == '/') {
    url_path = url_path.slice(0, url_path.length - 1);
}

var $Transfertable = $('#transTable');
var $Singletable = $('#transTables'); 
var dateTran;
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
});
$("#Transactiondate").on('click', function () {
    $("#ddlCustomerAccount").val(null).trigger("change");
    $("#ddlCustomerAccountGL").val(null).trigger("change");
    $("#ddlCustomerAccountBulk").val(null).trigger("change");
   
})
function dateFormatter(date) {
    return moment(date).format("DD MMMM, YYYY");
}
$(document).ready(function ($) {
    $("#ddlCustomerAccount").select2({
        theme: "bootstrap4",
        placeholder: "Loading..."
    });
    $.ajax({
        url: "../reversal/loadTeller",
        data: $('#Transactiondate').val(),
        cache: false
    }).then(function (response) {

        $("#ddlCustomerAccount").select2({
            theme: "bootstrap4",
            placeholder: "Select Teller",
            width: '100%',
            data: response.results
        });
    });
});
$(document).ready(function ($) {
    debugger
    $("#ddlCustomerAccountBulk").select2({
        theme: "bootstrap4",
        placeholder: "Loading..."
    });
    $.ajax({
        url: "../reversal/loadTeller",
    
        cache: false
    }).then(function (response) {

        $("#ddlCustomerAccountBulk").select2({
            theme: "bootstrap4",
            placeholder: "Select Teller",
            width: '100%',
            data: response.results
        });
    });
});
$(document).ready(function ($) {
    $("#ddlCustomerAccountGL").select2({
        theme: "bootstrap4",
        placeholder: "Loading..."
    });
    $.ajax({
        url: "../reversal/loadTeller",
        cache: false
    }).then(function (response) {

        $("#ddlCustomerAccountGL").select2({
            theme: "bootstrap4",
            placeholder: "Select Teller",
            width: '100%',
            data: response.results
        });
    });
});

$('#ddlCustomerAccount').on('change', function () {
    debugger
    $.ajax({
        url: "../reversal/listmultiple",
        data: { ID: $('#ddlCustomerAccount').val(), TranDate: $('#Transactiondate').val() },
        type: "POST",
        datatype: "application/json",
        success: function (response) {

            console.log(response);
            debugger;

            var $table = $('#transTable');
            $table.bootstrapTable("destroy");

            $table.bootstrapTable({
                data: response,

                columns: [
                    {
                        field: 'state',
                        checkbox: true

                    }, {
                        field: 'reference',
                        title: 'Reference(M)'
                    }

                    , {
                        field: 'accountNo',
                        title: 'Credit Account'
                    }

                    , {
                        field: 'accountNoDr',
                        title: 'Debit Account '
                    }

                    ,
                    {
                        field: 'amount',
                        title: 'Amount'

                    }
                    ,
                    {
                        field: 'createdBy',
                        title: 'Initiator'
                    }
                    , {
                        field: 'dateCreated',
                        title: 'Date',
                        formatter: 'dateFormatter'
                    }

                ]
            });

        },
    });

    $.ajax({
        url: "../reversal/listSingle",
        data: { ID: $('#ddlCustomerAccount').val(), TranDate: $('#Transactiondate').val() },
        type: "POST",
        datatype: "application/json",
        success: function (response) {

            console.log(response);
            debugger;

            var $table = $('#transTables');
            $table.bootstrapTable("destroy");

            $table.bootstrapTable({
                data: response,

                columns: [
                    {
                        field: 'state',
                        checkbox: true

                    }
                    , {
                        field: 'reference',
                        title: 'Reference(S)'
                    }

                    , {
                        field: 'accountCr',
                        title: 'Credit Account(S)'
                    }

                    , {
                        field: 'accountDr',
                        title: 'Debit Account(S) '
                    }
                    ,
                    {
                        field: 'amount',
                        title: 'Amount(S)'

                    }

                    , {
                        field: 'postDate',
                        title: 'Date(S)',
                        formatter: 'dateFormatter'
                    }
                ]
            });

        },
    });
});

$('#ddlCustomerAccountGL').on('change', function () {
    debugger
    $.ajax({
        url: "../reversal/listmultipleGL",
        data: { ID: $('#ddlCustomerAccountGL').val(), TranDate: $('#Transactiondate').val() },
        type: "POST",
        datatype: "application/json",
        success: function (response) {

            console.log(response);
            debugger;

            var $table = $('#transTableGL'); 
            $table.bootstrapTable("destroy");

            $table.bootstrapTable({
                data: response,

                columns: [
                    {
                        field: 'state',
                        checkbox: true

                    }, {
                        field: 'reference',
                        title: 'Reference(M)'
                    }

                    , {
                        field: 'accountNo',
                        title: 'Credit Account'
                    }

                    , {
                        field: 'accountNoDr',
                        title: 'Debit Account '
                    }

                    ,
                    {
                        field: 'amount',
                        title: 'Amount'

                    }
                    ,
                    {
                        field: 'createdBy',
                        title: 'Initiator'
                    }
                    , {
                        field: 'dateCreated',
                        title: 'Date',
                        formatter: 'dateFormatter'
                    }

                ]
            });

        },
    });

    $.ajax({
        url: "../reversal/listSingleGL",
        data: { ID: $('#ddlCustomerAccountGL').val(), TranDate: $('#Transactiondate').val() },
        type: "POST",
        datatype: "application/json",
        success: function (response) {

            console.log(response);
            debugger;

            var $table = $('#transTablesGL');
            $table.bootstrapTable("destroy");

            $table.bootstrapTable({
                data: response,

                columns: [
                    {
                        field: 'state',
                        checkbox: true

                    }
                    , {
                        field: 'reference',
                        title: 'Reference(S)'
                    }

                    , {
                        field: 'accountCr',
                        title: 'Credit Account(S)'
                    }

                    , {
                        field: 'accountDr',
                        title: 'Debit Account(S) '
                    }
                    ,
                    {
                        field: 'amount',
                        title: 'Amount(S)'

                    }

                    , {
                        field: 'postDate',
                        title: 'Date(S)',
                        formatter: 'dateFormatter'
                    }
                ]
            });

        },
    });
});
$(document).ready(function ($) {
    $('#btnAddReversal').on('click', function () {
        addReversal();
    });
    $('#btnAddSingelReversal').on('click', function () {
        addReversalSingle();
    });
    $('#btnAddReversalGL').on('click', function () {
        addReversalGLMultiple();
    });
    $('#btnAddReversalSingleGL').on('click', function () {
        addReversalGL();
    });
    $('#btnAddReversalBulk').on('click', function () {
        addBulkUploadMultiple();
    });
    $('#btnAddGLReversal').on('click', function () {
        addGL();
    });
});

//For submit reversal
function addReversal() {
    debugger
    var result = $('#Transactiondate');
    MultipleData = $Transfertable.bootstrapTable('getAllSelections');
   
    var tDate = $('#Transactiondate').val();
    var coment = $('#comment').val();
    $("input[type=submit]").attr("disabled", "disabled");
    if (MultipleData["length"] !== 0 ) {
        swal({
            title: "Are you sure?",
            text: "Reversal will be sent!",
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
                var Tran =
                {

                    comment: $('#comment').val(),
                    tranDate: $('#Transactiondate').val()
                };

                if (MultipleData["length"] !== 0) {
                    $.each(MultipleData, function (index, staffItemData) {
                        if (tDate !== "" || coment !== "") {
                            $.ajax({
                                url: '/reversal/ReverseCustomer',
                                type: 'POST',
                                data: {
                                    ID: staffItemData.id, Reference: staffItemData.reference, Amount: staffItemData.amount,
                                    Comment: $('#comment').val(), TranDate: $('#Transactiondate').val()
                                },

                                dataType: 'json',
                                success: function (result) {
                                    debugger
                                    if (result.message !== " ") {
                                        swal({ title: 'Transfer Reversal', text: 'Something went wrong: ' + result.message.toString(), type: 'error' }).then(function () { window.location.reload(true); });

                                        $('#transTable').bootstrapTable('refresh', {
                                            silent: true
                                        });
                                       

                                    }
                                    else {
                                        swal({ title: 'Transfer Reversal', text: 'New reversal added successfully!', type: 'success' }).then(function () { window.location.reload(true);});

                                      
                                        $('#transTable').bootstrapTable('refresh', {
                                            url: "/reversal/listMultiple"
                                        });
                                        $('#transTables').bootstrapTable('refresh', {
                                            url: "/reversal/listSingle"
                                        });
                                    }
                                },
                                error: function (e) {
                                    swal({ title: 'Transfer Reversal', text: 'Reversal encountered an error', type: 'error' }).then(function () { clear(); });

                                }
                            });
                        } else {
                            swal("Transaction date or comment cannot be empty");
                        }
                    });
                } 
            }

        })
    } else {
        swal("You have not selected any transaction(s)");
    }
}
function addReversalSingle() {
    debugger
    var result = $('#Transactiondate');
   
    SingleData = $Singletable.bootstrapTable('getAllSelections');
    var tDate = $('#Transactiondate').val();
    var coment = $('#comment').val();
    $("input[type=submit]").attr("disabled", "disabled");
    if (SingleData["length"] !== 0) {
        swal({
            title: "Are you sure?",
            text: "Reversal will be sent!",
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
                var Tran =
                {

                    comment: $('#comment').val(),
                    tranDate: $('#Transactiondate').val()
                };

                
                    $.each(SingleData, function (index, singleItemData) {
                        if (tDate !== "" || coment !== "") {
                            $.ajax({
                                url: '/reversal/ReverseCustomerSingle',
                                type: 'POST',
                                data: {
                                    ID: singleItemData.id,
                                    References: singleItemData.reference, Amounts: singleItemData.amount,
                                    Comment: $('#comment').val(), TranDate: $('#Transactiondate').val()
                                },

                                dataType: 'json',
                                success: function (result) {
                                    debugger
                                    if (result.message !== " ") {
                                        swal({ title: 'Transfer Reversal', text: 'Something went wrong: ' + result.message.toString(), type: 'error' }).then(function () { window.location.reload(true); });

                                        $('#transTable').bootstrapTable('refresh', {
                                            silent: true
                                        });

                                    }
                                    else {
                                        swal({ title: 'Transfer Reversal', text: 'New reversal added successfully!', type: 'success' }).then(function () { window.location.reload(true); });


                                        $('#transTable').bootstrapTable('refresh', {
                                            url: "/reversal/listMultiple"
                                        });
                                        $('#transTables').bootstrapTable('refresh', {
                                            url: "/reversal/listSingle"
                                        });

                                    }
                                },
                                error: function (e) {
                                    swal({ title: 'Transfer Reversal', text: 'Reversal encountered an error', type: 'error' }).then(function () { clear(); });

                                }
                            });
                        } else {
                            swal("Transaction date or comment cannot be empty");
                        }
                    });
                
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

function addReversalGL() {
    debugger
    var result = $('#Transactiondate'); 
    var $SingletableGL = $('#transTablesGL');

    SingleData = $SingletableGL.bootstrapTable('getAllSelections');
    
   
    var tDate = $('#Transactiondate').val();
    var coment = $('#comment').val();
    $("input[type=submit]").attr("disabled", "disabled");
    if (SingleData["length"] !== 0 || SingleData["length"] !== 0) {
        swal({
            title: "Are you sure?",
            text: "Reversal will be sent!",
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
                var Tran =
                {

                    comment: $('#comment').val(),
                    tranDate: $('#Transactiondate').val()
                };

                if (SingleData["length"] !== 0) {
                    $.each(SingleData, function (index, staffItemData) {
                        if (tDate !== "" || coment !== "") {
                            $.ajax({
                                url: '/reversal/ReverseGLSingle',
                                type: 'POST',
                                data: {
                                    ID: staffItemData.id, Reference: staffItemData.reference, Amount: staffItemData.amount,
                                    Comment: $('#comment').val(), TranDate: $('#Transactiondate').val()
                                },

                                dataType: 'json',
                                success: function (result) {
                                    debugger
                                    if (result.message !== " ") {
                                        swal({ title: 'Transfer Reversal', text: 'Something went wrong: ' + result.message.toString(), type: 'error' }).then(function () { window.location.reload(true); });

                                        $('#transTableGL').bootstrapTable('refresh', {
                                            silent: true
                                        });


                                    }
                                    else {
                                        swal({ title: 'Transfer Reversal', text: 'New reversal added successfully!', type: 'success' }).then(function () { window.location.reload(true); });


                                        $('#transTableGL').bootstrapTable('refresh', {
                                            url: "/reversal/listMultipleGL"
                                        });
                                        $('#transTablesGL').bootstrapTable('refresh', {
                                            url: "/reversal/listSingleGL"
                                        });
                                    }
                                },
                                error: function (e) {
                                    swal({ title: 'Transfer Reversal', text: 'Reversal encountered an error', type: 'error' }).then(function () { clear(); });

                                }
                            });
                        } else {
                            swal("Transaction date or comment cannot be empty");
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
function addReversalGLMultiple() {
    debugger
    var result = $('#Transactiondate');
    var $TransfertableGL = $('#transTableGL');
    MultipleData = $TransfertableGL.bootstrapTable('getAllSelections');

   
    var tDate = $('#Transactiondate').val();
    var coment = $('#comment').val();
    $("input[type=submit]").attr("disabled", "disabled");
    if (MultipleData["length"] !== 0) {
        swal({
            title: "Are you sure?",
            text: "Reversal will be sent!",
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
                var Tran =
                {

                    comment: $('#comment').val(),
                    tranDate: $('#Transactiondate').val()
                };

             
                $.each(MultipleData, function (index, singleItemData) {
                        if (tDate !== "" || coment !== "") {
                            $.ajax({
                                url: '/reversal/ReverseGL',
                                type: 'POST',
                                data: {
                                    ID: singleItemData.id,
                                    References: singleItemData.reference, Amounts: singleItemData.amount,
                                    Comment: $('#comment').val(), TranDate: $('#Transactiondate').val()
                                },

                                dataType: 'json',
                                success: function (result) {
                                    debugger
                                    if (result.message !== " ") {
                                        swal({ title: 'Transfer Reversal', text: 'Something went wrong: ' + result.message.toString(), type: 'error' }).then(function () { window.location.reload(true); });

                                        $('#transTable').bootstrapTable('refresh', {
                                            silent: true
                                        });

                                    }
                                    else {
                                        swal({ title: 'Transfer Reversal', text: 'New reversal added successfully!', type: 'success' }).then(function () { window.location.reload(true); });


                                        $('#transTable').bootstrapTable('refresh', {
                                            url: "/reversal/listMultiple"
                                        });
                                        $('#transTables').bootstrapTable('refresh', {
                                            url: "/reversal/listSingle"
                                        });

                                    }
                                },
                                error: function (e) {
                                    swal({ title: 'Transfer Reversal', text: 'Reversal encountered an error', type: 'error' }).then(function () { clear(); });

                                }
                            });
                        } else {
                            swal("Transaction date or comment cannot be empty");
                        }
                    });
                
            }

        })

           
    } else {
        swal("You have not selected any transaction(s)");
    }
}
//Bulk
$('#ddlCustomerAccountBulk').on('change', function () {
    debugger
    var Chk = $('#ddlCustomerAccountBulk').val();
    $.ajax({
        url: "../reversal/listBulkCustomer",
        data: { ID: $('#ddlCustomerAccountBulk').val(), TranDate: $('#Transactiondate').val() },
        type: "POST",
        datatype: "application/json",
        success: function (response) {

            console.log(response);
            debugger;

            var $table = $('#BulkCustomerTable');
            $table.bootstrapTable("destroy");

            $table.bootstrapTable({
                data: response,

                columns: [
                    {
                        field: 'state',
                        checkbox: true

                    }, {
                        field: 'batchRef',
                        title: 'Reference(M)'
                    }

                    , {
                        field: 'ref',
                        title: ' Account Number'
                    }
                    ,
                    {
                        field: 'creditAmount',
                        title: 'Credit Amount'

                    }
                    

                    ,
                    {
                        field: 'debitAmount',
                        title: 'Debit Amount'

                    }
                    ,
                    {
                        field: 'userName',
                        title: 'Initiator'
                    }
                    ,
                    {
                        
                        field: 'transactionDate',
                        visible: false,
                        formatter: 'dateFormatter'
                    }
                    , {
                        field: 'postDate',
                        title: 'Date',
                        formatter: 'dateFormatter'
                    }

                ]
            });

        },
    });

    $.ajax({
        url: "../reversal/listBulkGL",
        data: { ID: $('#ddlCustomerAccountBulk').val(), TranDate: $('#Transactiondate').val() },
        type: "POST",
        datatype: "application/json",
        success: function (response) {

            console.log(response);
            debugger;

            var $table = $('#BulkLedgerTable');
            $table.bootstrapTable("destroy");

            $table.bootstrapTable({
                data: response,

                columns: [
                    {
                        field: 'state',
                        checkbox: true

                    }, {
                        field: 'ref',
                        title: 'Reference(M)'
                    }

                    , {
                        field: 'accountId',
                        title: ' Account Number'
                    }

                    , {
                        field: 'debitAmt',
                        title: 'Debit Amount '
                    }

                    ,
                    {
                        field: 'creditAmt',
                        title: 'Credit Amount'

                    }
                    ,
                    {
                        field: 'postedBy',
                        title: 'Initiator'
                    }
                    , {
                        field: 'postDate',
                        title: 'Date',
                        formatter: 'dateFormatter'
                    }

                ]
            });

        },
    });
});

function addBulkUploadMultiple() {
    debugger
    var result = $('#Transactiondate');
    var $TransfertableGL = $('#BulkCustomerTable');
   var BulkData = $TransfertableGL.bootstrapTable('getAllSelections');


    var tDate = $('#Transactiondate').val();
    var coment = $('#comment').val();
    $("input[type=submit]").attr("disabled", "disabled");
    if (BulkData["length"] !== 0) {
        swal({
            title: "Are you sure?",
            text: "Reversal will be sent!",
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
                var Tran =
                {

                    comment: $('#comment').val(),
                    tranDate: $('#Transactiondate').val()
                };


                //$.each(BulkData, function (index, bulkItemData) {
                    if (tDate !== "" || coment !== "") {
                        $.ajax({
                            url: '/reversal/ReverseBulk',
                            type: 'POST',
                            data: {
                                transfers: BulkData,
                                Comment: $('#comment').val(), TranDate: $('#Transactiondate').val()
                            },

                            dataType: 'json',
                            success: function (result) {
                                debugger
                                if (result.message !== " ") {
                                    swal({ title: 'Transfer Reversal', text: 'Something went wrong: ' + result.message.toString(), type: 'error' }).then(function () { });

                                    $('#transTable').bootstrapTable('refresh', {
                                        silent: true
                                    });

                                }
                                else {
                                    swal({ title: 'Transfer Reversal', text: 'New reversal added successfully!', type: 'success' }).then(function () { window.location.reload(true); });


                                    $('#BulkCustomerTable').bootstrapTable('refresh', {
                                        url: "/reversal/listBulkCustomer"
                                    });
                                   

                                }
                            },
                            error: function (e) {
                                swal({ title: 'Transfer Reversal', text: 'Reversal encountered an error', type: 'error' }).then(function () { clear(); });

                            }
                        });
                    } else {
                        swal("Transaction date or comment cannot be empty");
                    }
                //});

            }

        })


    } else {
        swal("You have not selected any transaction(s)");
    }
}

function addGL() {
    debugger
    var result = $('#Transactiondate');
    var $TransfertableGL = $('#BulkLedgerTable');
    var BulkData = $TransfertableGL.bootstrapTable('getAllSelections');


    var tDate = $('#Transactiondate').val();
    var coment = $('#comment').val();
    $("input[type=submit]").attr("disabled", "disabled");
    if (BulkData["length"] !== 0) {
        swal({
            title: "Are you sure?",
            text: "Reversal will be sent!",
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
                var Tran =
                {

                    comment: $('#comment').val(),
                    tranDate: $('#Transactiondate').val()
                };


                //$.each(BulkData, function (index, bulkItemData) {
                if (tDate !== "" || coment !== "") {
                    $.ajax({
                        url: '../reversal/ReversalGL',
                        type: 'POST',
                        data: {
                            transfers: BulkData,
                            Comment: $('#comment').val(), TranDate: $('#Transactiondate').val()
                        },

                        dataType: 'json',
                        success: function (result) {
                            debugger
                            if (result.message !== " ") {
                                swal({ title: 'Transfer Reversal', text: 'Something went wrong: ' + result.message.toString(), type: 'error' }).then(function () { });

                                $('#transTable').bootstrapTable('refresh', {
                                    silent: true
                                });

                            }
                            else {
                                swal({ title: 'Transfer Reversal', text: 'New reversal added successfully!', type: 'success' }).then(function () { window.location.reload(true); });


                                $('#BulkCustomerTable').bootstrapTable('refresh', {
                                    url: "/reversal/listBulkCustomer"
                                });


                            }
                        },
                        error: function (e) {
                            swal({ title: 'Transfer Reversal', text: 'Reversal encountered an error', type: 'error' }).then(function () { clear(); });

                        }
                    });
                } else {
                    swal("Transaction date or comment cannot be empty");
                }
                //});

            }

        })


    } else {
        swal("You have not selected any transaction(s)");
    }
}
