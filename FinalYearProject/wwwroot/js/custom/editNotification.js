const emailInput = document.getElementById('email-input');
const selectedEmails = document.getElementById('selected-emails');
const notificationReceiver = document.getElementById('notification_receiver');

emailInput.addEventListener('keydown', function (event) {
    if (event.key === 'Enter') {
        event.preventDefault();
        const email = emailInput.value.trim();
        if (email) {
            const badge = document.createElement('div');
            badge.className = 'tag';
            badge.dataset.email = email;
            badge.innerHTML = `${email} <span class="remove" aria-label="Close">x</span>`;
            selectedEmails.appendChild(badge);
            emailInput.value = '';
            updateHiddenInput();
        }
    }
});

selectedEmails.addEventListener('click', function (event) {
    if (event.target.classList.contains('remove')) {
        event.target.parentElement.remove();
        updateHiddenInput();
    }
});

function updateHiddenInput() {
    const badges = selectedEmails.querySelectorAll('.tag');
    const emails = Array.from(badges).map(badge => badge.dataset.email);
    notificationReceiver.value = emails.join(',');
}

// Initialize Quill Editor
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

$('#submit-btn').on('click', function () {
    const message = quill.root.innerHTML;

    $('#notification_content').val(message);

    $('form').submit();
});