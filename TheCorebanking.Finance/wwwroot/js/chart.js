var url_path = window.location.pathname;
if (url_path.charAt(url_path.length - 1) == '/') {
    url_path = url_path.slice(0, url_path.length - 1);
}

var accountChrt = [];
$("#chartTable").on('change', function (e) {
    e.preventDefault();
    var ID = $(this).val();

    $('#detailsList').bootstrapTable('refresh', { url: '../chart/listchart/?ID=' + ID });

});
function UpdateFormatter(value, row, index) {
    return [
        '<div class="form-group text-right dropdown">'
        + '<button class="btn btn-danger btn-sm" id="chartupdate" data-toggle="dropdown" type="button" aria-haspopup="true" aria-expanded="false">'
        + '<i class="now-ui-icons design_bullet-list-67"></i> </button>'
        + '<div class="dropdown-menu">'
        + '<h6 class="dropdown-header">Choose account name update</h6>'
        + '<a class="dropdown-item accountupdate" id="1" data-toggle="modal" data-target="#UpdateNewChart">Chart of Account Update</a>'
        + '</div>'
        + '</div>'
    ].join('');
}

function chartFormatter(value, row, index) {
    return [
        //'<a style="color:white"  class="edits btn btn-sm btn-warning mr-2"  title="Edit Chart">'
        //+ '<i class="now-ui-icons ui-2_settings-90"></i>' +
        '<a style="color:white"  title="Remove chart" class="remove btn btn-sm btn-danger">'
        + '<i class="fas fa-trash"></i></a>' +
        '</a> '
    ].join('');
}
window.chartsEvents = {
    'click .edits': function (e, value, row, index) {
        var form = $("#frmcharts");
        form.trigger("reset");
        if (row.state = true) {
            var data = JSON.stringify(row);
            form.find("[name=Id]").val(row.id);
            //var CustName = $("#ddlAcctGroup").select2("data")[0].text;          
            form.find("[name=AccountId]").val(row.accountId).trigger('change');
            form.find("[name=ddlAName]").val(row.accountName).trigger('change');
            form.find("[name=ddlAccount]").val(row.accountTypeId).trigger('change');
            form.find("[name=ddlAcctGroup]").val(row.accountGroupId).trigger('change');
            form.find("[name=ddlAcctCategory]").val(row.accountCategoryId).trigger('change');
            $('#UpdateNewChart').modal('show');
            $('#btnChartUpdate').html('  <i class="now-ui-icons ui-1_check"></i> Update Record');
            $('#btnChart').hide();
            $('#btnChartUpdate').show();
        }
    },

};


