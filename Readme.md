### Project Background

The solution needs to:

-  Ingests data files and stores the raw data
-  Transforms the data to produce an aggregated result
-  Distribute the aggregated data as an API

### Technologies/Languages Used

#### Code Base:

-  Visual Studio 2017
  - External Tools:
    - Automapper
    - Moq
-  Net Core 2.2
-  Entity Framework Core
-  Web API
-  Extensions.Configuration.UserSecrets
- ** Angular: sample app only for invoking the service.**

#### Cloud:

-  Azure Data Factory (ADF)
-  Azure SQL Database
-  Azure Blob Storage
-  Azure Microsoft Event Grid
-  Azure App Services

#### Deployment:

-  Azure ARM Templates.

### Pattern and Practices Used

-  Repository and Unit of Work Pattern
-  Singleton
-  Single Responsibility Principle.
-  Dependency Inversion Principle.
-  N-tiered architecture.

### Important Links

#### Source Repository:

-  [https://github.com/agreshdoshi/ERMSolution](https://github.com/agreshdoshi/ERMSolution) - Rest API. Main App.
-  [https://github.com/agreshdoshi/ERMTest](https://github.com/agreshdoshi/ERMTest) - OBSOLETE - This holds the Azure Factory Data and its deployment template in Azure Resource Manager.
-  [https://github.com/agreshdoshi/ERMTOULPDataFactory](https://github.com/agreshdoshi/ERMTOULPDataFactory) - This holds the Azure Factory Data and its deployment template in Azure Resource Manager.
-  [https://github.com/agreshdoshi/SampleMeeterApp](https://github.com/agreshdoshi/SampleMeeterApp) - This is just a SampleApp source code in Angular. This is not for assessment purpose. However, It shows on how the service is invoked.

#### Front End:

-  [https://ermapi.azurewebsites.net/api/meter](https://ermapi.azurewebsites.net/api/meter) - Rest API. Main App. Data from Azure SQL Database.
-  [https://samplemeterapp.azurewebsites.net/](https://samplemeterapp.azurewebsites.net/) - Sample app in Angular.

### Solution

#### Cloud:

-  Azure Data factory is used for ETL purpose:
-  There is an event trigger which looks for csv file in the blobstorage.
-  The csv file is collected from the blob storage.
-  Then the raw mapping is done.
-  Then Aggregate activity flow is executed so that the following five fields are produced as an output:
    - MeterCode
    - Date
    - DataType
    - MedianValue (On Energy column)
    - MinimumValue (on Energy column)
    - MaximumValue (on Energy column)
-  The output is dumped it into the Azure SQL Database.
-  Then the file is deleted from the blob storage once it is processed.

#### Visual Studio – Code:

**Main Rest API App:**

-  The REST API is used to design the solution.
-  It uses Repository and Unit of Work pattern.
-  It has a unit tests at relevant area.
-  It&#39;s using Global Exception in the middleware component. Specially all 500 error would be handled there.
-  The solution is divided into layers to maintain separation of concern.
   - API
   - DataAccess
   - Build and Deploy
   - Tests
-  Logging of the various stages are flushed out in the console. Ideally something like Serilog would have been better.
-  Using Automapper to map between Models and Entities.
-  The solution uses user secrets for storing connecting string.

| Projects | Purpose |
|----------|---------|
| ERM.API |This is a API controller which have a single responsibility to get the data from DataAccess. It just have one Get function.|
| ERM.DataAccess | This follows the Repository Pattern to extract data via Entity Framework Core and then pass it over to Unit Of Work. |
| ERM.API.Tests |The unit tests for ERM.API.|
| ERM.DataAccess.Tests | The unit tests for ERM.DataAccess.Tests |
| ERMDeployToAzure | It user Azure Resource Manager to Deploy the application. |

**SampleApp:**

-  The sameple app is using ASP.Net core 2.2 with Angular as a front end.
-  This app is just to show how the Rest API service is invoked.
-  This app have few hardcoded values and doesn&#39;t form a part of main application. Hence, it doesn&#39;t follow a proper design principle.

#### Data Flow diagram of the solution:

 ![DFD - ERM](https://user-images.githubusercontent.com/38810792/63645390-75d15c00-c740-11e9-9040-de0dbefdbe0c.png)

### Limitations and risks of the solution (could have been done better):

-  The API could have been more sophisticated with proper Authorization and with API key in place.
-  The logging mechanism could have been better by using the tools like Serilog.
-  The Azure database is not secured. Could have had better security mechanism.
-  The data retrieval could have been sorted and should have been checked for duplicates. However, that wasn&#39;t the core requirement.

### Why Azure Data Factory (ADF)?

There were two other tools into consideration before jumping into Azure Data Factory i.e. Logic Apps and SSIS.

Logic apps is a fantastic tool to simplify the workflows by automation in a scalable format. It is a fantastic tool for sending an alerts and can be used in conjunction with Azure Data Factory. However, Logic Apps is not great for ETL or ELT purpose as it requires some heavy lifting.

SSIS is a on premise tool which requires hardware and software before it can be used. Also, it is very heavy weight. It also requires a license rather than pay as you go.

ADF is a cloud-based data integration service which have visual data driven workflows which allows orchestration and automation of the data. It allows visual constructions of data pipeline as per below. Also, it has a fantastic git integration and it uses Azure Resource Manager which can be used for build and deploy. Its pay as you go service rather than big investment upfront.

 ![ADF Flow](https://user-images.githubusercontent.com/38810792/63645413-1de72500-c741-11e9-9ddb-2b4b6be84183.png)
