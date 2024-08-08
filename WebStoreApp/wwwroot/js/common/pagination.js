$(document).ready(function () {
    
    $('#clearSearch').click(function () {
        $('input[name="searchTerm"]').val('');
        $('form').submit();
    });
});

