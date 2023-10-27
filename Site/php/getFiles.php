<?php
namespace Service;

class getFiles
{
    public function getFiles()
    {
        $files=scandir("./fileUsers");
        $files = array_diff($files, array('.', '..'));
        echo json_encode($files);
    }
}
?>