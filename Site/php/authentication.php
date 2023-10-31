<?php

namespace Service;
use Service\DB;

class authentication{

    public function auth($login,$password){
        $hash=md5($login.".*.".$password);
        $db= new DB();
        if($db->auth($hash)){
            echo "Good";
        }else{
            echo "Who are U?";
        }
    }
}