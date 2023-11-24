var url_path = window.location.pathname;
if (url_path.charAt(url_path.length - 1) == '/') {
    url_path = url_path.slice(0, url_path.length - 1);
}
//Validation
$(document).ready(function ($) {
    $("#Descriptions").mouseout(function (event) {
        debugger
        var descr = $('#Descriptions').val();
        if (descr == '') {
            $.notify({ icon: "add_alert", message: 'Your description is empty' }, { type: 'danger', timer: 1000 });
            $(this).css('border-color', 'red');
            $('#Descriptions').focus();
            return false;
        } else {
            $(this).css('border-color', 'green');
        }

    });
});
$(document).ready(function ($) {
    $("#ddlGroupp").mouseout(function (event) {
        debugger
        var grp = $('#ddlGroupp').val();
        if (grp == '') {
            $.notify({ icon: "add_alert", message: 'Your group is empty' }, { type: 'danger', timer: 1000 });
            $(this).css('border-color', 'red');
            $('#ddlGroupp').focus();
            return false;
        } else {
            $(this).css('border-color', 'green');
        }

    });
});
$(document).ready(function ($) {
    $("#ddlStatus").mouseout(function (event) {
        debugger
        var status = $('#ddlStatus').val();
        if (status == '') {
            $.notify({ icon: "add_alert", message: 'Your status is empty' }, { type: 'danger', timer: 1000 });
            $(this).css('border-color', 'red');
            $('#ddlStatus').focus();
            return false;
        } else {
            $(this).css('border-color', 'green');
        }

    });
});


function categoryFormatter(value, row, index) {
    return [
        '<div class="btn-group">' + '<a style="color:white"  class="edit btn btn-sm btn-info"  title="Edit Category">'
        + '<i class="fas fa-edit"></i>' +
        '<a style="color:white" title="Remove Category" class="remove btn btn-sm btn-danger">'
        + '<i class="fas fa-trash"></i></a>' +
        '</a> '
    ].join('');
}

window.categoryEvents = {
    
    'click .edit': function (e, value, row, index) {
        debugger
        if (row.state = true) {
            var data = JSON.stringify(row);
            $('#IdUpdate').val(row.id);
            $('#DescriptionUpdate').val(row.descriptions);   
            $('#ddlStatusUpdate :selected').text(row.active);         
            $('#ddlGrouppUpdate').val(row.accountGroupId).trigger('change');
            $('#UpdateCategory').modal('show');          
            $('#btnCategoryUpdate').html('<i class="now-ui-icons ui-1_check"></i> Update Record');         
         
        }
        
    },
    'click .remove': function (e, value, row, index) {
        info = JSON.stringify(row);
        console.log(info);
        var reg = {
            Id:$('#Id').val(),
            Description: $('#Description').val(),
            AccountGroupId: $('#ddlGroupp').val(),
            Active: $('#Active').val()            
        }
        debugger
        $('#ID').val(row.id);
        $.ajax({
            url: url_path +'/Financesetup/RemoveCategory',
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
                        $('#categoryTable').
                            bootstrapTable(
                                'refresh', { url:url_path +'/Financesetup/listcategory' }); 

                        //return false;
                    }
                    else {
                        swal("Category", "You cancelled add category.", "error");
                    }
                    $('#categoryTable').
                        bootstrapTable(
                            'refresh', { url:url_path +'/Financesetup/listcategory' });
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
    $('#btnUploadCategory').on('click', function () {
        UploadCategory();
    });
});
function UploadCategory() {
    $.ajax({
        url:url_path +'/Financesetup/CategoryUpload',
        type: 'POST',
        success: function (result) {
            if (result.toString != '' && result != null) {
                
                $('#AddNewCategory').modal('hide');  
                $('#categoryTable').
                    bootstrapTable(
                    'refresh', { url:url_path +'/Financesetup/listcategory' });
                swal({ title: 'Upload Category', text: 'Category uploaded successfully!', type: 'success' }).then(function () { clear(); });
            }
            else {
                swal({ title: 'Upload category ', text: 'Something went wrong: </br>' + result.toString(), type: 'error' }).then(function () { clear(); });

            }

        },

    });

}


function updateCategory() {
    debugger

    var json_data = {};  
    json_data.Id = $('#IdUpdate').val();
    json_data.Descriptions = $('#DescriptionUpdate').val();
    json_data.AccountGroupId = $('#ddlGrouppUpdate').val();
    json_data.Active = $('#ddlStatusUpdate').val();    
  
    $("input[type=submit]").attr("disabled", "disabled");

    $('#UpdatefrmCategory').validate({

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
                text: "Category will be updated!",
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
                    $("#btnCategoryUpdate").attr("disabled", "disabled");
                    debugger
                    $.ajax({
                        url:url_path +'/Financesetup/UpdateCategory',
                        type: 'POST',
                        data: json_data,
                        dataType: "json",
                        //headers: {
                        //    'VerificationToken': forgeryId
                        //},
                        success: function (result) {

                            if (result.toString != '' && result != null) {
                                swal({ title: 'Update Category', text: 'Category updated successfully!', type: 'success' }).then(function () { window.location.reload(true); });
                                $('#UpdateCategory').modal('hide'); 
                                $('#categoryTable').
                                    bootstrapTable(
                                        'refresh', { url:url_path +'/Financesetup/listcategory' });

                                $("#btnCategoryUpdate").removeAttr("disabled");
                            }
                            else {
                                swal({ title: 'Update Category', text: 'Something went wrong: </br>' + result.toString(), type: 'error' }).then(function () { clear(); });
                                $("#btnCategoryUpdate").removeAttr("disabled");
                            }
                        },
                        error: function (e) {
                            swal({ title: 'Update Category', text: 'Category update encountered an error', type: 'error' }).then(function () { clear(); });
                            $("#btnCategoryUpdate").removeAttr("disabled");
                        }
                    });
                }
            });
        }

    },
        function (dismiss) {
            swal('Update Category', 'You cancelled category update.', 'error');
            $("#btnCategoryUpdate").removeAttr("disabled");
        });

}



