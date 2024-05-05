#!/bin/bash

# Переходим в директорию со скриптом
cd "$(dirname "$0")"

# Очищаем файл code-report.txt перед началом
> code-report.txt

# Находим все .cs файлы, включая подпапки, и перебираем их
find ../ -type f -name "*.cs" ! -path "*/bin/*" ! -path "*/obj/*" ! -path "*/Migrations/*" | while IFS= read -r file; do
    # Получаем название файла без пути
    filename=$(basename "$file")

    # Добавляем название файла в code-report.txt
    echo "$filename" >> code-report.txt
    echo "" >> code-report.txt  # Добавляем пустую строку

    # Добавляем текст из файла в code-report.txt, подавляя вывод ошибок команды cat
    cat "$file" 2>/dev/null >> code-report.txt

    # Проверяем, заканчивается ли текст файла символом новой строки
    if [[ -n $(tail -c 1 "$file") ]]; then
        echo "" >> code-report.txt  # Добавляем символ новой строки, если это необходимо
    fi

    # Добавляем пустую строку в качестве разделителя между файлами
    echo "" >> code-report.txt
done
