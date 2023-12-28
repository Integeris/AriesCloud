<?php

namespace Service;

use Service\Scrambler;
use ZipArchive;


class webFiles
{
    public function getFiles($uid)
    {
        $files = scandir("./fileUsers/$uid/" . $_POST["dir"]);
        $files = array_diff($files, array('.', '..'));
        echo json_encode($files);
    }

    public function delFiles($uid)
    {
        $dir = "./fileUsers/$uid/" . $_POST["dir"]."/";
        $data = $_POST['dataFiles'];

        if (is_dir($dir . $data)) {
            $this->deleteDirectory($dir . $data);
        } else {
            unlink($dir . $data);
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

    public function uploadSite()
    {
        $keyFile = $_FILES['keyFile'];
        $fileToEncrypt = $_FILES['file'];

        $key = file_get_contents($keyFile['tmp_name']);
        $key = unpack('C*', $key);

        $file = file_get_contents($fileToEncrypt['tmp_name']);

        $file = unpack('C*', $file);
        $encryptedFileContent = array_chunk($file, 16);

        $lastArray = end($encryptedFileContent);
        if (count($lastArray) == 16) {
            $arr = [];
            for ($i = 0; $i < 15; $i++)
                $arr[] = 0;
            $arr[] = 16;
            $encryptedFileContent[] = $arr;
        } else if (count($lastArray) < 16) {
            $k = 16 - count($lastArray);
            for ($i = 0; $i < $k - 1; $i++)
                $lastArray[] = 0;
            $lastArray[] = $k;
            array_pop($encryptedFileContent);
            array_push($encryptedFileContent, $lastArray);
        }

        $scram = new Scrambler($key);
        for ($i = 0; $i < count($encryptedFileContent); $i++) {
            $encryptedFileContent[$i] = $scram->encriptBlock($encryptedFileContent[$i]);
        }

        $endArr = [];
        foreach ($encryptedFileContent as $str) {
            foreach ($str as $val) {
                $endArr[] = $val;
            }
        }
        $encryptedFileContent = $endArr;

        $encryptedFileContent = pack('C*', ...$encryptedFileContent);

        $encryptedFilePath = "./fileUsers/a001f87a8a7f6c2f009d7e2f8d3c588b/" . $_POST["dir"] . "/" . $fileToEncrypt['name'];
        file_put_contents($encryptedFilePath, $encryptedFileContent);
    }

    public function downloadSite()
    {
        $keyFilePath = $_FILES['keyFile'];
        $files = json_decode($_POST['nameFiles']);
        $key = file_get_contents($keyFilePath['tmp_name']);
        $key = unpack('C*', $key);

        if (isset($_POST['nameFiles'])) {
            ini_set('max_execution_time', 0);
            $count = count($files);

            if ($count === 1) {
                $file = $files[0];
                $fileName = $file;
                $file = file_get_contents("./fileUsers/a001f87a8a7f6c2f009d7e2f8d3c588b/" . $_POST["dir"] . "/" . $file);
                $fileTest = $file;
                $file = unpack('C*', $file);
                $encryptedFileContent = array_chunk($file, 16);

                $scram = new Scrambler($key);
                for ($i = 0; $i < count($encryptedFileContent); $i++) {
                    $encryptedFileContent[$i] = $scram->decriptBlock($encryptedFileContent[$i]);
                }


                $lastArray = end($encryptedFileContent);
                if (end($lastArray) == 16) {
                    array_pop($encryptedFileContent);
                } else if (end($lastArray) < 16) {
                    for ($i = end($lastArray); $i > 0; $i--) {
                        array_pop($lastArray);
                    }
                    array_pop($encryptedFileContent);
                    array_push($encryptedFileContent, $lastArray);
                }

                $endArr = [];
                foreach ($encryptedFileContent as $str) {
                    foreach ($str as $val) {
                        $endArr[] = $val;
                    }
                }
                $encryptedFileContent = $endArr;
                $encryptedFileContent = pack('C*', ...$encryptedFileContent);

                header('Content-Type: application/octet-stream');
                header('Content-Disposition: attachment; filename="' . $fileName . '"');

                echo ($encryptedFileContent);
                exit;
            } else {

                $zip = new ZipArchive();
                $zipName = 'archive.zip';

                if ($zip->open($zipName, ZipArchive::CREATE) === true) {
                    foreach ($files as $file) {
                        $fileContents = fopen("./fileUsers/a001f87a8a7f6c2f009d7e2f8d3c588b/" . $_POST["dir"] . "/" . $file, 'rb');
                        $encryptedFileContent = [];
                        $scram = new Scrambler($key);
                        while (!feof($fileContents)) {

                            $block = fread($fileContents, 16);
                            if (strlen($block) == 0) {
                                break;
                            }
                            $parse = [];
                            for ($i = 0; $i < strlen($block); $i++) {
                                array_push($parse, ord($block[$i]));
                            }
                            $block = $parse;

                            $processedBlock = $scram->decriptBlock($block);
                            $encryptedFileContent[] = $processedBlock;
                        }
                        $lastArray = end($encryptedFileContent);
                        array_pop($encryptedFileContent);

                        if ($lastArray[count($lastArray) - 1] == 15 && count(array_filter($lastArray, function ($value) {
                            return $value == 0;
                        })) == 15) {
                            $lastArray = "fall";
                        } elseif ($lastArray[count($lastArray) - 1] == count(array_filter($lastArray, function ($value) {
                            return $value == 0;
                        }))) {
                            array_splice($lastArray, 0, count(array_filter($lastArray, function ($value) {
                                return $value == 0;
                            })));
                            array_pop($lastArray);
                        }
                        $endArr = [];
                        foreach ($encryptedFileContent as $str) {
                            foreach ($str as $val) {
                                $endArr[] = $val;
                            }
                        }
                        if ($lastArray != "fall") {
                            foreach ($lastArray as $val) {
                                $endArr[] = $val;
                            }
                        }

                        $encryptedFileContent = implode(array_map("chr", $endArr));

                        $zip->addFromString(basename($file), $encryptedFileContent);
                    }

                    $zip->close();

                    header('Content-Type: application/octet-stream');
                    header('Content-Disposition: attachment; filename="' . $zipName . '"');
                    readfile($zipName);
                    exit;
                } else {
                    echo 'Не удалось создать архив';
                }
            }
        } else {
            echo 'Список файлов не передан';
        }
    }

    public static function uploadAPI($hash)
    {
        $dir = "./fileUsers/$hash/" . $_POST["dir"] . "/";
        $file = $dir . basename($_FILES["file"]["name"]);

        if (move_uploaded_file($_FILES["file"]["tmp_name"], $file)) {
            echo "True";
        } else {
            echo "False";
        }
    }

    public static function downloadAPI($hash)
    {
        $dir = "./fileUsers/$hash/" . $_POST["dir"] . "/" . $_POST["fileName"];
        if (file_exists($dir)) {
            header('Content-Description: File Transfer');
            header('Content-Type: application/octet-stream');
            header('Content-Disposition: attachment; filename="' . basename($dir) . '"');
            header('Expires: 0');
            header('Cache-Control: must-revalidate');
            header('Pragma: public');
            header('Content-Length: ' . filesize($dir));
            readfile($dir);
        }
    }

    public function createFolder($hash)
    {
        $dir = "./fileUsers/$hash/" . $_POST['dir'] . "/" . $_POST['nameFolder'];
        if (!file_exists($dir)) {
            mkdir($dir, 0777, true);
            echo "True";
        } else {
            echo "False";
        }
    }

    public function rename($hash)
    {

        $dir = "./fileUsers/$hash/" . $_POST['dir'] . "/";
        $oldName = $_POST['oldName'];
        $newName = $_POST['newName'];

        if (file_exists($dir . '/' . $oldName)) {
            if (is_file($dir . '/' . $oldName)) {
                $extension = pathinfo($oldName, PATHINFO_EXTENSION);
                $newName = $newName . '.' . $extension;
                rename($dir . '/' . $oldName, $dir . '/' . $newName);
                echo "True";
            } elseif (is_dir($dir . '/' . $oldName)) {
                rename($dir . '/' . $oldName, $dir . '/' . $newName);
                echo "True";
            }
        } else {
            echo "False";
        }
    }
}
