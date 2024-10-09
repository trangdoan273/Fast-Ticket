/*----------Giờ----------*/
const timeButtons = document.querySelectorAll('.time-btn');
timeButtons.forEach(button => {
    button.addEventListener('click', function() {
        timeButtons.forEach(btn => btn.classList.remove('selected'));
        this.classList.add('selected');
        console.log("Thời gian đã chọn: " + this.innerText);
    });
});
/*----------Giá ghế----------*/
document.addEventListener("DOMContentLoaded", function () {
    const seats = document.querySelectorAll(".seat");
    let selectedSeats = []; 
    let totalPrice = 0;

    seats.forEach(seat => {
        seat.addEventListener("click", function () {
            const seatNumber = this.textContent;
            const seatPrice = parseFloat(this.getAttribute('data-price'));

            if (this.classList.contains("selected")) {

                this.classList.remove("selected");
                selectedSeats = selectedSeats.filter(s => s !== seatNumber);
                totalPrice -= seatPrice;
            } else {
                this.classList.add("selected");
                selectedSeats.push(seatNumber);
                totalPrice += seatPrice; 
            }

            document.querySelector('.sum-label').textContent = `Thành Tiền: ${totalPrice} VND`;

            document.getElementById('selectedSeatsInput').value = JSON.stringify(selectedSeats);
        });
    });
});
/*----------Ngày----------*/
document.addEventListener('DOMContentLoaded', function () {
    const dateLinks = document.querySelectorAll('.date-link');

    dateLinks.forEach(link => {
        link.addEventListener('click', function () {

            dateLinks.forEach(l => l.classList.remove('active'));

            this.classList.add('active');
        });
    });
});