window.chartEvents = {
    'click .edit': function (e, value, row, index) {
        var form = $("#frmchart");
        form.trigger("reset");
        if (row.state = true) {
            var data = JSON.stringify(row);
            form.find("[name=Id]").val(row.id);
            //var CustName = $("#ddlAcctGroup").select2("data")[0].text;  

            form.find("[name=ddlAccount]").val(row.accountTypeId).trigger('change');
            form.find("[name=AccountId]").val(row.accountId).trigger('change');
            form.find("[name=AccountName]").val(row.accountName).trigger('change');
            form.find("[name=ddlBranch]").val(row.brId).trigger('change');
            form.find("[name=BrSpecific]").val(row.brSpecific);
            form.find("[name=ddlAcctGroup]").val(row.accountGroupId).trigger('change');
            form.find("[name=ddlAcctCategory]").val(row.accountCategoryId).trigger('change');
            form.find("[name=ddlCurrency]").val(row.currCode).trigger('change');
            form.find("[name=ddlCost]").val(row.costcode).trigger('change');
            form.find("[name=ddlStatus]").val(row.accountStatus).trigger('change');
            $('#AddNewChart').modal('show');
            $('#btnChartUpdate').html('  <i class="now-ui-icons ui-1_check"></i> Update Record');
            $('#btnChart').hide();
            $('#btnChartUpdate').show();
        }
    },
    'click .remove': function (e, value, row, index) {
        info = JSON.stringify(row);
        console.log(info);

        debugger
        $('#ID').val(row.Id);
        $.ajax({
            url: 'chart/RemoveChart',
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
                        $('#chartTable').
                            bootstrapTable(
                                'refresh', { url: 'chart/listchart' });

                        //return false;
                    }
                    else {
                        swal("Chart", "You cancelled delete chart.", "error");
                    }
                    $('#chartTable').
                        bootstrapTable(
                            'refresh', { url: 'chart/listchart' });
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
    $('#btnChart').hide();
    $('#btnChartUpdate').show();
    $('#btnChartUpdate').on('click', function () {
        updateChart();

    });

});
$(document).ready(function ($) {
    $('#btnUploadChart').on('click', function () {
        uploadChart();
    });


});
$(document).ready(function ($) {

    $('#btnChart').show();
    $('#btnChartUpdate').hide();
    $('#btnChart').on('click', function () {
        debugger
        addChart();


    });

});
function uploadChart() {
    $.ajax({
        type: 'POST',
        url: 'chart/ChartUpload',
        success: function (result) {

            if (result.toString != '' && result != null) {
                swal({ title: 'Upload Chart', text: 'Chart uploaded successfully!', type: 'success' }).then(function () { clear(); });

                $('#chartTable').
                    bootstrapTable(
                        'refresh', { url: 'chart/listchart' });


            }
            else {
                swal({ title: 'Upload Chart', text: 'Something went wrong: </br>' + result.toString(), type: 'error' }).then(function () { clear(); });

            }
        },
    })

}


function addChart() {
    debugger



    $('#btnChartUpdate').hide();
    $("input[type=submit]").attr("disabled", "disabled");
    $('#frmchart').validate({
        messages: {
            AccountName: { required: "AccountName is required" },
            CoyId: { required: "Company is required" },
            CurrCode: { required: "Currency is required" },


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
        submitHandler: function (e, form) {
            swal({
                title: "Are you sure?",
                text: "Chart of account will be added!",
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
                    $("#btnChart").attr("disabled", "disabled");

                    debugger
                    var json_data = {};
                    json_data.Id = $('#Id').val();

                    //var CustName = $("#ddlAcctGroup").select2("data")[0].text;                    
                    json_data.AccountTypeId = $('#ddlAccount').val();
                    json_data.AccountName = $('#AccountName').val();
                    json_data.AccountId = $('#AccountId').val();
                    json_data.BrId = $('#ddlBranch').val();
                    json_data.BrSpecific = $('#BrSpecific').val();
                    json_data.AccountGroupId = $('#ddlAcctGroup').val();
                    json_data.AccountCategoryId = $('#ddlAcctCategory').val();
                    json_data.CurrCode = $('#ddlCurrency').val();
                    json_data.AccountStatus = $('#ddlStatus').val();
                    json_data.CostCode = $('#ddlCost').val();

                    $.ajax({
                        url: 'chart/AddChart',
                        type: 'POST',
                        data: json_data,
                        dataType: "json",
                        success: function (result) {
                            if (result.message == " ") {
                                swal({ title: 'Add Chart', text: 'Chart added successfully!', type: 'success' }).then(function () { window.location.reload(true); });
                                $('#AddNewChart').modal('hide');
                                $('#chartTable').
                                    bootstrapTable(
                                        'refresh', { url: 'chart/listchart' });

                                $("#btnChart").removeAttr("disabled");
                            }
                            else {
                                swal({ title: 'Add Chart', text: 'Something went wrong: </br>' + result.message, type: 'error' }).then(function () { clear(); });
                                $("#btnChart").removeAttr("disabled");
                            }
                        },
                        error: function (e) {
                            swal({ title: 'Add Chart', text: 'Adding chart encountered an error', type: 'error' }).then(function () { clear(); });
                            $("#btnChart").removeAttr("disabled");
                        }
                    });
                }
            });
        }

    },
        function (dismiss) {
            swal('Add Chart', 'You cancelled add chart.', 'error');
            $("#btnChart").removeAttr("disabled");
        });

}
function reloadpage() {
    location.reload();
}

function clear() {
    $('#Id').val('');
    $('#Designation').val('');
}

function updateChart() {
    debugger

    $("input[type=submit]").attr("disabled", "disabled");
    $('#frmchart').validate({
        //messages: {
        //    Designation: { required: "Chaart nation is required" },

        //},
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
                text: "Chart will be updated!",
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
                    var form = $("#frmchart");
                    form.find("#btnChartUpdate").attr("disabled", "disabled");
                    debugger
                    var data = {
                        Id: form.find("#Id").val(),
                        AccountTypeId: form.find('#ddlAccount').val(),
                        AccountName: form.find('#AccountName').val(),
                        AccountId: form.find('#AccountId').val(),
                        BrId: form.find('#ddlBranch').val(),
                        BrSpecific: form.find('#BrSpecific').val(),
                        AccountGroupId: form.find('#ddlAcctGroup').val(),
                        AccountCategoryId: form.find('#ddlAcctCategory').val(),
                        CurrCode: form.find('#ddlCurrency').val(),
                        AccountStatus: form.find('#ddlStatus').val(),
                        CostCode: form.find('#ddlCost').val()
                    }
                    $.ajax({
                        url: 'chart/UpdateChart',
                        type: 'POST',
                        data: data,
                        dataType: "json",

                        success: function (result) {

                            if (result.toString != '' && result != null) {
                                swal({ title: 'Update Chart', text: 'Chart updated successfully!', type: 'success' }).then(function () { window.location.reload(true); });
                                $('#AddNewChart').modal('hide');
                                $('#chartTable').
                                    bootstrapTable(
                                        'refresh', { url: 'chart/listchart' });

                                $("#btnChartUpdate").removeAttr("disabled");
                            }
                            else {
                                swal({ title: 'Update Chart', text: 'Something went wrong: </br>' + result.toString(), type: 'error' }).then(function () { clear(); });
                                $("#btnChartUpdate").removeAttr("disabled");
                            }
                        },
                        error: function (e) {
                            swal({ title: 'Update Chart', text: 'Update chart encountered an error', type: 'error' }).then(function () { clear(); });
                            $("#btnChartUpdate").removeAttr("disabled");
                        }
                    });
                }
            });
        }

    },
        function (dismiss) {
            swal('Update Chart', 'You cancelled chart update.', 'error');
            $("#btnChartUpdate").removeAttr("disabled");
        });

}
$(document).ready(function () {

    $("#ddlBranch").select2({
        theme: "bootstrap4",
        placeholder: "Loading..."
    });
    $.ajax({
        url: "Chart/loadBranch",
    }).then(function (response) {

        $("#ddlBranch").select2({
            theme: "bootstrap4",
            placeholder: "Select Branch",
            data: response.results
        });
    });
});
//$(document).ready(function () {

