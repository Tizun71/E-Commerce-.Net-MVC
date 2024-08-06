var typingTimer;                
var doneTypingInterval = 500;   

document.getElementById('searchInput').addEventListener('keyup', function () {
    clearTimeout(typingTimer);
    typingTimer = setTimeout(doneTyping, doneTypingInterval);
});

document.getElementById('searchInput').addEventListener('keydown', function () {
    clearTimeout(typingTimer);
});

function doneTyping() {
    document.getElementById('formSearch').submit();
}

document.addEventListener('DOMContentLoaded', (event) => {
    var input = document.getElementById('searchInput');
    input.focus();
    input.setSelectionRange(input.value.length, input.value.length);
});