## Логирование в файл

Хотим логировать и в консоль, и в файл. Воспользуемся библиотекой для логирования. Пример конфигурации:

```csharp
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("my.log")
    .CreateLogger();
```

Пример логирования:

```csharp
Log.Information("Я есть лог");
```

Ещё примеры:
- [Склад](../Stock/Program.cs)
- [Оплата](../Payments/Program.cs)
- [Витрина](../Shop/Program.cs)

Запустим приложения:

```shell
docker-compose up
```

Достанем логи с машины с приложением витрины:

```shell
docker exec shop cat /app/shop.log
```

[Ещё улучшаем логи →](./1-3-seq-logs.md)

[← Введение в логирование](./1-1-logs-intro.md)
