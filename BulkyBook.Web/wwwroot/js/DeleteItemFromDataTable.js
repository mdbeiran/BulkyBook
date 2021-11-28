// Delete Category Object
function DeleteCategory(url) {
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
                        //$('#tblCategory').DataTable().ajax.reload();

                        setTimeout(function () {
                            location.reload();
                        }, 1000);
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
function DeleteCoverType(url) {
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
                        setTimeout(function () {
                            $('#tblCoverType').DataTable().ajax.reload();
                        }, 1000);
                    }
                    else {
                        swal({
                            title: "Oops...",
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
function DeleteBook(url) {
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


// Delete Company Object
function DeleteCompany(url) {
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


// Delete Slider Object
function DeleteSlider(url) {
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
                        $('#tblSlider').DataTable().ajax.reload();

                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    }));
}