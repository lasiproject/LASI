using System;
namespace LASI.Core.DocumentStructures
{
    public interface IFullEnglishStatement
    {
        System.Collections.Generic.IEnumerable<LASI.Core.Clause> Clauses { get; }
        System.Collections.Generic.IEnumerable<LASI.Core.IVerbal> GetActions();
        System.Collections.Generic.IEnumerable<LASI.Core.IEntity> GetEntities();
        System.Collections.Generic.IEnumerable<LASI.Core.Phrase> Phrases { get; }
        System.Collections.Generic.IEnumerable<LASI.Core.DocumentStructures.Sentence> Sentences { get; }
        System.Collections.Generic.IEnumerable<LASI.Core.Word> Words { get; }
    }
}
