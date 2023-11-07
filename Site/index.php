<?php

namespace Site;

require './php/DB.php';
require './php/webFiles.php';
require './php/authentication.php';

use Service\DB;
use Service\webFiles;
use Service\authentication;

$db = new DB();
$db->conn();
$route = $_GET['route'];
$segments = explode('/', $route);
$controllerName = '';
$actionName = '';


if (!empty($segments[0])) {
    $controllerName = ucfirst($segments[0]);
}

if ($segments[1] == "getFiles") {
    $get = new webFiles();
    $get->getFiles("4917b218a0c0ae2d48755a6a4accb711");
    return;
}

if ($segments[1] == "delFiles") {
    $get = new webFiles();
    $get->delFiles("a001f87a8a7f6c2f009d7e2f8d3c588b");
    return;
}

if ($segments[1] == "exit") {
    $get = new authentication();
    $get->exit();
    return;
}

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
    $get->reg($_POST["login"], $_POST['password'],$_POST['email']);
    return;
}

if ($segments[0] == null) {
    $url = ((!empty($_SERVER['HTTPS'])) ? 'https' : 'http') . '://' . $_SERVER['HTTP_HOST'] . $_SERVER['REQUEST_URI'];
    $url = explode('?', $url);
    $url = $url[0];
    header('Location: ' . $url . 'main');
    die();
}

if ($segments[0] == "checkCode") {
    $urlWithData = $_SERVER['REQUEST_URI'];
    $parsedUrl = parse_url($urlWithData);
    parse_str($parsedUrl['query'], $query);
    $code = $query['code'];
    $email = $query['email'];
    $get = new DB();
    if($get->checkCode($email,$code)){
        echo "Код успешно прошёл проверку";
    }else{
        echo "Проверка кода провалена";
    }
    return;
}

$controllerFile = 'php/' . $controllerName . '.php';
if (file_exists($controllerFile)) {
    require_once $controllerFile;
    $controller = new $controllerName();
} else {
    http_response_code(404);
    echo 'Page not found';
}
