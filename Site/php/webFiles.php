<?php

namespace Service;

use Service\Scrambler;
use ZipArchive;

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
        $fileToEncrypt = $_FILES['fileToEncrypt'];

        $key = file_get_contents($fileToEncrypt['tmp_name']);
        // Проверка наличия файлов
        if ($keyFile && $fileToEncrypt) {
            // Проверка размера файла
            if ($fileToEncrypt['size'] % 16 != 0) {
                // Дополнение блока нулями
                $paddingSize = 16 - ($fileToEncrypt['size'] % 16);
                $padding = str_repeat("\0", $paddingSize - 1) . chr($paddingSize);
                $fileContent = file_get_contents($fileToEncrypt['tmp_name']) . $padding;
            } else {
                // Добавление блока из нулей
                $padding = str_repeat("\0", 15) . chr(15);
                $fileContent = file_get_contents($fileToEncrypt['tmp_name']) . $padding;
            }

            // Шифрование файла
            $blocks = str_split($fileContent, 16);
            $encryptedFileContent = "";
            $scram = new Scrambler($key);
            foreach ($blocks as $block) {
                $processedBlock = $scram->encriptBlock($block);
                $encryptedFileContent .= $processedBlock;
            }

            // Сохранение файла в определенной директории
            $encryptedFilePath = 'encrypted_files/' . $fileToEncrypt['name'];
            file_put_contents($encryptedFilePath, $encryptedFileContent);

            echo 'Файл успешно загружен и зашифрован.';
        } else {
            echo 'Произошла ошибка при загрузке файла.';
        }
    }

    public function downloadSite()
    {
        $keyFilePath = $_POST['key_file'];

        $files = $_POST['files'];
        $directory = $_POST['directory'];

        $key = file_get_contents($keyFilePath['tmp_name']);
        if (isset($_POST['files'])) {
            $files = $_POST['files'];

            // Если список содержит только один файл, скачиваем его
            if (count($files) === 1) {
                $file = $files[0];
                $fileContents = file_get_contents($file);

                $chunks = str_split($fileContents, 16);
                $encryptedFileContent = "";
                $scram = new Scrambler($key);
                foreach ($chunks as $block) {
                    $processedBlock = $scram->decriptBlock($block);
                    $encryptedFileContent .= $processedBlock;
                }

                $lastByte = substr($encryptedFileContent, -1);
                $zeroCount = substr_count($encryptedFileContent, '0', -1 * $lastByte);

                if ($zeroCount >= $lastByte) {
                    $encryptedFileContent = substr($encryptedFileContent, 0, -1 * $lastByte);
                }

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
                        $fileContents = file_get_contents($file);

                        $chunks = str_split($fileContents, 16);
                        $encryptedFileContent = "";
                        $scram = new Scrambler($key);
                        foreach ($chunks as $block) {
                            $processedBlock = $scram->decriptBlock($block);
                            $encryptedFileContent .= $processedBlock;
                        }

                        $lastByte = substr($encryptedFileContent, -1);
                        $zeroCount = substr_count($encryptedFileContent, '0', -1 * $lastByte);

                        if ($zeroCount >= $lastByte) {
                            $encryptedFileContent = substr($encryptedFileContent, 0, -1 * $lastByte);
                        }

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

    public function uploadAPI()
    {
    }

    public function downloadAPI()
    {
    }
}
