let dataTable;

$(document).ready(function () {
    loadDataTableCategory();
    loadDataTableCoverType();
    loadDataTableBook();
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