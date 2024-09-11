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
    //$(document).on('click', '.clearSearch', function () {
    //    $('#searchTerm').val(''); // Clear the search input field
    //    loadSearchResults('', 'FirstName', 'ASC'); // Clear search results
    //});

    $('.sortable').on('click', function () {
        var $header = $(this);
        var sortColumn = $header.data('sort-column');
        var currentSortOrder = $header.data('sort-order') || 'Ascending'; // Default to Ascending if not set
        var newSortOrder = currentSortOrder === 'Ascending' ? 'Descending' : 'Ascending'; // Toggle sort order

        // Update UI to reflect sorting state
        $('.sortable').each(function () {
            $(this).data('sort-order', 'Ascending'); // Reset all to Ascending
            $(this).find('.sort-icon').remove(); // Remove any existing sort icon
        });

        $header.data('sort-order', newSortOrder);
        var sortIcon = newSortOrder === 'Ascending' ? '<i class="fas fa-sort-asc"></i>' : '<i class="fas fa-sort-desc"></i>';
        $header.append('<span class="sort-icon">' + sortIcon + '</span>');

        // Fetch results with updated sort parameters
        loadSearchResults({
            searchTerm: $('#searchTerm').val().trim(),
            sortColumn: sortColumn,
            sortOrder: newSortOrder
        });
    });

    // Handle sorting click
    //$(document).on('click', '.sortable', function () {
    //    var $clickedHeader = $(this);
    //    var sortColumn = $clickedHeader.data('sort-column');
    //    var currentSortOrder = $clickedHeader.data('sort-order') || 'ASC'; // Default to ASC if not set
    //    var newSortOrder = currentSortOrder === 'ASC' ? 'DESC' : 'ASC'; // Toggle sort order

    //    // Reset sort order and icons for all headers
    //    $('.sortable').each(function () {
    //        $(this).data('sort-order', 'ASC'); // Reset all to ASC
    //        $(this).find('.sort-icon').remove(); // Remove any existing sort icon
    //    });

    //    // Apply the new sort order and icon to the clicked column
    //    $clickedHeader.data('sort-order', newSortOrder);
    //    var sortIcon = newSortOrder === 'ASC' ? '<i class="fas fa-sort-asc"></i>' : '<i class="fas fa-sort-desc"></i>';
    //    $clickedHeader.append('<span class="sort-icon">' + sortIcon + '</span>');

    //    // Load results with the new sorting parameters
    //    var searchTerm = $('#searchTerm').val().trim();
    //    loadSearchResults(searchTerm, sortColumn, newSortOrder);
    //});

    //$(document).on('click', '.sortable', function () {

    //    var sortColumn = $(this).data('sort-column');
    //    var sortOrder = $(this).data('sort-order') === 'ASC' ? 'DESC' : 'ASC'; // Toggle sort order
    //    var searchTerm = $('#searchTerm').val().trim();

    //    if (validateSearchTerm(searchTerm)) {
    //        // Update sort order in the header
    //        $('.sortable').data('sort-order', 'ASC'); // Reset all to ASC
    //        $(this).data('sort-order', sortOrder); // Set the clicked one

    //        loadSearchResults(searchTerm, sortColumn, sortOrder);
    //    } else {
    //        showValidationError("Invalid search term for sorting.");
    //    }
    //});

    // Handle pagination click
    $(document).on('click', '.page-link', function (e) {
        e.preventDefault();
        var pageNumber = $(this).data('page-number');
        var pageSize = $(this).data('page-size');
        var sortColumn = $(this).data('sort-column');
        var sortOrder = $(this).data('sort-order');
        var searchTerm = $('#searchTerm').val().trim();

        // Check if the search term is empty
        if (searchTerm.length === 0) {
            // If no search term and pageNumber > 1, load results for the selected page
            if (pageNumber > 1) {
                /*loadSearchResults("", pageNumber, pageSize, sortColumn, sortOrder);*/
                loadSearchResults(searchTerm, sortColumn, sortOrder, pageNumber, pageSize);
            } else {
                // If on page 1, just load the initial results without any search filter
                loadSearchResults(searchTerm, sortColumn, sortOrder, pageNumber, pageSize);
            }
        } else {
            // Validate the search term if the user has entered one
            if (validateSearchTerm(searchTerm)) {
                // Load filtered results for the search term and the correct page
                /* loadSearchResults(searchTerm, pageNumber, pageSize, sortColumn, sortOrder);*/
                loadSearchResults(searchTerm, sortColumn, sortOrder, pageNumber, pageSize);
            } else {
                // Show an error message if the search term is invalid
                showValidationError("Invalid search term.");
            }
        }
        
    });

    $(document).on('click', '.searchButton', function () {
        const searchTerm = $('#searchTerm').val().trim();
        const sortColumn = $('.sortable').data('sort-column'); // Default or selected
        const sortOrder = $('.sortable').data('sort-order'); // Default or selected

        loadSearchResults(searchTerm, sortColumn, sortOrder);
    });

    $(document).on('click', '.clearSearch', function () {
        $('#searchTerm').val('');
        const sortColumn = $('.sortable').data('sort-column'); // Default or selected
        const sortOrder = $('.sortable').data('sort-order'); // Default or selected

        loadSearchResults('', sortColumn, sortOrder);
    });



    // Function to load search results with AJAX
    function loadSearchResults(searchTerm, sortColumn, sortOrder, pageNumber = 1, pageSize = 10) {
        $.ajax({
            url: ajaxUrl,
            type: 'GET',
            data: {
                searchTerm: searchTerm || '',
                sortColumn: sortColumn || 'FirstName',
                sortOrderColumn: sortOrder || 'ASC',
                pageNumber: pageNumber,
                pageSize: pageSize,
                _: new Date().getTime()
            },
            headers: {
                'X-Requested-With': 'XMLHttpRequest'
            },
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            beforeSend: function () {
                // Disable pagination and show loading spinner
                $('.page-link').addClass('disabled');
                showLoadingSpinner();
            },
            success: function (result) {
                // Update the search results and pagination content
                $('#searchResults').html($(result).find('#searchResults').html());
                $('.pagination').html($(result).find('.pagination').html());
            },
            complete: function () {
                // Re-enable pagination and hide loading spinner
                $('.page-link').removeClass('disabled');
                hideLoadingSpinner();
            },
            error: function (xhr, status, error) {
                console.error('Error: ' + error);
                showValidationError("Failed to load results. Please try again.");
            }
        });
    }

    //function loadSearchResults(searchTerm, sortColumn, sortOrder, pageNumber = 1, pageSize = 10) {
    //    $.ajax({
    //        url: ajaxUrl, // Use the dynamic URL from Razor
    //        type: 'GET',
    //        data: {
    //            searchTerm: searchTerm || '', // Ensure it's an empty string
    //            sortColumn: sortColumn || 'FirstName',
    //            sortOrder: sortOrder || 'ASC',
    //            pageNumber: pageNumber,
    //            pageSize: pageSize
    //        },
    //        headers: {
    //            'X-Requested-With': 'XMLHttpRequest' // Indicate that this is an AJAX request
    //        },
    //        beforeSend: function () {
    //            // Show a loading spinner from AdminLTE or disable the search button
    //            $('.searchButton').prop('disabled', true);
    //            showLoadingSpinner();
    //        },
    //        success: function (result) {
    //            $('#searchResults').html($(result).find('#searchResults').html());
    //            $('.pagination').html($(result).find('.pagination').html());
    //            $('.searchButton').prop('disabled', false); // Re-enable the search button
    //            hideLoadingSpinner();
    //        },
    //        error: function (xhr, status, error) {
    //            console.error('An error occurred: ' + error);
    //            $('.searchButton').prop('disabled', false);
    //            hideLoadingSpinner();
    //        }
    //    });
    //}

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
