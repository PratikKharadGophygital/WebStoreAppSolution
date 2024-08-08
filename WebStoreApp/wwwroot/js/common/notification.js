var NotificationModule = (function () {
    

    function showSuccess(message, redirectUrl) {
        
        // Set the message and redirect URL
        $('#successMessage').text(message);
        $('#redirectButton').off('click').on('click', function () {
            window.location.href = redirectUrl;
        });

        // Show the modal
        $('#successModal').modal('show');
    }

    function showError(message) {

        $('#errorMessage').text(message);
        $('#errorModal').modal('show');

    }

    return {
        showSuccess: showSuccess,
        showError: showError
    };
})();