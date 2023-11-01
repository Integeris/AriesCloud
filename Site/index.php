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
        $get->getFiles();
    return;
}

if ($segments[1] == "delFiles") {
    $get = new webFiles();
    $get->delFiles();
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
    if ($get->auth($_POST["hash"]))
        return True;
    else
        return False;
}

if ($segments[0] == null) {
    $url = ((!empty($_SERVER['HTTPS'])) ? 'https' : 'http') . '://' . $_SERVER['HTTP_HOST'] . $_SERVER['REQUEST_URI'];
    $url = explode('?', $url);
    $url = $url[0];
    header('Location: ' . $url . 'main');
    die();
}
$controllerFile = 'php/' . $controllerName . '.php';
if (file_exists($controllerFile)) {
    require_once $controllerFile;
    $controller = new $controllerName();
} else {
    http_response_code(404);
    echo 'Page not found';
}