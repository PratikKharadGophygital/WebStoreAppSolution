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

    $('#customerForm').on('submit', function (event) {
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
            url: $('#customerForm').attr('action'),
            type: 'POST',
            data: $('#customerForm').serialize(),
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

    // Function to validate the from 
    function validateForm() {

        let isValid = true;

        // Clear pervious error 
        $('.text-danger').text('');

        // Validate first name
        const firstName = $('#FirstName').val().trim();
        if (!firstName) {
            isValid = false;
            $('#FirstNameError').text('First name is rquired.');
        }

        // Validate last name
        const lastName = $('#LastName').val().trim();
        if (!lastName) {
            isValid = false;
            $('#LastNameError').text('Last name is required.');
        }

        // Validate Email
        const email = $('#Email').val().trim();
        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        if (!email || !emailRegex.test(email)) {
            isValid = false;
            $('#EmailError').text('A valid email address is required.');
        }

        // Validate Phone Number
        const phoneNumber = $('#PhoneNumber').val().trim();
        const phoneRegex = /^\+?[1-9]\d{1,14}$/;
        if (phoneNumber && !phoneRegex.test(phoneNumber)) {
            isValid = false;
            $('#PhoneNumberError').text('Invalid phone number format.');
        }

        // Validate Address
        const address = $('#Address').val().trim();
        if (!address) {
            isValid = false;
            $('#AddressError').text('Address is required.');
        }

        return isValid;

    }


    // document ready this line end here.
});


