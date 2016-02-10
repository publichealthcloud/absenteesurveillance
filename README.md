# absenteesurveillance
School Absentee Surveillance System

SASS is an online absentee surveillance system. The system is optimized to process, analyze and display daily absentee data for a school district. Expected users include schools districts and public health agencies.

The system not only displays absentee trends but also generates a range of different absentee warnings based on complex rules.
The solution consists of a district wide absentee dashboard as well as school-specific dashboards. The solution also consists of tools for uploading data files and downloading processed data. All data loaded into the system can be easily downloaded for additional processing.

The solution requires two sets of data to operate:
(1)	A one-time upload of school information for a district – an Excel file with the name, address, educational level of every school in the district
(2)	A daily upload of school absentee data for the district – Excel files with the absentee information for each school and classroom

The daily uploads of absentee data can be done manually or using an automated SFTP option.

Example data files can be found in the root of this solution.

The SASS solution requires a .NET stack to function: IIS 7 and MSSQL 2014.

You can find all necessary code and a template database (.bak file) in this solution. Once the database has been restored to a MSSQL server, you will need to create a database user and add those credentials to the example credentials in the web.config file.

Additionally, to get the Gooogle maps to work correctly, you will have to get a Google API key. This can easily be changed in the web.config file of the solution as well.

Contact Michael Fiore at info@epgtech.net with any technical questions.

