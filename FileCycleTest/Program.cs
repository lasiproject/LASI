using LASI.Algorithm;
using LASI.FileSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace FileCycleTest
{
    class Program
    {
        static void Main(string[] args) {
            var startTime = new TimeSpan(DateTime.Now.Millisecond);
            // Synchronous(args);
            Asynchronous(args);
            var timeElapsed = new TimeSpan((DateTime.Now.Millisecond - startTime.Milliseconds));
            Console.WriteLine(timeElapsed);
            Console.ReadKey();
        }



        private static void Asynchronous(string[] args) {
            var converts = from docpath in args.AsParallel()
                           select TagAsync(ConvertAsync(new DocFile(docpath)).Result);
            foreach (var taggingOperation in converts) {
                taggingOperation.Wait();
            }
        }

        async static Task<InputFile> ConvertAsync(InputFile infile) {
            var CtoCX = new DocToDocXConverter(infile);
            var converted = await CtoCX.ConvertFileAsync();
            return await new DocxToTextConverter(converted).ConvertFileAsync();

        }
        async static Task TagAsync(InputFile infile) {
            await Task.Run(() => {
                new SharpNLPTaggingModule.SharpNLPTagger(TaggingOption.TagAndAggregate, infile.FullPath, infile.PathSansExt + ".tagged").ProcessFile();
            });
        }

        private static void Synchronous(string[] args) {
            foreach (var df in args) {
                var docfile = new DocFile(df);
                DocToDocXConverter converter = new DocToDocXConverter(docfile);
                var docxfile = converter.ConvertFile();
                DocxToTextConverter dxttxt = new DocxToTextConverter(docxfile);
                var textfile = dxttxt.ConvertFile();
                Console.WriteLine(textfile.FullPath);
                SharpNLPTaggingModule.SharpNLPTagger tagger = new SharpNLPTaggingModule.SharpNLPTagger(TaggingOption.TagAndAggregate, textfile.FullPath);
                tagger.ProcessFile();
                //  Console.WriteLine(tagger.OutputFilePath);
            }
        }

    }
}


