
 

import os
import clr
import System

import System.Collections.Generic 
cwd = os.getcwd()# System.IO.Directory.GetParent(System.IO.Directory.GetParent(os.getcwd()).FullName).FullName
coreDir = os.path.join(cwd,"Core","bin","Debug")
clr.AddReferenceToFileAndPath(os.path.join(coreDir,"Core.dll"))


contentSystemDir = os.path.join(cwd,"ContentSystem","bin","Debug")
clr.AddReferenceToFileAndPath(os.path.join(contentSystemDir,"ContentSystem.dll"))



utilitiesDir = os.path.join(cwd,"Utilities","bin","Debug")
clr.AddReferenceToFileAndPath(os.path.join(utilitiesDir,"Utilities.dll"))

interopDir = os.path.join(cwd,"InteropLayer","bin","Debug")
clr.AddReferenceToFileAndPath(os.path.join(interopDir,"Interop.dll"))
    

import LASI as lasi
from LASI import *
import LASI.Core as core
filePath = os.path.join("C:\\","Users","Aluan","Documents","GitHub","LASI","ExperimentalClientProjects","IronPythonExperimentation","testDocs","testDoc1.txt")
ContentSystem.TxtFile(filePath)
a = ContentSystem.TxtFile(filePath)
b = Interop.AnalysisOrchestrator(a)

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

#print "incrementSum:", c.percentComplete

#print "Printing Verb Phrases"
#for doc in docs:
#    for vp in doc.Phrases.OfVerbPhrase():
#        print vp

#print "Printing Noun Phrases bound to a verbal"
#for doc in docs:
#    for np in doc.Phrases.OfNounPhrase().InSubjectOrObjectRole():
#        print np
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