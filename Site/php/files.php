<?php

use Service\DB;
class Files
{   
    // Конструктор страницы files
    
    public function __construct()
    {   
        $get=new DB();

        $i=$get->getName($_COOKIE["uid"]);
        include './templates/files.html';
    }
}
