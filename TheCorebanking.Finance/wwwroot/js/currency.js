var url_path = window.location.pathname;
if (url_path.charAt(url_path.length - 1) == '/') {
    url_path = url_path.slice(0, url_path.length - 1);
}

$(document).ready(function ($) {
    $("#ddlfrom").mouseout(function (event) {
        debugger
        var descri = $('#ddlfrom').val();
        if (descri == '') {
            $.notify({ icon: "add_alert", message: 'Your base currency is empty' }, { type: 'danger', timer: 1000 });
            $(this).css('border-color', 'red');
            $('#ddlfrom').focus();
            return false;
        } else {
            $(this).css('border-color', 'green');
        }

    });
});
$(document).ready(function ($) {
    $("#amount").mouseout(function (event) {
        debugger
        var amount = $('#amount').val();
        if (amount == '') {
            $.notify({ icon: "add_alert", message: 'Your amount is empty' }, { type: 'danger', timer: 1000 });
            $(this).css('border-color', 'red');
            $('#amount').focus();
            return false;
        } else {
            $(this).css('border-color', 'green');
        }

    });
});
$(document).ready(function ($) {
    $("#ddlto").mouseout(function (event) {
        debugger
        var to = $('#ddlto').val();
        if (to == '') {
            $.notify({ icon: "add_alert", message: 'Your to currency is empty' }, { type: 'danger', timer: 1000 });
            $(this).css('border-color', 'red');
            $('#ddlto').focus();
            return false;
        } else {
            $(this).css('border-color', 'green');
        }

    });
});

function currencyFormatter(value, row, index) {
    return [
        //'<a style="color:white"  class="edit btn btn-sm btn-warning mr-2"  title="Edit Currency">'
        //+ '<i class="now-ui-icons ui-2_settings-90"></i>' +
        '<a style="color:white"  title="Remove currency" class="remove btn btn-sm btn-danger">'
        +'<i class="now-ui-icons ui-1_simple-remove"></i></a>'+
        '</a> '
    ].join('');
}

window.currencyEvents = {
    'click .edit': function (e, value, row, index) {
       
        if (row.state = true) {
          
            var data = JSON.stringify(row);         
            $('#CurrCode').val(row.currCode);
            $('#ddlCurrency').val(row.currName).trigger('change');
            $('#CurrSymbol').val(row.currSymbol);
            $('#ExchangeRate').val(row.exchangeRate);
            $('#CountryCode').val(row.countryCode);
            $('#AddNewCurrency').modal('show'); 
            $('#btnCurrencyUpdate').html('  <i class="now-ui-icons ui-1_check"></i> Update Record');
            $('#btnCurrencyUpdate').show();
            $('#btnCurrency').hide();
        }
    },
    'click .remove': function (e, value, row, index) {
        info = JSON.stringify(row);
        console.log(info);
        var reg = {
          
            Code:$('#CurrCode').val(),
            Name:$('#CurrName').val(),
            Symbol:$('#CurrSymbol').val(),
            Rate:$('#ExchangeRate').val(),
           Country: $('#CountryCode').val()
        }
        debugger
        $('#ID').val(row.id);
        $.ajax({
            url: url_path +'/financesetup/RemoveCurrency',
            type: 'POST',
            data: { CurrCode: row.currCode},
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
                        $('#currencyTable').
                            bootstrapTable(
                                'refresh', { url: url_path +'/financesetup/listcurrency' });

                        //return false;
                    }
                    else {
                        swal("Currency", "You cancelled add bank.", "error");
                    }
                     $('#currencyTable').
                            bootstrapTable(
                                'refresh', { url: url_path +'/financesetup/listcurrency' });
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

    $('#btnUploadCurrency').on('click', function () {
        uploadCurrency()
    });
});

function uploadCurrency() {
    $.ajax({
        type: 'POST',
        url: url_path +'/financesetup/CurrencyUpload',
        success: function (result) {

            if (result.toString != '' && result != null) {
                swal({ title: 'Upload Curency', text: 'Currency uploaded successfully!', type: 'success' }).then(function () { clear(); });

                $('#currencyTable').
                    bootstrapTable(
                    'refresh', { url: url_path +'/financesetup/listcurrency' });

            }
            else {
                swal({ title: 'Upload Currency', text: 'Something went wrong: </br>' + result.toString(), type: 'error' }).then(function () { clear(); });

            }
        },
    })

}




function updateCurrency() {
    debugger
   
    var json_data = {};

    json_data.CurrCode = $('#CurrCode').val();
    json_data.CurrName = $('#CurrName').val();
    json_data.CurrSymbol = $('#ddlCurrency').val();
    json_data.ExchangeRate = $('#ExchangeRate').val();
    json_data.CountryCode = $('#CountryCode').val();

    $("input[type=submit]").attr("disabled", "disabled");  

    $('#frmCurrency').validate({

        
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
                text: "Currency will be updated!",
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
                    $("#btnCurrencyUpdate").attr("disabled", "disabled");
                    debugger
                    $.ajax({
                        url: url_path +'/financesetup/EditCurrency',
                        type: 'POST',
                        data: json_data,
                        dataType: "json",
                        //headers: {
                        //    'VerificationToken': forgeryId
                        //},
                        success: function (result) {
                            
                            if (result.toString != '' && result != null) {
                                swal({ title: 'Update Currency', text: 'Currency updated successfully!', type: 'success' }).then(function () { window.location.reload(true); });
                                $('#AddNewCurrency').modal('hide');
                                $('#currencyTable').
                                    bootstrapTable(
                                        'refresh', { url: url_path +'/financesetup/listcurrency' });

                                $("#btnCurrencyUpdate").removeAttr("disabled");
                            }
                            else {
                                swal({ title: 'Update Currency', text: 'Something went wrong: </br>' + result.toString(), type: 'error' }).then(function () { clear(); });
                                $("#btnCurrencyUpdate").removeAttr("disabled");
                            }
                        },
                        error: function (e) {
                            swal({ title: 'Update Currency', text: 'Currency update encountered an error', type: 'error' }).then(function () { clear(); });
                            $("#btnCurrencyUpdate").removeAttr("disabled");
                        }
                    });
                }
            });
        }

    },
        function (dismiss) {
            swal('Update Currency', 'You cancelled currency update.', 'error');
            $("#btnCurrencyUpdate").removeAttr("disabled");
        });

}



