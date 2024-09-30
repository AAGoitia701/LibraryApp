
var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#myTable').DataTable({
        ajax: { url: '/Admin/Product/GetAll' },
        columns: [
            { data: 'title' },
            { data: 'isbn' },
            { data: 'author' },
            { data: 'listPrice' },
            { data: 'category.name' },
            {
                data: 'id',
                'render': function (data) {
                    return `<div class="w-75 btn-group" role="group">
                            <a href="/admin/product/upsert?id=${data}"  class="btn btn-secondary text-info btn-sm ">
                                <i class="bi bi-pencil-square"></i> Edit
                            </a>
                            <a onClick="Delete('/admin/product/RemoveOne/${data}')" class="btn btn-secondary text-danger btn-sm ">
                                <i class="bi bi-trash3-fill"></i> Delete </a>
                            </div>`
                }
            }
        ]
    });
}

function Delete(url) {
    Swal.fire({
        title: "Are you sure you want to delete?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                        dataTable.ajax.reload();
                        toastr.success(data.message);
                    
                }
               

            })
        }
    });
}

