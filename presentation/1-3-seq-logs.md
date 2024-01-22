## Системы хранения логов

Сначала запустим приложения, а то система хранения логов долго стартует:

```shell
docker-compose up
```

Хотим видеть логи в удобном интерфейсе, а не в разрозненных файлах. Для этого нужно:

1. Поднять систему хранения логов (отдельный сервис).
2. Доработать конфигурацию логирования:

   ```csharp
   using Serilog;

   Log.Logger = new LoggerConfiguration()
       .WriteTo.Console()
       .WriteTo.File("my.log")
       .WriteTo.Seq("http://seq") // тут
       .CreateLogger();
   ```

Пример структурного логирования:

```csharp
Log.Information("Я есть лог со свойством {Property}", propertyValue);
```

Ещё примеры:
- [Склад](../Stock/Program.cs)
- [Оплата](../Payments/Program.cs)
- [Витрина](../Shop/Program.cs)

Логи доступны по адресу http://localhost:5341.

[Глава II →](./2-1-metrics-intro.md)

[← Логирование в файл](./1-2-file-logs.md)
