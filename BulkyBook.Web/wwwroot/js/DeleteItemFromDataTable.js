// Delete Category Object
function Delete(url) {
    swal({
        title: "Are you sure you want to Delete ?",
        text: "You will not be able to restore the data!",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    }));
}


// Delete CoverType Object
function Delete(url) {
    swal({
        title: "Are you sure you want to Delete ?",
        text: "You will not be able to restore the data!",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        swal({
                            title: data.message,
                            text: "CoverType object with successful deleted",
                            icon: "success",
                            dangerMode: true
                        });
                        dataTable.ajax.reload();
                    }
                    else {
                        swal({
                            title:"Oops...",
                            text: data.message,
                            icon: "error",
                            dangerMode: true
                        });
                    }
                }
            });
        }
    }));
}


// Delete Book Object
function Delete(url) {
    swal({
        title: "Are you sure you want to Delete ?",
        text: "You will not be able to restore the data!",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    location.reload();
                }
            });
        }
    }));
}