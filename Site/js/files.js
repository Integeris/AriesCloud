document.onclick = hideMenu;
document.oncontextmenu = rightClick;
function hideMenu() {
  document.getElementById("contextMenu").style.display = "none";
}
var elem;

//Упарвление нажатием на правую кнопку мыши

function rightClick(e) {
  e.preventDefault();
  if (document.getElementById("contextMenu").style.display == "block")
    hideMenu();
  else {
    var menu = document.getElementById("contextMenu");
    if (e.target.className == "file") {
      elem = e.target;
    } else {
      elem = e.target.parentElement;
    }
    menu.style.display = "block";
    menu.style.left = e.pageX + "px";
    menu.style.top = e.pageY + "px";
  }
}

var dir = "/";

//Создание из списка иконок с именем файла так же тут выбирается какая иконка будет стоять

function fileGeneration(files) {
  var parent = document.getElementById("files");
  parent.innerHTML = "";
  $.each(files, function (index, value) {
    var child = document.createElement("div");
    var img = document.createElement("img");
    var label = document.createElement("label");
    child.className = "file";
    img.className = "imgFile";
    label.className = "labelFile";
    label.innerText = value.name;
    label.title = value.name;
    var str = "../source/";
    if (value.type == "f") {
      var s;
      if (value.name.split(".").length > 1) {
        s = value.name.split(".")[value.name.split(".").length - 1];
      } else {
        s = "";
      }
      mode = "";
      switch (s) {
        case "xlsx":
          str += "xlsx.png";
          break;
        case "doc":
          str += "doc.png";
          break;
        case "docx":
          str += "docx.png";
          break;
        case "exe":
          str += "exe.png";
          break;
        case "pdf":
          str += "pdf.png";
          break;
        case "txt":
          str += "text_document.png";
          break;
        case "ods":
          str += "ods.png";
          break;
        case "cmd":
          str += "cmd.png";
          break;
        default:
          str += "default.png";
          break;
      }
    } else {
      str += "folder.png";
      child.setAttribute("onclick", "openFolder(this)");
    }
    img.src = str;
    child.appendChild(img);
    child.appendChild(label);
    mode = "";
    parent.appendChild(child);
  });
}

//Функция для получения файлов

function getFiles() {
  startLoader();
  $.ajax({
    url: window.location.href + "/getFiles",
    method: "post",
    dataType: "html",
    data: { dir: dir },
    success: function (data) {
      $("#way")[0].innerText = dir;
      fileGeneration(JSON.parse(data));
      stopLoader();
    },
  });
}

// Функция для удаления папок и файлов

function delFile() {
  if (
    elem == undefined ||
    elem.children.length == 0 ||
    elem.children[1].innerText == ""
  ) {
    new Toast({
      title: false,
      text: "Файл или папка для удаления не выбраны",
      theme: "danger",
      autohide: true,
      interval: 10000,
    });
    return;
  }
  $.ajax({
    url: window.location.href + "/delFiles",
    method: "post",
    dataType: "html",
    data: { dataFiles: elem.children[1].innerText, dir: dir },
    success: function (data) {
      getFiles();
    },
  });
}

// Функция дял выхода из системы

function exit() {
  $.ajax({
    url: window.location.href + "/exit",
    method: "post",
    dataType: "html",
    data: {},
    success: function (data) {
      window.location.href =
        window.location.href.substring(
          0,
          window.location.href.lastIndexOf("files")
        ) + "main";
    },
  });
}

// Первичное получение файлов

getFiles();

//Функция отвечающая за открыте всплывающих окон

function openWindows(name) {
  var popupWindow = document.getElementById("popup" + name);
  var popupClose = document.getElementById("close" + name);

  showPopup(popupWindow);

  popupClose.onclick = function () {
    hidePopup(popupWindow);
  };

  window.onclick = function (event) {
    if (event.target == popupWindow) {
      hidePopup(popupWindow);
    }
  };
}

function showPopup(e) {
  e.style.display = "block";
}

function hidePopup(e) {
  e.style.display = "none";
}

var files = [];
var test = "";

function compareStrings(str1, str2) {
  let byteDiff = -1;
  let diffChars = "";

  for (let i = 0; i < str1.length; i++) {
    if (str1.charCodeAt(i) !== str2.charCodeAt(i)) {
      byteDiff = i;
      diffChars += `${str1[i]}${str2[i]}`;
    }
  }

  return {
    byteDiff,
    diffChars,
  };
}

// Функция для скачивания файлов

