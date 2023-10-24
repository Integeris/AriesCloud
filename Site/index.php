<?php

namespace Site;

require './php/DB.php';

use Service\DB;

$db = new DB();
$db->conn();

$route = $_GET['route'];
$segments = explode('/', $route);

$controllerName = '';
$actionName = '';

if (!empty($segments[0])) {
    $controllerName = ucfirst($segments[0]);
}
if (!empty($segments[1])) {
    $actionName = $segments[1];
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
