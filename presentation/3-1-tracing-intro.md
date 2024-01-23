# Глава III. Трассировка

Трассировка запросов — информация о прохождении запроса по всем компонентам программной системы.

## Хранение трасс

Для хранения нам снова понадобится поднять ещё один сервис — Jaeger. Запустим все наши сервисы:

```shell
docker-compose up
```

## Добавление трассировки в приложение

Конфигурация для загрузки трасс в Jaeger:

```csharp
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

builder.Services
    .AddOpenTelemetry()
    .ConfigureResource(resource => resource.AddService("my-service")) // конфигурируем название сервиса
    .WithTracing(tracing => tracing
        .AddAspNetCoreInstrumentation() // добавляем сбор трасс для входящих запросов
        .AddOtlpExporter(options => options.Endpoint = new Uri("http://jaeger:4317"))); // указываем URL Jaeger
```

Готовые трассы можно посмотреть в Jaeger по адресу http://localhost:16686.

[Заключение →](./4-1-conclusion.md)

[← Визуализация метрик](./2-3-grafana.md)
