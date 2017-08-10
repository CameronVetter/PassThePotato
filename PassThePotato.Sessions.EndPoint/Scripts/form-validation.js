// Wait for the DOM to be ready
$(function () {
    // Initialize form validation on the registration form.
    // It has the name attribute "registration"
    $("form[name='registration']").validate({
        // Specify validation rules
        rules: {
            nameInput: {
                required: true,
                minlength: 3
            }
        },
        // Specify validation error messages
        messages: {
            nameInput: {
                required: "Please provide a name",
                minlength: "Your name must be at least 3 characters long"
            }
        },
        // Make sure the form is submitted to the destination defined
        // in the "action" attribute of the form when valid
        submitHandler: function (form) {
            var name = $('#nameInput').text();
            $('#displayname').text(name);
            connect();
        }
    });
});