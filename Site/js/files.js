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
    img.className = "imgFile";
    label.className = "labelFile";
    label.innerText = value;
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
  $.ajax({
    url: window.location.href + "/getFiles",
    method: "post",
    dataType: "html",
    data: {},
    success: function (data) {
      fileGeneration(JSON.parse(data));
    },
  });
}

function delFile() {
  console.log(elem);
  var data = [];
  data.push(elem.children[1].innerText);
  $.ajax({
    url: window.location.href + "/delFiles",
    method: "post",
    dataType: "html",
    data: { dataFiles: data },
    success: function (data) {
      getFiles()
    },
  });
}

getFiles()