//    $("#ddlCompany").select2({
//        theme: "bootstrap4",
//        placeholder: "Loading..."
//    });
//    $.ajax({
//        url: "/Chart/loadCompany",
//    }).then(function (response) {

//        $("#ddlCompany").select2({
//            theme: "bootstrap4",
//            placeholder: "Select Company",
//            data: response.results
//        });
//    });


//    $('#ddlCompany').on('change', function () {
//        debugger
//        $("#ddlBranch").select2({
//            theme: "bootstrap4",
//            width: '100%',
//            placeholder: "Loading..."
//        });
//        $.ajax({
//            url: "/Chart/loadBranch",
//            data: { CoyId: $('#ddlCompany').val() },
//            type: "POST"
//        }).then(function (response) {
//            $("#ddlBranch").empty().trigger('change');
//            $("#ddlBranch").select2({
//                theme: "bootstrap4",
//                placeholder: "Select Branch",
//                width: '100%',
//                data: response.results
//            });
//        });
//    });


//});
//New chart Account
$("#ddlProduct").select2({
    theme: "bootstrap4",
    placeholder: "Loading..."
});
$.ajax({
    url: "chart/loadProduct"
}).then(function (response) {

    $("#ddlProduct").select2({
        theme: "bootstrap4",
        placeholder: "Select Product",
        width: '100%',
        data: response.results
    });
});
$("#acctType").select2({
    theme: "bootstrap4",
    placeholder: "Loading..."
});
$.ajax({
    url: "chart/loadCategoryNewChart"
}).then(function (response) {

    $("#acctType").select2({
        theme: "bootstrap4",
        placeholder: "Select Account Type",
        width: '100%',
        data: response.results
    });
});
$(document).ready(function ($) {
    debugger
    //for dropdown list control for Comapny (id=ddlCompany)starts here

    $("#ddlCurrencyNew").select2({
        theme: "bootstrap4",
        placeholder: "Loading...",

    });

    $.ajax({
        url: "Chart/loadCurrency",
    }).then(function (response) {
        debugger
        $("#ddlCurrencyNew").select2({
            theme: "bootstrap4",
            // placeholder: "Select Company...",  
            width: '100%',
            data: response.results
        });
    });

    $("#GLLevel").select2({
        theme: "bootstrap4",
        placeholder: "Loading...",

    });

    $.ajax({
        url: "Chart/loadGLLevel",
    }).then(function (response) {
        debugger
        $("#GLLevel").select2({
            theme: "bootstrap4",
            // placeholder: "Select Company...",  
            width: '100%',
            data: response.results
        });
    });
    $('#GLLevel').on('change', function (e) {

        var Id = $('#GLLevel').val();
        if (Id == 1) {

            $("#isGL").removeAttr("disabled", true);
            $("#ParentGL").attr("disabled", "disabled");
            $("#ParentGL").val(null).trigger('change');
        }
        else {


            $("#isGL").attr("disabled", "disabled");
            $("#isGL").prop("checked", false);
            $("#ParentGL").removeAttr("disabled", true);
            $("#ParentGL").select2({
                theme: "bootstrap4",
                placeholder: "Loading...",

            });

            $.ajax({
                url: "Chart/loadNewGL",
            }).then(function (response) {
                debugger
                $("#ParentGL").select2({
                    theme: "bootstrap4",
                    // placeholder: "Select Company...",  
                    width: '100%',
                    data: response.results
                });
            });
        }
    });
});
$('#btnNewChart').on('click', function () {
    $('#btnNewChart').show();
    $('#btnNewChartUpdate').hide();
    debugger
    addNewChart();

});
function addNewChart() {
    debugger



    $('#btnNewChartUpdate').hide();
    $("input[type=submit]").attr("disabled", "disabled");
    $('#frmnewchart').validate({
        messages: {
            AccountName: { required: "AccountName is required" },
            CoyId: { required: "Company is required" },
            AccountId: { required: "Currency is required" },


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
        submitHandler: function (e, form) {
            swal({
                title: "Are you sure?",
                text: "Chart of account will be created!",
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
                    $("#btnnewChart").attr("disabled", "disabled");

                    debugger
                    var json_data = {};
                    json_data.Id = $('#Id').val();


                    json_data.AccountTypeId = $('#acctType').val();
                    json_data.AccountName = $('#AccountsName').val();
                    json_data.AccountId = $('#AccountId').val();
                    json_data.BrId = $('#ddlBranch').val();
                    json_data.BrSpecific = $('#BrSpecific').val();
                    json_data.ProductId = $('#ddlProduct').val();
                    json_data.LevelId = $('#GLLevel').val();
                    json_data.CurrCode = $('#ddlCurrencyNew').val();
                    json_data.AccountStatus = $('#ddlStatus').val();
                    json_data.IsParentGl = $('#isGL').prop("checked");
                    json_data.ParentGlid = $('#ParentGL').val();
                    $.ajax({
                        url: 'chart/CreateChartOfAccount',
                        type: 'POST',
                        data: json_data,
                        dataType: "json",
                        success: function (result) {
                            if (result.message == " ") {
                                swal({ title: 'Add Chart', text: 'Chart added successfully!', type: 'success' }).then(function () { clear(); });
                                $('#AddNewsChart').modal('hide');
                                $('#newchartTable').
                                    bootstrapTable(
                                        'refresh', { url: 'chart/listnewchart' });

                                $("#btnNewChart").removeAttr("disabled");
                            }
                            else {
                                swal({ title: 'Add Chart', text: 'Something went wrong: </br>' + result.message, type: 'error' }).then(function () { clear(); });
                                $("#btnNewChart").removeAttr("disabled");
                            }
                        },
                        error: function (e) {
                            swal({ title: 'Add Chart', text: 'Adding chart encountered an error', type: 'error' }).then(function () { clear(); });
                            $("#btnNewChart").removeAttr("disabled");
                        }
                    });
                }
            });
        }

    },
        function (dismiss) {
            swal('Add Chart', 'You cancelled add chart.', 'error');
            $("#btnChart").removeAttr("disabled");
        });

}