function downloadFiles(mode) {
  if ((mode = "right")) {
    if (elem != null) files.push(elem);
  }

  var key = $("#key")[0].files[0];
  var way = dir;
  var formData = new FormData();

  if (
    elem == undefined ||
    elem.children.length == 0 ||
    elem.children[1].innerText == "" ||
    elem == null
  ) {
    new Toast({
      title: false,
      text: "Файл не выбран для скачивания",
      theme: "danger",
      autohide: true,
      interval: 10000,
    });
    return;
  }

  nameFiles = [];
  files.forEach((element) => {
    nameFiles.push(element.children[1].innerText);
  });
  var key = $("#key")[0].files[0];
  var way = dir;
  var formData = new FormData();

  if (key == null) {
    new Toast({
      title: false,
      text: "Ключ не загружен",
      theme: "danger",
      autohide: true,
      interval: 10000,
    });
    return;
  }

  formData.append("keyFile", key);
  formData.append("nameFiles", JSON.stringify(nameFiles));
  formData.append("dir", way);

  $.ajax({
    url: window.location.href + "/downloadSiteFiles",
    type: "POST",
    data: formData,
    processData: false,
    contentType: false,
    xhrFields: {
      responseType: "blob",
    },
    success: function (response) {
      var blob = new Blob([response]);
      var link = document.createElement("a");
      link.href = window.URL.createObjectURL(blob);
      link.download = nameFiles[0];
      link.click();
      files = [];
      nameFiles = [];
    },
  });
}

// Функция для загрузки фалов на сайт

function uploadFiles(mode) {
  var key = $("#key")[0].files[0];
  var file = $("#file")[0].files[0];
  var way = dir;
  var formData = new FormData();
  formData.append("keyFile", key);
  formData.append("file", file);
  formData.append("dir", way);

  if (key == null) {
    new Toast({
      title: false,
      text: "Ключ не загружен",
      theme: "danger",
      autohide: true,
      interval: 10000,
    });
    return;
  }
  if (file == null) {
    new Toast({
      title: false,
      text: "Файл не загружен",
      theme: "danger",
      autohide: true,
      interval: 10000,
    });
    return;
  }

  $.ajax({
    url: window.location.href + "/uploadSiteFiles",
    type: "POST",
    data: formData,
    processData: false,
    contentType: false,
    success: function (data) {
      test = data;
      document.getElementById("closeUpload").click();
      getFiles();
    },
  });
}

// Функция для возврата назад по дирректории

function back() {
  dir = dir.replace(/\/[^/]+\/$/, "/");
  getFiles();
}

// Функция для продвижения вглубь дирректории

function openFolder(e) {
  dir += e.children[1].innerText + "/";
  getFiles();
}

// Функция дял создания папки

