# QueryAggregator
## Запуск программы
Для корректного запуска приложения:
1. Откройте проект в Visual Studio.
2. В папку QueryAggregator добавьте файл c ключами AppSettings.config, который будет отправлен отдельно.
3. В файле QueryAggregator/Web.config измените параметр connectionStrings/add/connectionString на имя своего сервер и базы данных.
4. В Packager Manager Console запустите команду update-database.
5. Запустите приложение

## Яндекс.XML
По умолчанию поиск будет производиться в поисковых системах Google и Bing, но не в Яндекс. Это связано с тем, что сервис Яндекс.XML жестко привязывает ключ к IP-адресу компьютера, с которого будут исходить запросы.
Чтобы включить возможность использования Яндекс поиска:
1. Получите ключ на сайте https://xml.yandex.ru/settings/
2. В файле AppSettings.config замените YandexKey и YandexUser на полученные значения.
3. В файле QueryAggregator/Util/NinjectRegistrations.cs откомментируйте 24-строчку.
4. Запустите приложение.
