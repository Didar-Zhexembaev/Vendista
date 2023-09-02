# Vendista (C#, MVC)

## Задача

### Повторить страницу “Отправка команды на терминал” нашего портала Вендиста.
* [Видео, как должно работать выполненное задание](https://drive.google.com/file/d/1v6s1YePSDxN_nNzBKmZHJ-DFTWXjsErV)
* [Все данные в готовом виде получаются через уже имеющееся HTTP Json API. Описание HTTP Json API (использовать это готовое апи, свое делать не нужно, нужно только фронтенд):](https://docs.google.com/document/d/1RR9j7tk34-aM5hZZjO6jr4xf7EWc9qMG)
* [Шаблон проекта C#, MVC (можно взять за основу, но необязательно)](https://drive.google.com/file/d/19T6c9MjX5zr6O03dIAGM4FFaOxsCvS4y )

## Описание задачи
1. Отправка команды на терминал
	* Открывается страница, где вверху можно ввести номер терминала, выбрать команду для отправки (выбор из списка) и параметр 1- 3 (целое число). Кнопка “Отправить”. 
Ниже - таблица с ранее отправленными командами
* Дата и время отправки
* Команда
* Параметр 1-3 (только значения)
* Статус (отправка в процессе, доставлено, не доставлено)

Сначала сделать запрос [endpoint](http://178.57.218.210:398/commands/types), он вернет ответ вида
```json
{
	"id": 3,
	"name": "Стартовый кредит",
	"parameter_name1": "Сумма кредита (в копейках)",
	"parameter_name2": null,
	"parameter_name3": null,
	"parameter_default_value1": 10000,
	"parameter_default_value2": 0,
	"parameter_default_value3": 0
}
```

### На странице вывести поля ввода (с подсказками)
 * ID Терминала 
 * Команда (выпадающий список name)
 * Параметр 1 (перед ним вывести подсказку parameter_name1, автозаполнять parameter_default_value1)
 * Параметр 2 (перед ним вывести подсказку parameter_name2, автозаполнять parameter_default_value2)
 * Параметр 3 (перед ним вывести подсказку parameter_name3, автозаполнять parameter_default_value3)

Если parameter_nameX = null, поле ввода и подсказку для этого параметра не выводить. Если все = null, значит команда без параметров и запрашивать их не нужно

По кнопке Отправить выполнять запрос /terminals/{id}/commands

Пример

```shell
curl -X 'POST' \
  'http://178.57.218.210:398/terminals/129/commands?token=f0d17d3cae184917802e2ef2' \
  -H 'accept: text/plain' \
  -H 'Content-Type: application/json' \
  -d '{
  "command_id": 1
}
```

Авторизацию, пагинацию, таблицу терминалов - делать не обязательно

## Формат выдачи выполненного тестового задания

### В расшаренную папку на google drive (или другом облаке) поместить
* Видео работы вашей программы в хорошем качестве (чем меньше оно будет отличаться от эталонного видео [video](https://drive.google.com/file/d/1v6s1YePSDxN_nNzBKmZHJ-DFTWXjsErV) - тем лучше. В идеале - вообще не должно никак отличаться)
* Архив с исходным кодом проекта. Он должен открываться и компилироваться без проблем, одним кликом, без ссылок на несуществующие файлы и т.д.
* Текстовый документ со ссылкой на развернутый веб-сервер, где запущена ваша программа. Переход по ссылке тоже должен производиться одним кликом (или копипастом в строку браузера), без проблем и уточнений.

Ссылку на расшаренную папку прислать на [marul@vendista.ru](mailto:marul@vendista.ru) или в переписку на hh.ru 
Вопросы можно задавать на [mailto:marul@vendista.ru](mailto:marul@vendista.ru)

### Срок выполнения задания:
* Идеальный - 1 день
* Средний - 7 дней
* Максимальный - 14 дней


PS: Что требуется для запуска проекта
1. Комплект для разработки программного обеспечения Dotnet SDK 3.1 [dotnet sdk](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/sdk-3.1.426-windows-x64-installer)
2. Visual Studio [visual studio](https://visualstudio.microsoft.com/thank-you-downloading-visual-studio/?sku=Community&channel=Release&version=VS2022&source=VSLandingPage&cid=2414&workload=dotnetwebcloud&flight=FlipMacCodeCF;35d&installerFlight=FlipMacCodeCF;35d&passive=false#dotnet)
3. Если при первом запуске проекта появляется ошибка nuget пакетов выполняем следующую команду
```shell
dotnet restore
```