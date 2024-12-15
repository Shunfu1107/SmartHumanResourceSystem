let selectedEmails = ['Everyone'];

// Initialize Quill editor
const quill = new Quill('#editor', {
    theme: 'snow',
    modules: {
        toolbar: [
            [{ 'header': [1, 2, false] }],
            ['bold', 'italic', 'underline'],
            ['link', 'image', 'code-block'],
            [{ 'list': 'ordered' }, { 'list': 'bullet' }],
            ['clean']
        ]
    }
});

updateSelectedEmailsDisplay();

// Prevent form submission on enter key
$('form').on('keypress', function (event) {
    if (event.keyCode === 13) {
        event.preventDefault();
        return false;
    }
});

// Handle email input
$('#email-input').on('keydown', function (event) {
    const inputValue = event.target.value.trim();

    if (event.keyCode === 13) {
        event.preventDefault();

        if (inputValue && inputValue.toLowerCase() !== 'everyone') {
            addEmail(inputValue);
            event.target.value = '';
        }
    }
});

$('#email-input').on('keyup', function (event) {
    // Only handle non-enter keypresses
    if (event.keyCode !== 13) {
        const inputValue = event.target.value.trim();
        const suggestions = getSuggestions(inputValue);
        displaySuggestions(suggestions);
    }
});

function addEmail(email) {
    // Don't allow duplicate emails
    if (!selectedEmails.includes(email)) {
        selectedEmails.push(email);
        updateSelectedEmailsDisplay();
    }
}

function removeEmail(email) {
    selectedEmails = selectedEmails.filter(e => e !== email);
    updateSelectedEmailsDisplay();
}

function updateSelectedEmailsDisplay() {
    const selectedEmailsDiv = $('#selected-emails');
    selectedEmailsDiv.empty();

    selectedEmails.forEach(email => {
        const tag = $(`
                    <div class="tag">
                        ${email}
                        <span class="remove" onclick="removeEmail('${email}')">×</span>
                    </div>
                `);
        selectedEmailsDiv.append(tag);
    });

    // Update the hidden input for form submission
    $('#notification_receiver').val(selectedEmails.join(','));
}

function displaySuggestions(suggestions) {
    const suggestionsDiv = $('#suggestions');
    suggestionsDiv.empty();

    suggestions.forEach(suggestion => {
        const suggestionItem = $(`
                    <div class="suggestion-item" onclick="addEmail('${suggestion}')">${suggestion}</div>
                `);
        suggestionsDiv.append(suggestionItem);
    });
}

function getSuggestions(input) {
    if (!input) return [];
    const mockReceivers = ["Everyone", "employee1@example.com", "employee2@example.com", "admin@example.com"];
    return mockReceivers.filter(email => email.toLowerCase().includes(input.toLowerCase()));
}

// Handle form submission
$('#submit-btn').on('click', function (event) {
    event.preventDefault();

    // Get the message content from Quill editor
    const message = quill.root.innerHTML;

    // Update the hidden input with the message content
    $('#notification_content').val(message);

    $('form').submit();
});