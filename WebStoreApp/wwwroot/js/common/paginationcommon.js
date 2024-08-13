$(document).ready(function () {
    // Handle search button click
    $(document).on('click', '.searchButton', function () {
        var searchTerm = $('#searchTerm').val().trim();
        if (validateSearchTerm(searchTerm)) {
            loadSearchResults(searchTerm, 'FirstName', 'ASC');
        } else {
            // Display an error if validation fails
            showValidationError("Please enter a valid search term.");
        }
    });

    // Handle clear button click
    $(document).on('click', '.clearSearch', function () {
        $('#searchTerm').val(''); // Clear the search input field
        loadSearchResults('', 'FirstName', 'ASC'); // Clear search results
    });

    // Handle sorting click
    $(document).on('click', '.sortable', function () {
        var sortColumn = $(this).data('sort-column');
        var sortOrder = $(this).data('sort-order') === 'ASC' ? 'DESC' : 'ASC'; // Toggle sort order
        var searchTerm = $('#searchTerm').val().trim();

        if (validateSearchTerm(searchTerm)) {
            // Update sort order in the header
            $('.sortable').data('sort-order', 'ASC'); // Reset all to ASC
            $(this).data('sort-order', sortOrder); // Set the clicked one

            loadSearchResults(searchTerm, sortColumn, sortOrder);
        } else {
            showValidationError("Invalid search term for sorting.");
        }
    });

    // Handle pagination click
    $(document).on('click', '.page-link', function () {
        var pageNumber = $(this).data('page-number');
        var pageSize = $(this).data('page-size');
        var sortColumn = $(this).data('sort-column');
        var sortOrder = $(this).data('sort-order');
        var searchTerm = $('#searchTerm').val().trim();

        if (validateSearchTerm(searchTerm)) {
            loadSearchResults(searchTerm, sortColumn, sortOrder, pageNumber, pageSize);
        } else {
            showValidationError("Invalid search term for pagination.");
        }
    });

    // Function to load search results with AJAX
    function loadSearchResults(searchTerm, sortColumn, sortOrder, pageNumber = 1, pageSize = 10) {
        $.ajax({
            url: ajaxUrl, // Use the dynamic URL from Razor
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
            beforeSend: function () {
                // Show a loading spinner from AdminLTE or disable the search button
                $('.searchButton').prop('disabled', true);
                showLoadingSpinner();
            },
            success: function (result) {
                $('#searchResults').html($(result).find('#searchResults').html());
                $('.pagination').html($(result).find('.pagination').html());
                $('.searchButton').prop('disabled', false); // Re-enable the search button
                hideLoadingSpinner();
            },
            error: function (xhr, status, error) {
                console.error('An error occurred: ' + error);
                $('.searchButton').prop('disabled', false);
                hideLoadingSpinner();
            }
        });
    }

    // Function to validate the search term
    function validateSearchTerm(searchTerm) {
        // Example validation: search term must be alphanumeric and at least 2 characters long
        var regex = /^[a-zA-Z0-9\s]{2,}$/;
        return regex.test(searchTerm);
    }

    // Function to display validation error
    function showValidationError(message) {
        // Use AdminLTE's Toast or Alert component to display the error
        $(document).Toasts('create', {
            class: 'bg-danger',
            title: 'Validation Error',
            body: message
        });
    }

    // Function to show loading spinner
    function showLoadingSpinner() {
        // Example: Show a spinner in the search area or next to the button
        $('.searchButton').append('<i class="fas fa-spinner fa-spin ml-2"></i>');
    }

    // Function to hide loading spinner
    function hideLoadingSpinner() {
        $('.searchButton').find('i.fas.fa-spinner').remove();
    }
});
