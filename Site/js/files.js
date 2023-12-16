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

function fileGeneration(files) {
  var parent = document.getElementById("files");
  parent.innerHTML = "";
  $.each(files, function (index, value) {
    var child = document.createElement("div");
    var img = document.createElement("img");
    var label = document.createElement("label");
    child.className = "file";
    //child.setAttribute("dblclick", "dublClick(this)");
    //child.setAttribute("onclick","preventDefaultClick(event)")
    img.className = "imgFile";
    label.className = "labelFile";
    label.innerText = value;
    label.title = value;
    var str = "../source/";
    var s;
    if (value.split(".").length > 1) {
      s = value.split(".")[value.split(".").length - 1];
    } else {
      s = "";
    }
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
      case "":
        str += "folder.png";
        break;
      default:
        str += "default.png";
        break;
    }
    img.src = str;
    child.appendChild(img);
    child.appendChild(label);
    parent.appendChild(child);
  });
}

function getFiles() {
  startLoader();
  $.ajax({
    url: window.location.href + "/getFiles",
    method: "post",
    dataType: "html",
    data: {},
    success: function (data) {
      fileGeneration(JSON.parse(data));
      stopLoader();
    },
  });
}

function delFile() {
  var data = [];
  data.push(elem.children[1].innerText);
  $.ajax({
    url: window.location.href + "/delFiles",
    method: "post",
    dataType: "html",
    data: { dataFiles: data },
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
    success: function (data) {},
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

function downloadFiles(mode) {
  if ((mode = "right")) {
    files.push(elem);
  }
  nameFiles = [];
  files.forEach((element) => {
    nameFiles.push(element.children[1].innerText);
  });
  var key = $("#key")[0].files[0];
  var way = $("#way")[0].innerText;
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
    success: function (response) {
      //console.log(response);
      var link = document.createElement("a");
        link.href = "data:application/octet-stream," + encodeURIComponent(response);
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
  var way = $("#way")[0].innerText;
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
      getFiles();
    },
  });
}

function selectedMod() {}

function selected(e) {}

function back() {}

function openFolder() {}

function createFolder() {}

function move() {}

function rename() {}

function unselected() {}
