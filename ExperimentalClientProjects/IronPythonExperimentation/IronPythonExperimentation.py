
import os
import clr
from System.Collections.Generic import *

"""
Adding compiled LASI dll's to ironPython interpreter 
"""
contentSystemDir=os.path.join(os.getcwd(),"ContentSystem","bin","Debug")
clr.AddReferenceToFileAndPath(os.path.join(contentSystemDir,"ContentSystem.dll"))

coreDir=os.path.join(os.getcwd(),"Core","bin","Debug")
clr.AddReferenceToFileAndPath(os.path.join(coreDir,"Core.dll"))

utilitiesDir=os.path.join(os.getcwd(),"Utilities","bin","Debug")
clr.AddReferenceToFileAndPath(os.path.join(utilitiesDir,"Utilities.dll"))

interopDir=os.path.join(os.getcwd(),"InteropLayer","bin","Debug")
clr.AddReferenceToFileAndPath(os.path.join(interopDir,"Interop.dll"))

clr.AddReference("System.Linq")

import LASI as lasi
import System
from System.Collections import Generic
from System import Text
from System.Threading import Tasks
from LASI.Core import IEntity
from LASI.Core import *
import cmd

clr.ImportExtensions(lasi.Core)
import LASI.Core.LASIEnumerable

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

lasiE=LASI.Core.LASIEnumerable

class callbackclass():
    def __init__(self):
        self.incrementSum=0.
    def callback(self,sender, event_args):
        self.incrementSum+=event_args.Increment
        print "Increment:",event_args.Increment
        print "Message:",event_args.Message
        #print event_args.ChangeType, event_args.Name

def func1(docs):
    for d in docs:
        for w in d.Words:
            yield w

filePath=os.path.join("C:\\","Users","books","Source","Repos","LASI","ExperimentalClientProjects","IronPythonExperimentation","testDocs","testDoc1.txt")
lasi.Output.SetToFile()
lasi.ContentSystem.TxtFile(filePath)
a=lasi.ContentSystem.TxtFile(filePath)
b=lasi.Interop.AnalysisController(a)
c=callbackclass()
b.ProgressChanged+=c.callback
docs=b.ProcessAsync().Result
print "incrementSum:",c.incrementSum

for word in func1(docs):
    print word