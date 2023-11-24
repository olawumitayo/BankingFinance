var url_path = window.location.pathname;
if (url_path.charAt(url_path.length - 1) == '/') {
    url_path = url_path.slice(0, url_path.length - 1);
}

$(document).ready(function ($) {
    $("#Costname").mouseout(function (event) {
        debugger
        var descri = $('#Costname').val();
        if (descri == '') {
            $.notify({ icon: "add_alert", message: 'Your cost name is empty' }, { type: 'danger', timer: 1000 });
            $(this).css('border-color', 'red');
            $('#Costname').focus();
            return false;
        } else {
            $(this).css('border-color', 'green');
        }

    });
});

function costFormatter(value, row, index) {
    return [
        '<div class="btn-group">' + '<a style="color:white"  class="edit btn btn-sm btn-info"  title="Edit Cost">'
        + '<i class="fas fa-edit"></i>' +
        '<a style="color:white"  title="Remove Cost" class="remove btn btn-sm btn-danger">'
        +'<i class="fas fa-trash"></i></a>'+
        '</a> '
    ].join('');
}

window.costEvents = {
    'click .edit': function (e, value, row, index) {
       
        if (row.state = true) {
          
            var data = JSON.stringify(row);
            $('#IdUpdate').val(row.id);        
            $('#CostcodeUpdate').val(row.costcode);
            $('#CostnameUpdate').val(row.costname);           
            $('#UpdateCost').modal('show'); 
            $('#btnCostUpdate').html('  <i class="now-ui-icons ui-1_check"></i> Update Record');            
        
        }
    },
    'click .remove': function (e, value, row, index) {
        info = JSON.stringify(row);
        console.log(info);
  
        debugger
        $('#ID').val(row.id);
        $.ajax({
            url: url_path +'/financesetup/RemoveCost',
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
                        $('#costTable').
                            bootstrapTable(
                                'refresh', { url: url_path +'/financesetup/listcost' });

                        //return false;
                    }
                    else {
                        swal("Cost", "You cancelled add cost.", "error");
                    }
                    $('#costTable').
                            bootstrapTable(
                        'refresh', { url: url_path +'/financesetup/listcost' });
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
   
    $('#btnCostUpdate').on('click', function () {
        debugger
        updateCost();
    });

});
function updateCost() {
    debugger

    var json_data = {};
    json_data.Id = $('#IdUpdate').val();
    json_data.Costname = $('#CostnameUpdate').val();
    json_data.Costcode = $('#CostcodeUpdate').val();

    $("input[type=submit]").attr("disabled", "disabled");  

    $('#frmUpdatecost').validate({

        
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
                text: "Cost will be updated!",
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
                    $("#btnCostUpdate").attr("disabled", "disabled");
                    debugger
                    $.ajax({
                        url: url_path +'/financesetup/UpdateCost',
                        type: 'POST',
                        data: json_data,
                        dataType: 'json',
                        cache: false,
                        //headers: {
                        //    'VerificationToken': forgeryId
                        //},
                        success: function (result) {
                            
                            if (result.toString != '' && result != null) {
                                swal({ title: 'Update Cost', text: 'Cost updated successfully!', type: 'success' }).then(function () { window.location.reload(true); });
                                $('#UpdateCost').modal('hide'); 
                                $('#costTable').
                                    bootstrapTable(
                                        'refresh', { url: url_path +'/financesetup/listcost' });

                                $("#btnCostUpdate").removeAttr("disabled");
                            }
                            else {
                                swal({ title: 'Update Cost', text: 'Something went wrong: </br>' + result.toString(), type: 'error' }).then(function () { clear(); });
                                $("#btnCostUpdate").removeAttr("disabled");
                            }
                        },
                        error: function (e) {
                            swal({ title: 'Update Cost', text: 'Cost update encountered an error', type: 'error' }).then(function () { clear(); });
                            $("#btnCostUpdate").removeAttr("disabled");
                        }
                    });
                }
            });
        }

    },
        function (dismiss) {
            swal('Update Cost', 'You cancelled cost update.', 'error');
            $("#btnCostUpdate").removeAttr("disabled");
        });

}



$(document).ready(function ($) { 
    $('#btnCost').on('click', function () {
        debugger    
            addCosts();   
    });
});

$(document).ready(function ($) {

    $('#btnUploadCost').on('click', function () {
        uploadCost()
    });
});

function uploadCost() {
    $.ajax({
        type: 'POST',
        url: url_path +'/financesetup/CostUpload',
        success: function (result) {

            if (result.toString != '' && result != null) {             

                $('#costTable').
                    bootstrapTable(
                    'refresh', { url: url_path +'/financesetup/listcost' });
                swal({ title: 'Upload Chart', text: 'Cost center uploaded successfully!', type: 'success' }).then(function () { clear(); });

            }
            else {
                swal({ title: 'Upload Cost Center', text: 'Something went wrong: </br>' + result.toString(), type: 'error' }).then(function () { clear(); });

            }
        },
    })

}


function addCosts() {
    debugger
    
    $("input[type=submit]").attr("disabled", "disabled");
    $('#frmcost').validate({
        messages: {

            Costname: { required: "Description is required" }
             
       
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
                text: "Cost will be added!",
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
                    $("#btnCost").attr("disabled", "disabled");

                    debugger
                    var cost_data = {
                        id: $('#Id').val(),
                        costcode: $('#Costcode').val(),
                        costname: $('#Costname').val()                      
                    }
                           

                    $.ajax({
                        url: url_path +'/financesetup/AddCost',
                        type: 'POST',
                        data: cost_data,
                        dataType: "json",                      
                       
                        success: function (result) {
                         
                            if (result.toString != '' && result != null) {
                                swal({ title: 'Add Cost', text: 'Cost added successfully!', type: 'success' }).then(function () { window.location.reload(true); });
                                $('#AddNewCost').modal('hide');
                                $('#costTable').
                                    bootstrapTable(
                                        'refresh', { url: url_path +'/financesetup/listcost' });

                                $("#btnCost").removeAttr("disabled");
                            }
                            else {
                                swal({ title: 'Add Cost', text: 'Something went wrong: </br>' + result.toString(), type: 'error' }).then(function () { clear(); });
                                $("#btnCost").removeAttr("disabled");
                            }
                        },
                        error: function (e) {
                            swal({ title: 'Add Cost', text: 'Adding cost encountered an error', type: 'error' }).then(function () { clear(); });
                            $("#btnCost").removeAttr("disabled");
                        }
                    });
                }
            });
        }

    },
        function (dismiss) {
            swal('Add Cost', 'You cancelled add cost.', 'error');
            $("#btnCost").removeAttr("disabled");
        });

}


function reloadpage() {
    location.reload();
}

function clear() {
    $('#Id').val('');
    $('#Costname').val(''); 
    $('#Costcode').val('');
  

}



