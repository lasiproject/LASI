using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.UnitTests.LexicalElementInfo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LASI.Core;
using System.Dynamic;
using LASI.WebApp;

namespace WebApp.UnitTests.LexicalElementInfo.Tests
{
    [TestClass]
    public class ContextMenuDataProviderTests
    {
        /// <summary>
        /// Tests that the integral ids assigned are bound by reference identity and remain fixed when retrieved multiple times.
        /// </summary>
        [TestMethod]
        public void GetSerializationIdTest() {
            IEnumerable<ILexical> elements = new List<ILexical> { new Verb("run", VerbForm.Base), new Adverb("swiftly"), new Preposition("through") };

            Assert.IsTrue(elements.Select(e => e.GetSerializationId()).SequenceEqual(elements.Select(e => e.GetSerializationId())));

            IEnumerable<ILexical> elements1 = new List<ILexical> { new Verb("run", VerbForm.Base), new Adverb("swiftly"), new Preposition("through") };

            Assert.IsFalse(elements.Select(e => e.GetSerializationId()).SequenceEqual(elements1.Select(e => e.GetSerializationId())));
        }
        /// <summary>
        /// Tests, in a parallel context, that the integral ids assigned are bound by reference identity and remain fixed when retrieved multiple times.
        /// </summary>
        [TestMethod]
        public void GetSerializationIdTest1() {
            ParallelQuery<ILexical> elements = new List<ILexical> { new Verb("run", VerbForm.Base), new Adverb("swiftly"), new Preposition("through") }
                .AsParallel();
            Assert.IsTrue(elements
                .Select(e => e.GetSerializationId())
                .OrderBy(id => id)
                    .SequenceEqual(elements
                .Select(e => e.GetSerializationId())
                .OrderBy(id => id)));

            ParallelQuery<ILexical> elements1 = new List<ILexical> {
                new Verb("run", VerbForm.Base), new Adverb("swiftly"), new Preposition("through")
            }.AsParallel();
            Assert.IsFalse(elements
                .Select(e => e.GetSerializationId())
                .OrderBy(id => id)
                    .SequenceEqual(elements1
                .Select(e => e.GetSerializationId())
                .OrderBy(id => id)));
        }
        /// <summary>
        /// Tests menu data serialized for a verbal with 1 subject, 1 direct object, and 1 indirect object.
        /// </summary>
        [TestMethod]
        public void VerbalGetJsonMenuDataTest() {
            // Relevant elements from a clause such as "I helped her with him"
            IEntity subject = new PersonalPronoun("I");
            IEntity directObject = new PersonalPronoun("her");
            IEntity indirectObject = new PersonalPronoun("him");
            IVerbal verbal = new Verb("helped", VerbForm.Past);
            // Ids for serialization context
            int subjectId = subject.GetSerializationId();
            int directObjectId = directObject.GetSerializationId();
            int indirectObjectId = indirectObject.GetSerializationId();
            verbal.BindSubject(subject);
            verbal.BindDirectObject(directObject);
            verbal.BindIndirectObject(indirectObject);
            dynamic result = verbal.GetJsonMenuData();
            // note double {{ escapes to { and does not indicate a nested object literal
            Assert.IsTrue(result == string.Format("{{\"verbal\":" + verbal.GetSerializationId() + ",\"subjects\":[{0}],\"directObjects\":[{1}],\"indirectObjects\":[{2}]}}", subjectId, directObjectId, indirectObjectId));
        }
        /// <summary>
        /// Tests menu data serialized for a verbal with 1 subject, 1 direct object, and 0 indirect object.
        /// </summary>
        [TestMethod]
        public void VerbalGetJsonMenuDataTest1() {
            // Relevant elements from a clause such as "I helped her"
            IEntity subject = new PersonalPronoun("I");
            IEntity directObject = new PersonalPronoun("her");
            IVerbal verbal = new Verb("helped", VerbForm.Past);
            // Ids for serialization context
            int subjectId = subject.GetSerializationId();
            int directObjectId = directObject.GetSerializationId();
            verbal.BindSubject(subject);
            verbal.BindDirectObject(directObject);
            dynamic result = verbal.GetJsonMenuData();
            string expected = string.Format("{{\"verbal\":" + verbal.GetSerializationId() + ",\"subjects\":[{0}],\"directObjects\":[{1}]}}", subjectId, directObjectId);
            // note double {{ escapes to { and does not indicate a nested object literal
            Assert.IsTrue(result == expected);
        }
        /// <summary>
        /// Tests menu data serialized for a verbal with 1 subject, 0 direct objects, and 0 indirect objects.
        /// </summary>
        [TestMethod]
        public void VerbalGetJsonMenuDataTest2() {
            // Relevant elements from a clause such as "I helped"
            IEntity subject = new PersonalPronoun("I");
            IVerbal verbal = new Verb("helped", VerbForm.Past);
            // Ids for serialization context
            int subjectId = subject.GetSerializationId();
            verbal.BindSubject(subject);
            dynamic result = verbal.GetJsonMenuData();
            // note double {{ escapes to { and does not indicate a nested object literal
            Assert.IsTrue(result == string.Format("{{\"verbal\":" + verbal.GetSerializationId() + ",\"subjects\":[{0}]}}", subjectId));
        }
        /// <summary>
        /// Tests menu data serialized for a verbal with 0 subjects, 0 direct objects, and 0 indirect objects.
        /// </summary>
        [TestMethod]
        public void VerbalGetJsonMenuDataTest3() {
            // Relevant elements from an element such as "helped"

            IVerbal verbal = new Verb("helped", VerbForm.Past);
            // Ids for serialization context 
            dynamic result = verbal.GetJsonMenuData();
            // note double {{ escapes to { and does not indicate a nested object literal
            Assert.IsTrue(result == string.Format("{{\"verbal\":" + verbal.GetSerializationId() + "}}"));
        }
        /// <summary>
        /// Tests menu data serialized for a verbal with 1 subject, 0 direct objects, and 1 indirect object.
        /// </summary>
        [TestMethod]
        public void VerbalGetJsonMenuDataTest4() {
            // Relevant elements from a clause such as "I helped her with him"
            IEntity subject = new PersonalPronoun("I");
            IEntity indirectObject = new PersonalPronoun("him");
            IVerbal verbal = new Verb("helped", VerbForm.Past);
            // Ids for serialization context
            int subjectId = subject.GetSerializationId();
            int indirectObjectId = indirectObject.GetSerializationId();
            verbal.BindSubject(subject);
            verbal.BindIndirectObject(indirectObject);
            dynamic result = verbal.GetJsonMenuData();
            // note double {{ escapes to { and does not indicate a nested object literal
            Assert.IsTrue(result == string.Format("{{\"verbal\":" + verbal.GetSerializationId() + ",\"subjects\":[{0}],\"indirectObjects\":[{1}]}}", subjectId, indirectObjectId));
        }
        /// <summary>
        /// Tests menu data serialized for a verbal with 0 subjects, 1 direct object, and 1 indirect object.
        /// </summary>
        [TestMethod]
        public void VerbalGetJsonMenuDataTest5() {
            // Relevant elements from a clause such as "I helped her with him"
            IEntity directObject = new PersonalPronoun("her");
            IEntity indirectObject = new PersonalPronoun("him");
            IVerbal verbal = new Verb("helped", VerbForm.Past);
            // Ids for serialization context
            int directObjectId = directObject.GetSerializationId();
            int indirectObjectId = indirectObject.GetSerializationId();
            verbal.BindDirectObject(directObject);
            verbal.BindIndirectObject(indirectObject);
            dynamic result = verbal.GetJsonMenuData();
            // note double {{ escapes to { and does not indicate a nested object literal
            Assert.IsTrue(result == string.Format("{{\"verbal\":" + verbal.GetSerializationId() + ",\"directObjects\":[{0}],\"indirectObjects\":[{1}]}}", directObjectId, indirectObjectId));
        }
        /// <summary>
        /// Tests menu data serialized for a verbal with 1 subject, 1 direct object, and 0 indirect objects.
        /// </summary>
        [TestMethod]
        public void VerbalGetJsonMenuDataTest6() {
            // Relevant elements from a clause such as "I helped her with him"
            IEntity subject = new PersonalPronoun("I");
            IEntity directObject = new PersonalPronoun("her");
            IVerbal verbal = new Verb("helped", VerbForm.Past);
            // Ids for serialization context
            int subjectId = subject.GetSerializationId();
            int directObjectId = directObject.GetSerializationId();
            verbal.BindSubject(subject);
            verbal.BindDirectObject(directObject);
            dynamic result = verbal.GetJsonMenuData();
            // note double {{ escapes to { and does not indicate a nested object literal
            Assert.IsTrue(result == string.Format("{{\"verbal\":" + verbal.GetSerializationId() + ",\"subjects\":[{0}],\"directObjects\":[{1}]}}", subjectId, directObjectId));
        }
        /// <summary>
        /// Tests menu data serialized for a verbal with 1 subject, 0 direct objects, and 1 indirect objects.
        /// </summary>
        [TestMethod]
        public void VerbalGetJsonMenuDataTest7() {
            // Relevant elements from a clause such as "I helped her with him"
            IEntity subject = new PersonalPronoun("I");
            IEntity indirectObject = new PersonalPronoun("her");
            IVerbal verbal = new Verb("helped", VerbForm.Past);
            // Ids for serialization context
            int subjectId = subject.GetSerializationId();
            int indirectObjectId = indirectObject.GetSerializationId();
            verbal.BindSubject(subject);
            verbal.BindIndirectObject(indirectObject);
            dynamic result = verbal.GetJsonMenuData();
            // note double {{ escapes to { and does not indicate a nested object literal
            Assert.IsTrue(result == string.Format("{{\"verbal\":" + verbal.GetSerializationId() + ",\"subjects\":[{0}],\"indirectObjects\":[{1}]}}", subjectId, indirectObjectId));
        }
        /// <summary>
        /// Tests menu data serialized for a verbal with 0 subjects, 1 direct object, and 0 indirect objects.
        /// </summary>
        [TestMethod]
        public void VerbalGetJsonMenuDataTest8() {
            // Relevant elements from a clause such as "I helped her with him"
            IEntity directObject = new PersonalPronoun("her");
            IVerbal verbal = new Verb("helped", VerbForm.Past);
            // Ids for serialization context 
            int directObjectId = directObject.GetSerializationId();
            verbal.BindDirectObject(directObject);
            dynamic result = verbal.GetJsonMenuData();
            // note double {{ escapes to { and does not indicate a nested object literal
            Assert.IsTrue(result == string.Format("{{\"verbal\":" + verbal.GetSerializationId() + ",\"directObjects\":[{0}]}}", directObjectId));
        }
        /// <summary>
        /// Tests menu data serialized for a verbal with 0 subjects, 0 direct objects, and 1 indirect object.
        /// </summary>
        [TestMethod]
        public void VerbalGetJsonMenuDataTest9() {
            // Relevant elements from a clause such as "I helped her with him"
            IEntity indirectObject = new PersonalPronoun("her");
            IVerbal verbal = new Verb("helped", VerbForm.Past);
            // Ids for serialization context 
            int indirectObjectId = indirectObject.GetSerializationId();
            verbal.BindIndirectObject(indirectObject);
            dynamic result = verbal.GetJsonMenuData();
            // note double {{ escapes to { and does not indicate a nested object literal
            Assert.IsTrue(result == string.Format("{{\"verbal\":" + verbal.GetSerializationId() + ",\"indirectObjects\":[{0}]}}", indirectObjectId));
        }
    }
}
