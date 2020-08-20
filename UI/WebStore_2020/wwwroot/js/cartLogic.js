Cart = {
    _properties: {
        // Ссылка на метод добавления товара в корзину
        addToCartLink: '',
        // Ссылка на получение представления корзины
        getCartViewLink: '',
        // Ссылка на удаление товара из корзины
        removeFromCartLink: ''

    },

    init: function (properties) {
        // Копируем свойства
        $.extend(Cart._properties, properties);
        // Инициализируем перехват события

        $('a.callAddToCart').on('click', Cart.addToCart);

        // Кнопка «Удалить товар из корзины»
        $('.cart_quantity_delete').on('click', Cart.removeFromCart);

        // Кнопка «+»
        $('.cart_quantity_up').on('click', Cart.incrementItem);

    },

    // Событие добавления товара в корзину
    addToCart: function (event) {
        var button = $(this);
        // Отменяем дефолтное действие
        event.preventDefault();
        // Получение идентификатора из атрибута
        var id = button.data('id');
        // Вызов метода контроллера
        $.get(Cart._properties.addToCartLink + '/' + id).done(function () {
            // Отображаем сообщение, что товар добавлен в корзину
            Cart.showToolTip(button);
            // В случае успеха – обновляем представление
            Cart.refreshCartView();
        }).fail(function () { console.log('addToCart error'); });
    },


    refreshCartView: function () {
        // Получаем контейнер корзины
        var container = $('#cartContainer');
        // Получение представления корзины
        $.get(Cart._properties.getCartViewLink).done(function (result) {
            // Обновление html 
            container.html(result);
        }).fail(function () { console.log('refreshCartView error'); });
    },

    showToolTip: function (button) {
        // Отображаем тултип
        button.tooltip({
            title: 'Добавлено в корзину'
        }).tooltip('show');

        // Дестроим его через 0.5 секунды
        setTimeout(function () {
            button.tooltip('destroy');
        }, 500);
    },

    removeFromCart: function (event) {
        var button = $(this);
        // Отменяем дефолтное действие
        event.preventDefault();
        // Получение идентификатора из атрибута
        var id = button.data('id');
        // вызываем ajax-метод get по адресу removeFromCartLink
        $.get(Cart._properties.removeFromCartLink + '/' + id)
            .done(function () {
                button.closest('tr').remove();
                // В случае успеха – обновляем представление
                Cart.refreshCartView();
            })
            .fail(function () {
                console.log('removeFromCart error');
            });
    },

    incrementItem: function (event) {
        var button = $(this);
        // Строка товара
        var container = button.closest('tr');
        // Отменяем дефолтное действие
        event.preventDefault();
        // Получение идентификатора из атрибута
        var id = button.data('id');	

        // Вызов метода контроллера
        $.get(Cart._properties.addToCartLink + '/' + id).done(function () {
            // Получаем значение
            var value = parseInt($('.cart_quantity_input', container).val());
            // Увеличиваем его на 1
            $('.cart_quantity_input', container).val(value + 1);
            // Обновляем цену
            Cart.refreshPrice(container);
            // В случае успеха – обновляем представление
            Cart.refreshCartView();
        }).fail(function () { console.log('incrementItem error'); });
    },
    
    refreshPrice: function (container) {
        // Получаем количество
        var quantity = parseInt($('.cart_quantity_input', container).val());
        // Получаем цену
        var price = parseFloat($('.cart_price', container).data('price'));
        // Рассчитываем общую стоимость
        var totalPrice = quantity * price;
        // Для отображения в виде валюты
        var value = totalPrice.toLocaleString('en-US', { style: 'currency', currency: 'USD' });

        // Сохраняем стоимость для поля «Итого»
        $('.cart_total_price', container).data('price', totalPrice);
        // Меняем значение
        $('.cart_total_price', container).html(value);
    }

}