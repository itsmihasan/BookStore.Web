// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener('DOMContentLoaded', function () {
    var tableId = '#booksTable';
    var dataTable = $(tableId);

    if (dataTable && !$.fn.DataTable.isDataTable(tableId)) {
        dataTable.DataTable({
            scrollY: '38vh',
            scrollCollapse: true,
        }).addClass('bs-select');
    }
});


