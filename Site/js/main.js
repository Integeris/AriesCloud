$('#buttonCheckReg').click(function () {

  var clone = $('#containerAuth')
  clone.css({
    top: $('#containerAuth').offset().top,
    left: $('#containerAuth').offset().left,
    position: 'absolute'
  })

  clone.animate({
      top: $('#target').offset().top,
      left: $('#target').offset().left,
      opacity: 0
    },

    function () {
      document.getElementById("containerAuth").classList.add("non");
    })

  document.getElementById("containerReg").classList.remove("non");
  document.getElementById("containerReg").style.cssText=""
  clone.animate({
    top: $('#containerReg').offset().top,
    left: $('#containerReg').offset().left,
    opacity: 0
  })
})

$('#buttonCheckAuth').click(function () {

  var clone = $('#containerReg')
  clone.css({
    top: $('#containerReg').offset().top,
    left: $('#containerReg').offset().left,
    position: 'absolute'
  })

  clone.animate({
      top: $('#target').offset().top,
      left: $('#target').offset().left,
      opacity: 0
    },

    function () {
      document.getElementById("containerReg").classList.add("non");
    })

  document.getElementById("containerAuth").classList.remove("non");
  document.getElementById("containerAuth").style.cssText=""
  clone.animate({
    top: $('#containerAuth').offset().top,
    left: $('#containerAuth').offset().left,
    opacity: 0
  })
})