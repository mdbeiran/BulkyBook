let dataTable;

$(document).ready(function () {
    loadDataTableCategory();
    loadDataTableCoverType();
    loadDataTableBook();
    loadDataTableCompany();
    loadDataTableUser();
    loadDataTableSlider();
    loadDataTableContactUs();
    loadDataTableTicket();


    // Load dataTable order
    let url = window.location.search;
    if (url.includes("inProcess")) {
        loadDataTableOrder("GetOrders?status=inProcess");
    }
    else {
        if (url.includes("pending")) {
            loadDataTableOrder("GetOrders?status=pending");
        }
        else {
            if (url.includes("completed")) {
                loadDataTableOrder("GetOrders?status=completed");
            }
            else {
                if (url.includes("rejected")) {
                    loadDataTableOrder("GetOrders?status=rejected");
                }
                else {
                    if (url.includes("newest")) {
                        loadDataTableOrder("GetOrders?status=newest");
                    }
                    else {
                        if (url.includes("all")) {
                            loadDataTableOrder("GetOrders?status=all");
                        }
                        else {
                            loadDataTableOrder("GetOrders?status=newest");
                        }
                    }
                }
            }
        }
    }
});


// Load DataTabl Category
function loadDataTableCategory() {
    dataTable = $('#tblCategory').DataTable({
        "ajax": {
            "url": "/Admin/ManageCategory/GetCategories"
        },
        "columns": [
            { "data": "title", "width": "40%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="text-center">
                                <a href="/Admin/ManageCategory/Upsert/${data}" class="btn btn-warning text-white" style="cursor:pointer;">
                                    <i class="fas fa-edit"></i>
                                </a>
                                <a onclick=DeleteCategory("/Admin/ManageCategory/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer;">
                                    <i class="fas fa-trash-alt"></i>
                                </a>
                            </div>
                            `;
                }, "width": "60%"
            }
        ]
    });
}


// Load DataTable CoverType
function loadDataTableCoverType() {
    dataTable = $('#tblCoverType').DataTable({
        "ajax": {
            "url": "/Admin/ManageCoverType/GetCoverTypes"
        },
        "columns": [
            { "data": "title", "width": "40%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="text-center">
                                <a href="/Admin/ManageCoverType/Upsert/${data}" class="btn btn-warning text-white" style="cursor:pointer;">
                                    <i class="fas fa-edit"></i>
                                </a>
                                <a onclick=DeleteCoverType("/Admin/ManageCoverType/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer;">
                                    <i class="fas fa-trash-alt"></i>
                                </a>
                            </div>
                            `;
                }, "width": "60%"
            }
        ]
    });
}


// Load DataTable Book
function loadDataTableBook() {
    dataTable = $('#tblBook').DataTable({
        "ajax": {
            "url": "/Admin/ManageBooks/GetAllBooks"
        },
        "columns": [
            { "data": "title", "width": "15%" },
            { "data": "isbn", "width": "15%" },
            { "data": "price", "width": "15%" },
            { "data": "author", "width": "15%" },
            { "data": "category.title", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="text-center">
                                <a href="/Admin/ManageBooks/Upsert/${data}" class="btn btn-warning text-white" style="cursor:pointer;">
                                    <i class="fas fa-edit"></i>
                                </a>
                                <a onclick=DeleteBook("/Admin/ManageBooks/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer;">
                                    <i class="fas fa-trash-alt"></i>
                                </a>
                            </div>
                            `;
                }, "width": "35%"
            }
        ]
    });
}


// Load DataTable Company
function loadDataTableCompany() {
    dataTable = $('#tblCompany').DataTable({
        "ajax": {
            "url": "/Admin/ManageCompany/GetAllCompanies"
        },
        "columns": [
            { "data": "name", "width": "15%" },
            { "data": "streetAddress", "width": "15%" },
            { "data": "city", "width": "15%" },
            { "data": "state", "width": "15%" },
            { "data": "phoneNumber", "width": "15%" },
            {
                "data": "isAuthorizedCompany",
                "render": function (data) {
                    if (data) {
                        return `<input type="checkbox" disabled checked />`
                    }
                    else {
                        return `<input type="checkbox" disabled />`
                    }
                }, "width": "10%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="text-center">
                                <a href="/Admin/ManageCompany/Upsert/${data}" class="btn btn-warning text-white" style="cursor:pointer;">
                                    <i class="fas fa-edit"></i>
                                </a>
                                <a onclick=DeleteCompany("/Admin/ManageCompany/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer;">
                                    <i class="fas fa-trash-alt"></i>
                                </a>
                            </div>
                            `;
                }, "width": "15%"
            }
        ]
    });
}


