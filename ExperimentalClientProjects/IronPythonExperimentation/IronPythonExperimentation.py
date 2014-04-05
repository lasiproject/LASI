import os
import clr
import System
 
clr.AddReference("System.Core")
import System.Collections.Generic 


"""
Adding compiled LASI dll's to ironPython interpreter 
"""


coreDir = os.path.join(os.getcwd(),"Core","bin","Debug")
clr.AddReferenceToFileAndPath(os.path.join(coreDir,"Core.dll"))


contentSystemDir = os.path.join(os.getcwd(),"ContentSystem","bin","Debug")
clr.AddReferenceToFileAndPath(os.path.join(contentSystemDir,"ContentSystem.dll"))



utilitiesDir = os.path.join(os.getcwd(),"Utilities","bin","Debug")
clr.AddReferenceToFileAndPath(os.path.join(utilitiesDir,"Utilities.dll"))

interopDir = os.path.join(os.getcwd(),"InteropLayer","bin","Debug")
clr.AddReferenceToFileAndPath(os.path.join(interopDir,"Interop.dll"))


import LASI as lasi
from LASI import Core as C
clr.ImportExtensions(C)
import System.Linq as L
clr.ImportExtensions(L)
from LASI import *  
from LASI.Core import *  
"""
strlist=["Hello there you fool.",]


lasiDoc=lasi.ContentSystem.Tagger.DocumentFromRaw(strlist)

entities=lasiDoc.GetEntities()
entitiesEnumerator=IEnumerable[IEntity].GetEnumerator(entities)
print type(entitiesEnumerator)

print "lasiDoc Info:"
print "type:",type(lasiDoc)
print "available:",dir(lasiDoc)
print "entities:",entities

print lasiDoc.Words.Count
"""
"""
class lasiPrompt(cmd.Cmd):
    def preloop(self):
        self.docList=[]
        self.taggedDocList=[]
        self.prompt="lasi cmd prompt: "
    
    def do_showDocs(self,line):
        print self.docList
    
    def do_showTagged(self,line):
        print self.taggedDocList
    
    def do_addDocFromRawLine(self,line):
        self.docList.append(lasi.ContentSystem.Tagger.DocumentFromRaw(line.split(".")))

    def do_createAnalysisController(self,line):
        for doc in self.docList:
            print type(lasi.Interop.AnalysisController(doc))

    def do_exit(self,line):
        return True
    
    def postloop(self):
        print

if __name__=='__main__':
#    lasiPrompt().cmdloop()

"""
lasi.Core.Phrase.VerboseOutput = True
filePath = os.path.join("C:\\","Users","Aluan","Documents","GitHub","LASI","ExperimentalClientProjects","IronPythonExperimentation","testDocs","testDoc1.txt")
lasi.Output.SetToFile()
lasi.ContentSystem.TxtFile(filePath)
a = lasi.ContentSystem.TxtFile(filePath)
b = lasi.Interop.AnalysisOrchestrator(a)

class callbackclass():
    def __init__(self):
        self.percentComplete = 0.
    def callback(self,sender, event_args):
        self.percentComplete += event_args.PercentWorkRepresented
        print "Increment:",event_args.PercentWorkRepresented
        print "Message:",event_args.Message

c = callbackclass()
b.ProgressChanged +=c .callback

docs = b.ProcessAsync().Result

print "incrementSum:", c.percentComplete

print "Printing Verb Phrases"
for doc in docs:
    for vp in doc.Phrases.OfVerbPhrase():
        print vp

print "Printing Noun Phrases bound to a verbal"
for doc in docs:
    for np in doc.Phrases.OfNounPhrase().InSubjectOrObjectRole():
        print np
