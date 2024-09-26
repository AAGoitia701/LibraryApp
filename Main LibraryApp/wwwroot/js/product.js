$document.ready(function () {
    loadDataTable();
});

function loadDataTable() {
    $('#myTable').DataTable({
        "ajax": url: '/admin/product/getall'
    },
        "columns": [
        { data: 'name' },
        { data: 'position' },
        { data: 'salary' },
        { data: 'office' }
        ]
    );
}

