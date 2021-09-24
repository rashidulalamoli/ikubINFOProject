# ikubINFOProject

This is a demo project to instrate few thing as mentioned below:
A) Functional requirements:
    1.Create a REST API which provides CREATE / READ / UPDATE / DELETE (CRUD) methods for 2 connected entities/tables. Example (User-> Roles or Department-> Employees). *
    2.API should be accessible only to authorized clients/users. *
B) Technical requirements:
    1.Set up an application with .Net Core/ Entity Framework.
    2.Data can be stored in SQL or NoSQL databases.
    3.Create unit tests. *
    4.Have centralized exception management. *
    5.Have a log file for the main actions. *
    6.The application must be organized in layers. Example (Controller -> Service ->Repository). *
    7.The code should be uploaded to Git repository (GitHub / GitLab / Bitbucket) and should not be copied from similar articles/tutorials or projects.
    8.Add a readme file to explain the technical part of the application and give instructions on how to run/deploy the created application.

Tools and Technologies: 
 1. dotnet core 5.
 2. entityframework core.
 3. sql server(mslocal).

Layers:
 1. Api
 2. DataAccess
 3. Entity
 4. Service
 5. Repository
 6. Utility
 7. UnitTests

Project Description:
In the Api project, I user 3 controllers. Authorization, User and Role. For logging, I used NLog and for excention handling, I used exemtionhandlermiddleware. The log in writing in aglobal field named as IkubInfoErrorHandlingLogs/logs/date_logfile.txt and configured in nlog.congig file.
The service dependency injection is seperated. A migration is added, so that the first run could make all necessary things like database and seed data.

The DataAccess and Entity projects are made of reverse engineering as i made db first. So the fluent validation  in made in DataAccess and entity objects are made in Entity.
The seed data for role is also given in this fluent api, so that new migration can use it.
I have also used extension methods for making encapsulation of entity. For Dtos, I used record type objects as it is value based.

The Service layer is pretty much straight forward. It just calls Repository to get data and write business logics.

The Repository layer is mainly built on generic design pattern. As we know, if we want to change ORMs(entityframework, hybernate), it helps us to do change quickly.
But I also used custom repositories, because it gives more flexibily to access and modify data by using entityframworf dbcontext class.

The Utility layer is just a helper for some static data and function.

The UnitTest project built on Xunit,moq and fluientassertions. I tried to make test on all aspects, some could be missing.


How to run:
Just build the project by .net cli tool and hit dotnet run.

How to test:
The .net 5 is packaged with swagger. But I have also provided postman collection to test. 





