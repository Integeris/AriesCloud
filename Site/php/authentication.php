<?php

namespace Service;

use Service\DB;

class authentication
{

    public function auth($login, $password)
    {
        $hash = md5($login . ".*." . $password);
        $db = new DB();
        if ($db->auth($hash)) {
            $time = 2 * 60 * 60;
            ini_set('session.gc_maxlifetime', $time);
            ini_set('session.cookie_lifetime', $time); 
            session_start();
            $_SESSION["hash"] = $hash;
            echo "Good";
        } else {
            echo "Who are U?";
        }
    }

    public function exit()
    {
        session_unset();
        session_destroy();
        $url = ((!empty($_SERVER['HTTPS'])) ? 'https' : 'http') . '://' . $_SERVER['HTTP_HOST'] . $_SERVER['REQUEST_URI'];
        $url = explode('?', $url);
        $url = $url[0];
        header('Location: ' . $url . 'main');
        die();
    }
}
