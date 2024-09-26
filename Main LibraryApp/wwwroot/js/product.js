$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    $('#myTable').DataTable({
        ajax: { url: '/Admin/Product/GetAll' },
        columns: [
            { data: 'title' },
            { data: 'isbn' },
            { data: 'author' },
            { data: 'listPrice' },
            { data: 'category.name'}
        ]
    });
}

