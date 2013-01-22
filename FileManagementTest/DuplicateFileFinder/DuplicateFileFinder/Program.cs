using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuplicateFileFinder
{
    class Program
    {
        static void Main(string[] args) {
            var working = (args.Length > 0 && Directory.Exists(args[0])) ? args[0] : PromptForDir();
            CheckFiles(working);
        }
        private static void CheckFiles(string workDir) {
            var directory = new DirectoryInfo(workDir);
            var fileGroupsBySubDir = from dir in directory.EnumerateDirectories("*", SearchOption.AllDirectories)
                                     from file in dir.EnumerateFiles()
                                     group file by dir.FullName into dirgrp
                                     from file in dirgrp
                                     group file by file.Extension into extgrp
                                     from f in extgrp
                                     group f by f.Length into gr
                                     where gr.Count() > 1
                                     group gr by gr.First().DirectoryName;
            foreach (var fdg in fileGroupsBySubDir) {
                if (fdg.Count() > 0) {
                    using (var writer = new StreamWriter(fdg.Key + @"\possible_duplicates.txt", false)) {
                        var num = 0;
                        foreach (var grp in fdg) {
                            writer.WriteLine("group {0}  -  size {1}", num++, grp.Key);
                            foreach (var file in grp) {
                                writer.WriteLine("\t\'{0}'", file.Name);
                            }
                        }
                    }
                } else if (File.Exists(fdg.Key + @"\possible_duplicates.txt"))
                    File.Delete(fdg.Key + @"\possible_duplicates.txt");
            }
        }

        private static string PromptForDir() {
            Console.WriteLine("enter the directory to check");
            var dir = Console.ReadLine();
            try {
                Directory.SetCurrentDirectory(dir);
                return dir;
            } catch (DirectoryNotFoundException) {
                return PromptForDir();
            }
        }
    }
}