// Load DataTable User
function loadDataTableUser() {
    dataTable = $('#tblUser').DataTable({
        "ajax": {
            "url": "/Admin/ManageUser/GetAllUsers"
        },
        "columns": [
            { "data": "fullName", "width": "15%" },
            { "data": "email", "width": "15%" },
            { "data": "phoneNumber", "width": "15%" },
            { "data": "company.name", "width": "15%" },
            { "data": "role", "width": "15%" },
            {
                "data": {
                    id: "id", lockoutEnd: "lockoutEnd"
                },
                "render": function (data) {
                    let today = new Date().getTime();
                    let lockout = new Date(data.lockoutEnd).getTime();
                    if (lockout > today) {
                        // User is currently lock
                        return `
                            <div class="text-center">
                                <a onclick=LockUnlock('${data.id}') class="btn btn-danger text-white" style="cursor:pointer;">
                                    <i class="fas fa-unlock"></i> Unlock
                                </a>
                            </div>
                            `;
                    }
                    else {
                        // User is currently Unlock
                        return `
                            <div class="text-center">
                                <a onclick=LockUnlock('${data.id}') class="btn btn-success text-white" style="cursor:pointer;">
                                    <i class="fas fa-lock"></i> Lock
                                </a>
                            </div>
                            `;
                    }
                }, "width": "15%"
            }
        ]
    });
}


// Load DataTable Order
function loadDataTableOrder(url) {
    dataTable = $('#tblOrder').DataTable({
        "ajax": {
            "url": "/Admin/ManageOrder/" + url
        },
        "columns": [
            { "data": "id", "width": "10%" },
            { "data": "applicationUser.fullName", "width": "20%" },
            { "data": "applicationUser.phoneNumber", "width": "10%" },
            { "data": "applicationUser.email", "width": "20%" },
            { "data": "orderStatus", "width": "15%" },
            { "data": "orderTotal", "width": "10%" },
            {
                "data": "isViewByAdmin",
                "render": function (data) {
                    if (data) {
                        return `<input type="checkbox" disabled checked />`
                    }
                    else {
                        return `<input type="checkbox" disabled />`
                    }
                },
                "width": "5%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="text-center">
                                <a href="/Admin/ManageOrder/OrderDetails/${data}" class="btn btn-info text-white" style="cursor:pointer;">
                                    <i class="fas fa-info"></i>
                                </a>
                            </div>
                            `;
                }, "width": "10%"
            }
        ]
    });
}


// Load DataTable Slider
function loadDataTableSlider() {
    dataTable = $('#tblSlider').DataTable({
        "ajax": {
            "url": "/Admin/ManageSlider/GetAllSliders"
        },
        "columns": [
            {
                "data": "imageName",
                "render": function (data) {
                    return `
                              <img src="/images/Slider/Thumb/${data}" style="width: 100px" />
                            `;
                },
                "width": "20%"
            },
            { "data": "title", "width": "10%" },
            { "data": "url", "width": "15%" },
            { "data": "visit", "width": "10%" },
            {
                "data": "isActive",
                "render": function (data) {
                    if (data) {
                        return `<input type="checkbox" disabled checked />`
                    }
                    else {
                        return `<input type="checkbox" disabled />`
                    }
                },
                "width": "10%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="text-center">
                                <a href="/Admin/ManageSlider/Upsert/${data}" class="btn btn-warning text-white" style="cursor:pointer;">
                                    <i class="fas fa-edit"></i>
                                </a>
                                <a onclick=DeleteSlider("/Admin/ManageSlider/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer;">
                                    <i class="fas fa-trash-alt"></i>
                                </a>
                            </div>
                            `;
                }, "width": "30%"
            }
        ]
    });
}


// Load DataTable ContactUs
function loadDataTableContactUs() {
    dataTable = $('#tblContactUs').DataTable({
        "ajax": {
            "url": "/Admin/ManageSite/GetAllContactUs"
        },
        "columns": [
            { "data": "fullName", "width": "30%" },
            {
                "data": "answer",
                "render": function (data) {
                    if (data == null) {
                        return data = "No Answer";
                    }
                    else {
                        return data = "has been answered";
                    }
                },
                "width": "20%"
            },
            { "data": "subject", "width": "15%" },
            { "data": "createDate", "width": "20%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="text-center">
                                <a title="Reply to message" href="/Admin/ManageSite/ShowMessage/${data}" class="btn btn-warning text-white" style="cursor:pointer;">
                                    <i class="fas fa-reply"></i>
                                </a>
                            </div>
                            `;
                }, "width": "15%"
            }
        ]
    });
}


// Load DataTable Ticket
function loadDataTableTicket() {
    dataTable = $('#tblTicket').DataTable({
        "ajax": {
            "url": "/Admin/ManageTicket/GetTickets"
        },
        "columns": [
            { "data": "id", "width": "10%" },
            { "data": "applicationUser.fullName", "width": "20%" },
            { "data": "applicationUser.email", "width": "10%" },
            { "data": "subject", "width": "20%" },
            {
                "data": {
                    id: "id", status: "status"
                },
                "render": function (data) {
                    if (data.status) {
                        return `<div class="text-center">
                                    <a onclick=OpenCloseTicket('${data.id}') class="btn btn-danger text-white" style="cursor:pointer;">
                                        Close
                                    </a>
                                </div>`
                    }
                    else {
                        return `<div class="text-center">
                                    <a onclick=OpenCloseTicket('${data.id}') class="btn btn-success text-white" style="cursor:pointer;">
                                        Open
                                    </a>
                                </div>`
                    }
                },
                "width": "5%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="text-center">
                                <a href="/Admin/ManageTicket/TicketDetail/${data}" class="btn btn-info text-white" style="cursor:pointer;">
                                    View Ticket
                                </a>
                            </div>
                            `;
                }, "width": "10%"
            }
        ]
    });
}