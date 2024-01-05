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
            setcookie('uid', $hash, time() + 3600,'/');
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

        $length = 8;
        $characters = '0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ';
        $randomCode = '';
        $max = strlen($characters) - 1;

        for ($i = 0; $i < $length; $i++) {
            $randomCode .= $characters[random_int(0, $max)];
        }
        $db = new DB();
        if ($db->checkDB('users', 'email', $email)) {
            $mailSMTP = new SendMailSmtpClass('AriesCloud@yandex.ru', 'nbyvastcxppngtzr', 'ssl://smtp.yandex.ru', 'AriesCloud@yandex.ru', 465);
            $url = ((!empty($_SERVER['HTTPS'])) ? 'https' : 'http') . '://' . $_SERVER['HTTP_HOST'] . $_SERVER['REQUEST_URI'];
            $url = explode('?', $url);
            $url = $url[0];
            $url = explode('/', $url);
            $url = $url[0] . "//" . $url[2] . "/";
            $headers = "MIME-Version: 1.0\r\n";
            $headers .= "Content-type: text/html; charset=utf-8\r\n";
            $headers .= "From: AriesCloud@yandex.ru\r\n";
            $result =  $mailSMTP->send($email, 'Code Verefication', $url."checkCode?code=$randomCode&email=$email", $headers);
            if ($result === true) {
                $db->updateCode($email,$randomCode);
                return "True";
            } else {
                return "Письмо не отправлено. Ошибка: " . $result;
            }
        } else {
            return "False";
        }
    }

    public function reg($login, $password, $email)
    {
        $db = new DB();
        if ($db->checkDB('users', 'email', $email)) {
            if ($db->checkDB('users', 'login', $login)) {
                if ($db->reg($login, $password, $email)) {
                    if ($this->sendMail($email)=="True")
                        echo "Регистрация прошла успешно, проверьте почту";
                    else
                        echo "Отправить письмо не удалось";
                } else {
                    echo "Регистрация закончилась ошибкой";
                }
            } else {
                echo "Логин занят";
            }
        } else {
            echo "Почтовый ящик занят";
        }
    }
}
