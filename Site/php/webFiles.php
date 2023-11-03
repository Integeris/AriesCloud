<?php

namespace Service;

class webFiles
{
    public function getFiles($uid)
    {
        $files = scandir("./fileUsers/$uid");
        $files = array_diff($files, array('.', '..'));
        echo json_encode($files);
    }

    public function delFiles($uid)
    {
        $dir = "./fileUsers/$uid/";
        $data = $_GET['dataFiles'];
        foreach ($data as $val) {
            if (is_dir($dir . $val)) {
                $this->deleteDirectory($dir . $val);
            } else {
                unlink($dir . $val);
            }
        }
    }

    private function deleteDirectory($dir)
    {
        $files = glob($dir . '/*');
        foreach ($files as $file) {
            if (is_dir($file)) {
                $this->deleteDirectory($file);
            } else {
                unlink($file);
            }
        }
        rmdir($dir);
    }

}