
Used Nuget Packages:


Purpose: 
Main objective of this project is to gather in one place most useful and commnly used functionality by me
in convenient re-usable API. 

KeyNote:
-
-

Project consists of few Projects:
- Common
- Unit Test Nunit3
- Unit Test MSTest2
- Selenium Basic 
- Selenium API 
- WindowsShurtcut


Project: NunitTest - content
DriverFactory - dictionary of working WebDriver. Logic can be reused to created parallel execution 
CaseCommonDataSource - class as an test attribute with custom method passed as an parameter. 
	Generic possibility to run test scenario with changing amount of data. Iteration count be modified by json file content.
RunMultipleScenarioByConfiguration - Example of clean up at the class scope that is called only once and close all driver that are register
	at driver factory.


TODO: SpecFlow





