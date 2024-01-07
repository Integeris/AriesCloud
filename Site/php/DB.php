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

    public function getName($hash)
    {
        $pdo = $this->conn();

        $state = $pdo->prepare("SELECT * FROM users WHERE hash = :hash");
        $state->execute(['hash' => $hash]);
        $result = $state->fetch();

        return $result["login"];
    }

    public function checkCode($email, $code)
    {


        $pdo = $this->conn();

        $state = $pdo->prepare("SELECT * FROM code WHERE email = :email AND cod=:cod");
        $state->execute(['email' => $email, 'cod' => $code]);
        $result = $state->fetch(PDO::FETCH_ASSOC);

        if ($result) {
            $login = $result["login"];
            $password = $result["password"];
            $email = $result["email"];
            $hash = md5($login . ".*." . $password);
            mkdir("./fileUsers/$hash");

            $query = "INSERT INTO users (email, password, login,hash) VALUES (:email, :password, :login,:hash)";
            $values = [
                'email' => $email,
                'login' => $login,
                'password' => $password,
                'hash' => $hash
            ];
            $stmt = $pdo->prepare($query);
            $stmt->execute($values);

            $deleteQuery = "DELETE FROM code WHERE email = :email";
            $deleteValues = [
                'email' => $email
            ];
            $deleteStmt = $pdo->prepare($deleteQuery);
            $deleteStmt->execute($deleteValues);
            if ($stmt->rowCount() > 0) {
                return True;
            } else {
                return False;
            }
        } else {
            return False;
        }
    }

    public function checkDB($nameDB, $name, $what)
    {
        $pdo = $this->conn();

        $state = $pdo->prepare("SELECT * FROM $nameDB WHERE $name = :$name");
        $state->execute(["$name" => $what]);
        $result = $state->fetch();

        if ($result) {
            return False;
        } else {
            return True;
        }
    }

    public function updateCode($email, $code)
    {
        $pdo = $this->conn();

        $state = $pdo->prepare("SELECT * FROM code WHERE email = :email");
        $state->execute(["email" => $email]);
        $result = $state->fetch();

        if ($result) {
            $sql = "UPDATE code SET cod = :cod WHERE email = :email";
            $stmt = $pdo->prepare($sql);
            $stmt->execute(['email' => $email, 'cod' => $code]);
            return True;
        } else {
            return False;
        }
    }

    public function reg($login, $password, $email)
    {
        $pdo = $this->conn();
        $state = $pdo->prepare("SELECT * FROM code WHERE email = :email");
        $state->execute(['email' => $email]);
        if ($state->rowCount() > 0) {
            $deleteQuery = "DELETE FROM code WHERE email = :email";
            $deleteValues = [
                'email' => $email
            ];
            $deleteStmt = $pdo->prepare($deleteQuery);
            $deleteStmt->execute($deleteValues);
        }

        $query = "INSERT INTO code (email, password, login) VALUES (:email, :password, :login)";
        $values = [
            'email' => $email,
            'login' => $login,
            'password' => $password
        ];
        $stmt = $pdo->prepare($query);
        $stmt->execute($values);
        if ($stmt->rowCount() > 0) {
            return True;
        } else {
            return False;
        }
    }

    public function changePassword($hash, $password)
    {
        $pdo = $this->conn();

        $state = $pdo->prepare("SELECT * FROM users WHERE hash = :hash");
        $state->execute(['hash' => $hash]);
        $result = $state->fetch();
        $login=$result["login"];
        $newHash = md5($login . ".*." . $password);

        $sql = "UPDATE users SET password = :password, hash= :newHash WHERE hash = :oldHash";
        $stmt = $pdo->prepare($sql);
        $stmt->execute(['password' => $password, 'newHash' => $newHash, 'oldHash' => $hash]);
        return $newHash;
    }
}
