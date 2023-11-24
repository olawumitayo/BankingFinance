var url_path = window.location.pathname;
if (url_path.charAt(url_path.length - 1) == '/') {
    url_path = url_path.slice(0, url_path.length - 1);
}

$(document).ready(function ($) {
    $("#LevelName").mouseout(function (event) {
        debugger
        var descri = $('#LevelName').val();
        if (descri == '') {
            $.notify({ icon: "add_alert", message: 'Your Gl Level is empty' }, { type: 'danger', timer: 1000 });
            $(this).css('border-color', 'red');
            $('#LevelName').focus();
            return false;
        } else {
            $(this).css('border-color', 'green');
        }

    });
});

function glLevelFormatter(value, row, index) {
    return [
        '<div class="btn-group">' + '<a style="color:white"  class="edit btn btn-sm btn-info"  title="Edit glLevel">'
        + '<i class="fas fa-edit"></i>'+
        '</a> '
    ].join('');
}

window.glLevelEvents = {
    'click .edit': function (e, value, row, index) {
       
        if (row.state = true) {
          
            var data = JSON.stringify(row);
            $('#IdUpdate').val(row.id);        
            $('#LevelNameUpdate').val(row.levelName);                
            $('#UpdateGLLevel').modal('show'); 
            $('#btnGLLevelUpdate').html('  <i class="now-ui-icons ui-1_check"></i> Update Record');            
           
        }
    },
   

};
$(document).ready(function ($) {

    $('#btnGLLevelUpdate').on('click', function () {
        debugger
        updateGLLevel();
    });

});
function updateGLLevel() {
    debugger

    var json_data = {};
    json_data.Id = $('#IdUpdate').val();
    json_data.LevelName = $('#LevelNameUpdate').val();
  

    $("input[type=submit]").attr("disabled", "disabled");  

    $('#frmUpdateGLLevel').validate({

        
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
                text: "GLLevel will be updated!",
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
                    $("#btnGLLevelUpdate").attr("disabled", "disabled");
                    debugger
                    $.ajax({
                        url: url_path +'/financesetup/UpdateGLLevel',
                        type: 'POST',
                        data: json_data,
                        dataType: 'json',
                        cache: false,
                        //headers: {
                        //    'VerificationToken': forgeryId
                        //},
                        success: function (result) {
                            
                            if (result.toString != '' && result != null) {
                                swal({ title: 'Update GLLevel', text: 'GLLevel updated successfully!', type: 'success' }).then(function () { window.location.reload(true); });
                                $('#Update').modal('hide'); 
                                $('#GLLevelTable').
                                    bootstrapTable(
                                        'refresh', { url: url_path +'/financesetup/listlevel' });

                                $("#btnGLLevelUpdate").removeAttr("disabled");
                            }
                            else {
                                swal({ title: 'Update GLLevel', text: 'Something went wrong: </br>' + result.toString(), type: 'error' }).then(function () { clear(); });
                                $("#btnGLLevelUpdate").removeAttr("disabled");
                            }
                        },
                        error: function (e) {
                            swal({ title: 'Update GLLevel', text: 'GLLevel update encountered an error', type: 'error' }).then(function () { clear(); });
                            $("#btnGLLevelUpdate").removeAttr("disabled");
                        }
                    });
                }
            });
        }

    },
        function (dismiss) {
            swal('Update GLLevel', 'You cancelled GLLevel update.', 'error');
            $("#btnGLLevelUpdate").removeAttr("disabled");
        });

}



$(document).ready(function ($) { 
    $('#btnGLLevel').on('click', function () {
        debugger    
            addGLLevels();   
    });
});


function addGLLevels() {
    debugger
  
    $("input[type=submit]").attr("disabled", "disabled");
    $('#frmGLLevel').validate({
        messages: {

            LevelName: { required: "Level Name is required" }
             
       
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
                text: "GLLevel will be added!",
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
                    $("#btnGLLevel").attr("disabled", "disabled");

                    debugger
                    var GLLevel_data = {
                        Id: $('#Id').val(),
                        LevelName: $('#LevelName').val(),
                                          
                    }
                           

                    $.ajax({
                        url: url_path +'/financesetup/AddGLLevel',
                        type: 'POST',
                        data: GLLevel_data,
                        dataType: "json",                      
                       
                        success: function (result) {
                         
                            if (result.toString != '' && result != null) {
                                swal({ title: 'Add GLLevel', text: 'GLLevel added successfully!', type: 'success' }).then(function () { window.location.reload(true); });
                                $('#AddNewGLLevel').modal('hide');
                                $('#GLLevelTable').
                                    bootstrapTable(
                                        'refresh', { url: url_path +'/financesetup/listLevel' });

                                $("#btnGLLevel").removeAttr("disabled");
                            }
                            else {
                                swal({ title: 'Add GLLevel', text: 'Something went wrong: </br>' + result.toString(), type: 'error' }).then(function () { clear(); });
                                $("#btnGLLevel").removeAttr("disabled");
                            }
                        },
                        error: function (e) {
                            swal({ title: 'Add GLLevel', text: 'Adding GLLevel encountered an error', type: 'error' }).then(function () { clear(); });
                            $("#btnGLLevel").removeAttr("disabled");
                        }
                    });
                }
            });
        }

    },
        function (dismiss) {
            swal('Add GLLevel', 'You cancelled add GLLevel.', 'error');
            $("#btnGLLevel").removeAttr("disabled");
        });

}


function reloadpage() {
    location.reload();
}

function clear() {
    $('#Id').val('');
    $('#LevelName').val(''); 
 
  

}



