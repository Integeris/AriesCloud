<?php

namespace Site;

require './php/DB.php';
require './php/getFiles.php';

use Service\DB;
use Service\getFiles;

$db = new DB();
$db->conn();

$route = $_GET['route'];
$segments = explode('/', $route);

$controllerName = '';
$actionName = '';
if (!empty($segments[0])) {
    $controllerName = ucfirst($segments[0]);
}
if ($segments[1]=="getFiles") {
    $get=new getFiles();
    $get->getFiles();
    return;
}
if ($segments[0] == null) {
    $url = ((!empty($_SERVER['HTTPS'])) ? 'https' : 'http') . '://' . $_SERVER['HTTP_HOST'] . $_SERVER['REQUEST_URI'];
    $url = explode('?', $url);
    $url = $url[0];
    header('Location: '.$url.'main');
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
