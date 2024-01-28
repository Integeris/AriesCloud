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

//Обработчик для кода из письма

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

// Обработчик для переадрисации авторизованными пользователям 

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

// Обработчик дял распеределения фукций файлов и страницы входа

if ($segments[0] == "files" && $acces != "NO" && !empty($segments[1])) {

    //Обработчик дял получения файлов

    if ($segments[1] == "getFiles") {
        $get = new webFiles();
        $get->getFiles($acces);
        return;
    }

    //Обработчик для удаления файлов

    if ($segments[1] == "delFiles") {
        $get = new webFiles();
        $get->delFiles($acces);
        return;
    }

    //Обработчик для создания папки

    if ($segments[1] == "createFolder") {
        $get = new webFiles();
        $get->createFolder($acces);
        return;
    }

    //Обработчик для переименования

    if ($segments[1] == "rename") {
        $get = new webFiles();
        $get->rename($acces);
        return;
    }

    //Обработчик для получения дирректорий

    if ($segments[1] == "getDir") {
        $get = new webFiles();
        $get->getDir($acces);
        return;
    }

    //Обработчик для получения всех дирректорий

    if ($segments[1] == "getAllDir") {
        $get = new webFiles();
        $get->getAllDir($acces);
        return;
    }

    //Обработчик для перемещения

    if ($segments[1] == "move") {
        $get = new webFiles();
        $get->move($acces);
        return;
    }

    //Обработчик для скачиваняи файлов через сайт

    if ($segments[1] == "downloadSiteFiles") {
        $get = new webFiles();
        $get->downloadSite($acces);
        return;
    }

    //Обработчик для загрузки файлов через сайт

    if ($segments[1] == "uploadSiteFiles") {
        $get = new webFiles();
        $get->uploadSite($acces);
        return;
    }

    //Обработчик для скачивания файлов через АПИ

    if ($segments[1] == "downloadAPI") {
        $get = new webFiles();
        $get->downloadAPI($_POST["hash"]);
        return;
    }

    //Обработчик для загрузки файлов через АПИ

    if ($segments[1] == "uploadAPI") {
        $get = new webFiles();
        $get->uploadAPI($_POST["hash"]);
        return;
    }

    //Обработчик для выхода с сайта

    if ($segments[1] == "exit") {
        $get = new authentication();
        $get->exit();
        return;
    }

    //Обработчик для смены пароля через АПИ

    if ($segments[1] == "changePasswordAPI") {
        $get = new authentication();
        $ret = $get->changePassword($_POST["hash"]);
        if ($ret) {
            echo $ret;
            return;
        } else {
            echo "False";
            return;
        }
    }

    //Обработчик для смены пароля через Сайт

    if ($segments[1] == "changePasswordWeb") {
        $get = new authentication();
        $ret = $get->changePassword($acces);
        if ($ret) {
            setcookie('uid', $ret, time() + 3600, '/');
            echo "True";
            return;
        } else {
            echo "False";
            return;
        }
    }

    //Обработчик для создания ключа

    if ($segments[1] == "createKey") {
        $get = new webFiles();
        $get->createKey();
        return;
    }
}
if (($segments[0] == "main" || $acces == "NO") && !empty($segments[1])) {

    //Обработчик для авторизации

    if ($segments[1] == "autorization") {
        $get = new authentication();
        $get->auth($_POST["login"], $_POST['password']);
        return;
    }

    //Обработчик для авторизации через АПИ

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

    //Обработчик для регистрации

    if ($segments[1] == "registration") {
        $get = new authentication();
        $get->reg($_POST["login"], $_POST['password'], $_POST['email']);
        return;
    }
}

//Обработчик для переадрисации на main если пользователь не авторизован

if ($segments[0] == null && $acces == "NO") {
    $url = ((!empty($_SERVER['HTTPS'])) ? 'https' : 'http') . '://' . $_SERVER['HTTP_HOST'] . $_SERVER['REQUEST_URI'];
    $url = explode('?', $url);
    $url = $url[0];
    header('Location: ' . $url . 'main');
    die();
}

//Обработчик для переадрисации на failes если пользователь авторизован

if ($acces != "NO" && $segments[0] == null) {
    $url = ((!empty($_SERVER['HTTPS'])) ? 'https' : 'http') . '://' . $_SERVER['HTTP_HOST'] . $_SERVER['REQUEST_URI'];
    $url = explode('?', $url);
    $url = $url[0];
    header('Location: ' . $url . 'files');
    die();
}

//Обработчик страниц

$controllerFile = 'php/' . $controllerName . '.php';
if (file_exists($controllerFile) && ($segments[0] == "files" && $acces != 'NO' || $segments[0] == "main")) {
    require_once $controllerFile;
    $controller = new $controllerName();
} else {
    http_response_code(404);
    echo 'Page not found';
}
