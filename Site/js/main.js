$('#buttonCheckReg').click(function() {
    //создаем копию объекта:
      var clone = $('#containerAuth')
      
      //задаем первоначальную позицию:
      clone.css({
        top: $('#containerAuth').offset().top,
        left: $('#containerAuth').offset().left,
        position: 'absolute'
      })
      

      //анимируем к позиции цели:
      clone.animate({
        top: $('#target').offset().top,
        left: $('#target').offset().left,
        opacity: 0 //с уменьшением прозрачности
      },
      //по завершении анимации удаляем элемент:
      function() {
        $('#containerAuth').classList.add("non")
      })
      document.getElementById("containerReg").classList.remove("non");
    })