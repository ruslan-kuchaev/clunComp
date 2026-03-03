// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// === АВТОМАТИЧЕСКИЙ СЛАЙДЕР ===
document.addEventListener('DOMContentLoaded', function() {
    const sliderTrack = document.querySelector('.slider-track');
    const slides = document.querySelectorAll('.slide');
    const dots = document.querySelectorAll('.dot');
    
    if (!sliderTrack || slides.length === 0) return;
    
    let currentSlide = 0;
    const totalSlides = slides.length;
    const slideInterval = 4000; // 4 секунды
    
    function goToSlide(index) {
        currentSlide = index;
        sliderTrack.style.transform = `translateX(-${currentSlide * 100}%)`;
        
        // Обновляем точки
        dots.forEach((dot, i) => {
            if (i === currentSlide) {
                dot.classList.add('active');
            } else {
                dot.classList.remove('active');
            }
        });
    }
    
    function nextSlide() {
        currentSlide = (currentSlide + 1) % totalSlides;
        goToSlide(currentSlide);
    }
    
    // Автоматическая прокрутка
    let autoSlide = setInterval(nextSlide, slideInterval);
    
    // Клик по точкам
    dots.forEach((dot, index) => {
        dot.addEventListener('click', () => {
            goToSlide(index);
            // Сбрасываем таймер при ручном переключении
            clearInterval(autoSlide);
            autoSlide = setInterval(nextSlide, slideInterval);
        });
    });
    
    // Пауза при наведении
    const gallerySlider = document.querySelector('.gallery-slider');
    if (gallerySlider) {
        gallerySlider.addEventListener('mouseenter', () => {
            clearInterval(autoSlide);
        });
        
        gallerySlider.addEventListener('mouseleave', () => {
            autoSlide = setInterval(nextSlide, slideInterval);
        });
    }
});

// === ИНТЕРАКТИВНАЯ КАРТА ===
document.addEventListener('DOMContentLoaded', function() {
    const mapContainer = document.getElementById('map');
    
    if (!mapContainer) return;
    
    // Координаты Москвы (замените на реальные координаты вашего клуба)
    const clubLat = 55.7558;
    const clubLng = 37.6173;
    
    // Инициализация карты
    const map = L.map('map', {
        center: [clubLat, clubLng],
        zoom: 16,
        zoomControl: true,
        scrollWheelZoom: false,
        attributionControl: false // Отключаем атрибуцию
    });
    
    // Темная тема карты
    L.tileLayer('https://{s}.basemaps.cartocdn.com/dark_all/{z}/{x}/{y}{r}.png', {
        attribution: '',
        subdomains: 'abcd',
        maxZoom: 20
    }).addTo(map);
    
    // Кастомная иконка маркера
    const customIcon = L.divIcon({
        className: 'custom-marker',
        html: '<div style="background: linear-gradient(135deg, #8b5cf6, #ec4899); width: 40px; height: 40px; border-radius: 50% 50% 50% 0; transform: rotate(-45deg); border: 3px solid #fff; box-shadow: 0 4px 12px rgba(139, 92, 246, 0.5);"><div style="transform: rotate(45deg); font-size: 20px; margin-top: 6px; margin-left: 8px;">⚡</div></div>',
        iconSize: [40, 40],
        iconAnchor: [20, 40],
        popupAnchor: [0, -40]
    });
    
    // Добавление маркера
    const marker = L.marker([clubLat, clubLng], { icon: customIcon }).addTo(map);
    
    // Popup с информацией
    marker.bindPopup(`
        <div style="font-family: 'Inter', sans-serif; padding: 10px; min-width: 200px;">
            <h3 style="margin: 0 0 10px 0; font-family: 'Rajdhani', sans-serif; font-size: 1.3rem; background: linear-gradient(135deg, #8b5cf6, #ec4899); -webkit-background-clip: text; -webkit-text-fill-color: transparent;">КИБЕРСПОРТ КЛУБ</h3>
            <p style="margin: 5px 0; color: #9ca3af;">📍 г. Москва, ул. Киберспортивная, 1</p>
            <p style="margin: 5px 0; color: #9ca3af;">📞 +7 (999) 123-45-67</p>
            <p style="margin: 5px 0; color: #9ca3af;">🕐 Круглосуточно</p>
            <a href="https://yandex.ru/maps/?pt=${clubLng},${clubLat}&z=16&l=map" target="_blank" style="display: inline-block; margin-top: 10px; padding: 8px 16px; background: linear-gradient(135deg, #8b5cf6, #ec4899); color: white; text-decoration: none; border-radius: 8px; font-weight: 600;">Построить маршрут</a>
        </div>
    `).openPopup();
    
    // Включить прокрутку колесиком при клике на карту
    map.on('click', function() {
        map.scrollWheelZoom.enable();
    });
    
    // Отключить при уходе курсора
    map.on('mouseout', function() {
        map.scrollWheelZoom.disable();
    });
});
