<?php

namespace Service;

use PDO;
use PDOException;

class DB
{
    private $host = 'localhost';
    private $db = 'site';
    private $user = 'admin';
    private $password = 'admin';
    private $dsn = "";

    public function __construct()
    {
        $this->dsn = "pgsql:host=$this->host;port=5432;dbname=$this->db;";
    }

    public function conn()
    {
        try {
            $pdo = new PDO($this->dsn, $this->user, $this->password, [PDO::ATTR_ERRMODE => PDO::ERRMODE_EXCEPTION]);
            if ($pdo) {
                return $pdo;
            }
        } catch (PDOException $e) {
            die($e->getMessage());
        } finally {
            if ($pdo) {
                $pdo = null;
            }
        }
    }

    public function auth($hash)
    {

        $pdo = $this->conn();

        $state = $pdo->prepare("SELECT * FROM users WHERE hash = :hash");
        $state->execute(['hash' => $hash]);
        $result = $state->fetch();

        if ($result) {
            return True;
        } else {
            return False;
        }
    }

    public function reg($login, $password, $email)
    {
    }

    public function regCode($code, $email)
    {
        $conn = $this->conn();

        $state = $conn->prepare("SELECT * FROM code WHERE email = :email");
        $state->execute(['email' => $email]);
        if ($state->rowCount() > 0) {
            $deleteQuery = "DELETE FROM code WHERE email = :email";
            $deleteValues = [
                'email' => $email
            ];

            $deleteStmt = $conn->prepare($deleteQuery);
            $deleteStmt->execute($deleteValues);
        }

        $query = "INSERT INTO code (email, cod, status) VALUES (:email, :cod, :status)";
        $values = [
            'email' => $email,
            'cod' => $code,
            'status' => 'non'
        ];
        $stmt = $conn->prepare($query);
        $stmt->execute($values);
        $conn = null;
    }

    public function checkCode($email, $code)
    {
        $pdo = $this->conn();

        $state = $pdo->prepare("SELECT * FROM code WHERE email = :email AND cod=:cod");
        $state->execute(['email' => $email, 'cod' => $code]);
        $result = $state->fetch();

        if ($result) {
            $sql = "UPDATE code SET status = :newStatus WHERE email = :email";
            $stmt = $pdo->prepare($sql);
            $stmt->execute(['email' => $email, 'newStatus' => "yes"]);
            echo "Good";
            return True;
        } else {
            return False;
        }
    }
}
