$(document).ready(function () {

    // Bind from submit event
    $('#productUpdateForm').on('submit', function (event) {
        event.preventDefault();
        if (validateForm()) {
            submitEditRequest();
        } else {
            event.preventDefault();
        }

    });

    $('#productForm').on('submit', function (event) {
        event.preventDefault();
        if (validateForm()) {
            submitAddRequest();

        } else {
            event.preventDefault();
        }
    });

    // Function to handle Ajax request
    //function submitEditRequest() {

    //    $.ajax({
    //        url: $('#productUpdateForm').attr('action'),
    //        type: 'POST',
    //        data: $('#productUpdateForm').serialize(),
    //        success: function (response) {

    //            if (response.success) {
    //                console.log('This is url redired request', response.redirectUrl);
    //                NotificationModule.showSuccess('Product updated successfully. Click to go back to the list.', response.redirectUrl);

    //            } else {
    //                NotificationModule.showError(response.message);
    //            }
    //        },
    //        error: function () {
    //            $('.alert').remove();
    //            $('#notificationContainer').html('<div class="alert alert-danger">An error occurred while updating the user.</div>');
    //        }
    //    });
    //}

    function submitEditRequest() {
        $.ajax({
            url: $('#productUpdateForm').attr('action'), // URL for the form submission
            type: 'POST',
            data: $('#productUpdateForm').serialize(), // Serialize form data
            contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
            headers: {
                'X-Requested-With': 'XMLHttpRequest', // This helps the server distinguish between AJAX and regular requests
                'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val() // CSRF token
            },
            success: function (response) {

                if (response.success) {
                    NotificationModule.showSuccess('Product updated successfully. Click to go back to the list.', response.redirectUrl);

                } else {
                    NotificationModule.showError(response.message);
                }
            },
            error: function (xhr, status, error) {
                $('.alert').remove();
                $('#notificationContainer').html(`<div class="alert alert-danger">An error occurred: ${xhr.responseText || error}</div>`);
            }
        });
    }



    function submitAddRequest() {

        $.ajax({
            url: $('#productForm').attr('action'),
            type: 'POST',
            data: $('#productForm').serialize(),
            success: function (response) {

                if (response.success) {
                    NotificationModule.showSuccess('Product updated successfully. Click to go back to the list.', response.redirectUrl);
                } else {
                    NotificationModule.showError(response.message);
                }
            },
            error: function () {
                $('.alert').remove();
                $('#notificationContainer').html('<div class="alert alert-danger">An error occurred while updating the user.</div>');
            }
        });
    }

    // Function to validate the from 
    function validateForm() {

        let isValid = true;

        // Clear pervious error 
        $('.text-danger').text('');

        // Validate first name
        const name = $('#Name').val().trim();
        if (!name) {
            isValid = false;
            $('#NameError').text('Name is rquired.');
        }

        
        const code = $('#Code').val().trim();
        if (!code) {
            isValid = false;
            $('#CodeError').text('Code name is required.');
        }

        const price = $('#Price').val().trim();
        if (!price) {
            isValid = false;
            $('#PriceError').text('Price name is required.');
        }

        
        // Validate Address
        const quantity = $('#Quantity').val().trim();
        if (!quantity) {
            isValid = false;
            $('#QuantityError').text('Quantity is required.');
        }

        return isValid;

    }


    // document ready this line end here.
});


