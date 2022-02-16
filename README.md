# How to deploy

1. Install [.net 5.0](https://dotnet.microsoft.com/en-us/download/dotnet/5.0).
2. Install [MySql](https://dev.mysql.com/doc/mysql-installation-excerpt/5.7/en/).
3. Open MySql console and crate DB with user.
```ssh
CREATE DATABASE customers_db;

CREATE USER 'customer_user'@'localhost' IDENTIFIED BY 'password';

GRANT ALL PRIVILEGES ON customers_db.* TO 'customer_user'@'localhost';
```
4. [Restart](https://www.mysqltutorial.org/mysql-adminsitration/restart-mysql/#:~:text=Restart%20MySQL%20Server%20on%20Windows,-If%20MySQL%20installed&text=First%2C%20open%20the%20Run%20window,and%20click%20the%20restart%20button.) MySql service.
5. Clone repository to your machine.
6. Compile project.
```ssh
cd  [your path]\TestTask_WebClients

dotnet publish -c Release
```
7. Run web application
```ssh
dotnet run [your path]\TestTask_WebClients\TestTask_WebClients\bin\Release\net5.0\TestTask_WebClients.dll```
