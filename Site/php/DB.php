<?php
namespace Service;

use PDO;
use PDOException;

class DB{
    private $host='localhost';
    private $db='site';
    private $user='admin';
    private $password='admin';
    private $dsn="";

    public function __construct()
    {
        $this->dsn="pgsql:host=$this->host;port=5432;dbname=$this->db;";
    }

    public function conn(){
        try{
        $pdo=new PDO($this->dsn,$this->user,$this->password,[PDO::ATTR_ERRMODE => PDO::ERRMODE_EXCEPTION]);
        if ($pdo) {
            echo "Connected to the $this->db database successfully!";
        }
    } catch (PDOException $e) {
        die($e->getMessage());
    } finally {
        if ($pdo) {
            $pdo = null;
        }
    }
}
}