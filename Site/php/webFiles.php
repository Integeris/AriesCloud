<?php

namespace Service;

use Service\Scrambler;
use ZipArchive;

use function PHPSTORM_META\type;

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
        $data = $_POST['dataFiles'];
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

    public function uploadSite()
    {
        $keyFile = $_FILES['keyFile'];
        $fileToEncrypt = $_FILES['file'];
        $key = fopen($keyFile['tmp_name'], 'rb');
        $key = fread($key, 32);
        $parse = [];
        for ($i = 0; $i < strlen($key); $i++) {

            array_push($parse, ord($key[$i]));
        }
        $key = $parse;
        // Проверка наличия файлов
        //if ($keyFile && $fileToEncrypt) {
        $file = fopen($fileToEncrypt['tmp_name'], 'rb');
        $encryptedFileContent = [];
        while (!feof($file)) {

            $block = fread($file, 16);
            $parse = [];
            for ($i = 0; $i < strlen($block); $i++) {
                array_push($parse, ord($block[$i]));
            }
            $block = $parse;
            $encryptedFileContent[] = $block;
        }

        $lastArray = end($encryptedFileContent);
        if (count($lastArray) == 16) {
            $arr = [];
            for ($i = 0; $i < 15; $i++) {
                $arr[] = 0;
            }
            $arr[] = 15;
            $lastArray = $arr;
        } else if (count($lastArray) < 16) {
            array_pop($encryptedFileContent);
            $i = 15 - count($lastArray);
            $k = 15 - count($lastArray);
            while ($i != 0) {
                $lastArray[] = 0;
                $i -= 1;
            }
            $lastArray[] = $k;
        }
        array_push($encryptedFileContent, $lastArray);
        var_dump($encryptedFileContent);
        $criptMass = [];
        $scram = new Scrambler($key);
        foreach ($encryptedFileContent as $value) {
            $processedBlock = $scram->encriptBlock($value);
            $criptMass[] = $processedBlock;
        }
        // Шифрование файла
        $endArr = [];
        foreach ($criptMass as $str) {
            foreach ($str as $val) {
                $endArr[] = $val;
            }
        }
        $parse = [];
        for ($i = 0; $i < count($endArr); $i++) {
            array_push($parse, chr($key[$i] + 65));
        }
        $encryptedFileContent = $parse;
        $encryptedFileContent = implode('', $encryptedFileContent);
        echo $encryptedFileContent;
        // Сохранение файла в определенной директории
        $encryptedFilePath = "./fileUsers/a001f87a8a7f6c2f009d7e2f8d3c588b/" . $fileToEncrypt['name'];
        file_put_contents($encryptedFilePath, $encryptedFileContent);

        echo 'Файл успешно загружен и зашифрован.';
        /* } else {
            echo 'Произошла ошибка при загрузке файла.';
        } */
    }

    public function downloadSite()
    {
        $keyFilePath = $_FILES['keyFile'];
        $files = json_decode($_POST['nameFiles']);
        $directory = $_POST['dir'];
        $key = fopen($keyFilePath['tmp_name'], 'rb');
        $key = fread($key, 32);
        $parse = [];
        for ($i = 0; $i < strlen($key); $i++) {
            array_push($parse, ord($key[$i]));
        }
        $key = $parse;
        if (isset($_POST['nameFiles'])) {
            ini_set('max_execution_time', 0);
            $count = count($files);
            // Если список содержит только один файл, скачиваем его
            if ($count === 1) {
                $file = $files[0];
                $fileContents = fopen("./fileUsers/a001f87a8a7f6c2f009d7e2f8d3c588b/" . $file, 'rb');
                $encryptedFileContent = [];
                $scram = new Scrambler($key);
                while (!feof($fileContents)) {

                    $block = fread($fileContents, 16);
                    var_dump($block);
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


                die;

                $rev = array_reverse($lastArray);
                $k = 0;
                for ($i = 1; $i < count($rev); $i++) {
                    if ($rev[$i] == 0) {
                        $k++;
                    } else {
                        break;
                    }
                }

                if ($k == 0 && end($lastArray) == 0) {
                    array_pop($lastArray);
                } else if ($k == end($lastArray) && $k == 16) {
                    $lastArray == "fall";
                } else if ($k == end($lastArray)) {
                    $i = 0;
                    while ($k + 1 != $i) {
                        array_pop($lastArray);
                        $i++;
                    }
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
                $parse = [];
                for ($i = 0; $i < count($endArr); $i++) {
                    array_push($parse, chr($key[$i] + 65));
                }
                $encryptedFileContent = $parse;
                $encryptedFileContent = implode('', $encryptedFileContent);
                // Отправляем файл пользователю для скачивания
                header('Content-Type: application/octet-stream');
                header('Content-Disposition: attachment; filename="' . basename($file) . '"');
                echo $encryptedFileContent;
                exit;
            } else {
                // Если список содержит несколько файлов, создаем архив
                $zip = new ZipArchive();
                $zipName = 'archive.zip';

                if ($zip->open($zipName, ZipArchive::CREATE) === true) {
                    foreach ($files as $file) {
                        $fileContents = fopen("./fileUsers/a001f87a8a7f6c2f009d7e2f8d3c588b/" . $file, 'rb');
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

                    // Отправляем архив пользователю для скачивания
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

    public static function uploadAPI($socket, $hash)
    {
        $filePath = socket_read($socket, 1024);
        $path = "./fileUsers/$hash/$filePath";
        socket_write($socket, "READY");
        $file = fopen($path, "wr");
        while ($data = socket_read($socket, 1024)) {
            fwrite($file, $data, strlen($data));
        }

        fclose($file);
        socket_write($socket, "GOOD");
        socket_close($socket);
    }

    public static function downloadAPI($socket, $hash)
    {
        $filePath = socket_read($socket, 1024);
        $path = "./fileUsers/$hash/$filePath";
        socket_write($socket, "READY");
        $file = fopen($path, 'rb');
        while (!feof($file)) {
            $buffer = fread($file, 1024);
            socket_write($socket, $buffer, strlen($buffer));
        }
        fclose($file);
        socket_write($socket, "GOOD");
        socket_close($socket);
    }
}
