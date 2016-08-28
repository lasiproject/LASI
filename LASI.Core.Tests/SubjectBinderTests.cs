using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LASI.Core.Analysis.Binding.Tests
{
    public class SubjectBinderTests
    {
        [Fact(Skip = "TODO: Implement code to verify target")]
        public void BindTest()
        {
            var target = new SubjectBinder();
            throw new NotImplementedException("TODO: Implement code to verify target");
        }
        [Fact]
        public void HasSubjectPronounTest()
        {
            var phrase = new PronounPhrase(
                new PersonalPronoun("they"),
                new PersonalPronoun("themselves")
            );
            Assert.True(phrase.HasSubjectPronoun());
        }
        [Fact]
        public void HasSubjectPronounTest1()
        {
            var phrase = new PronounPhrase(
                new PersonalPronoun("he"),
                new Conjunction("and"),
                new PersonalPronoun("it")
            );
            Assert.True(phrase.HasSubjectPronoun());
        }
        [Fact]
        public void HasSubjectPronounTest2()
        {
            var phrase = new PronounPhrase(
                new PersonalPronoun("she"),
                new Conjunction("and"),
                new PersonalPronoun("it")
            );
            Assert.True(phrase.HasSubjectPronoun());
        }
        [Fact]
        public void HasSubjectPronounTest3()
        {
            var phrase = new PronounPhrase(
                new PersonalPronoun("him"),
                new Conjunction("and"),
                new PersonalPronoun("her")
            );
            Assert.False(phrase.HasSubjectPronoun());
        }
    }
}
