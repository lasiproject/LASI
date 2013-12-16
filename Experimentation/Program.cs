using LASI.ContentSystem;
using LASI.Core;
using LASI.Core.Binding;
using LASI.Core.DocumentStructures;
using LASI.Core.Heuristics;
using LASI.Core.Heuristics.Morphemization;
using LASI.Core.Patternization;
using LASI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Experimentation.CommandLine
{
    class Program
    {

        static void Main(string[] args) {
            var document = Tagger.DocumentFromRaw(new[] { "Hello there you fool." });

            var dd = document.GetEntities().FirstOrDefault();
            var k = dd.Match()
                .Yield<string>()
                .With((IEntity e) => e.Text)
                .Result();
            Output.WriteLine(document);

            Output.WriteLine(0.To(10).Format());

            Input.WaitForKey();









            var toAttack = new Verb("attack", VerbForm.Base);

            var warlike = new Adjective("warlike");

            var bellicoseDescriptors =
                from d in document.Words.OfAdjective()
                where d.IsSimilarTo(warlike)
                select d;

            var bellicoseVerbals =
                from act in document.GetActions()
                where act.IsSimilarTo(toAttack)
                select act;

            var bellicoseIndividuals =
                from entity in document.GetEntities().InSubjectRole()
                where bellicoseDescriptors.Intersect(entity.Descriptors).Any()
                || bellicoseVerbals.Contains(entity.SubjectOf)
                select entity;

            var attackerAttackeePairs =
                from victim in document.GetEntities().InObjectRole()
                from attacker in bellicoseIndividuals
                from act in bellicoseVerbals
                where attacker.IsRelatedTo(victim).On(act)
                select new { attacker, victim };

            var thoseWhoHaveYetToAttack =
                bellicoseIndividuals.Except(
                from pair in attackerAttackeePairs
                select pair.attacker);















        }
    }


}

