LASI
====

[![Join the chat at https://gitter.im/lasi-project/Lobby](https://badges.gitter.im/lasi-project/Lobby.svg)](https://gitter.im/lasi-project/Lobby?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

[![Build status](https://ci.appveyor.com/api/projects/status/axnagwc2wt6yhmg5?svg=true)](https://ci.appveyor.com/project/aluanhaddad/lasi-190ha)

LASI is a lexical parsing tool that attempts to identify important associations and generalizable patterns between the words and phrases over a set of one or more documents.
Some of the heuristics applied include: 

lifted frequencies, syntactic identification, sentence level locality, and gender aware pronoun inference

LASI is implemented in C# 7.1 and targets the .NET Framework Version 4.7

(The .NET 4.5 requirement stems from the use of the async function modifier together with the await operator)

(The Mono framework supports this implicit-callback-generating, language level asynchrony since version 3.0)

You are also welcome to extend LASI using any .NET 4.5 compliant language, including Visual Basic and F#, note that use of C++/CLI is supported but discouraged

LASI has added IronPython support. We are working to integrate full Python support.

LASI has added a preliminary web application running on ASP.NET using MVC framework. See more in the WebApp project

User - System - Requirements
- x64 based dual core CPU
- 64 bit operating system
- 4GB RAM

Developer - System - Recommendations
- Intel core i7 quad core CPU
- 8GB DDR3 1333+ with low latency and tight timings.
- Solid State Disk with 200+ MB/s read

The Preferred IDEs are Visual Studio 2012 and Visual Studio 2013.
You may use any versions of these IDEs : from Express (free) to Ultimate


Using Visual Studio 2012 or 2013 and the included .sln  file should provide you with 
ideal settings to make sure that this project compiles and performs as expected.  

To test the standard program, make L_App the startup project in Visual Studio. 

The graphical app will not run under Mono due to the fact that Mono does not currently implement the Windows Presentation Foundation (WPF) runtime and libraries which are required by the graphical user interface. See the Wiki for more details.

LASI is [Open Source Software](http://opensource.org/) Licensed Under the [MIT License](http://opensource.org/licenses/MIT)
