LASI
====

LASI is a lexical parsing tool that attempts to identify important words and phrases across multiple document based on 
frequency, 
location in a sentence, 
part of speech,
and a number of other measures.

LASI is built in C# and must run on a 64 bit Windows machine.

This project should be built as a project using Visual Studio 2012 Ultimate and the SLN file should provide you with 
ideal settings to make sure that this project compiles and performs as expected.

To test the standard program, make LASI_Userinterface the startup project in Visual Studio. 

This project will not run in Mono due to the fact that Mono doesn't currently support WPF Toolkit, which is the tool
that renders the graphs of our results in the "Top Results" screen.