$(document).ready(function ($) {

    $('#btnCurrency').on('click', function () {
        debugger      
       
            addCurrency();   
 
    });

});
function addCurrency() {
    debugger
    $('#btnCurrencyUpdate').hide();

  

    $("input[type=submit]").attr("disabled", "disabled");
    $('#frmCurrency').validate({
        messages: {
           
            CurrName: { required: "Currency Name is required" },
            //CurrSymbol: { required: "Currency Symbol is required" },
            //ExchangeRate: { required: "Exchange Rate is required" },
            //CountryCode: { required: "Country Code is required" }
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
                text: "Currency will be added!",
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
                    $("#btnCurrency").attr("disabled", "disabled");
                    debugger
                    var json_data = {};
                    json_data.CurrName = $('#ddlfrom').val();
                    json_data.amount = $('#amount').val();                    
                    json_data.CurrSymbol = $('#ddlto').val();
                    $.ajax({
                        url: url_path +'/financesetup/AddCurrency',
                        type: 'POST',
                        data: json_data,
                        dataType: "json",                      
                        success: function (result) {
                            
                            if (result.toString != '' && result != null) {
                                swal({ title: 'Add Currency', text: 'Currency added successfully!', type: 'success' }).then(function () { window.location.reload(true); });
                                $.notify({ icon: "add_alert", message: ' Your exchange rate: ' + result.toString() }, { type: 'success' });   
                                $('#AddNewCurrency').modal('hide');
                                $('#currencyTable').
                                    bootstrapTable(
                                        'refresh', { url: url_path +'/financesetup/listcurrency' });
                                $("#btnCurrency").removeAttr("disabled");
                            }
                            else {
                                swal({ title: 'Add Currency', text: 'Something went wrong: </br>' + result.toString(), type: 'error' }).then(function () { clear(); });
                                $("#btnCurrency").removeAttr("disabled");
                            }
                        },
                        error: function (e) {
                            swal({ title: 'Add Currency', text: 'Adding currency encountered an error', type: 'error' }).then(function () { clear(); });
                            $("#btnCurrency").removeAttr("disabled");
                        }
                    });
                }
            });
        }

    },
        function (dismiss) {
            swal('Add Currency', 'You cancelled add currency.', 'error');
            $("#btnCurrency").removeAttr("disabled");
        });

}

