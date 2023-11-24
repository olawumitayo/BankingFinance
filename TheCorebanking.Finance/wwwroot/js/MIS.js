var url_path = window.location.pathname;
if (url_path.charAt(url_path.length - 1) == '/') {
    url_path = url_path.slice(0, url_path.length - 1);
}
function misFormatter(value, row, index) {
    return [
      
        '<div class="btn-group">' +'<a style="color:white"  class="edit btn btn-sm btn-info"  title="Edit MIS">'
        + '<i class="fas fa-edit"></i>'+'</a>' +
        '<a  style="color:white" title="Remove MIS" class="remove btn btn-sm btn-danger">'
        +'<i class="fas fa-trash"></i></a>'+
        '</a> '
    ].join('');
}
function dateFormatter(value, row, $element) {
    var format = moment(value).format("DD MMMM, YYYY");
    var html = '<div>' + format + '</div>';
    return html;
}
window.misEvents = {
    'click .view': function (e, value, row, index) {
        info = JSON.stringify(row);

        //$.notify({
        //    title: 'MIS Information',
        //    message: info
        //}, {
        //        type: 'pastel-danger',
        //        delay: 5000,
        //        template: '<div data-notify="container" class="col-xs-11 col-sm-3 alert alert-{0}" role="alert">' +
        //            '<span data-notify="title">{1}</span>' +
        //            '<span data-notify="message">{2}</span>' +
        //            '</div>'
        //    });
        swal('You click view icon', info);
       console.log(info);
    },
    'click .edit': function (e, value, row, index) {
       
        if (row.state = true) {          
            var data = JSON.stringify(row);        
            $('#Id').val(row.id);
            $('#MisCode').val(row.misCode);
            $('#MisName').val(row.misName);
            $('#MisTypeId').val(row.misTypeId);
            $('#ParentMisCode').val(row.parentMisCode);
            $('#CompanyCode').val(row.companyCode);
            $('#Deleted').val(row.deleted);
            $('#DateCreated').val(row.dateCreated);
            $('#AddNewMIS').modal('show'); 

            $('#btnMISUpdate').html('  <i class="now-ui-icons ui-1_check"></i> Update Record');
        }
    },
    'click .remove': function (e, value, row, index) {
        info = JSON.stringify(row);
        console.log(info);
        var reg = {
            Id:$('#Id').val(),
            MisCode:$('#MisCode').val(),
            MisName:$('#MisName').val(),
            MisTypeId:$('#MisTypeId').val(),
            ParentMisCode:$('#ParentMisCode').val(),
            CompanyCode:$('#ddlCompany').val(),
            Deleted:$('#Deleted').val(),
            DateCreated:$('#DateCreated').val()
        }
        debugger
        $('#ID').val(row.Id);
        $.ajax({
            url: url_path + '/RemoveMIS',
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


                      
                        $.notify("Deleted Successfully", {
                            animate: {
                                enter: 'animated flipInY',
                                exit: 'animated flipOutX'
                            }
                        });
                        $('#misTable').
                            bootstrapTable(
                            'refresh', { url: url_path + '/listmis' });

                        //return false;
                    }
                    else {
                        swal("MIS", "You cancelled add bank.", "error");
                    }
                     $('#misTable').
                            bootstrapTable(
                                'refresh', { url: url_path + '/listmis' });
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

    $('#btnMISUpdate').on('click', function () {
        debugger
        updateMIS();
    });

});
function updateMIS() {
    debugger
   
    var json_data = {};
    json_data.Id = $('#Id').val();
    json_data.MisCode= $('#MisCode').val();
    json_data.MisName= $('#MisName').val();
    json_data.MisTypeId= $('#MisTypeId').val();
    json_data.ParentMisCode= $('#ParentMisCode').val();
    json_data.CompanyCode= $('#ddlCompany').val();
    json_data.Deleted= $('#Deleted').val();
    json_data.DateCreated = $('#DateCreated').val();

    $("input[type=submit]").attr("disabled", "disabled");  

    $('#frmMIS').validate({

        //messages: {
        //    MisCode: { required: "MIS Code is required" },
        //    MisName: { required: "MIS Name is required" },
        //    MisTypeId: { required: "MIS Type is required" },
        //    ParentMisCode: { required: "Parent Mis Code is required" },
        
        //    DateCreated: { required: "Date Created is required" }
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
                text: "MIS will be updated!",
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
                    $("#btnMISUpdate").attr("disabled", "disabled");
                    debugger
                    $.ajax({
                        url: url_path + '/UpdateMIS',
                        type: 'POST',
                        data: json_data,
                        dataType: "json",
                        //headers: {
                        //    'VerificationToken': forgeryId
                        //},
                        success: function (result) {
                            
                            if (result.toString != '' && result != null) {
                                swal({ title: 'Update MIS', text: 'MIS updated successfully!', type: 'success' }).then(function () { window.location.reload(true); });
                                $('#AddNewMIS').modal('hide'); 
                                $('#misTable').
                                    bootstrapTable(
                                    'refresh', { url: url_path + '/listmis' });

                                $("#btnMISUpdate").removeAttr("disabled");
                            }
                            else {
                                swal({ title: 'Update MIS', text: 'Something went wrong: </br>' + result.toString(), type: 'error' }).then(function () { clear(); });
                                $("#btnMISUpdate").removeAttr("disabled");
                            }
                        },
                        error: function (e) {
                            swal({ title: 'Update MIS', text: 'MIS update encountered an error', type: 'error' }).then(function () { clear(); });
                            $("#btnMISUpdate").removeAttr("disabled");
                        }
                    });
                }
            });
        }

    },
        function (dismiss) {
            swal('Update MIS', 'You cancelled MIS update.', 'error');
            $("#btnMISUpdate").removeAttr("disabled");
        });

}



