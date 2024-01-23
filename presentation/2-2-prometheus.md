## Хранение метрик

Для хранения метрик можно использовать, например, Prometheus. Он разворачивается в виде отдельного сервиса
и периодически собирает метрики со всех известных ему источников.

Конфигурация для Prometheus:

```yaml
scrape_configs:
  - job_name: 'shop'
    scrape_interval: 5s
    static_configs:
      - targets: ['shop:8080']
  - job_name: 'stock'
    scrape_interval: 5s
    static_configs:
      - targets: ['stock:8080']
  - job_name: 'payments'
    scrape_interval: 5s
    static_configs:
      - targets: ['payments:8080']
```

[Работа с метриками →](./2-3-grafana.md)

[← Введение в метрики](./2-1-metrics-intro.md)