$(document).ready(function ($) {
    $('#btnCurrencyUpdate').hide();
    $('#btnCurrency').show();
    $('#btnCurrencyUpdate').on('click', function () {
        debugger
        updateCurrency();
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

$('#currencyTable').on('expand-row.bs.table', function (e, index, row, $detail) {
    $detail.html('Loading request...');

    var htmlData = '';
    var header = '<div>';
    var footer = '</div>';
    htmlData = htmlData + header;

    debugger

    var html =
        '<h8>' +
        '<p style="text-align:left">' +
   
        '<strong> Currency Code:</strong> &nbsp' + row.currCode + '' + '<p>' +
        '<strong> Currency Name:</strong> &nbsp' + row.currName + '' + '<p>' +
        '<strong> Currency Symbol:</strong> &nbsp' + row.currSymbol + '' + '<p>' +
        '<strong> Exchange Rate:</strong> &nbsp' + row.exchangeRate + '' + '<p>' +
        '<strong> Country Code:</strong> &nbsp' + row.countryCode + '';
      
    htmlData = htmlData + html;
    htmlData = htmlData + footer;
    $detail.html(htmlData);
});

$(document).ready(function ($) {
    $('#btnCurrent').click(function (d) {
        debugger
        var errormsg = "";
        var amount = $('#Amount').val();
        var from = $('#currencyFrom').val();
        var to = $('#currencyTo').val();
        $.ajax({
            async:false,
            type: "POST",
            url: "http://localhost/financeconverter/CurrencyService.asmx/ConvertCurrency",
            data: "{amount:" + amount + ",fromCurrency:'" + from + "',toCurrency:'" + to + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            beforeSend: function () {
                $('#results').html("Converting...");
            },
            success: function (data) {
                $('#results').html(amount + ' ' + from + '=' + data.d.toFixed(2) + ' ' + to);
                //$('#results').html(amount);
            },
            error: function (jqXHR, exception) {
                if (jqXHR.status === 0) {
                    errormsg = 'Not connect.\n Verify Network.';;
                } else if (jqXHR.status == 404) {
                    errormsg = 'Requested page not found. [404]';;
                } else if (jqXHR.status == 500) {
                    errormsg = 'Internal Server Error [500].';;
                } else if (exception === 'parsererror') {
                    errormsg = 'Requested JSON parse failed.';;
                } else if (exception === 'timeout') {
                    errormsg = 'Time out error.';;
                } else if (exception === 'abort') {
                    errormsg = 'Ajax request aborted.';;
                } else {
                    errormsg = 'Uncaught Error.';
                }
                $('#results').html(errormsg);
                $('<a href="#" >Click here for more details</a>').click(function () {
                    alert(jqXHR.responseText);
                }).appendTo('#results');
            }
        });
    });
});

$(document).ready(function ($) {
    debugger
    //for dropdown list control for Comapny (id=ddlCompany)starts here

    $("#ddlCurrencys").select2({
        theme: "bootstrap4",
        placeholder: "Loading...",

    });

    $.ajax({
        url: "FinanceSetup/loadCurrency",
    }).then(function (response) {
        debugger
        $("#ddlCurrencys").select2({
            theme: "bootstrap4",
            // placeholder: "Select Company...",  
            width: '100%',
            data: response.results
        });
    });
});

$(document).ready(function ($) {
    $('#ddlCurrencys').select2({
        theme: "bootstrap4",
        placeholder: "Click here to choose your base currency"
    });
    $.ajax({
        url: "../FinanceSetup/loadCurrencyAPI",
    }).then(function (response) {
        debugger
        $('#ddlCurrencys').select2({
            theme: "bootstrap4",
            placeholder: "Select base currency",
            width: "100%",
            data: response.result
        });
        });
    $('#ddlCurrencys').on("select2:select", function (e) {
        debugger
        var datas = e.params.data;
        //var to = new Option(datas.text, datas.id, true, true);
        //var to = new Option(datas.cate, datas.catid, true, true);
        //var amount = new Option(datas.amount, datas.id, true, true);
        //$("#ddlCurrency").val(datas.id);
        //$("#ddlto").append(to).trigger('change');
        $("#ddlRate").val(datas.id);
        //$('#ddlAmount').append(amount).trigger('change');
       

        //$("#ddlCurrency").val(e.params.data.id);
        //$("#ddlto").val(e.params.data.text);
        //$("#ddlAmount").val(e.params.data.amount);
        
    })
});



