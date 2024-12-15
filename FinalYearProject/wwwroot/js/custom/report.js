let selectedEmails = [];

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

$('#submit-btn').on('click', function (event) {
    event.preventDefault();

    const message = quill.root.innerHTML;

    $('#report_content').val(message);

    $('form').submit();
});

$('form').on('keypress', function (event) {
    if (event.keyCode === 13) {
        event.preventDefault();
        return false;
    }
});

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
    if (event.keyCode !== 13) {
        const inputValue = event.target.value.trim();
        const suggestions = getSuggestions(inputValue);
        displaySuggestions(suggestions);
    }
});

function addEmail(email) {
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

    $('#report_receiver').val(selectedEmails.join(','));
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

// File upload preview and validation
$('input[name="attachments"]').on('change', function () {
    const files = $(this)[0].files;
    // Have 10MB limit
    const maxSize = 10 * 1024 * 1024; 

    for (let i = 0; i < files.length; i++) {
        if (files[i].size > maxSize) {
            alert(`File ${files[i].name} is too large. Maximum size is 10MB.`);
            $(this).val('');
            return;
        }
    }
});

function confirmDelete() {
    return confirm("Are you sure you want to delete this attachment?");
}