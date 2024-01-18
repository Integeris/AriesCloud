// Функции для аниммации

$("#buttonCheckReg").click(function () {
  var clone = $("#containerAuth");
  clone.css({
    top: $("#containerAuth").offset().top,
    left: $("#containerAuth").offset().left,
    position: "absolute",
  });

  clone.animate(
    {
      top: $("#target").offset().top,
      left: $("#target").offset().left,
      opacity: 0,
    },

    function () {
      document.getElementById("containerAuth").classList.add("non");
    }
  );

  document.getElementById("containerReg").classList.remove("non");
  document.getElementById("containerReg").style.cssText = "";
  clone.animate({
    top: $("#containerReg").offset().top,
    left: $("#containerReg").offset().left,
    opacity: 0,
  });
});

$("#buttonCheckAuth").click(function () {
  var clone = $("#containerReg");
  clone.css({
    top: $("#containerReg").offset().top,
    left: $("#containerReg").offset().left,
    position: "absolute",
  });

  clone.animate(
    {
      top: $("#target").offset().top,
      left: $("#target").offset().left,
      opacity: 0,
    },

    function () {
      document.getElementById("containerReg").classList.add("non");
    }
  );

  document.getElementById("containerAuth").classList.remove("non");
  document.getElementById("containerAuth").style.cssText = "";
  clone.animate({
    top: $("#containerAuth").offset().top,
    left: $("#containerAuth").offset().left,
    opacity: 0,
  });
});

// Функция для входа

function auth() {
  login = document.getElementById("login").value;
  password = document.getElementById("password").value;
  if (login.length < 5 || login.length > 20) {
    new Toast({
      title: false,
      text: "Размер логина должен превышать 6, но быть менее 20 символов",
      theme: "danger",
      autohide: true,
      interval: 10000,
    });
    return;
  }
  if (password.length < 5 || password.length > 20) {
    new Toast({
      title: false,
      text: "Размер пароля должен превышать 6, но быть менее 20 символов",
      theme: "danger",
      autohide: true,
      interval: 10000,
    });
    return;
  }

  const specialCharRegex = /[$&'"`@]/;
  if (specialCharRegex.test(login) || specialCharRegex.test(password)) {
    new Toast({
      title: false,
      text: "Пароль или логин содержат недопустимые символы",
      theme: "danger",
      autohide: true,
      interval: 10000,
    });
    return;
  }

  $.ajax({
    url: window.location.href + "/autorization",
    method: "post",
    dataType: "html",
    data: {
      login: login,
      password: password,
    },
    success: function (data) {
      if (data == "Good") {
        window.location.href =
          window.location.href.substring(
            0,
            window.location.href.lastIndexOf("main")
          ) + "files";
      } else {
        new Toast({
          title: false,
          text: "Аутентификация не была произведена",
          theme: "danger",
          autohide: true,
          interval: 10000,
        });
      }
    },
  });
}

// Функция для регистрации

function reg() {
  login = document.getElementById("regLogin").value;
  password = document.getElementById("regPassword").value;
  email = document.getElementById("regEmail").value;
  if (login.length >= 6 && login.length <= 20) {
    if (password.length >= 6 && password.length <= 20) {
      const uppercaseRegex = /[A-Z]/;
      const lowercaseRegex = /[a-z]/;
      const digitRegex = /[0-9]/;
      const specialCharRegex = /[!_+=-?,.]/;

      const hasUppercase = uppercaseRegex.test(password);
      const hasLowercase = lowercaseRegex.test(password);
      const hasDigit = digitRegex.test(password);
      const hasSpecialChar = specialCharRegex.test(password);
      if (hasUppercase && hasLowercase && hasDigit && hasSpecialChar) {
        if(password!=document.getElementById("regPasswordRepiat").value){
          new Toast({
            title: false,
            text: "Пароли не совпадают",
            theme: "danger",
            autohide: true,
            interval: 10000,
          });
          return
        }
        const regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        if (regex.test(email)) {
          $.ajax({
            url: window.location.href + "/registration",
            method: "post",
            dataType: "html",
            data: {
              email: email,
              password: password,
              login: login,
            },
            success: function (data) {
              if(data!="Регистрация прошла успешно, проверьте почту"){
                new Toast({
                  title: false,
                  text: data,
                  theme: "danger",
                  autohide: true,
                  interval: 10000,
                });
              }else{
                new Toast({
                  title: false,
                  text: data,
                  theme: "success",
                  autohide: true,
                  interval: 10000,
                });
              }
              document.getElementById("buttonRegB").click()
            },
          });
        } else {
          new Toast({
            title: false,
            text: "Введена некорректная почта",
            theme: "danger",
            autohide: true,
            interval: 10000,
          });
        }
      } else {
        new Toast({
          title: false,
          text: "Пароль должен содержать хотябы одну строчную букву, заглавную букву, цифру и спец символ !_+=-?,.",
          theme: "danger",
          autohide: true,
          interval: 10000,
        });
      }
    } else {
      new Toast({
        title: false,
        text: "Размер пароля должен превышать 6, но быть менее 20 символов",
        theme: "danger",
        autohide: true,
        interval: 10000,
      });
    }
  } else {
    new Toast({
      title: false,
      text: "Размер логина должен превышать 6, но быть менее 20 символов",
      theme: "danger",
      autohide: true,
      interval: 10000,
    });
  }
}
