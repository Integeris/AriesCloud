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

function auth() {
  login = document.getElementById("login").value;
  password = document.getElementById("password").value;
  if (!(login >= 6 && login <= 20)) {
    console.log("Размер логина должен привышать 6, но быть менее 20 символов")
    return
  }
  if (!(password >= 6 && password <= 20)) {
    console.log("Размер пароля должен привышать 6, но быть менее 20 символов")
    return
  }
  
  const specialCharRegex = /[$&'"`@]/;
  if(!(specialCharRegex.test(login)&&specialCharRegex.test(password))){
    console.log("Пароль или логин содержат недопустимые символы")
    return
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
      }
    },
  });
}

function reg() {
  login = document.getElementById("regLogin").value;
  password = document.getElementById("regPassword").value;
  email = document.getElementById("regEmail").value;
  if (login >= 6 && login <= 20) {
    if (password >= 6 && password <= 20) {
      const uppercaseRegex = /[A-Z]/;
      const lowercaseRegex = /[a-z]/;
      const digitRegex = /[0-9]/;
      const specialCharRegex = /[!_+=-?,.]/;

      const hasUppercase = uppercaseRegex.test(password);
      const hasLowercase = lowercaseRegex.test(password);
      const hasDigit = digitRegex.test(password);
      const hasSpecialChar = specialCharRegex.test(password);
      if (hasUppercase && hasLowercase && hasDigit && hasSpecialChar) {
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
              console.log(data);
            },
          });
        } else {
          console.log("Введена неправильная почта");
        }
      } else {
        console.log(
          "Пароль должен содержать хотябы одну строчную букву, заглавную букву, цифру и спец символ !_+=-?,."
        );
      }
    } else {
      console.log(
        "Размер пароля должен привышать 6, но быть менее 20 символов"
      );
    }
  } else {
    console.log("Размер логина должен привышать 6, но быть менее 20 символов");
  }
}
