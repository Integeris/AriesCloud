# Проект
<style>
    p
    {
        text-indent: 1.5em;
        font-size: 16px;
    }
</style>

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
6. Навигация по папкам;