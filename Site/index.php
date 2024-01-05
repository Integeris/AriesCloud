<?php

namespace Site;

require './php/DB.php';
require './php/webFiles.php';
require './php/authentication.php';
require './php/scrambler.php';

use Files;
use Service\DB;
use Service\webFiles;
use Service\authentication;
use HttpContext;



$route = $_GET['route'];
$segments = explode('/', $route);
$controllerName = '';
$actionName = '';

$acces = "";

if ($segments[0] == "checkCode") {
    $urlWithData = $_SERVER['REQUEST_URI'];
    $parsedUrl = parse_url($urlWithData);
    parse_str($parsedUrl['query'], $query);
    $code = $query['code'];
    $email = $query['email'];
    $get = new DB();
    if ($get->checkCode($email, $code)) {
        echo "Код успешно прошёл проверку";
    } else {
        echo "Проверка кода провалена";
    }
    return;
}



if ($segments[0] == "main") {
    $acces = "NO";
} else {
    if (isset($_COOKIE['uid'])) {
        $acces = $_COOKIE['uid'];
        $get = new DB();
        if (!$get->auth($acces)) {
            $acces = "NO";
        }
    } else {
        $acces = $_POST['hash'];
        $get = new DB();
        if (!$get->auth($acces)) {
            $acces = "NO";
        }
    }
}


if (!empty($segments[0])) {
    $controllerName = ucfirst($segments[0]);
}
if ($segments[0] == "files" && $acces != "NO") {

    if ($segments[1] == "getFiles") {
        $get = new webFiles();
        $get->getFiles("a001f87a8a7f6c2f009d7e2f8d3c588b");
        return;
    }

    if ($segments[1] == "delFiles") {
        $get = new webFiles();
        $get->delFiles("a001f87a8a7f6c2f009d7e2f8d3c588b");
        return;
    }

    if ($segments[1] == "createFolder") {
        $get = new webFiles();
        $get->createFolder("a001f87a8a7f6c2f009d7e2f8d3c588b");
        return;
    }

    if ($segments[1] == "rename") {
        $get = new webFiles();
        $get->rename("a001f87a8a7f6c2f009d7e2f8d3c588b");
        return;
    }

    if ($segments[1] == "getDir") {
        $get = new webFiles();
        $get->getDir("a001f87a8a7f6c2f009d7e2f8d3c588b");
        return;
    }

    if ($segments[1] == "move") {
        $get = new webFiles();
        $get->move("a001f87a8a7f6c2f009d7e2f8d3c588b");
        return;
    }

    if ($segments[1] == "downloadSiteFiles") {
        $get = new webFiles();
        $get->downloadSite();
        return;
    }

    if ($segments[1] == "uploadSiteFiles") {
        $get = new webFiles();
        $get->uploadSite();
        return;
    }

    if ($segments[1] == "downloadAPI") {
        $get = new webFiles();
        $get->downloadAPI($_POST["hash"]);
        return;
    }

    if ($segments[1] == "uploadAPI") {
        $get = new webFiles();
        $get->uploadAPI($_POST["hash"]);
        return;
    }

    if ($segments[1] == "exit") {
        $get = new authentication();
        $get->exit();
        return;
    }
}
if ($segments[0] == "main" || $acces == "NO") {
    if ($segments[1] == "autorization") {
        $get = new authentication();
        $get->auth($_POST["login"], $_POST['password']);
        return;
    }

    if ($segments[1] == "autorizationHash") {
        $get = new DB();
        if ($get->auth($_POST["hash"])) {
            echo "True";
            return;
        } else {
            echo "False";
            return;
        }
    }

    if ($segments[1] == "registration") {
        $get = new authentication();
        $get->reg($_POST["login"], $_POST['password'], $_POST['email']);
        return;
    }
}



if ($segments[0] == null && $acces=="NO") {
    $url = ((!empty($_SERVER['HTTPS'])) ? 'https' : 'http') . '://' . $_SERVER['HTTP_HOST'] . $_SERVER['REQUEST_URI'];
    $url = explode('?', $url);
    $url = $url[0];
    header('Location: ' . $url . 'main');
    die();
}

if($acces!="NO" && $segments[0] == null){
    $url = ((!empty($_SERVER['HTTPS'])) ? 'https' : 'http') . '://' . $_SERVER['HTTP_HOST'] . $_SERVER['REQUEST_URI'];
    $url = explode('?', $url);
    $url = $url[0];
    header('Location: ' . $url . 'files');
    die();
}


$controllerFile = 'php/' . $controllerName . '.php';
if (file_exists($controllerFile) && ($segments[0] == "files" && $acces != 'NO'|| $segments[0]=="main")) {
    require_once $controllerFile;
    $controller = new $controllerName();
} else {
    http_response_code(404);
    echo 'Page not found';
}