$(document).ready(function ($) {
    //$('#btnCategory').show();
    //$('#btnCategoryUpdate').hide();
    $('#btnCategory').on('click', function () {
        debugger
        addCategory();

    });

});
function addCategory() {
   
    debugger 
    $("input[type=submit]").attr("disabled", "disabled");
    $('#frmCategory').validate({
        messages: {
          
            Descriptions: { required: "Description is required" },         
            AccountGroupId: { required: "Account Group is required" },
            Active: { required: "Active is required" }           
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
                text: "Category will be added!",
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
                    $("#btnCategory").attr("disabled", "disabled");

                    debugger            
                    var str = $("#Descriptionscategory").val();
                   // var Description = str.valueOf()
                    //var category_data = {
                    //    id: $('#Id').val(),
                    //    Descriptions: ('#Descriptions').val(),
                    //    accountGroupId: $('#ddlGroup').val(),
                    //    Active: $('#Active').val()
                    //}  
                    var json_data = {};

                    json_data.Id = $('#Id').val();
                    json_data.Descriptions = $('#Descriptionscategory').val();
                    json_data.AccountGroupId = $('#ddlGroupp').val();
                    json_data.Active = $('#ddlStatus').val();
                 
                    $.ajax({
                        url:url_path +'/Financesetup/AddCategory',
                        type: 'POST',
                        data: json_data,
                        dataType: "json",                    
                        success: function (result) {
                            
                            if (result.toString != '' && result != null) {
                                swal({ title: 'Add Category', text: 'Category added successfully!', type: 'success' }).then(function () { window.location.reload(true); });
                                $('#AddNewCategory').modal('hide'); 
                                $('#categoryTable').
                                    bootstrapTable(
                                        'refresh', { url:url_path +'/Financesetup/listcategory' });

                                $("#btnCategory").removeAttr("disabled");
                            }
                            else {
                                swal({ title: 'Add Category', text: 'Something went wrong: </br>' + result.toString(), type: 'error' }).then(function () { clear(); });
                                $("#btnCategory").removeAttr("disabled");
                            }
                        },
                        error: function (e) {
                            swal({ title: 'Add Category', text: 'Adding category encountered an error', type: 'error' }).then(function () { clear(); });
                            $("#btnCategory").removeAttr("disabled");
                        }
                    });
                }
            });
        }

    },
        function (dismiss) {
            swal('Add Category', 'You cancelled add category.', 'error');
            $("#btnCategory").removeAttr("disabled");
        });

}
$(document).ready(function ($) {
    debugger
    //for dropdown list control for Comapny (id=ddlCompany)starts here

    $("#ddlGroupp").select2({
        theme: "bootstrap4",
        placeholder: "Loading...",

    });

    $.ajax({
        url: "FinanceSetup/loadgroup",
    }).then(function (response) {
        debugger
        $("#ddlGroupp").select2({
            theme: "bootstrap4",
            // placeholder: "Select Company...",  
            width: '100%',
            data: response.results
        });
    });

    $("#ddlGrouppUpdate").select2({
        theme: "bootstrap4",
        placeholder: "Loading...",

    });

    $.ajax({
        url: "FinanceSetup/loadgroup",
    }).then(function (response) {
        debugger
        $("#ddlGrouppUpdate").select2({
            theme: "bootstrap4",
             
            width: '100%',
            data: response.results
        });
    });
});

$(document).ready(function ($) {
    //$('#btnCategory').show();
    //$('#btnCategoryUpdate').hide();
    $('#btnCategoryUpdate').on('click', function () {
        debugger
        updateCategory();
    });

});
function reloadpage() {
    location.reload();
}

function clear() {
    $('#Id').val('');
    $('#Active').val('');   
    $('#AccountGroupId').val('');
    $('#Descriptions').val('');
   
}



