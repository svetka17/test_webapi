# test_webapi

Используется EntityFramework для работы с подключением к MSSQL Server.
Стандартная  строка подключения прописывается  в файле **Web.config** .
По умолчанию ее вид следующий:

      <connectionStrings>
        <add name="DefaultConnection" connectionString="Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;App=EntityFramework"
            providerName="System.Data.SqlClient"/>
      </connectionStrings>
Был использован подход CodeFirst, поэтому если схема БД не совпадает - она будет пересоздана.

Адрес по умолчанию следующий: http://localhost:52916 . Его можно изменить, изменив константу (host) в файле App.config
Путь для доступа к API : /api/dateintervals
Поддерживаются  запросы **POST** (добавление) и **GET** (выборка по заданию)
Содержится контролер с двумя методами Select (для выбора записей из БД по условию) и Insert (для добавления записи в БД)

## Консольное приложение
Для запуска приложения используется 3 параметра: 
1) insert|select (insert используется для добавления записи в базу данных, select используется для отображения инервалов, пересекающихся с датами, указанными в параметрах)
2) дата С (принимается в формате yyyy-MM-dd)
3) дата по (принимается в формате yyyy-MM-dd)

Пример запуска:
ConsoleDateTool insert 2021-05-01 2021-06-01
добавит в БД интервал Дата С = 2021-05-01, Дата По = 2021-06-01

ConsoleDateTool select 2021-05-01 2021-06-01
выберет из БД все интервалы, которые пересекаются с датами либо 2021-05-01, либо 2021-06-01

если приложение было вызвано с параметрами 
insert 2018-01-01 2018-01-03
insert 2018-01-01 2018-01-31
insert 2018-01-03 2018-01-05,
то select 2018-01-04 2018-02-03 вернет 2 диапазона:               
2018-01-01 / 2018-01-31 и 2018-01-03 / 2018-01-05 

## БД
Состоит из двух таблиц **DateInterval** и **AppLogs** 

> **Intervals**

DateIntervalId - int - primary key
DateFrom - datetime [Начальная дата]
DateTo - datetime [Конечная]

> **AppLogs**
ID - int-primary key 
DateLog - datetime [Время создания записи]
Message - nvchar(MAX) [Сообщение]
