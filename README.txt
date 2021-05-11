It's a Console App, thus it doesn't require any additional referances or plugins apart from .Net Framework.
Below are the steps describing how to setup and run the solution,

(1) Go to the github link
(2) Clone the solution. Better if it's cloned in a machine that has Visual Studio. 
(3) If the machine has visual studio, It's pretty simple to run by clicking 'Run' button visible on top of the window.
(4) In the absense of Visual Studio, Please go to "C:\Users\rajesh\source\repos\ConsoleApp1\ConsoleApp1\bin\Debug\netcoreapp3.1" folder, and run ConsoleApp1 file.
(5) The report(.txt file) will be available(with generated date and time) at "C:\Users\rajesh\source\repos\ConsoleApp1\ConsoleApp1\Reports"

Below are the components of the solution,

(1) Program.cs - Class with main method containing steps to perform.
(2) Data.xml - Test Data File.
(3) Utility.cs - Class with all utility functions.
(4) WeatherData.cs - Property class to access/manipulate the fields.
(5) Report.cs - Class with a function to generate .txt report.