<?php

namespace Service;

use Service\DB;
use PHPMailer\PHPMailer\PHPMailer;
use PHPMailer\PHPMailer\Exception;
require_once "SendMailSmtpClass.php";
use SendMailSmtpClass;

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

    public function sendMail($email)
    {
        $length=8;
        $characters = '0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ';
        $randomCode = '';
        $max = strlen($characters) - 1;
        
        for ($i = 0; $i < $length; $i++) {
            $randomCode .= $characters[random_int(0, $max)];
        }
        $db = new DB();
        $db->regCode($randomCode,$email);



        $mailSMTP = new SendMailSmtpClass('AriesCloud@yandex.ru', 'nbyvastcxppngtzr', 'ssl://smtp.yandex.ru', 'AriesCloud@yandex.ru', 465);

        $headers = "MIME-Version: 1.0\r\n";
        $headers .= "Content-type: text/html; charset=utf-8\r\n";
        $headers .= "From: AriesCloud@yandex.ru\r\n";
        $result =  $mailSMTP->send($email, 'Code Verefication', $randomCode, $headers); 
        if ($result === true) {
            return True;
        } else {
            echo "Письмо не отправлено. Ошибка: " . $result;
        }
    }

    

}
