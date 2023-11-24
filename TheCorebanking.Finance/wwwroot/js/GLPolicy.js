var url_path = window.location.pathname;
if (url_path.charAt(url_path.length - 1) == '/') {
    url_path = url_path.slice(0, url_path.length - 1);
}

$(document).ready(function ($) {
    $("#GlLength").mouseout(function (event) {
        debugger
        var descri = $('#GlLength').val();
        if (descri == '') {
            $.notify({ icon: "add_alert", message: 'Your Gl lenght is empty' }, { type: 'danger', timer: 1000 });
            $(this).css('border-color', 'red');
            $('#GlLength').focus();
            return false;
        } else {
            $(this).css('border-color', 'green');
        }

    });
});

function glPolicyFormatter(value, row, index) {
    return [
        '<div class="btn-group">' + '<a style="color:white"  class="edit btn btn-sm btn-info"  title="Edit glPolicy">'
        + '<i class="fas fa-edit"></i>'+
        '</a> '
    ].join('');
}

window.glPolicyEvents = {
    'click .edit': function (e, value, row, index) {
       
        if (row.state = true) {
          
            var data = JSON.stringify(row);
            $('#Id').val(row.id);        
            $('#GlLength').val(row.gL_Length);                
            $('#AddNewGLPolicy').modal('show'); 
            $('#btnGLPolicyUpdate').html('  <i class="now-ui-icons ui-1_check"></i> Update Record');            
            $('#btnGLPolicyUpdate').show();
            $('#btnGLPolicy').hide();
        }
    },
   

};
$(document).ready(function ($) {
    $('#btnGLPolicyUpdate').hide();
    $('#btnAddGLPolicy').show(); 
    $('#btnGLPolicyUpdate').on('click', function () {
        debugger
        updateGLPolicy();
    });

});
function updateGLPolicy() {
    debugger

   
    var GLPolicy_data = {
        Id: $('#Id').val(),
        GlLength: $('#GlLength').val(),
        //CompanyCode: $('#isCompany').prop("checked"),
        //BranchCode: $('#isBranch').prop("checked"),
        //PfoductGroupId: $('#isProduct').prop("checked"),
        //AccountType: $('#isAccount').prop("checked"),
        //CurrencyID: $('#isCurrency').prop("checked"),
    }

    $("input[type=submit]").attr("disabled", "disabled");  

    $('#frmGLPolicy').validate({

        
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
                text: "GLPolicy will be updated!",
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
                    $("#btnGLPolicyUpdate").attr("disabled", "disabled");
                    debugger
                    $.ajax({
                        url: url_path +'/financesetup/UpdateGLPolicy',
                        type: 'POST',
                        data: GLPolicy_data,
                        dataType: 'json',
                        cache: false,                        
                        success: function (result) {
                            
                            if (result.toString != '' && result != null) {
                                swal({ title: 'Update GLPolicy', text: 'GLPolicy updated successfully!', type: 'success' }).then(function () { window.location.reload(true); });
                                $('#AddNewGLPolicy').modal('hide'); 
                                $('#GLPolicyTable').
                                    bootstrapTable(
                                        'refresh', { url: url_path +'/financesetup/listPolicy' });

                                $("#btnGLPolicyUpdate").removeAttr("disabled");
                            }
                            else {
                                swal({ title: 'Update GLPolicy', text: 'Something went wrong: </br>' + result.toString(), type: 'error' }).then(function () { clear(); });
                                $("#btnGLPolicyUpdate").removeAttr("disabled");
                            }
                        },
                        error: function (e) {
                            swal({ title: 'Update GLPolicy', text: 'GLPolicy update encountered an error', type: 'error' }).then(function () { clear(); });
                            $("#btnGLPolicyUpdate").removeAttr("disabled");
                        }
                    });
                }
            });
        }

    },
        function (dismiss) {
            swal('Update GLPolicy', 'You cancelled GLPolicy update.', 'error');
            $("#btnGLPolicyUpdate").removeAttr("disabled");
        });

}



$(document).ready(function ($) { 
    $('#btnGLPolicy').on('click', function () {
        debugger    
            addGLPolicys();   
    });
});


function addGLPolicys() {
    debugger
    $('#btnGLPolicyUpdate').hide();
    $("input[type=submit]").attr("disabled", "disabled");
    $('#frmGLPolicy').validate({
        messages: {

            GlLength: { required: "Policy Name is required" }
             
       
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
                text: "GLPolicy will be added!",
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
                    $("#btnGLPolicy").attr("disabled", "disabled");

                    debugger
                    var GLPolicy_data = {
                        Id: $('#Id').val(),
                        GlLength: $('#GlLength').val(),
                        CompanyCode: $('#isCompany').prop("checked"),
                        BranchCode: $('#isBranch').prop("checked"),
                        PfoductGroupId: $('#isProduct').prop("checked"),
                        AccountType: $('#isAccount').prop("checked"),
                        CurrencyID: $('#isCurrency').prop("checked"),
                    }
                    
                           

                    $.ajax({
                        url: url_path +'/financesetup/AddGLPolicy',
                        type: 'POST',
                        data: GLPolicy_data,
                        dataType: "json",                      
                       
                        success: function (result) {

                            if (result.message == "") {
                                swal({ title: 'Add GLPolicy', text: 'GLPolicy added successfully!', type: 'success' }).then(function () { window.location.reload(true); });
                                $('#AddNewGLPolicy').modal('hide');
                                $('#GLPolicyTable').
                                    bootstrapTable(
                                        'refresh', { url: url_path +'/financesetup/listPolicy' });

                                $("#btnGLPolicy").removeAttr("disabled");
                            }
                            else {
                                swal({ title: 'Add GLPolicy', text: 'Something went wrong: </br>' + result.message, type: 'error' }).then(function () { clear(); });
                                $("#btnGLPolicy").removeAttr("disabled");
                            }
                        },
                        error: function (e) {
                            swal({ title: 'Add GLPolicy', text: 'Adding GLPolicy encountered an error', type: 'error' }).then(function () { clear(); });
                            $("#btnGLPolicy").removeAttr("disabled");
                        }
                    });
                }
            });
        }

    },
        function (dismiss) {
            swal('Add GLPolicy', 'You cancelled add GLPolicy.', 'error');
            $("#btnGLPolicy").removeAttr("disabled");
        });

}


function reloadpage() {
    location.reload();
}

function clear() {
    $('#Id').val('');
    $('#GlLength').val(''); 
 
  

}



