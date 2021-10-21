let dataTable;

$(document).ready(function () {
    loadDataTableCategory();
    loadDataTableCoverType();
    loadDataTableBook();
    loadDataTableCompany();
    loadDataTableUser();

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
                    loadDataTableOrder("GetOrders?status=all");
                }
            }
        }
    }
});


// Load DataTable related to Category
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
                                <a onclick=Delete("/Admin/ManageCategory/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer;">
                                    <i class="fas fa-trash-alt"></i>
                                </a>
                            </div>
                            `;
                }, "width": "60%"
            }
        ]
    });
}


// Load DataTable related to CoverType
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
                                <a onclick=Delete("/Admin/ManageCoverType/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer;">
                                    <i class="fas fa-trash-alt"></i>
                                </a>
                            </div>
                            `;
                }, "width": "60%"
            }
        ]
    });
}


// Load DataTable related to Book
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
                                <a onclick=Delete("/Admin/ManageBooks/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer;">
                                    <i class="fas fa-trash-alt"></i>
                                </a>
                            </div>
                            `;
                }, "width": "35%"
            }
        ]
    });
}


// Load DataTable related to Company
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
                                <a onclick=Delete("/Admin/ManageCompany/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer;">
                                    <i class="fas fa-trash-alt"></i>
                                </a>
                            </div>
                            `;
                }, "width": "15%"
            }
        ]
    });
}


// Load DataTable related to Slider
function loadDataTableSlider() {
    dataTable = $('#tblSlider').DataTable({
        "ajax": {
            "url": "/Admin/ManageSlider/GetAllSliders"
        },
        "columns": [
            { "data": "imageUrl", "width": "20%" },
            { "data": "title", "width": "15%" },
            { "data": "url", "width": "30%" },
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
                }, "width": "10%"
            },
            {
                "data": "id",
                "render": function (data) {
                    return `
                            <div class="text-center">
                                <a href="/Admin/ManageSlider/Upsert/${data}" class="btn btn-warning text-white" style="cursor:pointer;">
                                    <i class="fas fa-edit"></i>
                                </a>
                                <a onclick=Delete("/Admin/ManageSlider/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer;">
                                    <i class="fas fa-trash-alt"></i>
                                </a>
                            </div>
                            `;
                }, "width": "15%"
            }
        ]
    });
}


// Load DataTable related to User
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


// Load DataTable related to Order
function loadDataTableOrder(url) {
    dataTable = $('#tblOrder').DataTable({
        "ajax": {
            "url": "/Admin/ManageOrder/" + url
        },
        "columns": [
            { "data": "id", "width": "10%" },
            { "data": "applicationUser.fullName", "width": "20%" },
            { "data": "applicationUser.phoneNumber", "width": "15%" },
            { "data": "applicationUser.email", "width": "20%" },
            { "data": "orderStatus", "width": "15%" },
            { "data": "orderTotal", "width": "10%" },
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