//End
$("#ddlAccount").select2({
    theme: "bootstrap4",
    placeholder: "Loading...",

});
$.ajax({
    url: "Chart/loadAccountTypeNew",
}).then(function (response) {
    debugger
    $("#ddlAccount").select2({
        theme: "bootstrap4",
        placeholder: "Select Account",
        width: '100%',
        data: response.results
    });
});
//$("#ddlAccount").on("select2:select", function (e) {
//    debugger        
//    var datas = e.params.data;
//    var TypeName = new Option(datas.text, datas.id,  true, true);
//    var CategoryName = new Option(datas.cate, datas.catid, true,true);
//    var GroupName = new Option(datas.branch, datas.typ, true,true);
//    $("#AccountId").val(datas.id); 
//    $("#ddlAccount").append(TypeName).trigger('change');
//    $('#ddlAcctCategory').append(CategoryName).trigger('change');
//    $("#ddlAcctGroup").append(GroupName).trigger('change');
//});

$('#ddlAccount').on('change', function (e) {
    debugger

    $("#ddlAccount").on("select2:select", function (e) {
        debugger


        $.ajax({
            url: "chart/loadChartNumber",
            data: { Id: $('#ddlAccount').val() },
            type: "POST",
            cache: false
        }).then(function (response) {
            var datas = e.params.data;
            var groupName = new Option(datas.text, datas.id, true, true);

            $('#AccountId').val(e.params.data.id).trigger('change');

        });

    });

    $("#ddlAccount").on("select2:select", function (e) {
        debugger

        $("#ddlAcctCategory").select2({
            theme: "bootstrap4",
            placeholder: "Loading..."
        });
        $.ajax({
            url: "chart/loadCategoryNew",
            data: { Id: $('#ddlAccount').val() },
            type: "POST",
            cache: false
        }).then(function (response) {

            $("#ddlAcctCategory").select2({
                theme: "bootstrap4",
                placeholder: "Select Operation Type",
                width: '100%',
                data: response.results
            });

            //$('#ddlAcctCategory').append(CategoryName).trigger('change');

        });

    });

    $("#ddlAccount").on("select2:select", function (e) {
        debugger

        $("#ddlAcctGroup").select2({
            theme: "bootstrap4",
            placeholder: "Loading..."
        });
        $.ajax({
            url: "chart/loadAccountGroupNew",
            data: { Id: $('#ddlAccount').val() },
            type: "POST",
            cache: false
        }).then(function (response) {
            //var datas = e.params.data;
            //var groupName = new Option(datas.text, datas.id, true, true);

            //$('#ddlAcctGroup').append(groupName).trigger('change');
            $("#ddlAcctGroup").select2({
                theme: "bootstrap4",
                placeholder: "Select Account Group",
                width: '100%',
                data: response.results
            });

        });

    });


});



