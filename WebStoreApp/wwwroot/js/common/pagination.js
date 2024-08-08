$(document).ready(function () {
    // Handle search button click
    $('#searchButton').click(function () {
        var searchTerm = $('#searchTerm').val();
        loadSearchResults(searchTerm, 'FirstName', 'ASC');
    });

    // Handle clear button click
    $('#clearSearch').click(function () {
        $('#searchTerm').val(''); // Clear the search input field
        loadSearchResults('', 'FirstName', 'ASC'); // Clear search results
    });

    // Handle sorting click
    $(document).on('click', '.sortable', function () {
        var sortColumn = $(this).data('sort-column');
        var sortOrder = $(this).data('sort-order') === 'ASC' ? 'DESC' : 'ASC'; // Toggle sort order
        var searchTerm = $('#searchTerm').val();

        // Update sort order in the header
        $('.sortable').data('sort-order', 'ASC'); // Reset all to ASC
        $(this).data('sort-order', sortOrder); // Set the clicked one

        loadSearchResults(searchTerm, sortColumn, sortOrder);
    });

    // Handle pagination click
    $(document).on('click', '.page-link', function () {
        var pageNumber = $(this).data('page-number');
        var pageSize = $(this).data('page-size');
        var sortColumn = $(this).data('sort-column');
        var sortOrder = $(this).data('sort-order');
        var searchTerm = $(this).data('search-term');
        loadSearchResults(searchTerm, sortColumn, sortOrder, pageNumber, pageSize);
    });

    function loadSearchResults(searchTerm, sortColumn, sortOrder, pageNumber = 1, pageSize = 10) {
        $.ajax({
            url: '/Customer/Index', // Adjust the URL to match your controller and action
            type: 'GET',
            data: {
                searchTerm: searchTerm || '', // Ensure it's an empty string
                sortColumn: sortColumn || 'FirstName',
                sortOrder: sortOrder || 'ASC',
                pageNumber: pageNumber,
                pageSize: pageSize
            },
            headers: {
                'X-Requested-With': 'XMLHttpRequest' // Indicate that this is an AJAX request
            },
            success: function (result) {
                $('#searchResults').html($(result).find('#searchResults').html());
                $('.pagination').html($(result).find('.pagination').html());
                
            },
            error: function (xhr, status, error) {
                console.error('An error occurred: ' + error);
            }
        });
    }
});
