# Глава II. Метрики

Метрики — статистические данные о событиях, происходящих в системе.

## Сбор метрик

Снова заранее запустим наши сервисы, чтобы они успели наработать статистику по запросам:

```shell
docker-compose up
```

Добавим конфигурацию в наши сервисы:

```csharp
using Prometheus;

app.UseMetricServer(url: "/metrics"); // конфигурируем URL, по которому будут доступны метрики
app.UseHttpMetrics();                 // включаем сбор метрик по HTTP-запросам
```

Ещё примеры:
- [Склад](../Stock/Program.cs)
- [Оплата](../Payments/Program.cs)
- [Витрина](../Shop/Program.cs)

Посмотрим метрики по адресу http://localhost:8180/metrics

[Хранение метрик →](./2-2-prometheus.md)

[← Системы хранения логов](./1-3-seq-logs.md)