$(document).ready(function ($) {
    debugger
    //for dropdown list control for Comapny (id=ddlCompany)starts here

    $("#ddlGroup").select2({
        theme: "bootstrap4",
        placeholder: "Loading...",

    });

    $.ajax({
        url: "Chart/loadAccountGroup",
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
    //for dropdown list control for Comapny (id=ddlCompany)starts here

    $("#ddlCurrency").select2({
        theme: "bootstrap4",
        placeholder: "Loading...",

    });

    $.ajax({
        url: "Chart/loadCurrency",
    }).then(function (response) {
        debugger
        $("#ddlCurrency").select2({
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
        url: "Chart/loadCategory",
    }).then(function (response) {
        debugger
        $("#ddlAccountCategory").select2({
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

    $("#ddlCost").select2({
        theme: "bootstrap4",
        placeholder: "Loading...",

    });

    $.ajax({
        url: "Chart/loadCost",
    }).then(function (response) {
        debugger
        $("#ddlCost").select2({
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

    $("#ddlStatus").select2({
        theme: "bootstrap4",
        placeholder: "Loading...",

    });

    $.ajax({
        url: "Chart/loadStatus",
    }).then(function (response) {
        debugger
        $("#ddlStatus").select2({
            theme: "bootstrap4",
            // placeholder: "Select Company...",  
            width: '100%',
            data: response.results
        });
    });
});

//$('#chartTable').on('expand-row.bs.table', function (e, index, row, $detail) {
//    $detail.html('Loading request...');

//    var htmlData = '';
//    var header = '<div>';
//    var footer = '</div>';
//    htmlData = htmlData + header;

//    debugger

//    var html =
//        '<p style="text-align:left">'+
//        '<strong> Id:</strong>&nbsp' + row.id + '' + '<p>' +      
//        '<strong>Designation: </strong>&nbsp' + row.designation + ''+'</div>';
//    htmlData = htmlData + html;
//    htmlData = htmlData + footer;
//    $detail.html(htmlData);
//});