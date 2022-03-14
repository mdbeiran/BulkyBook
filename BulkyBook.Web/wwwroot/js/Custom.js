// Lock or Unlock User
function LockUnlock(id) {
    $.ajax({
        type: "POST",
        url: "/Admin/ManageUser/LockUnlock",
        data: JSON.stringify(id),
        contentType: "application/json",
        success: function (data) {
            if (data.success) {
                toastr.success(data.message);
                $('#tblUser').DataTable().ajax.reload();
            }
            else {
                toastr.error(data.message);
            }
        }
    });
}


// Validate for Carrier and Tracking Number inputs
function validateInput() {
    let carrier = document.getElementById('carrier').value;
    let trackingNumber = document.getElementById('trackingNumber').value;

    if (carrier.toString() == '') {
        swal("Error", "Please Enter Carrier", "error");
        return false;
    }
    else {
        if (trackingNumber.toString() == '') {
            swal("Error", "Please Enter TrackingNumber", "error");
            return false;
        }
        else {
            return true;
        }
    }
}


// Convert shippingDate and paymentDate to empty
$(document).ready(function () {
    let shippingDate = document.getElementById('shippingDate');
    if (shippingDate.value == '1/1/0001') {
        shippingDate.value = "";
    }

    let paymentDate = document.getElementById('paymentDate');
    if (paymentDate.value == '1/1/0001') {
        paymentDate.value = "";
    }
});


// Validate Answer TextArea
function ValidateTextArea() {
    let answer = document.getElementById('answer').value;

    if (answer.toString() == '') {
        swal("Error", "Please enter answer", "error");
        return false;
    }
    else {
        return true;
    }
}


// Open or close ticket
function OpenCloseTicket(id) {
    $.ajax({
        type: "POST",
        url: "/Admin/ManageTicket/OpenCloseTicket/" + id,
        contentType: "application/json",
        success: function (data) {
            if (data.success) {
                toastr.success(data.message);
                $('#tblTicket').DataTable().ajax.reload();
            }
            else {
                toastr.error(data.message);
            }
        }
    });
}