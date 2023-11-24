var url_path = window.location.pathname;
if (url_path.charAt(url_path.length - 1) == '/') {
    url_path = url_path.slice(0, url_path.length - 1);
}
var transferAmounts;
var transferBalances;
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
var Amount=[];
var value = [];
var stotal; var sessionId;
var $multipleTransfertable = $('#tempTable');
function dateFormatter(date) {
    return moment(date).format("DD MMMM, YYYY");
}
//sectorIndustries.unshift({ id: '', text: '' });
//tab.find("[name=Industryid]").select2('destroy')
//    .empty().select2({
//        placeholder: "Select industry",
//        width: "100%",
//        data: sectorIndustries
//    });
function multipleFormatter(value, row, index) {
    return [

        '<a style="color:white"  class="edit btn btn-sm btn-info mr-2"  title="Edit Amount" >'
        + '<i class="fas fa-edit"></i>' + "</a>"+
        '<a style="color:white"  title="Remove Beneficiary" class="remove btn btn-sm btn-danger">'
        + '<i class="fas fa-trash"></i></a>' +
        '</a> '
    ].join('');
}

window.multipleEvents = {
    'click .edit': function (e, value, row, index) {
            
            var data = JSON.stringify(row);
            $('#Id').val(row.id);
            $('#sessionId').val(row.reference);
            $('#AmountsUpCr').val(row.amount);
            $('#acctNo').val(row.accountNo);     
            $('#amountUpdateModal').modal('show');
            $('#btnBranchUpdate').html('  <i class="now-ui-icons ui-1_check"></i> Update Record');
            $('#btnMultipleUpdate').show();
  
    }
   

};
function handleAmountUpdate(self) {
    UpdateCustomer = $("#tempTable")
        .bootstrapTable('getRowByUniqueId', self.id);
    $("#amountUpdateModal").modal("show");

}

