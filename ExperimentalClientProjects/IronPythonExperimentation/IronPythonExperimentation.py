import os
import clr
import System
import System.Collections.Generic
import System.Configuration
from ProgressReporter import ProgressReporter
from TxtFile import TxtFile 
#
def make_path(*segments):
    return os.path.join(segments)

lasi_root_dir = unicode("C:\Users\Aluan\Documents\GitHub\LASI")
System.IO.Directory.SetCurrentDirectory(lasi_root_dir)
lasi_assembly_dir = make_path(lasi_root_dir, "App","bin", "Debug")
clr.AddReferenceToFileAndPath(make_path("OpenNLP.dll"))
clr.AddReferenceToFileAndPath(make_path("Newtonsoft.Json.dll"))
clr.AddReferenceToFileAndPath(make_path("LASI.Utilities.dll"))
clr.AddReferenceToFileAndPath(make_path("LASI.Core.dll"))
clr.AddReferenceToFileAndPath(make_path("LASI.Interop.dll"))

# This is necessary to expose extension methods on the Enumerable components of
# the LASI.Core data structures.
# The order must be exactly as follows and the actual assembly previously
# loaded.
# First add an additional reference to LASI.Core;
# we must use the name which is brought into scope by the 4th call to
# clr.AddReferenceToFileAndPath above
clr.AddReference("LASI.Core")
# Next we need to import LASI to otherwise lookup of LASI.Core, which is not by
# string but references the actual namespace object, will fail.
import LASI
# Finally we call clr.ImportExtensions passing the qualified namespace which
# contains extension providing static classes.
clr.ImportExtensions(LASI.Core)

try:
 
    from LASI.Utilities import *  
    from LASI.Interop import AnalysisOrchestrator, ConfigFormat
    from LASI.Interop.Configuration import *
    from LASI.Core import *

    cwd = os.getcwd()
    app_path = os.path.join(cwd, "ExperimentalClientProjects","IronPythonExperimentation")
    Initialize(os.path.join(app_path, "config.json"), ConfigFormat.Json, "Data")
    
    filePath = os.path.join(app_path, "testDocs","testDoc1.txt")
    textToAnalyze = TxtFile("testDoc1", filePath)
    analyzer = AnalysisOrchestrator([textToAnalyze])
    reporter = ProgressReporter()
    analyzer.ProgressChanged += lambda s, event_args: reporter.Report(event_args)
    task = analyzer.ProcessAsync()
    docs = task.Result

    print ("Printing Verb Phrases")
    for doc in docs:
        for vp in doc.Phrases.OfVerbPhrase():
            print vp

    print "Printing Noun Phrases bound to a verbal"
    for doc in docs:
        for np in doc.Phrases.OfNounPhrase().InSubjectOrObjectRole():
            print np
except Exception as exc: 
    print exc
    #for (m, s) in [(x.Message,x.StackTrace) for x in (exc.InnerExceptions if exc.InnerExceptions else [exc])]: 
    #    print (m,s)