$(document).ready(function ($) {

    $('#btnMIS').on('click', function () {
        debugger      
       
            addMIS();   
 
    });

});
function addMIS() {
    debugger
    $('#btnMISUpdate').hide();


    $("input[type=submit]").attr("disabled", "disabled");
    $('#frmMIS').validate({
        messages: {
            MisCode: { required: "MIS Code is required" },
            MisName: { required: "MIS Name is required" },
            MisTypeId: { required: "MIS Type is required" },
            ParentMisCode: { required: "Parent Mis Code is required" },
       
            DateCreated: { required: "Date Created is required" }
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
                text: "MIS will be added!",
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
                    $("#btnMIS").attr("disabled", "disabled");

                    debugger
                    var Id = $('#Id').val();
                  
                    var MisCode= $('#MisCode').val();
                    var MisName= $('#MisName').val();
                    var MisTypeId= $('#MisTypeId').val();
                    var ParentMisCode= $('#ParentMisCode').val();
                    var CompanyCode = $('#ddlCompany').val();
                    var Deleted= $('#Deleted').val();
                    var DateCreated = $('#DateCreated').val();

                    $.ajax({
                        url: url_path + '/AddMIS',
                        type: 'POST',
                        data: { Id, MisCode, MisName, MisTypeId, ParentMisCode, CompanyCode, Deleted, DateCreated },
                        dataType: "json",
                        //headers: {
                        //    'VerificationToken': forgeryId
                        //},
                        success: function (result) {
                           
                            if (result.toString != '' && result != null) {
                                swal({ title: 'Add MIS', text: 'MIS added successfully!', type: 'success' }).then(function () { window.location.reload(true); });
                                $('#AddNewMIS').modal('hide'); 
                                $('#misTable').
                                    bootstrapTable(
                                    'refresh', { url: url_path + '/listmis' });

                                $("#btnMIS").removeAttr("disabled");
                            }
                            else {
                                swal({ title: 'Add MIS', text: 'Something went wrong: </br>' + result.toString(), type: 'error' }).then(function () { clear(); });
                                $("#btnMIS").removeAttr("disabled");
                            }
                        },
                        error: function (e) {
                            swal({ title: 'Add MIS', text: 'Adding MIS encountered an error', type: 'error' }).then(function () { clear(); });
                            $("#btnMIS").removeAttr("disabled");
                        }
                    });
                }
            });
        }

    },
        function (dismiss) {
            swal('Add MIS', 'You cancelled add mis.', 'error');
            $("#btnMIS").removeAttr("disabled");
        });

}

$(document).ready(function ($) {
    debugger
    //for dropdown list control for Comapny (id=ddlCompany)starts here

    $("#ddlCompany").select2({
        theme: "bootstrap4",
        placeholder: "Loading...",

    });

    $.ajax({
        url: "CustomerSetup/loadCompany",
    }).then(function (response) {
        debugger
        $("#ddlCompany").select2({
            theme: "bootstrap4",
            // placeholder: "Select Company...",  
            width: '100%',
            data: response.results
        });
    });
});

function reloadpage() {
    location.reload();
}

function clear() {
    $('#Id').val('');
    $('#MisCode').val('');
    $('#MisName').val('');
    $('#MisTypeId').val('');
    $('#ParentMisCode').val('');
    $('#CompanyCode').val('');
    $('#Deleted').val('');
    $('#DateCreated').val('');
}

$('#misTable').on('expand-row.bs.table', function (e, index, row, $detail) {
    $detail.html('Loading request...');

    var htmlData = '';
    var header = '<div>';
    var footer = '</div>';
    htmlData = htmlData + header;

    debugger

    var html =
        '<h8>' +
        '<p style="text-align:left">' +
        '<strong>MisCode:</strong>&nbsp' + row.misCode + '' + '<p>' +
        ' <strong>MisName: </strong>&nbsp' + row.misName + '' + '<p>' +
        '<strong> Company Code:</strong>&nbsp' + row.companyCode + ' </div>';

    htmlData = htmlData + html;
    htmlData = htmlData + footer;
    $detail.html(htmlData);
});