function createFolder() {
  if (
    $("#inputFolder")[0].value.length < 3 ||
    $("#inputFolder")[0].value.length > 30
  ) {
    console.log(
      "Длинна имени папки должно превышать 3 символа и быть менее 30"
    );
    return;
  }
  specialCharRegex = /[$&'"`@]/;

  specialCharRegex = specialCharRegex.test($("#inputFolder")[0].value);
  console.log(specialCharRegex);
  if (specialCharRegex) {
    new Toast({
      title: false,
      text: "Имя папки должно состоять из букв и цифр",
      theme: "danger",
      autohide: true,
      interval: 10000,
    });
    return;
  }

  $.ajax({
    url: window.location.href + "/createFolder",
    method: "post",
    dataType: "html",
    data: { dir: dir, nameFolder: $("#inputFolder")[0].value },
    success: function (data) {
      if (data == "True") {
        new Toast({
          title: false,
          text: "Папка создана успешно",
          theme: "success",
          autohide: true,
          interval: 10000,
        });
        document.getElementById("closeNewFolder").click();
        getFiles();
        $("#inputFolder")[0].value=""
      } else {
        new Toast({
          title: false,
          text: "Данная папка уже существует",
          theme: "danger",
          autohide: true,
          interval: 10000,
        });
      }
    },
  });
}

//Функция для заполнения select-а дирректориями

function getDir() {
  if (
    elem == undefined ||
    elem.children.length == 0 ||
    elem.children[1].innerText == ""
  ) {
    return;
  }

  $.ajax({
    url: window.location.href + "/getDir",
    method: "post",
    dataType: "html",
    data: { dir: dir, excludeDir: elem.children[1].innerText },
    success: function (data) {
      $("#selectMove")[0].innerHTML = "";
      JSON.parse(data).forEach((element) => {
        $("#selectMove")[0].innerHTML += "<option> " + element + " </option>";
      });
    },
  });
}

//Функция для перемещения папок и файлов

function move() {
  if (
    elem == undefined ||
    elem.children.length == 0 ||
    elem.children[1].innerText == ""
  ) {
    new Toast({
      title: false,
      text: "Файл или папка не выбраны для перемещения",
      theme: "danger",
      autohide: true,
      interval: 10000,
    });
    return;
  }
  $.ajax({
    url: window.location.href + "/move",
    method: "post",
    dataType: "html",
    data: {
      dir: dir,
      oldPath: dir + "/" + elem.children[1].innerText,
      newPath: $("#selectMove")[0].value + "/" + elem.children[1].innerText,
    },
    success: function (data) {
      if (data == "True") {
        new Toast({
          title: false,
          text: "Перемещение прошло успешно",
          theme: "success",
          autohide: true,
          interval: 10000,
        });
        document.getElementById("closeMove").click();
        getFiles();
      } else {
        new Toast({
          title: false,
          text: "Во время перемещения произошла ошибка",
          theme: "danger",
          autohide: true,
          interval: 10000,
        });
      }
    },
  });
}

// Функция срабатывающая при переименовании дял ввода старого имени файла

function openRename() {
  $("#newName")[0].value = elem.children[1].innerText.replace(/\.[^/.]+$/, "");
}

//Функция переименования файлов

function rename() {
  if (
    elem == undefined ||
    elem.children.length == 0 ||
    elem.children[1].innerText == ""
  ) {
    new Toast({
      title: false,
      text: "Файл или папка не выбраны для переименования",
      theme: "danger",
      autohide: true,
      interval: 10000,
    });
    console.log();
    return;
  }

  if ($("#newName")[0].value.length < 3 || $("#newName")[0].value.length > 30) {
    new Toast({
      title: false,
      text: "Длинна имени должна превышать 3 символа и быть менее 30",
      theme: "danger",
      autohide: true,
      interval: 10000,
    });
    return;
  }

  specialCharRegex = /[$&'"`@]/;

  specialCharRegex = specialCharRegex.test($("#inputFolder")[0].value);
  console.log(specialCharRegex);
  if (specialCharRegex) {
    new Toast({
      title: false,
      text: "Имя должно состоять из букв и цифр",
      theme: "danger",
      autohide: true,
      interval: 10000,
    });
    return;
  }

  $.ajax({
    url: window.location.href + "/rename",
    method: "post",
    dataType: "html",
    data: {
      dir: dir,
      oldName: elem.children[1].innerText,
      newName: $("#newName")[0].value,
    },
    success: function (data) {
      if (data == "True") {
        new Toast({
          title: false,
          text: "Переименование прошло успешно",
          theme: "success",
          autohide: true,
          interval: 10000,
        });
        document.getElementById("closeMove").click();
        getFiles();
      } else {
        new Toast({
          title: false,
          text: "Во время переименования произошла ошибка",
          theme: "danger",
          autohide: true,
          interval: 10000,
        });
      }
    },
  });
}

//Функция смены пароля

function changePassword() {
  if ($("#newPassword")[0].value != $("#newPasswordCheck")[0].value) {
    new Toast({
      title: false,
      text: "Пароли не сходятся",
      theme: "danger",
      autohide: true,
      interval: 10000,
    });
    return;
  }

  if ($("#newPassword")[0].value.length < 6 || $("#newPassword")[0].value.length > 20) {
    new Toast({
      title: false,
      text: "Размер пароля должен привышать 6, но быть менее 20 символов",
      theme: "danger",
      autohide: true,
      interval: 10000,
    });
    return;
  }

  const uppercaseRegex = /[A-Z]/;
  const lowercaseRegex = /[a-z]/;
  const digitRegex = /[0-9]/;
  const specialCharRegex = /[!_+=-?,.]/;

  const hasUppercase = uppercaseRegex.test($("#newPassword")[0].value);
  const hasLowercase = lowercaseRegex.test($("#newPassword")[0].value);
  const hasDigit = digitRegex.test($("#newPassword")[0].value);
  const hasSpecialChar = specialCharRegex.test($("#newPassword")[0].value);
  if (!(hasUppercase && hasLowercase && hasDigit && hasSpecialChar)) {
    new Toast({
      title: false,
      text: "Пароль должен содержать хотябы одну строчную букву, заглавную букву, цифру и спец символ !_+=-?,.",
      theme: "danger",
      autohide: true,
      interval: 10000,
    });
    return;
  }

  $.ajax({
    url: window.location.href + "/changePasswordWeb",
    method: "post",
    dataType: "html",
    data: {
      newPassword: $("#newPassword")[0].value,
    },
    success: function (data) {
      if (data != "False") {
        new Toast({
          title: false,
          text: "Смена пароля прошла успешна",
          theme: "success",
          autohide: true,
          interval: 10000,
        });
        document.getElementById("closeChangePass").click();
        getFiles();
        $("#newPassword")[0].value=""
        $("#newPasswordCheck")[0].value=""
      } else {
        new Toast({
          title: false,
          text: "Во время смены пароля произошла ошибка",
          theme: "danger",
          autohide: true,
          interval: 10000,
        });
      }
    },
  });
}

//Функция для создания ключа

function createKey() {
  $.ajax({
    url: window.location.href + "/createKey",
    type: "POST",
    processData: false,
    contentType: false,
    xhrFields: {
      responseType: "blob",
    },
    success: function (response) {
      var blob = new Blob([response]);
      var link = document.createElement("a");
      link.href = window.URL.createObjectURL(blob);
      link.download = "key.key";
      link.click();
    },
  });
}
