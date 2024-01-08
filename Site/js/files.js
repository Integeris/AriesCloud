document.onclick = hideMenu;
document.oncontextmenu = rightClick;
function hideMenu() {
  document.getElementById("contextMenu").style.display = "none";
}
var elem;
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
    console.log(elem);
    menu.style.display = "block";
    menu.style.left = e.pageX + "px";
    menu.style.top = e.pageY + "px";
  }
}

var dir = "/";

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

function delFile() {
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

getFiles();

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

function downloadFiles(mode) {
  if ((mode = "right")) {
    files.push(elem);
  }
  nameFiles = [];
  files.forEach((element) => {
    nameFiles.push(element.children[1].innerText);
  });
  var key = $("#key")[0].files[0];
  var way = dir;
  var formData = new FormData();
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
      console.log(response);
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

function uploadFiles(mode) {
  var key = $("#key")[0].files[0];
  var file = $("#file")[0].files[0];
  var way = dir;
  var formData = new FormData();
  formData.append("keyFile", key);
  formData.append("file", file);
  formData.append("dir", way);
  $.ajax({
    url: window.location.href + "/uploadSiteFiles",
    type: "POST",
    data: formData,
    processData: false,
    contentType: false,
    success: function (data) {
      test = data;
      getFiles();
    },
  });
}

function selectedMod() {}

function selected(e) {}

function back() {
  dir = dir.replace(/\/[^/]+\/$/, "/");
  getFiles();
}

function openFolder(e) {
  dir += e.children[1].innerText + "/";
  getFiles();
}

function createFolder() {
  $.ajax({
    url: window.location.href + "/createFolder",
    method: "post",
    dataType: "html",
    data: { dir: dir, nameFolder: $("#inputFolder")[0].value },
    success: function (data) {
      getFiles();
    },
  });
}

function getDir() {
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

function move() {
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
      getFiles();
    },
  });
}

function openRename() {
  $("#newName")[0].value = elem.children[1].innerText.replace(/\.[^/.]+$/, '');
}

function rename() {
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
      getFiles();
    },
  });
}

function changePassword(){
  $.ajax({
    url: window.location.href + "/changePasswordWeb",
    method: "post",
    dataType: "html",
    data: {
      newPassword: $("#newPassword")[0].value,
    },
    success: function (data) {
      console.log(data);
    },
  });
}

function unselected() {}
