
$(document).ready(function () {

    $('#Customers').dataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "ajax": {
            "url": "/api/customers",
            "type": "POST",
            "datatype": "json",
            "dataSrc": function (json) {
                $('#loader').hide(); // Hide the loader when data is received
                return json.data;
            }
        },
        //visible property
        "columnDefs": [
        {
            "targets": [0],
            "visible": false,
            "searchable": false,
        },
        {
            "targets": [6],
            "sortable":false    
        },
        ],
        "columns": [
            
            { "data": "id", "name": "Id", "autowidth": true },
            { "data": "firstName", "name": "FirstName", "autowidth": true },
            { "data": "lastName", "name": "LastName", "autowidth": true },
            { "data": "contact", "name": "Contact", "autowidth": true },
            { "data": "email", "name": "Email", "autowidth": true },

            { "data": "dateOfBirth", "name": "Date Of Birth", "autowidth": true },
            //{
            //    "render": function (data, row) { return "<a href='#' class='btn btn-danger' onclick=DeleteCustomer('" + row.id + "'); >Delete</a>"; }
            //},
            {
                render: function (data, type, row) {
                    return '<a href="#" class="btn btn-danger" onclick="DeleteCustomer(' + row.id + ');">Delete</a>';
                }
            }


        ]
    });
    // Show the loader when an AJAX request starts
    $(document).ajaxStart(function () {
        $('#loader').show();
    });

    // Hide the loader when an AJAX request completes
    $(document).ajaxComplete(function () {
        $('#loader').hide();
    });
});

function DeleteCustomer(customerId) {
    if (customerId) {
        // Show confirmation dialog
        if (confirm('Are you sure you want to delete this customer?')) {
            $.ajax({
                url: '/api/customers/' + customerId,
                type: 'DELETE',
                success: function (result) {
                    // Reload the table data after successful deletion
                    $('#Customers').DataTable().ajax.reload();
                    toastr.success('Customer deleted successfully.');
                },
                error: function (errormessage) {
                    toastr.error('Error deleting customer: ' + errormessage.responseText);
                }
            });
        }
    } else {
        toastr.error('Customer ID is undefined.');
    }
}


toastr.options = {
    "closeButton": true,
    "debug": false,
    "newestOnTop": false,
    "progressBar": true,
    "positionClass": "toast-top-right",
    "preventDuplicates": false,
    "onclick": null,
    "showDuration": "300",
    "hideDuration": "1000",
    "timeOut": "5000",
    "extendedTimeOut": "1000",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
};