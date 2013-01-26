using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Algorithm;
using LASI.FileSystem;
using SharpNLPTaggingModule;

namespace Aluan_Experimentation
{
    class Program
    {
        static void Main(string[] args) {





            //Keeps the console window open until the escape key is pressed
            Console.WriteLine("Press escape to exit");
            for (var k = Console.ReadKey(); k.Key != ConsoleKey.Escape; k = Console.ReadKey()) {
                Console.WriteLine("Press escape to exit");
            }
        }
    }

    abstract class StoredByTypeWordCollection<Type, WordCollectionType> : KeyedCollection<Type, IEnumerable<Type>>
        where Type: Word
    {
        protected StoredByTypeWordCollection(Type BaseType, IEnumerable<Type> Items) {
        }

        protected override System.Type GetKeyForItem(IEnumerable<Type> item) {
            return Items.First().GetType();
        }
    }
}
