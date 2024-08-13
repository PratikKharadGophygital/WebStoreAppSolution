$(document).ready(function () {

    // Bind from submit event
    $('#customerUpdateForm').on('submit', function (event) {
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
    function submitEditRequest() {

        $.ajax({
            url: $('#customerUpdateForm').attr('action'),
            type: 'POST',
            data: $('#customerUpdateForm').serialize(),
            success: function (response) {

                if (response.success) {
                    NotificationModule.showSuccess('Customer updated successfully. Click to go back to the list.', response.redirectUrl);
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


