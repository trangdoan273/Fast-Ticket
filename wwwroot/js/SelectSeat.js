var button = document.getElementById('seatClick');

button.addEventListener('click', function(event){
    event.preventDefault();

    if(button.classList.contains('default')){
        button.classList.remove('default');
        button.classList.add('clicked');
    } else {
        button.classList.remove('clicked');
        button.classList.add('default');
    }
});