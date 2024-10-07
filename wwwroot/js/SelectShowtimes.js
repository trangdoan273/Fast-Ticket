const timeButtons = document.querySelectorAll('.time-btn');
timeButtons.forEach(button => {
    button.addEventListener('click', function() {
        timeButtons.forEach(btn => btn.classList.remove('selected'));
        this.classList.add('selected');
        console.log("Thời gian đã chọn: " + this.innerText);
    });
});

document.addEventListener('DOMContentLoaded', function () {
    const dateLinks = document.querySelectorAll('.date-link');

    dateLinks.forEach(link => {
        link.addEventListener('click', function () {

            dateLinks.forEach(l => l.classList.remove('active'));

            this.classList.add('active');
        });
    });
});

