# test_webapi

Используется EntityFramework для работы с подключением к SQL Server.
Стандартная  строка подключения прописывается  в файле **Web.config** .
По умолчанию ее вид следующий:

      <connectionStrings>
        <add name="DefaultConnection" connectionString="Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;App=EntityFramework"
            providerName="System.Data.SqlClient"/>
      </connectionStrings>
Был использован подход CodeFirst, поэтому если схема БД не совпадает - она будет пересоздана.

Путь для доступа к API : /api/dateintervals
Поддерживаются  запросы **POST** (добавление) и **GET** (выборка по заданию)
