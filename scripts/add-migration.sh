#!/bin/bash

project_path="../EShop.Infrastructure/EShop.Infrastructure.csproj"
migration_name="$1"

# Проверка наличия аргумента
if [ -z "$migration_name" ]; then
  echo "Необходимо указать название миграции в качестве аргумента."
  exit 1
fi

cd "$(dirname "$project_path")" || exit

dotnet ef migrations add "$migration_name" --project "$(basename "$project_path")" --startup-project "../EShop.Console/EShop.Console.csproj" --output-dir "Migrations" --context DatabaseContext