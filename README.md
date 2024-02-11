# Проект

Aries Cloud - удалённое хранилище зашифрованных файлов.

# Описание

Проект представляет собой удалённое хранилище зашифрованных файлов, 
встроенный Web-клиент и клиентское приложение для работы с ним.

Все файлы шифруются на стороне клиента, используя криптографическое средство шифрования "Кузнечик", что обеспечивает дополнительную безопасность.

Сгенерированные ключи хранятся только у пользователя, поэтому 
администратор сервера не имеет возможности получить доступ к зашифрованным 
данным.


В данном проекте реализованы:

1. Регистрация пользователей;
2. Загрузка файлов и папок в удалённое хранилище;
3. Выгрузка файлов и папок из удалённого хранилищя;
4. Шифрование файлов;
5. Работа с файлами на сервере: перемещение, удаление, переименование;
6. Навигация по папкам.

# Установка

Установку необходимо выполнять в следующем порядке: база даннх, сервер, desktop-клент.

## База данных

В качестве базы даннх проект использует [PostgreSQL](https://www.postgresql.org).  
После установки необходимо создать базу данных:

```sql
CREATE DATABASE site
    WITH
    OWNER = postgres
    ENCODING = 'UTF8'
    LC_COLLATE = 'Russian_Russia.1251'
    LC_CTYPE = 'Russian_Russia.1251'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1
    IS_TEMPLATE = False;
```

Далее необходимо переключиться на созданную базу данных и выполнить:

```sql
CREATE TABLE IF NOT EXISTS public.code
(
    email text COLLATE pg_catalog."default" NOT NULL,
    cod text COLLATE pg_catalog."default",
    password text COLLATE pg_catalog."default" NOT NULL,
    login text COLLATE pg_catalog."default" NOT NULL
);

CREATE TABLE IF NOT EXISTS public.users
(
    login text COLLATE pg_catalog."default" NOT NULL,
    password text COLLATE pg_catalog."default" NOT NULL,
    hash text COLLATE pg_catalog."default" NOT NULL,
    email text COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT users_pkey PRIMARY KEY (hash)
);
```

## Сервер
Для установки сервера необходимо либо развернуть ```http```-сервер, либо установить специальное приложение такое как [Denver](http://www.denwer.ru/) или [Open Server Panel](https://ospanel.io/). Необходимо скачать ветку ```Full-Stack```:  

```git clone https://github.com/Integeris/AriesCloud.git -b Full-Stack```  

Следующим шагом необходимо переместить папку ```Site``` в папку с доменами. Изменить настройки подключения к базе данных можно в файле ```Site/php/DB.php```:  

```php
private $host = 'localhost';
private $db = 'site';
private $user = 'admin';
private $password = 'admin';
private $dsn = "";
```

## Desktop-клиент

Для установки desktop-клиента необходимо скачать ветку ```Main```:  

```git clone https://github.com/Integeris/AriesCloud.git -b Main```  

Далее необходимо скомпилировать проект через [Visual Studio](https://visualstudio.microsoft.com/ru/). В файле ```AriesCloud//Classes/Core.cs``` можно поменять доменное имя сервера:  

```C#
/// <summary>
/// Адрес сервера.
/// </summary>
private const string server = "http://ariescloud.ru";
```