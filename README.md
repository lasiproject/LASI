LASI
====

LASI is a lexical parsing tool that attempts to identify important words and phrases across multiple document based on 
frequency, 
location in a sentence, 
part of speech,
and a number of other measures.

LASI is built in C# and must run on a 64 bit Windows machine running .NET 4.5 + framework.

The Preferred IDE is Visual Studio 2012. 

This project should be built as a project using Visual Studio 2012 and the SLN file should provide you with 
ideal settings to make sure that this project compiles and performs as expected. 
This project has also been successfully built using Visual Studio 2013 RC.


To test the standard program, make LASI_Userinterface the startup project in Visual Studio. 

The primary application will not run under Mono due to the fact that Mono does not currently implement the Windows Presentation Foundation (WPF) runtime and libraries which are required by the graphical user interface.
