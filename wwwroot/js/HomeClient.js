/*----------Poster----------*/
new Swiper('.slide', {
    loop: true,
    spaceBetween: 30,
  
    pagination: {
      el: '.slide .swiper-pagination',
      clickable: true,
      dynamicBullets: true
    },
  

    navigation: {
      nextEl: '.swiper-button-next',
      prevEl: '.swiper-button-prev',
    },
  
  });
/*----------Movie----------*/
new Swiper('.slide-movie', {
    slidesPerView: 3,
    spaceBetween: 40,
    loop: true,
    centerSlide:'true',
    fade: 'true',
    loopFillGroupWithBlank: true,
    pagination: {
      el: ".swiper-pagination",
      clickable: true,
      dynamicBullets: true
    },

    navigation: {
      nextEl: ".swiper-button-next",
      prevEl: ".swiper-button-prev",
    },
  });