import clr
from __builtin__ import object
clr.AddReferenceToFileAndPath(unicode("C:\\Users\\Aluan\\Documents\\GitHub\\LASI\\App\\bin\\Debug\\LASI.Content.dll"))
import os
import System
from _io import open
import LASI.Content

class TxtFile (LASI.Content.IRawTextSource):
    def __init__(self, name, filePath):
        self.name = name
        self.filePath = filePath
        
    def get_Name(self):
        return self.name
    def GetText(self):
        reader = open(self.filePath,'r')
        result = basestring.join('\n',reader.readlines())
        return result
    def GetTextAsync(self):
        return System.Threading.Tasks.Task.FromResult(self.GetText())