$(document).ready(function ($) {
    $("#Valuedate").datetimepicker({
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
$(document).ready(function ($) {
    $("#Postingdate").datetimepicker({
        format: "Do MMM YYYY",
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
    $("#ddlChargeType").select2({
        theme: "bootstrap4",
        placeholder: "Loading..."
    });
    $.ajax({
        url: "ChargeTypeList"
    }).then(function (response) {

        $("#ddlChargeType").select2({
            theme: "bootstrap4",
            placeholder: "Select Transaction Charge Type",
            width: '100%',
            data: response.results
        });
    });
});
$(document).ready(function ($) {

  
    $("#ddlCustAccount").select2({
        theme: "bootstrap4",
        placeholder: "Loading..."
    });
    $.ajax({
        url: "loadCustAccount",
        cache: false
    }).then(function (response) {
        $("#ddlCustAccount").select2({
            theme: "bootstrap4",
            placeholder: "Select Account",
            width: '100%',
            data: response.results
        });

    });
    $("#ddlCustAccount").on("select2:select", function (e) {
        
        var datas = e.params.data;
        $('#ddlOperations').val(datas.operationid);
        $("#Accountname").val(datas.id);
        $("#Accountnumber").val(datas.amount);      
        $('#AmountsCr').val('');
        $('#AccountNoCr').val('');
        $('#AccountNameCr').val('');
       
        availbalance = datas.availablebalance;
        $("#operationid").select2({
            theme: "bootstrap4",
            placeholder: "Loading..."
        });
        $.ajax({
            url: "loadGLBalance",
            data: { AccountID: $('#ddlCustAccount').val() },
            type: "POST",
            cache: false            
        }).then(function (response) {          
            $("#availablebalance").val(response);
            balAccounts = response;
        });
           
            $("#TransactionsType").select2({
                theme: "bootstrap4",
                placeholder: "Loading..."
            });
            $.ajax({
                url: "loadTransactionsType"
            }).then(function (response) {
                $('#TransactionsType').val(null).trigger('change.select2');
                $("#TransactionsType").select2({
                    theme: "bootstrap4",
                    placeholder: "Transaction Type",
                    width: '100%',
                    data: response.results
                });
            });      
     
            $('#BeneficiaryName').val(null).change();
            $('#BeneficiaryNo').val(null).change();
            
            $("#ddlAccts").select2({
                theme: "bootstrap4",
                placeholder: "Loading..."
            });
            $.ajax({
                url: "loadCustAccountCr"
            }).then(function (response) {

                $('#ddlAccts').val(null).trigger('change');
                $('#BeneficiaryName').val(null).trigger('change');
                $('#BeneficiaryNo').val(null).trigger('change');
                $("#ddlAccts").select2({
                    theme: "bootstrap4",
                    placeholder: "Enter your account",
                    width: '100%',
                    data: response.results
                });
               
            });
      


    });

    $("#ddlAccts").on("select2:select", function (e) {
        var datas = e.params.data;
        
        $("#BeneficiaryName").val(datas.id);
        $("#BeneficiaryNo").val(datas.amount);          
        availableBalances = datas.availablebalance;
        var value = $.trim($('#AmountsCr').val()).replace(/,/g, "");
      
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
                transferAmounts = Number(value);
                transferBal = Number(availableBalances);
                transferBalances = Number(transferBal) + Number(transferAmounts);
                $('#BalancesCr').val(transferBalances);
                $('#AmountsCr').val();
            }
        }
        $.ajax({
            url: "loadProduct",
            data: { id: $('#ddlAccts').val() },
            type: "POST",
            cache: false
        }).then(function (response) {
            $("#ProductsCr").select2({
                theme: "bootstrap4",
                placeholder: "Select Product",
                width: '100%',
                data: response.results
            });     
            });
        $.ajax({
            url: "loadTransactionsType"           
        }).then(function (response) {
            $("#transTypeCr").select2({
                theme: "bootstrap4",
                placeholder: "Select Transaction Type",
                width: '100%',
                data: response.results
            });
        });
    });

   
});

$('#AmountsCr').on("mouseout", function () {
    
    var value = $.trim($('#AmountsCr').val()).replace(/,/g, "");
   valueDr = $.trim($('#Amounts').val()).replace(/,/g, "");
   totals = $('#TotalCredit').val();
    //var svalue = $.trim(availbalance).replace(/,/g, "");
    var svalue = balAccounts;
    if ($.isNumeric(value)) {
        if (Number(value) <= 0) {
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
            $('#ddlAccts').prop('disable', true);
        }
        totalDebit = Number(valueDr);
        transferAmounts = Number(value);
        transferBal = Number(availableBalances);
        transferBalances = Number(transferBal) + Number(transferAmounts);
        $('#BalancesCr').val(transferBalances);
        $('#AmountsCr').val(transferAmounts);         
        $('#TotalDebit').val(valueDr);
        //totals = Number(totalDebit) - TotalAmount();
       // $('#TotalCredit').val(totals);
    }
});
$(document).ready(function ($) {
    $(".btn-finish").on('click', function () {  
        
        initFormValidations();
        addMultipleTransfer();
    });
});

$(document).ready(function () { 
    initFormValidations();
    addMultipleTransfer();
    $(".modal").perfectScrollbar();
  
});

$('#tempTable').on('change', function () {
    
    TotalAmount();
});
$('#updateBtn').on('click', function () {

    updateMultiple();
});
$('#btnAddFinish').on('click', function () {

    SendMultiple();
});
//$('#btnAddFinish').on('click', function () {
//    
//    var tabl = $("#multipleTable").bootstrapTable("getSelections");    
//    var sumVal = 0;
//    for (var i = 0; i < tabl.length; i++) {
//        sumVal = sumVal + parseFloat(tabl[i].amount);
//    }
//    return alert(sumVal);
    
   
//});
function TotalAmount() {
    
    var tabl = $("#tempTable").bootstrapTable("getSelections");
    var sumVal = 0;
    for (var i = 0; i < tabl.length; i++) {
        sumVal = sumVal + parseFloat(tabl[i].amount);
    }
    alert(sumVal);
    $('#TotalCredit').val(sumVal);
    $('#TotalCredit').attr('disabled', 'disabled');
    $('#TotalDebit').attr('disabled', 'disabled');
    return sumVal;

}
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
    $("#multipleTransferCredit #multipleTransferDebit #personalPrimary #bank")
        .each(function () {
            $(this).validate({
        
                messages: {
                    Accountnumber: {
                        required: "Account number is required"

                    },
                    BeneficiaryNo: {
                        required: "Beneficiary No is required"
                    },
                    Amounts: { required: "Debit Amount is required" },
                    TransactionsType: { required: "Transaction Type is required" },
                    NarrationDr: { required: "Narration Dr is required" },
                    NarrationsCr: { required: "Narration Cr is required" },
                    Valuedate: { required: "Value date is required" },
                    availablebalance: { required: "availablebalance is required" },
                    Accountname: { required: "Account name is required" },
                    AmountsCr: { required: "Amount Cr is required" }
                }
            });
        });

    $("#multipleTransferCredit #multipleTransferDebit #personalPrimary #bank").validate({

        messages: {
            Accountnumber: {
                required: "Account number is required"
                
            },
            BeneficiaryNo: {
                required: "Beneficiary No is required"
            },
            Amounts: {required: "Debit Amount is required"},
            TransactionsType: { required: "Transaction Type is required" },
            NarrationDr: { required: "Narration Dr is required" },
            NarrationsCr: { required: "Narration Cr is required"},
            Valuedate: { required: "Value date is required"},
            availablebalance: { required: "availablebalance is required"},
            Accountname: { required: "Account name is required" },
            AmountsCr: { required: "Amount Cr is required" }
        }
    });
 
}

function openModal(e) {
    $('#wizardComponent').bootstrapWizard("resetWizard");
    CustomerType = e;
    switch (e.text.toLowerCase()) {
        case "individual":
        case "minor":
            $("#wizardComponent").bootstrapWizard("hide", 1);
            $("#wizardComponent").bootstrapWizard("hide", 5);
            $("#wizardComponent").bootstrapWizard("display", 0);
            $("#wizardComponent").bootstrapWizard("display", 4);

            // hide and disable institution type
            $("#wizardComponent #Institutiontypeid").attr("disabled", "true");
            $("#wizardComponent .institution-hide").hide();
            // handle minor and individual dob logic
            var datefield =
                $("#wizardComponent #frmPersonalPrimary input[name=Dateofbirth]");

            if (e.text.toLowerCase() === "minor") {
                datefield.data("DateTimePicker").maxDate(
                    moment()
                );
                datefield.data("DateTimePicker").minDate(
                    moment().subtract(18, "years")
                        .add(1, "days")
                );
            } else {
                datefield.data("DateTimePicker").minDate(
                    false
                );
                datefield.data("DateTimePicker").maxDate(
                    moment().subtract(18, "years")
                );
            }
            break;
        case "estate":
        case "joint":
            $('#wizardComponent').bootstrapWizard("show", 1);

            $("#wizardComponent").bootstrapWizard("hide", 0);
            $("#wizardComponent").bootstrapWizard("hide", 4);
            $("#wizardComponent").bootstrapWizard("hide", 5);
            $("#wizardComponent").bootstrapWizard("display", 1);

            $("#wizardComponent #Institutiontypeid").removeAttr("disabled");
            $("#wizardComponent .institution-hide").show();
            break;
        case "unincorporated":
        case "corporate":
            $('#wizardComponent').bootstrapWizard("show", 2);

            $("#wizardComponent").bootstrapWizard("hide", 0);
            $("#wizardComponent").bootstrapWizard("hide", 1);
            $("#wizardComponent").bootstrapWizard("hide", 4);
            $("#wizardComponent").bootstrapWizard("display", 5);

            $("#wizardComponent #Institutiontypeid").removeAttr("disabled");
            $("#wizardComponent .institution-hide").show();
            break;
    }

    // hide/disable or show/enable KYC tables/fields
    $("#kyc #frmKyc table").addClass("d-none");
    var table_name = "#KYC" + e.text.substr(0, 1).toUpperCase() + e.text.substr(1);
    if (!$(table_name).hasClass("empty")) {
        $(table_name).removeClass("d-none");
    }

    // add customer type to wizard header
    $("#wizardComponent #createCustType")
        .text(e.text);

    // set active wizard & show modal
    $('#wizardModal').modal('show');
    $("#updateWizard").removeClass("active").hide();
    $("#wizardComponent").addClass("active").show();
}
function clear() {
    $(".wizard-container form select").val(null)
        .trigger("select2:unselect")
        .trigger("change");
    $(".wizard-container form").trigger("reset");
}
function addMultipleTransfer() {
    // Add Wizard Initialization
    $('#wizardComponent').bootstrapWizard({
        'tabClass': 'nav nav-pills',
        'nextSelector': '.btn-next',
        'previousSelector': '.btn-previous',
        'finishSelector': '.btn-finish',
        'addSelector': '.btn-add',
        'btnSelector': '#tempTable',
        'finishTempSelector':'#btnAddFinish',

        onNext: function (tab, navigation, index) {
            var form = getTabForm(tab);
            if (!form.valid()) {
                return false;
            }
            return true;
        },
        onAdd: function (tab, navigation, index) {
            var form = getTabForm(tab);
            if (!form.valid()) {
                return false;
            }
            return true;
        },
        onInit: function (tab, navigation, index) {
            var $wizard = navigation.closest('.card-wizard');
            refreshAnimation($wizard, index);
        },

        onFinish: function (tab, navigation, index) {
            // Get and validate last form
            var form = getTabForm(tab);

            if (!form.valid()) {
                return false;
            }
            swal({
                title: "Are you sure?",
                text: "Transfer will be initiated",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#34D027",
                confirmButtonText: "Yes, continue",
                cancelButtonText: "No, stop!",
                cancelButtonColor: "#ff9800",
                showLoaderOnConfirm: true,
                preConfirm: function () {
                    return new Promise(function (resolve) {
                        setTimeout(function () {
                            resolve();
                        }, 1000);
                    });
                }
            }).then(function (isConfirm) {
                if (isConfirm) {
                    // freeze finish button
                    //$("#wizardComponent .btn-finish").attr("disabled", "true");
                    var form_data = [];
                    var tabsArray = $(navigation)
                        .find("li:not(.d-none)")
                        .map(function () {
                            return getTabForm(this);
                        });
                    tabsArray.each(function () {
                        form_data.push(this.serializeArray());
                    });
                    save(form_data);
                }
            }, function (isRejected) {
                return;
            });
     
        },

        onTabClick: function (tab, navigation, index) {
            return true;// false;
        },

        onTabShow: function (tab, navigation, index) {
            var navLength = $(navigation).find("li:visible").length;

            var wizard = navigation.closest('.card-wizard');
            wizard.find(".btn-next").removeClass("disabled");

            if (index + 1 === navLength && index !== -1) {
                $(wizard).find('.btn-next').hide();
                $(wizard).find('.btn-finish').show();
                $(wizard).find('.btn-add').show();
                $('#btnAddFinish').show();
                $('#tempTable').show();
               
            } else {
                $(wizard).find('.btn-next').show();
                $('#btnAddFinish').hide();
                $('#tempTable').hide();
                $(wizard).find('.btn-add').hide();
                $(wizard).find('.btn-finish').hide();
               
            }

            refreshAnimation(wizard, index);
        }
    });

    function save(form) {
        // flatten form arrays
        form = $.map(form, function (arr) {
            return arr;
        });
        //remove empty stringed entries
        form = $.map(form, function (n) {
            if ($.trim(n.value).length !== 0)
                return n;
            return null;
        });
        // Flattening further...
        var transactionModel = {};
        $.each(form, function (index, item) {
            transactionModel[item.name] = item.value;
        });
        var signatureimgData = new FormData();
       
        transactionModel.AccountDr = $('#Accountnumber').val();
        transactionModel.AccountCr = $('#BeneficiaryNo').val();
        transactionModel.Amount = $('#Amounts').val();
        transactionModel.TransactionType = $('#ddlChargeType').val(); 
        transactionModel.NarrationDr = $('#NarrationDr').val();
        transactionModel.NarrationCr = $('#NarrationsCr').val();    
        transactionModel.PostDate = $('#Valuedate').val();
        transactionModel.BatchName = $('#BatchName').val();
        transactionModel.availablebalance = $('#availablebalance').val();
        transactionModel.Accountname = $('#Accountname').val();
        transactionModel.AccountNo = $('#Accountnumber').val();
        transactionModel.AmountCr = $('#AmountsCr').val();  
        transactionModel.ValueDate = $('#Valuedate').val();       
        transactionModel.availablebalanceCr = $('#BalancesCr').val();
        transactionModel.transTypeCr = $('#transTypeCr').val();
        transactionModel.ProductsCr = $('#ProductsCr').val();
        transactionModel.TotalDebit = $('#Amounts').val();
        transactionModel.TotalCredit = $('#TotalCredit').val();
        var TotalResult = $('#Amounts').val();
        
        //var transactionModel = JSON.stringify(form_data);
        if (parseInt(transactionModel.AmountCr) <= parseInt(transactionModel.Amount)) {            
            $.ajax({
                url: '/FundTransfer/TempTransfer',
                data: JSON.stringify( transactionModel ),
                type: 'POST',                
                contentType: 'application/json',
                success: function (result) {             
                  
                    if (result.message !== " ") {
                        swal({ title: 'Initiate new transfer', text: 'Something went wrong: ' + result.message.toString(), type: 'error' }).then(function () { clear(); });

                        $('#tempTable').bootstrapTable('refresh', {
                            silent: true
                        });
                        $('#wizardModal').modal('show');
                        $("#wizardComponent .btn-finish").attr("enabled", "true");
                    }
                    else {
                        swal({ title: 'Initiate new transfer', text: 'New transaction added successfully!', type: 'success' }).then(function () { });

                        $('#tempTable').bootstrapTable('refresh', {
                            silent: true
                        });
                        $('#wizardModal').modal('show');
                        $("#wizardComponent .btn-finish").attr("enabled", "true");
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
                        
                        $('#tempTable').bootstrapTable('refresh', {
                            silent: true
                        });
                        $('#wizardModal').modal('show');
                        //$("#wizardComponent .btn-finish").removeAttr("disabled");
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
                    $("#wizardComponent .btn-finish").removeAttr("disabled");
                });
            });
        } else {
            alert("Credit must not be greater than debit");
        }

    }

    function getTabForm(tab) {
        var nav_id = $(tab).find("a.nav-link").attr("href");
        var wizard = $("#" + $(tab).closest('.card-wizard').attr("id"));
        var tabpane = wizard.find("div" + nav_id + ".tab-pane");
        return tabpane.find("form");
    }

    $(window).resize(function () {
        $('.card-wizard').each(function () {
            $wizard = $(this);

            index = $wizard.bootstrapWizard('currentIndex');
            refreshAnimation($wizard, index);
        });
    });

    function refreshAnimation($wizard, index) {
        $total = $wizard.find('.nav li').length;
        $li_width = 100 / $total;

        total_steps = $wizard.find('.nav li').length;
        move_distance = $wizard.width() / total_steps;
        index_temp = index;
        vertical_level = 0;

        mobile_device = $(document).width() < 600 && $total > 3;

        if (mobile_device) {
            move_distance = $wizard.width() / 2;
            index_temp = index % 2;
            $li_width = 50;
        }

        $wizard.find('.nav li').css('width', $li_width + '%');

        step_width = move_distance;
        move_distance = move_distance * index_temp;

        $current = index + 1;

        if (mobile_device) {
            vertical_level = parseInt(index / 2);
            vertical_level = vertical_level * 38;
        }
    }
}
function SendMultiple() {
    
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
                        // freeze finish button
                        //$("#wizardComponent .btn-finish").attr("enabled", "true");
                        
                        var MultipleData = [];
                
                        //var tabsArray = $(navigation)
                        //    .find("li:not(.d-none)")
                        //    .map(function () {
                        //        return getTabForm(this);
                        //    });
                        MultipleData = $multipleTransfertable.bootstrapTable('getAllSelections');  
                            MultipleData.push();
                      
                        saveMultiple(MultipleData);
                    }
                }, function (isRejected) {
                    return;
                    
            });

}
function saveMultiple(form) {
    // flatten form arrays
    
    // flatten form arrays
    form = $.map(form, function (arr) {
        return arr;
    });
    //remove empty stringed entries
    form = $.map(form, function (n) {
        if ($.trim(n.value).length !== 0)
            return n;
        return null;
    });
    // Flattening further...
    var TotalResult = {};
    $.each(form, function (index, item) {
        TotalResult[item.name] = item.value;
    });
    TotalResult.TotalCredit = $('#TotalCredit').val();
    TotalResult.TotalDebit = $('#TotalDebit').val();
    // Flattening further...
    var MultipleData = {};
    $.each(form, function (index, item) {
        MultipleData[item.name] = item.reference;
    });
    MultipleData = $multipleTransfertable.bootstrapTable('getAllSelections');
    $.each(form, function (index, multipleTransferItemData) {
        MultipleData[multipleTransferItemData.reference] = multipleTransferItemData.value;
    });
    //var TotalResult = {
    //    TotalDebit: $('#TotalDebit').val(),
    //    TotalCredit: $('#TotalCredit').val()
    //};
    if (parseInt(TotalResult.TotalCredit) === parseInt(TotalResult.TotalDebit)) {
        $.ajax("/Fundtransfer/MultipleTransfers", {
            data: JSON.stringify(MultipleData, TotalResult),
            method: "POST",
            contentType: "application/json",
            success: function (result) {
                
                if (result.message !== " ") {
                    swal({ title: 'Initiate new transfer', text: 'Something went wrong: ' + result.message.toString(), type: 'error' }).then(function () { clear(); });

                    $('#tempTable').bootstrapTable('refresh', {
                        silent: true
                    });
                    $('#wizardModal').modal('show');
                }
                else {
                    swal({ title: 'Initiate new transfer', text: 'New transaction added successfully!', type: 'success' }).then(function () { });

                    $('#tempTable').bootstrapTable('refresh', {
                        silent: true
                    });
                    $('#wizardModal').modal('show');
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
    } else {
        alert("The total credit must be equal total debit");
    }

}
function updateMultiple(form) {
    // flatten form arrays
    
        form = $.map(form, function (arr) {
            return arr;
        });
        //remove empty stringed entries
        form = $.map(form, function (n) {
            if ($.trim(n.value).length !== 0)
                return n;
            return null;
        });
        // Flattening further...
        var json_data = {};
        $.each(form, function (index, item) {
            json_data[item.name] = item.value;
        });
        json_data.Id = $('#Id').val();
        json_data.Reference = $('#sessionId').val();
        json_data.Amount = $('#AmountsUpCr').val();
        json_data.acctNo = $('#acctNo').val();
        sessionId = $('#sessionId').val();      
            $.ajax("/Fundtransfer/UpdateMultiple", {
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




