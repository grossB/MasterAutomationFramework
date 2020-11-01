
Used Nuget Packages:


Purpose: 
Main objective of this project is to gather in one place most useful and commonly used functionality by me
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

https://github.com/peterrexj/Selenium.Essentials -	
https://github.com/App-vNext/Polly

TODO: SpecFlow, AutoIt


Project: NunitTest - content
DriverFactory - dictionary of working WebDriver. Logic can be reused to created parallel execution 
CaseCommonDataSource - class as an test attribute with custom method passed as an parameter. 
	Generic possibility to run test scenario with changing amount of data. Iteration count be modified by json file content.
RunMultipleScenarioByConfiguration - Example of clean up at the class scope that is called only once and close all driver that are register
	at driver factory.


Project: T4GenericTemplates
T4 Template - great tool to automatically update/recreate similar structure of file. Example usage when operating on command schema that 
is frequently changing. By declaring properties/fields as an xml attribute or node it is possible to generate all files automatically.

Example: 
	PageClasses																	 - xml that contain all classes to be created
	[MicrosoftPage.xml, GooglePagePaths.xml, GooglePagePath2s.xml]				 - xml that contain body of the page
	[ GooglePagePath2s.xml] -	<PageClass page="GooglePage2By" folder="Google"> -  Declaration of Folder and ClassName

	PageByGenerator.tt															- <# manager.StartNewFile(page +".cs", "SeleniumAPI", folder); #>					 - ClassName, ProjectName, Folder Name,
	Note: As it is only for example purpose the structure of folder and classPage and made by hand by there is ofc area for further improvement and
	automate the whole folder tree structure generation however thus this is not best usage of this functionality but at the same very 
	recognizable it is made	as and kind of abstract example to let brief idea to what so ever this tool can be used for


Project NUnitTest:
Parallel execution:		
		Children - one per each class in folder 
		Fixture - all in parallel class attribute [TestFixture, Parallelizable(ParallelScope.All)]
			method attribute needed [[Parallelizable(ParallelScope.Self)] || [Parallelizable(ParallelScope.Children)]]
		RunMultipleScenarioByConfiguration - Custom Attribute



