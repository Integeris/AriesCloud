<?php

use Service\DB;
class Files
{
    public function __construct()
    {   
        $get=new DB();

        $i=$get->getName($_COOKIE["uid"]);
        include './templates/files.html';
    }
}
