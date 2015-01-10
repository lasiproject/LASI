using LASI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using LASI.Core.DocumentStructures;

namespace L_CoreTests
{
    
    
    /// <summary>
    ///This is a test class for LASIEnumerableTest and is intended
    ///to contain all LASIEnumerableTest Unit Tests
    ///</summary>
    [TestClass()]
    public class LASIEnumerableTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext {
            get {
                return testContextInstance;
            }
            set {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for WordsFollowing
        ///</summary>
        [TestMethod()]
        public void WordsFollowingTest() {
            IEnumerable<Word> words = null; // TODO: Initialize to an appropriate value
            Word startAfter = null; // TODO: Initialize to an appropriate value
            IEnumerable<Word> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<Word> actual;
            actual = LASIEnumerable.WordsFollowing(words, startAfter);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for WithSubjectOrObject
        ///</summary>
        public void WithSubjectOrObjectTestHelper<TVerbal>()
            where TVerbal : IVerbal {
            IEnumerable<TVerbal> verbals = null; // TODO: Initialize to an appropriate value
            Func<IEntity, bool> condition = null; // TODO: Initialize to an appropriate value
            IEnumerable<TVerbal> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<TVerbal> actual;
            actual = LASIEnumerable.WithSubjectOrObject<TVerbal>(verbals, condition);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void WithSubjectOrObjectTest() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of TVe" +
                    "rbal. Please call WithSubjectOrObjectTestHelper<TVerbal>() with appropriate type" +
                    " parameters.");
        }

        /// <summary>
        ///A test for WithSubjectOrObject
        ///</summary>
        public void WithSubjectOrObjectTest1Helper<TVerbal>()
            where TVerbal : IVerbal {
            IEnumerable<TVerbal> verbals = null; // TODO: Initialize to an appropriate value
            IEnumerable<TVerbal> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<TVerbal> actual;
            actual = LASIEnumerable.WithSubjectOrObject<TVerbal>(verbals);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void WithSubjectOrObjectTest1() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of TVe" +
                    "rbal. Please call WithSubjectOrObjectTest1Helper<TVerbal>() with appropriate typ" +
                    "e parameters.");
        }

        /// <summary>
        ///A test for WithSubjectOrObject
        ///</summary>
        public void WithSubjectOrObjectTest2Helper<TVerbal>()
            where TVerbal : IVerbal {
            ParallelQuery<TVerbal> verbals = null; // TODO: Initialize to an appropriate value
            Func<IEntity, bool> condition = null; // TODO: Initialize to an appropriate value
            ParallelQuery<TVerbal> expected = null; // TODO: Initialize to an appropriate value
            ParallelQuery<TVerbal> actual;
            actual = LASIEnumerable.WithSubjectOrObject<TVerbal>(verbals, condition);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void WithSubjectOrObjectTest2() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of TVe" +
                    "rbal. Please call WithSubjectOrObjectTest2Helper<TVerbal>() with appropriate typ" +
                    "e parameters.");
        }

        /// <summary>
        ///A test for WithSubjectOrObject
        ///</summary>
        public void WithSubjectOrObjectTest3Helper<TVerbal>()
            where TVerbal : IVerbal {
            ParallelQuery<TVerbal> verbals = null; // TODO: Initialize to an appropriate value
            ParallelQuery<TVerbal> expected = null; // TODO: Initialize to an appropriate value
            ParallelQuery<TVerbal> actual;
            actual = LASIEnumerable.WithSubjectOrObject<TVerbal>(verbals);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void WithSubjectOrObjectTest3() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of TVe" +
                    "rbal. Please call WithSubjectOrObjectTest3Helper<TVerbal>() with appropriate typ" +
                    "e parameters.");
        }

        /// <summary>
        ///A test for WithSubject
        ///</summary>
        public void WithSubjectTestHelper<TVerbal>()
            where TVerbal : IVerbal {
            ParallelQuery<TVerbal> verbals = null; // TODO: Initialize to an appropriate value
            ParallelQuery<TVerbal> expected = null; // TODO: Initialize to an appropriate value
            ParallelQuery<TVerbal> actual;
            actual = LASIEnumerable.WithSubject<TVerbal>(verbals);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void WithSubjectTest() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of TVe" +
                    "rbal. Please call WithSubjectTestHelper<TVerbal>() with appropriate type paramet" +
                    "ers.");
        }

        /// <summary>
        ///A test for WithSubject
        ///</summary>
        public void WithSubjectTest1Helper<TVerbal>()
            where TVerbal : IVerbal {
            ParallelQuery<TVerbal> verbals = null; // TODO: Initialize to an appropriate value
            Func<IEntity, bool> condition = null; // TODO: Initialize to an appropriate value
            ParallelQuery<TVerbal> expected = null; // TODO: Initialize to an appropriate value
            ParallelQuery<TVerbal> actual;
            actual = LASIEnumerable.WithSubject<TVerbal>(verbals, condition);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void WithSubjectTest1() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of TVe" +
                    "rbal. Please call WithSubjectTest1Helper<TVerbal>() with appropriate type parame" +
                    "ters.");
        }

        /// <summary>
        ///A test for WithSubject
        ///</summary>
        public void WithSubjectTest2Helper<TVerbal>()
            where TVerbal : IVerbal {
            IEnumerable<TVerbal> verbals = null; // TODO: Initialize to an appropriate value
            IEnumerable<TVerbal> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<TVerbal> actual;
            actual = LASIEnumerable.WithSubject<TVerbal>(verbals);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void WithSubjectTest2() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of TVe" +
                    "rbal. Please call WithSubjectTest2Helper<TVerbal>() with appropriate type parame" +
                    "ters.");
        }

        /// <summary>
        ///A test for WithSubject
        ///</summary>
        public void WithSubjectTest3Helper<TVerbal>()
            where TVerbal : IVerbal {
            IEnumerable<TVerbal> verbals = null; // TODO: Initialize to an appropriate value
            Func<IEntity, bool> condition = null; // TODO: Initialize to an appropriate value
            IEnumerable<TVerbal> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<TVerbal> actual;
            actual = LASIEnumerable.WithSubject<TVerbal>(verbals, condition);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void WithSubjectTest3() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of TVe" +
                    "rbal. Please call WithSubjectTest3Helper<TVerbal>() with appropriate type parame" +
                    "ters.");
        }

        /// <summary>
        ///A test for WithObject
        ///</summary>
        public void WithObjectTestHelper<TVerbal>()
            where TVerbal : IVerbal {
            IEnumerable<TVerbal> verbals = null; // TODO: Initialize to an appropriate value
            Func<IEntity, bool> condition = null; // TODO: Initialize to an appropriate value
            IEnumerable<TVerbal> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<TVerbal> actual;
            actual = LASIEnumerable.WithObject<TVerbal>(verbals, condition);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void WithObjectTest() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of TVe" +
                    "rbal. Please call WithObjectTestHelper<TVerbal>() with appropriate type paramete" +
                    "rs.");
        }

        /// <summary>
        ///A test for WithObject
        ///</summary>
        public void WithObjectTest1Helper<TVerbal>()
            where TVerbal : IVerbal {
            IEnumerable<TVerbal> verbals = null; // TODO: Initialize to an appropriate value
            IEnumerable<TVerbal> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<TVerbal> actual;
            actual = LASIEnumerable.WithObject<TVerbal>(verbals);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void WithObjectTest1() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of TVe" +
                    "rbal. Please call WithObjectTest1Helper<TVerbal>() with appropriate type paramet" +
                    "ers.");
        }

        /// <summary>
        ///A test for WithObject
        ///</summary>
        public void WithObjectTest2Helper<TVerbal>()
            where TVerbal : IVerbal {
            ParallelQuery<TVerbal> verbals = null; // TODO: Initialize to an appropriate value
            Func<IEntity, bool> condition = null; // TODO: Initialize to an appropriate value
            ParallelQuery<TVerbal> expected = null; // TODO: Initialize to an appropriate value
            ParallelQuery<TVerbal> actual;
            actual = LASIEnumerable.WithObject<TVerbal>(verbals, condition);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void WithObjectTest2() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of TVe" +
                    "rbal. Please call WithObjectTest2Helper<TVerbal>() with appropriate type paramet" +
                    "ers.");
        }

        /// <summary>
        ///A test for WithObject
        ///</summary>
        public void WithObjectTest3Helper<TVerbal>()
            where TVerbal : IVerbal {
            ParallelQuery<TVerbal> verbals = null; // TODO: Initialize to an appropriate value
            ParallelQuery<TVerbal> expected = null; // TODO: Initialize to an appropriate value
            ParallelQuery<TVerbal> actual;
            actual = LASIEnumerable.WithObject<TVerbal>(verbals);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void WithObjectTest3() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of TVe" +
                    "rbal. Please call WithObjectTest3Helper<TVerbal>() with appropriate type paramet" +
                    "ers.");
        }

        /// <summary>
        ///A test for WithIndirectObject
        ///</summary>
        public void WithIndirectObjectTestHelper<TVerbal>()
            where TVerbal : IVerbal {
            ParallelQuery<TVerbal> verbals = null; // TODO: Initialize to an appropriate value
            ParallelQuery<TVerbal> expected = null; // TODO: Initialize to an appropriate value
            ParallelQuery<TVerbal> actual;
            actual = LASIEnumerable.WithIndirectObject<TVerbal>(verbals);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void WithIndirectObjectTest() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of TVe" +
                    "rbal. Please call WithIndirectObjectTestHelper<TVerbal>() with appropriate type " +
                    "parameters.");
        }

        /// <summary>
        ///A test for WithIndirectObject
        ///</summary>
        public void WithIndirectObjectTest1Helper<TVerbal>()
            where TVerbal : IVerbal {
            IEnumerable<TVerbal> verbals = null; // TODO: Initialize to an appropriate value
            Func<IEntity, bool> condition = null; // TODO: Initialize to an appropriate value
            IEnumerable<TVerbal> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<TVerbal> actual;
            actual = LASIEnumerable.WithIndirectObject<TVerbal>(verbals, condition);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void WithIndirectObjectTest1() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of TVe" +
                    "rbal. Please call WithIndirectObjectTest1Helper<TVerbal>() with appropriate type" +
                    " parameters.");
        }

        /// <summary>
        ///A test for WithIndirectObject
        ///</summary>
        public void WithIndirectObjectTest2Helper<TVerbal>()
            where TVerbal : IVerbal {
            IEnumerable<TVerbal> verbals = null; // TODO: Initialize to an appropriate value
            IEnumerable<TVerbal> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<TVerbal> actual;
            actual = LASIEnumerable.WithIndirectObject<TVerbal>(verbals);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void WithIndirectObjectTest2() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of TVe" +
                    "rbal. Please call WithIndirectObjectTest2Helper<TVerbal>() with appropriate type" +
                    " parameters.");
        }

        /// <summary>
        ///A test for WithIndirectObject
        ///</summary>
        public void WithIndirectObjectTest3Helper<TVerbal>()
            where TVerbal : IVerbal {
            ParallelQuery<TVerbal> verbals = null; // TODO: Initialize to an appropriate value
            Func<IEntity, bool> condition = null; // TODO: Initialize to an appropriate value
            ParallelQuery<TVerbal> expected = null; // TODO: Initialize to an appropriate value
            ParallelQuery<TVerbal> actual;
            actual = LASIEnumerable.WithIndirectObject<TVerbal>(verbals, condition);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void WithIndirectObjectTest3() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of TVe" +
                    "rbal. Please call WithIndirectObjectTest3Helper<TVerbal>() with appropriate type" +
                    " parameters.");
        }

        /// <summary>
        ///A test for WithDirectObject
        ///</summary>
        public void WithDirectObjectTestHelper<TVerbal>()
            where TVerbal : IVerbal {
            ParallelQuery<TVerbal> verbals = null; // TODO: Initialize to an appropriate value
            Func<IEntity, bool> condition = null; // TODO: Initialize to an appropriate value
            ParallelQuery<TVerbal> expected = null; // TODO: Initialize to an appropriate value
            ParallelQuery<TVerbal> actual;
            actual = LASIEnumerable.WithDirectObject<TVerbal>(verbals, condition);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void WithDirectObjectTest() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of TVe" +
                    "rbal. Please call WithDirectObjectTestHelper<TVerbal>() with appropriate type pa" +
                    "rameters.");
        }

        /// <summary>
        ///A test for WithDirectObject
        ///</summary>
        public void WithDirectObjectTest1Helper<TVerbal>()
            where TVerbal : IVerbal {
            ParallelQuery<TVerbal> verbals = null; // TODO: Initialize to an appropriate value
            ParallelQuery<TVerbal> expected = null; // TODO: Initialize to an appropriate value
            ParallelQuery<TVerbal> actual;
            actual = LASIEnumerable.WithDirectObject<TVerbal>(verbals);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void WithDirectObjectTest1() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of TVe" +
                    "rbal. Please call WithDirectObjectTest1Helper<TVerbal>() with appropriate type p" +
                    "arameters.");
        }

        /// <summary>
        ///A test for WithDirectObject
        ///</summary>
        public void WithDirectObjectTest2Helper<TVerbal>()
            where TVerbal : IVerbal {
            IEnumerable<TVerbal> verbals = null; // TODO: Initialize to an appropriate value
            IEnumerable<TVerbal> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<TVerbal> actual;
            actual = LASIEnumerable.WithDirectObject<TVerbal>(verbals);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void WithDirectObjectTest2() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of TVe" +
                    "rbal. Please call WithDirectObjectTest2Helper<TVerbal>() with appropriate type p" +
                    "arameters.");
        }

        /// <summary>
        ///A test for WithDirectObject
        ///</summary>
        public void WithDirectObjectTest3Helper<TVerbal>()
            where TVerbal : IVerbal {
            IEnumerable<TVerbal> verbals = null; // TODO: Initialize to an appropriate value
            Func<IEntity, bool> condition = null; // TODO: Initialize to an appropriate value
            IEnumerable<TVerbal> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<TVerbal> actual;
            actual = LASIEnumerable.WithDirectObject<TVerbal>(verbals, condition);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void WithDirectObjectTest3() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of TVe" +
                    "rbal. Please call WithDirectObjectTest3Helper<TVerbal>() with appropriate type p" +
                    "arameters.");
        }

        /// <summary>
        ///A test for WithDescriptor
        ///</summary>
        public void WithDescriptorTestHelper<T>()
            where T : IDescribable {
            ParallelQuery<T> describables = null; // TODO: Initialize to an appropriate value
            Func<IDescriptor, bool> condition = null; // TODO: Initialize to an appropriate value
            ParallelQuery<T> expected = null; // TODO: Initialize to an appropriate value
            ParallelQuery<T> actual;
            actual = LASIEnumerable.WithDescriptor<T>(describables, condition);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void WithDescriptorTest() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call WithDescriptorTestHelper<T>() with appropriate type parameters.");
        }

        /// <summary>
        ///A test for WithDescriptor
        ///</summary>
        public void WithDescriptorTest1Helper<T>()
            where T : IDescribable {
            IEnumerable<T> describables = null; // TODO: Initialize to an appropriate value
            Func<IDescriptor, bool> condition = null; // TODO: Initialize to an appropriate value
            IEnumerable<T> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<T> actual;
            actual = LASIEnumerable.WithDescriptor<T>(describables, condition);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void WithDescriptorTest1() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call WithDescriptorTest1Helper<T>() with appropriate type parameters.");
        }

        /// <summary>
        ///A test for Union
        ///</summary>
        public void UnionTestHelper<TLexical>()
            where TLexical : ILexical {
            IEnumerable<TLexical> first = null; // TODO: Initialize to an appropriate value
            IEnumerable<TLexical> second = null; // TODO: Initialize to an appropriate value
            Func<TLexical, TLexical, bool> comparison = null; // TODO: Initialize to an appropriate value
            IEnumerable<TLexical> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<TLexical> actual;
            actual = LASIEnumerable.Union<TLexical>(first, second, comparison);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void UnionTest() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of TLe" +
                    "xical. Please call UnionTestHelper<TLexical>() with appropriate type parameters." +
                    "");
        }

        /// <summary>
        ///A test for Union
        ///</summary>
        public void UnionTest1Helper<TLexical>()
            where TLexical : ILexical {
            ParallelQuery<TLexical> first = null; // TODO: Initialize to an appropriate value
            ParallelQuery<TLexical> second = null; // TODO: Initialize to an appropriate value
            Func<TLexical, TLexical, bool> comparison = null; // TODO: Initialize to an appropriate value
            ParallelQuery<TLexical> expected = null; // TODO: Initialize to an appropriate value
            ParallelQuery<TLexical> actual;
            actual = LASIEnumerable.Union<TLexical>(first, second, comparison);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void UnionTest1() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of TLe" +
                    "xical. Please call UnionTest1Helper<TLexical>() with appropriate type parameters" +
                    ".");
        }

        /// <summary>
        ///A test for ToSet
        ///</summary>
        public void ToSetTestHelper<TLexical>()
            where TLexical : ILexical {
            IEnumerable<TLexical> source = null; // TODO: Initialize to an appropriate value
            Func<TLexical, TLexical, bool> comparison = null; // TODO: Initialize to an appropriate value
            ISet<TLexical> expected = null; // TODO: Initialize to an appropriate value
            ISet<TLexical> actual;
            actual = LASIEnumerable.ToSet<TLexical>(source, comparison);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void ToSetTest() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of TLe" +
                    "xical. Please call ToSetTestHelper<TLexical>() with appropriate type parameters." +
                    "");
        }

        /// <summary>
        ///A test for ToSet
        ///</summary>
        public void ToSetTest1Helper<TLexical>()
            where TLexical : ILexical {
            ParallelQuery<TLexical> source = null; // TODO: Initialize to an appropriate value
            Func<TLexical, TLexical, bool> comparison = null; // TODO: Initialize to an appropriate value
            ISet<TLexical> expected = null; // TODO: Initialize to an appropriate value
            ISet<TLexical> actual;
            actual = LASIEnumerable.ToSet<TLexical>(source, comparison);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void ToSetTest1() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of TLe" +
                    "xical. Please call ToSetTest1Helper<TLexical>() with appropriate type parameters" +
                    ".");
        }

        /// <summary>
        ///A test for Singulars
        ///</summary>
        [TestMethod()]
        public void SingularsTest() {
            ParallelQuery<CommonNoun> nouns = null; // TODO: Initialize to an appropriate value
            ParallelQuery<CommonSingularNoun> expected = null; // TODO: Initialize to an appropriate value
            ParallelQuery<CommonSingularNoun> actual;
            actual = LASIEnumerable.Singulars(nouns);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Singulars
        ///</summary>
        [TestMethod()]
        public void SingularsTest1() {
            IEnumerable<ProperNoun> nouns = null; // TODO: Initialize to an appropriate value
            IEnumerable<ProperSingularNoun> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<ProperSingularNoun> actual;
            actual = LASIEnumerable.Singulars(nouns);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Singulars
        ///</summary>
        [TestMethod()]
        public void SingularsTest2() {
            IEnumerable<CommonNoun> nouns = null; // TODO: Initialize to an appropriate value
            IEnumerable<CommonSingularNoun> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<CommonSingularNoun> actual;
            actual = LASIEnumerable.Singulars(nouns);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Singulars
        ///</summary>
        [TestMethod()]
        public void SingularsTest3() {
            ParallelQuery<ProperNoun> nouns = null; // TODO: Initialize to an appropriate value
            ParallelQuery<ProperSingularNoun> expected = null; // TODO: Initialize to an appropriate value
            ParallelQuery<ProperSingularNoun> actual;
            actual = LASIEnumerable.Singulars(nouns);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for SequenceEqual
        ///</summary>
        public void SequenceEqualTestHelper<TLexical>()
            where TLexical : ILexical {
            ParallelQuery<TLexical> first = null; // TODO: Initialize to an appropriate value
            ParallelQuery<TLexical> second = null; // TODO: Initialize to an appropriate value
            Func<TLexical, TLexical, bool> comparison = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = LASIEnumerable.SequenceEqual<TLexical>(first, second, comparison);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void SequenceEqualTest() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of TLe" +
                    "xical. Please call SequenceEqualTestHelper<TLexical>() with appropriate type par" +
                    "ameters.");
        }

        /// <summary>
        ///A test for SequenceEqual
        ///</summary>
        public void SequenceEqualTest1Helper<TLexical>()
            where TLexical : ILexical {
            IEnumerable<TLexical> first = null; // TODO: Initialize to an appropriate value
            IEnumerable<TLexical> second = null; // TODO: Initialize to an appropriate value
            Func<TLexical, TLexical, bool> comparison = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = LASIEnumerable.SequenceEqual<TLexical>(first, second, comparison);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void SequenceEqualTest1() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of TLe" +
                    "xical. Please call SequenceEqualTest1Helper<TLexical>() with appropriate type pa" +
                    "rameters.");
        }

        /// <summary>
        ///A test for Referencing
        ///</summary>
        public void ReferencingTestHelper<T>()
            where T : IReferencer {
            IEnumerable<T> source = null; // TODO: Initialize to an appropriate value
            IEntity referenced = null; // TODO: Initialize to an appropriate value
            IEnumerable<T> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<T> actual;
            actual = LASIEnumerable.Referencing<T>(source, referenced);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void ReferencingTest() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call ReferencingTestHelper<T>() with appropriate type parameters.");
        }

        /// <summary>
        ///A test for Referencing
        ///</summary>
        public void ReferencingTest1Helper<T>()
            where T : IReferencer {
            IEnumerable<T> source = null; // TODO: Initialize to an appropriate value
            Func<IEntity, bool> condition = null; // TODO: Initialize to an appropriate value
            IEnumerable<T> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<T> actual;
            actual = LASIEnumerable.Referencing<T>(source, condition);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void ReferencingTest1() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call ReferencingTest1Helper<T>() with appropriate type parameters.");
        }

        /// <summary>
        ///A test for Referencing
        ///</summary>
        public void ReferencingTest2Helper<T>()
            where T : IReferencer {
            ParallelQuery<T> source = null; // TODO: Initialize to an appropriate value
            Func<IEntity, bool> condition = null; // TODO: Initialize to an appropriate value
            ParallelQuery<T> expected = null; // TODO: Initialize to an appropriate value
            ParallelQuery<T> actual;
            actual = LASIEnumerable.Referencing<T>(source, condition);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void ReferencingTest2() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call ReferencingTest2Helper<T>() with appropriate type parameters.");
        }

        /// <summary>
        ///A test for Referencing
        ///</summary>
        public void ReferencingTest3Helper<T>()
            where T : IReferencer {
            ParallelQuery<T> source = null; // TODO: Initialize to an appropriate value
            ParallelQuery<T> expected = null; // TODO: Initialize to an appropriate value
            ParallelQuery<T> actual;
            actual = LASIEnumerable.Referencing<T>(source);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void ReferencingTest3() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call ReferencingTest3Helper<T>() with appropriate type parameters.");
        }

        /// <summary>
        ///A test for Referencing
        ///</summary>
        public void ReferencingTest4Helper<T>()
            where T : IReferencer {
            ParallelQuery<T> source = null; // TODO: Initialize to an appropriate value
            IEntity referenced = null; // TODO: Initialize to an appropriate value
            ParallelQuery<T> expected = null; // TODO: Initialize to an appropriate value
            ParallelQuery<T> actual;
            actual = LASIEnumerable.Referencing<T>(source, referenced);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void ReferencingTest4() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call ReferencingTest4Helper<T>() with appropriate type parameters.");
        }

        /// <summary>
        ///A test for Referencing
        ///</summary>
        public void ReferencingTest5Helper<T>()
            where T : IReferencer {
            IEnumerable<T> source = null; // TODO: Initialize to an appropriate value
            IEnumerable<T> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<T> actual;
            actual = LASIEnumerable.Referencing<T>(source);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void ReferencingTest5() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call ReferencingTest5Helper<T>() with appropriate type parameters.");
        }

        /// <summary>
        ///A test for Plurals
        ///</summary>
        [TestMethod()]
        public void PluralsTest() {
            ParallelQuery<CommonNoun> nouns = null; // TODO: Initialize to an appropriate value
            ParallelQuery<CommonPluralNoun> expected = null; // TODO: Initialize to an appropriate value
            ParallelQuery<CommonPluralNoun> actual;
            actual = LASIEnumerable.Plurals(nouns);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Plurals
        ///</summary>
        [TestMethod()]
        public void PluralsTest1() {
            ParallelQuery<ProperNoun> nouns = null; // TODO: Initialize to an appropriate value
            ParallelQuery<ProperPluralNoun> expected = null; // TODO: Initialize to an appropriate value
            ParallelQuery<ProperPluralNoun> actual;
            actual = LASIEnumerable.Plurals(nouns);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Plurals
        ///</summary>
        [TestMethod()]
        public void PluralsTest2() {
            IEnumerable<CommonNoun> nouns = null; // TODO: Initialize to an appropriate value
            IEnumerable<CommonPluralNoun> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<CommonPluralNoun> actual;
            actual = LASIEnumerable.Plurals(nouns);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Plurals
        ///</summary>
        [TestMethod()]
        public void PluralsTest3() {
            IEnumerable<ProperNoun> nouns = null; // TODO: Initialize to an appropriate value
            IEnumerable<ProperPluralNoun> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<ProperPluralNoun> actual;
            actual = LASIEnumerable.Plurals(nouns);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for PhrasesFollowing
        ///</summary>
        [TestMethod()]
        public void PhrasesFollowingTest() {
            IEnumerable<Phrase> phrases = null; // TODO: Initialize to an appropriate value
            Phrase after = null; // TODO: Initialize to an appropriate value
            IEnumerable<Phrase> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<Phrase> actual;
            actual = LASIEnumerable.PhrasesFollowing(phrases, after);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for PhrasesFollowing
        ///</summary>
        [TestMethod()]
        public void PhrasesFollowingTest1() {
            IEnumerable<Phrase> phrases = null; // TODO: Initialize to an appropriate value
            Func<Phrase, bool> startSelector = null; // TODO: Initialize to an appropriate value
            IEnumerable<Phrase> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<Phrase> actual;
            actual = LASIEnumerable.PhrasesFollowing(phrases, startSelector);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for PhrasesFollowing
        ///</summary>
        [TestMethod()]
        public void PhrasesFollowingTest2() {
            ParallelQuery<Phrase> phrases = null; // TODO: Initialize to an appropriate value
            Func<Phrase, bool> startSelector = null; // TODO: Initialize to an appropriate value
            ParallelQuery<Phrase> expected = null; // TODO: Initialize to an appropriate value
            ParallelQuery<Phrase> actual;
            actual = LASIEnumerable.PhrasesFollowing(phrases, startSelector);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for PhrasesFollowing
        ///</summary>
        [TestMethod()]
        public void PhrasesFollowingTest3() {
            ParallelQuery<Phrase> phrases = null; // TODO: Initialize to an appropriate value
            Phrase after = null; // TODO: Initialize to an appropriate value
            ParallelQuery<Phrase> expected = null; // TODO: Initialize to an appropriate value
            ParallelQuery<Phrase> actual;
            actual = LASIEnumerable.PhrasesFollowing(phrases, after);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for OfWord
        ///</summary>
        [TestMethod()]
        public void OfWordTest() {
            IEnumerable<Paragraph> paragraphs = null; // TODO: Initialize to an appropriate value
            IEnumerable<Word> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<Word> actual;
            actual = LASIEnumerable.OfWord(paragraphs);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for OfWord
        ///</summary>
        [TestMethod()]
        public void OfWordTest1() {
            IEnumerable<Phrase> elements = null; // TODO: Initialize to an appropriate value
            IEnumerable<Word> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<Word> actual;
            actual = LASIEnumerable.OfWord(elements);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for OfWord
        ///</summary>
        [TestMethod()]
        public void OfWordTest2() {
            ParallelQuery<Sentence> sentences = null; // TODO: Initialize to an appropriate value
            ParallelQuery<Word> expected = null; // TODO: Initialize to an appropriate value
            ParallelQuery<Word> actual;
            actual = LASIEnumerable.OfWord(sentences);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for OfWord
        ///</summary>
        [TestMethod()]
        public void OfWordTest3() {
            ParallelQuery<Paragraph> paragraphs = null; // TODO: Initialize to an appropriate value
            ParallelQuery<Word> expected = null; // TODO: Initialize to an appropriate value
            ParallelQuery<Word> actual;
            actual = LASIEnumerable.OfWord(paragraphs);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for OfWord
        ///</summary>
        [TestMethod()]
        public void OfWordTest4() {
            IEnumerable<Sentence> sentences = null; // TODO: Initialize to an appropriate value
            IEnumerable<Word> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<Word> actual;
            actual = LASIEnumerable.OfWord(sentences);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for OfWord
        ///</summary>
        [TestMethod()]
        public void OfWordTest5() {
            ParallelQuery<Phrase> elements = null; // TODO: Initialize to an appropriate value
            ParallelQuery<Word> expected = null; // TODO: Initialize to an appropriate value
            ParallelQuery<Word> actual;
            actual = LASIEnumerable.OfWord(elements);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for OfVerbPhrase
        ///</summary>
        [TestMethod()]
        public void OfVerbPhraseTest() {
            ParallelQuery<Phrase> phrases = null; // TODO: Initialize to an appropriate value
            ParallelQuery<VerbPhrase> expected = null; // TODO: Initialize to an appropriate value
            ParallelQuery<VerbPhrase> actual;
            actual = LASIEnumerable.OfVerbPhrase(phrases);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for OfVerbPhrase
        ///</summary>
        [TestMethod()]
        public void OfVerbPhraseTest1() {
            IEnumerable<Phrase> phrases = null; // TODO: Initialize to an appropriate value
            IEnumerable<VerbPhrase> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<VerbPhrase> actual;
            actual = LASIEnumerable.OfVerbPhrase(phrases);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for OfVerb
        ///</summary>
        [TestMethod()]
        public void OfVerbTest() {
            IEnumerable<Word> words = null; // TODO: Initialize to an appropriate value
            VerbForm tense = new VerbForm(); // TODO: Initialize to an appropriate value
            IEnumerable<Verb> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<Verb> actual;
            actual = LASIEnumerable.OfVerb(words, tense);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for OfVerb
        ///</summary>
        [TestMethod()]
        public void OfVerbTest1() {
            IEnumerable<Word> words = null; // TODO: Initialize to an appropriate value
            IEnumerable<Verb> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<Verb> actual;
            actual = LASIEnumerable.OfVerb(words);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for OfVerb
        ///</summary>
        [TestMethod()]
        public void OfVerbTest2() {
            ParallelQuery<Word> words = null; // TODO: Initialize to an appropriate value
            VerbForm tense = new VerbForm(); // TODO: Initialize to an appropriate value
            ParallelQuery<Verb> expected = null; // TODO: Initialize to an appropriate value
            ParallelQuery<Verb> actual;
            actual = LASIEnumerable.OfVerb(words, tense);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for OfVerb
        ///</summary>
        [TestMethod()]
        public void OfVerbTest3() {
            ParallelQuery<Word> words = null; // TODO: Initialize to an appropriate value
            ParallelQuery<Verb> expected = null; // TODO: Initialize to an appropriate value
            ParallelQuery<Verb> actual;
            actual = LASIEnumerable.OfVerb(words);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for OfToLinker
        ///</summary>
        [TestMethod()]
        public void OfToLinkerTest() {
            IEnumerable<Word> words = null; // TODO: Initialize to an appropriate value
            IEnumerable<ToLinker> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<ToLinker> actual;
            actual = LASIEnumerable.OfToLinker(words);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for OfToLinker
        ///</summary>
        [TestMethod()]
        public void OfToLinkerTest1() {
            ParallelQuery<Word> words = null; // TODO: Initialize to an appropriate value
            ParallelQuery<ToLinker> expected = null; // TODO: Initialize to an appropriate value
            ParallelQuery<ToLinker> actual;
            actual = LASIEnumerable.OfToLinker(words);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for OfRelativePronoun
        ///</summary>
        [TestMethod()]
        public void OfRelativePronounTest() {
            ParallelQuery<Word> words = null; // TODO: Initialize to an appropriate value
            ParallelQuery<RelativePronoun> expected = null; // TODO: Initialize to an appropriate value
            ParallelQuery<RelativePronoun> actual;
            actual = LASIEnumerable.OfRelativePronoun(words);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for OfRelativePronoun
        ///</summary>
        [TestMethod()]
        public void OfRelativePronounTest1() {
            IEnumerable<Word> words = null; // TODO: Initialize to an appropriate value
            IEnumerable<RelativePronoun> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<RelativePronoun> actual;
            actual = LASIEnumerable.OfRelativePronoun(words);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for OfQuantifier
        ///</summary>
        [TestMethod()]
        public void OfQuantifierTest() {
            ParallelQuery<Word> words = null; // TODO: Initialize to an appropriate value
            ParallelQuery<Quantifier> expected = null; // TODO: Initialize to an appropriate value
            ParallelQuery<Quantifier> actual;
            actual = LASIEnumerable.OfQuantifier(words);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for OfQuantifier
        ///</summary>
        [TestMethod()]
        public void OfQuantifierTest1() {
            IEnumerable<Word> words = null; // TODO: Initialize to an appropriate value
            IEnumerable<Quantifier> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<Quantifier> actual;
            actual = LASIEnumerable.OfQuantifier(words);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for OfProperNoun
        ///</summary>
        [TestMethod()]
        public void OfProperNounTest() {
            ParallelQuery<Word> words = null; // TODO: Initialize to an appropriate value
            ParallelQuery<ProperNoun> expected = null; // TODO: Initialize to an appropriate value
            ParallelQuery<ProperNoun> actual;
            actual = LASIEnumerable.OfProperNoun(words);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for OfProperNoun
        ///</summary>
        [TestMethod()]
        public void OfProperNounTest1() {
            IEnumerable<Word> words = null; // TODO: Initialize to an appropriate value
            IEnumerable<ProperNoun> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<ProperNoun> actual;
            actual = LASIEnumerable.OfProperNoun(words);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for OfPronounPhrase
        ///</summary>
        [TestMethod()]
        public void OfPronounPhraseTest() {
            ParallelQuery<Phrase> phrases = null; // TODO: Initialize to an appropriate value
            ParallelQuery<PronounPhrase> expected = null; // TODO: Initialize to an appropriate value
            ParallelQuery<PronounPhrase> actual;
            actual = LASIEnumerable.OfPronounPhrase(phrases);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for OfPronounPhrase
        ///</summary>
        [TestMethod()]
        public void OfPronounPhraseTest1() {
            IEnumerable<Phrase> phrases = null; // TODO: Initialize to an appropriate value
            IEnumerable<PronounPhrase> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<PronounPhrase> actual;
            actual = LASIEnumerable.OfPronounPhrase(phrases);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for OfPronoun
        ///</summary>
        [TestMethod()]
        public void OfPronounTest() {
            IEnumerable<Word> words = null; // TODO: Initialize to an appropriate value
            IEnumerable<Pronoun> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<Pronoun> actual;
            actual = LASIEnumerable.OfPronoun(words);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for OfPronoun
        ///</summary>
        [TestMethod()]
        public void OfPronounTest1() {
            ParallelQuery<Word> words = null; // TODO: Initialize to an appropriate value
            ParallelQuery<Pronoun> expected = null; // TODO: Initialize to an appropriate value
            ParallelQuery<Pronoun> actual;
            actual = LASIEnumerable.OfPronoun(words);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for OfPrepositionalPhrase
        ///</summary>
        [TestMethod()]
        public void OfPrepositionalPhraseTest() {
            ParallelQuery<Phrase> phrases = null; // TODO: Initialize to an appropriate value
            ParallelQuery<PrepositionalPhrase> expected = null; // TODO: Initialize to an appropriate value
            ParallelQuery<PrepositionalPhrase> actual;
            actual = LASIEnumerable.OfPrepositionalPhrase(phrases);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for OfPrepositionalPhrase
        ///</summary>
        [TestMethod()]
        public void OfPrepositionalPhraseTest1() {
            IEnumerable<Phrase> phrases = null; // TODO: Initialize to an appropriate value
            IEnumerable<PrepositionalPhrase> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<PrepositionalPhrase> actual;
            actual = LASIEnumerable.OfPrepositionalPhrase(phrases);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for OfPhrase
        ///</summary>
        [TestMethod()]
        public void OfPhraseTest() {
            IEnumerable<Paragraph> paragraphs = null; // TODO: Initialize to an appropriate value
            IEnumerable<Phrase> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<Phrase> actual;
            actual = LASIEnumerable.OfPhrase(paragraphs);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for OfPhrase
        ///</summary>
        [TestMethod()]
        public void OfPhraseTest1() {
            IEnumerable<Sentence> sentences = null; // TODO: Initialize to an appropriate value
            IEnumerable<Phrase> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<Phrase> actual;
            actual = LASIEnumerable.OfPhrase(sentences);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for OfPhrase
        ///</summary>
        [TestMethod()]
        public void OfPhraseTest2() {
            IEnumerable<Clause> elements = null; // TODO: Initialize to an appropriate value
            IEnumerable<Phrase> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<Phrase> actual;
            actual = LASIEnumerable.OfPhrase(elements);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for OfPhrase
        ///</summary>
        [TestMethod()]
        public void OfPhraseTest3() {
            ParallelQuery<Sentence> sentences = null; // TODO: Initialize to an appropriate value
            ParallelQuery<Phrase> expected = null; // TODO: Initialize to an appropriate value
            ParallelQuery<Phrase> actual;
            actual = LASIEnumerable.OfPhrase(sentences);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for OfPhrase
        ///</summary>
        [TestMethod()]
        public void OfPhraseTest4() {
            ParallelQuery<Paragraph> paragraphs = null; // TODO: Initialize to an appropriate value
            ParallelQuery<Phrase> expected = null; // TODO: Initialize to an appropriate value
            ParallelQuery<Phrase> actual;
            actual = LASIEnumerable.OfPhrase(paragraphs);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for OfNounPhrase
        ///</summary>
        [TestMethod()]
        public void OfNounPhraseTest() {
            IEnumerable<Phrase> phrases = null; // TODO: Initialize to an appropriate value
            IEnumerable<NounPhrase> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<NounPhrase> actual;
            actual = LASIEnumerable.OfNounPhrase(phrases);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for OfNounPhrase
        ///</summary>
        [TestMethod()]
        public void OfNounPhraseTest1() {
            ParallelQuery<Phrase> phrases = null; // TODO: Initialize to an appropriate value
            ParallelQuery<NounPhrase> expected = null; // TODO: Initialize to an appropriate value
            ParallelQuery<NounPhrase> actual;
            actual = LASIEnumerable.OfNounPhrase(phrases);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for OfNoun
        ///</summary>
        [TestMethod()]
        public void OfNounTest() {
            IEnumerable<Word> words = null; // TODO: Initialize to an appropriate value
            IEnumerable<Noun> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<Noun> actual;
            actual = LASIEnumerable.OfNoun(words);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for OfNoun
        ///</summary>
        [TestMethod()]
        public void OfNounTest1() {
            ParallelQuery<Word> words = null; // TODO: Initialize to an appropriate value
            ParallelQuery<Noun> expected = null; // TODO: Initialize to an appropriate value
            ParallelQuery<Noun> actual;
            actual = LASIEnumerable.OfNoun(words);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for OfModal
        ///</summary>
        [TestMethod()]
        public void OfModalTest() {
            ParallelQuery<Word> words = null; // TODO: Initialize to an appropriate value
            ParallelQuery<ModalAuxilary> expected = null; // TODO: Initialize to an appropriate value
            ParallelQuery<ModalAuxilary> actual;
            actual = LASIEnumerable.OfModal(words);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for OfModal
        ///</summary>
        [TestMethod()]
        public void OfModalTest1() {
            IEnumerable<Word> words = null; // TODO: Initialize to an appropriate value
            IEnumerable<ModalAuxilary> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<ModalAuxilary> actual;
            actual = LASIEnumerable.OfModal(words);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for OfGenericNoun
        ///</summary>
        [TestMethod()]
        public void OfGenericNounTest() {
            ParallelQuery<Word> words = null; // TODO: Initialize to an appropriate value
            ParallelQuery<CommonNoun> expected = null; // TODO: Initialize to an appropriate value
            ParallelQuery<CommonNoun> actual;
            actual = LASIEnumerable.OfGenericNoun(words);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for OfGenericNoun
        ///</summary>
        [TestMethod()]
        public void OfGenericNounTest1() {
            IEnumerable<Word> words = null; // TODO: Initialize to an appropriate value
            IEnumerable<CommonNoun> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<CommonNoun> actual;
            actual = LASIEnumerable.OfGenericNoun(words);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for OfEntity
        ///</summary>
        public void OfEntityTestHelper<TLexical>() {
            ParallelQuery<TLexical> elements = null; // TODO: Initialize to an appropriate value
            ParallelQuery<IEntity> expected = null; // TODO: Initialize to an appropriate value
            ParallelQuery<IEntity> actual;
            actual = LASIEnumerable.OfEntity<TLexical>(elements);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void OfEntityTest() {
            OfEntityTestHelper<GenericParameterHelper>();
        }

        /// <summary>
        ///A test for OfEntity
        ///</summary>
        public void OfEntityTest1Helper<TLexical>()
            where TLexical : ILexical {
            IEnumerable<TLexical> elements = null; // TODO: Initialize to an appropriate value
            IEnumerable<IEntity> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<IEntity> actual;
            actual = LASIEnumerable.OfEntity<TLexical>(elements);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void OfEntityTest1() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of TLe" +
                    "xical. Please call OfEntityTest1Helper<TLexical>() with appropriate type paramet" +
                    "ers.");
        }

        /// <summary>
        ///A test for OfDeterminer
        ///</summary>
        [TestMethod()]
        public void OfDeterminerTest() {
            IEnumerable<Word> words = null; // TODO: Initialize to an appropriate value
            IEnumerable<Determiner> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<Determiner> actual;
            actual = LASIEnumerable.OfDeterminer(words);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for OfDeterminer
        ///</summary>
        [TestMethod()]
        public void OfDeterminerTest1() {
            ParallelQuery<Word> words = null; // TODO: Initialize to an appropriate value
            ParallelQuery<Determiner> expected = null; // TODO: Initialize to an appropriate value
            ParallelQuery<Determiner> actual;
            actual = LASIEnumerable.OfDeterminer(words);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for OfConjunctionPhrase
        ///</summary>
        [TestMethod()]
        public void OfConjunctionPhraseTest() {
            IEnumerable<Phrase> phrases = null; // TODO: Initialize to an appropriate value
            IEnumerable<ConjunctionPhrase> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<ConjunctionPhrase> actual;
            actual = LASIEnumerable.OfConjunctionPhrase(phrases);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for OfConjunctionPhrase
        ///</summary>
        [TestMethod()]
        public void OfConjunctionPhraseTest1() {
            ParallelQuery<Phrase> phrases = null; // TODO: Initialize to an appropriate value
            ParallelQuery<ConjunctionPhrase> expected = null; // TODO: Initialize to an appropriate value
            ParallelQuery<ConjunctionPhrase> actual;
            actual = LASIEnumerable.OfConjunctionPhrase(phrases);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for OfAdverbPhrase
        ///</summary>
        [TestMethod()]
        public void OfAdverbPhraseTest() {
            IEnumerable<Phrase> phrases = null; // TODO: Initialize to an appropriate value
            IEnumerable<AdverbPhrase> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<AdverbPhrase> actual;
            actual = LASIEnumerable.OfAdverbPhrase(phrases);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for OfAdverbPhrase
        ///</summary>
        [TestMethod()]
        public void OfAdverbPhraseTest1() {
            ParallelQuery<Phrase> phrases = null; // TODO: Initialize to an appropriate value
            ParallelQuery<AdverbPhrase> expected = null; // TODO: Initialize to an appropriate value
            ParallelQuery<AdverbPhrase> actual;
            actual = LASIEnumerable.OfAdverbPhrase(phrases);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for OfAdverb
        ///</summary>
        [TestMethod()]
        public void OfAdverbTest() {
            IEnumerable<Word> words = null; // TODO: Initialize to an appropriate value
            IEnumerable<Adverb> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<Adverb> actual;
            actual = LASIEnumerable.OfAdverb(words);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for OfAdverb
        ///</summary>
        [TestMethod()]
        public void OfAdverbTest1() {
            ParallelQuery<Word> words = null; // TODO: Initialize to an appropriate value
            ParallelQuery<Adverb> expected = null; // TODO: Initialize to an appropriate value
            ParallelQuery<Adverb> actual;
            actual = LASIEnumerable.OfAdverb(words);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for OfAdjectivePhrase
        ///</summary>
        [TestMethod()]
        public void OfAdjectivePhraseTest() {
            IEnumerable<Phrase> phrases = null; // TODO: Initialize to an appropriate value
            IEnumerable<AdjectivePhrase> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<AdjectivePhrase> actual;
            actual = LASIEnumerable.OfAdjectivePhrase(phrases);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for OfAdjectivePhrase
        ///</summary>
        [TestMethod()]
        public void OfAdjectivePhraseTest1() {
            ParallelQuery<Phrase> phrases = null; // TODO: Initialize to an appropriate value
            ParallelQuery<AdjectivePhrase> expected = null; // TODO: Initialize to an appropriate value
            ParallelQuery<AdjectivePhrase> actual;
            actual = LASIEnumerable.OfAdjectivePhrase(phrases);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for OfAdjective
        ///</summary>
        [TestMethod()]
        public void OfAdjectiveTest() {
            ParallelQuery<Word> words = null; // TODO: Initialize to an appropriate value
            ParallelQuery<Adjective> expected = null; // TODO: Initialize to an appropriate value
            ParallelQuery<Adjective> actual;
            actual = LASIEnumerable.OfAdjective(words);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for OfAdjective
        ///</summary>
        [TestMethod()]
        public void OfAdjectiveTest1() {
            IEnumerable<Word> words = null; // TODO: Initialize to an appropriate value
            IEnumerable<Adjective> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<Adjective> actual;
            actual = LASIEnumerable.OfAdjective(words);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Intersect
        ///</summary>
        public void IntersectTestHelper<TLexical>()
            where TLexical : ILexical {
            ParallelQuery<TLexical> first = null; // TODO: Initialize to an appropriate value
            ParallelQuery<TLexical> second = null; // TODO: Initialize to an appropriate value
            Func<TLexical, TLexical, bool> comparison = null; // TODO: Initialize to an appropriate value
            ParallelQuery<TLexical> expected = null; // TODO: Initialize to an appropriate value
            ParallelQuery<TLexical> actual;
            actual = LASIEnumerable.Intersect<TLexical>(first, second, comparison);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void IntersectTest() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of TLe" +
                    "xical. Please call IntersectTestHelper<TLexical>() with appropriate type paramet" +
                    "ers.");
        }

        /// <summary>
        ///A test for Intersect
        ///</summary>
        public void IntersectTest1Helper<TLexical>()
            where TLexical : ILexical {
            IEnumerable<TLexical> first = null; // TODO: Initialize to an appropriate value
            IEnumerable<TLexical> second = null; // TODO: Initialize to an appropriate value
            Func<TLexical, TLexical, bool> comparison = null; // TODO: Initialize to an appropriate value
            IEnumerable<TLexical> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<TLexical> actual;
            actual = LASIEnumerable.Intersect<TLexical>(first, second, comparison);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void IntersectTest1() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of TLe" +
                    "xical. Please call IntersectTest1Helper<TLexical>() with appropriate type parame" +
                    "ters.");
        }

        /// <summary>
        ///A test for InSubjectRole
        ///</summary>
        public void InSubjectRoleTestHelper<T>()
            where T : IEntity {
            IEnumerable<T> entities = null; // TODO: Initialize to an appropriate value
            Func<IVerbal, bool> condition = null; // TODO: Initialize to an appropriate value
            IEnumerable<T> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<T> actual;
            actual = LASIEnumerable.InSubjectRole<T>(entities, condition);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void InSubjectRoleTest() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call InSubjectRoleTestHelper<T>() with appropriate type parameters.");
        }

        /// <summary>
        ///A test for InSubjectRole
        ///</summary>
        public void InSubjectRoleTest1Helper<T>()
            where T : IEntity {
            IEnumerable<T> entities = null; // TODO: Initialize to an appropriate value
            IEnumerable<T> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<T> actual;
            actual = LASIEnumerable.InSubjectRole<T>(entities);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void InSubjectRoleTest1() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call InSubjectRoleTest1Helper<T>() with appropriate type parameters.");
        }

        /// <summary>
        ///A test for InSubjectRole
        ///</summary>
        public void InSubjectRoleTest2Helper<T>()
            where T : IEntity {
            ParallelQuery<T> entities = null; // TODO: Initialize to an appropriate value
            ParallelQuery<T> expected = null; // TODO: Initialize to an appropriate value
            ParallelQuery<T> actual;
            actual = LASIEnumerable.InSubjectRole<T>(entities);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void InSubjectRoleTest2() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call InSubjectRoleTest2Helper<T>() with appropriate type parameters.");
        }

        /// <summary>
        ///A test for InSubjectRole
        ///</summary>
        public void InSubjectRoleTest3Helper<T>()
            where T : IEntity {
            ParallelQuery<T> entities = null; // TODO: Initialize to an appropriate value
            Func<IVerbal, bool> condition = null; // TODO: Initialize to an appropriate value
            ParallelQuery<T> expected = null; // TODO: Initialize to an appropriate value
            ParallelQuery<T> actual;
            actual = LASIEnumerable.InSubjectRole<T>(entities, condition);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void InSubjectRoleTest3() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call InSubjectRoleTest3Helper<T>() with appropriate type parameters.");
        }

        /// <summary>
        ///A test for InSubjectOrObjectRole
        ///</summary>
        public void InSubjectOrObjectRoleTestHelper<T>()
            where T : IEntity {
            IEnumerable<T> entities = null; // TODO: Initialize to an appropriate value
            IEnumerable<T> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<T> actual;
            actual = LASIEnumerable.InSubjectOrObjectRole<T>(entities);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void InSubjectOrObjectRoleTest() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call InSubjectOrObjectRoleTestHelper<T>() with appropriate type parameter" +
                    "s.");
        }

        /// <summary>
        ///A test for InSubjectOrObjectRole
        ///</summary>
        public void InSubjectOrObjectRoleTest1Helper<T>()
            where T : IEntity {
            IEnumerable<T> entities = null; // TODO: Initialize to an appropriate value
            Func<IVerbal, bool> condition = null; // TODO: Initialize to an appropriate value
            IEnumerable<T> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<T> actual;
            actual = LASIEnumerable.InSubjectOrObjectRole<T>(entities, condition);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void InSubjectOrObjectRoleTest1() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call InSubjectOrObjectRoleTest1Helper<T>() with appropriate type paramete" +
                    "rs.");
        }

        /// <summary>
        ///A test for InSubjectOrObjectRole
        ///</summary>
        public void InSubjectOrObjectRoleTest2Helper<T>()
            where T : IEntity {
            ParallelQuery<T> entities = null; // TODO: Initialize to an appropriate value
            ParallelQuery<T> expected = null; // TODO: Initialize to an appropriate value
            ParallelQuery<T> actual;
            actual = LASIEnumerable.InSubjectOrObjectRole<T>(entities);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void InSubjectOrObjectRoleTest2() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call InSubjectOrObjectRoleTest2Helper<T>() with appropriate type paramete" +
                    "rs.");
        }

        /// <summary>
        ///A test for InSubjectOrObjectRole
        ///</summary>
        public void InSubjectOrObjectRoleTest3Helper<T>()
            where T : IEntity {
            ParallelQuery<T> entities = null; // TODO: Initialize to an appropriate value
            Func<IVerbal, bool> condition = null; // TODO: Initialize to an appropriate value
            ParallelQuery<T> expected = null; // TODO: Initialize to an appropriate value
            ParallelQuery<T> actual;
            actual = LASIEnumerable.InSubjectOrObjectRole<T>(entities, condition);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void InSubjectOrObjectRoleTest3() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call InSubjectOrObjectRoleTest3Helper<T>() with appropriate type paramete" +
                    "rs.");
        }

        /// <summary>
        ///A test for InObjectRole
        ///</summary>
        public void InObjectRoleTestHelper<T>()
            where T : IEntity {
            IEnumerable<T> entities = null; // TODO: Initialize to an appropriate value
            Func<IVerbal, bool> condition = null; // TODO: Initialize to an appropriate value
            IEnumerable<T> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<T> actual;
            actual = LASIEnumerable.InObjectRole<T>(entities, condition);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void InObjectRoleTest() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call InObjectRoleTestHelper<T>() with appropriate type parameters.");
        }

        /// <summary>
        ///A test for InObjectRole
        ///</summary>
        public void InObjectRoleTest1Helper<T>()
            where T : IEntity {
            IEnumerable<T> entities = null; // TODO: Initialize to an appropriate value
            IEnumerable<T> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<T> actual;
            actual = LASIEnumerable.InObjectRole<T>(entities);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void InObjectRoleTest1() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call InObjectRoleTest1Helper<T>() with appropriate type parameters.");
        }

        /// <summary>
        ///A test for InObjectRole
        ///</summary>
        public void InObjectRoleTest2Helper<T>()
            where T : IEntity {
            ParallelQuery<T> entities = null; // TODO: Initialize to an appropriate value
            ParallelQuery<T> expected = null; // TODO: Initialize to an appropriate value
            ParallelQuery<T> actual;
            actual = LASIEnumerable.InObjectRole<T>(entities);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void InObjectRoleTest2() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call InObjectRoleTest2Helper<T>() with appropriate type parameters.");
        }

        /// <summary>
        ///A test for InObjectRole
        ///</summary>
        public void InObjectRoleTest3Helper<T>()
            where T : IEntity {
            ParallelQuery<T> entities = null; // TODO: Initialize to an appropriate value
            Func<IVerbal, bool> condition = null; // TODO: Initialize to an appropriate value
            ParallelQuery<T> expected = null; // TODO: Initialize to an appropriate value
            ParallelQuery<T> actual;
            actual = LASIEnumerable.InObjectRole<T>(entities, condition);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void InObjectRoleTest3() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call InObjectRoleTest3Helper<T>() with appropriate type parameters.");
        }

        /// <summary>
        ///A test for InIndirectObjectRole
        ///</summary>
        public void InIndirectObjectRoleTestHelper<T>()
            where T : IEntity {
            ParallelQuery<T> entities = null; // TODO: Initialize to an appropriate value
            ParallelQuery<T> expected = null; // TODO: Initialize to an appropriate value
            ParallelQuery<T> actual;
            actual = LASIEnumerable.InIndirectObjectRole<T>(entities);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void InIndirectObjectRoleTest() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call InIndirectObjectRoleTestHelper<T>() with appropriate type parameters" +
                    ".");
        }

        /// <summary>
        ///A test for InIndirectObjectRole
        ///</summary>
        public void InIndirectObjectRoleTest1Helper<T>()
            where T : IEntity {
            IEnumerable<T> entities = null; // TODO: Initialize to an appropriate value
            IEnumerable<T> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<T> actual;
            actual = LASIEnumerable.InIndirectObjectRole<T>(entities);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void InIndirectObjectRoleTest1() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call InIndirectObjectRoleTest1Helper<T>() with appropriate type parameter" +
                    "s.");
        }

        /// <summary>
        ///A test for InIndirectObjectRole
        ///</summary>
        public void InIndirectObjectRoleTest2Helper<T>()
            where T : IEntity {
            ParallelQuery<T> entities = null; // TODO: Initialize to an appropriate value
            Func<IVerbal, bool> condition = null; // TODO: Initialize to an appropriate value
            ParallelQuery<T> expected = null; // TODO: Initialize to an appropriate value
            ParallelQuery<T> actual;
            actual = LASIEnumerable.InIndirectObjectRole<T>(entities, condition);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void InIndirectObjectRoleTest2() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call InIndirectObjectRoleTest2Helper<T>() with appropriate type parameter" +
                    "s.");
        }

        /// <summary>
        ///A test for InIndirectObjectRole
        ///</summary>
        public void InIndirectObjectRoleTest3Helper<T>()
            where T : IEntity {
            IEnumerable<T> entities = null; // TODO: Initialize to an appropriate value
            Func<IVerbal, bool> condition = null; // TODO: Initialize to an appropriate value
            IEnumerable<T> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<T> actual;
            actual = LASIEnumerable.InIndirectObjectRole<T>(entities, condition);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void InIndirectObjectRoleTest3() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call InIndirectObjectRoleTest3Helper<T>() with appropriate type parameter" +
                    "s.");
        }

        /// <summary>
        ///A test for InDirectObjectRole
        ///</summary>
        public void InDirectObjectRoleTestHelper<T>()
            where T : IEntity {
            IEnumerable<T> entities = null; // TODO: Initialize to an appropriate value
            Func<IVerbal, bool> condition = null; // TODO: Initialize to an appropriate value
            IEnumerable<T> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<T> actual;
            actual = LASIEnumerable.InDirectObjectRole<T>(entities, condition);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void InDirectObjectRoleTest() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call InDirectObjectRoleTestHelper<T>() with appropriate type parameters.");
        }

        /// <summary>
        ///A test for InDirectObjectRole
        ///</summary>
        public void InDirectObjectRoleTest1Helper<T>()
            where T : IEntity {
            ParallelQuery<T> entities = null; // TODO: Initialize to an appropriate value
            Func<IVerbal, bool> condition = null; // TODO: Initialize to an appropriate value
            ParallelQuery<T> expected = null; // TODO: Initialize to an appropriate value
            ParallelQuery<T> actual;
            actual = LASIEnumerable.InDirectObjectRole<T>(entities, condition);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void InDirectObjectRoleTest1() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call InDirectObjectRoleTest1Helper<T>() with appropriate type parameters." +
                    "");
        }

        /// <summary>
        ///A test for InDirectObjectRole
        ///</summary>
        public void InDirectObjectRoleTest2Helper<T>()
            where T : IEntity {
            ParallelQuery<T> entities = null; // TODO: Initialize to an appropriate value
            ParallelQuery<T> expected = null; // TODO: Initialize to an appropriate value
            ParallelQuery<T> actual;
            actual = LASIEnumerable.InDirectObjectRole<T>(entities);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void InDirectObjectRoleTest2() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call InDirectObjectRoleTest2Helper<T>() with appropriate type parameters." +
                    "");
        }

        /// <summary>
        ///A test for InDirectObjectRole
        ///</summary>
        public void InDirectObjectRoleTest3Helper<T>()
            where T : IEntity {
            IEnumerable<T> entities = null; // TODO: Initialize to an appropriate value
            IEnumerable<T> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<T> actual;
            actual = LASIEnumerable.InDirectObjectRole<T>(entities);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void InDirectObjectRoleTest3() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call InDirectObjectRoleTest3Helper<T>() with appropriate type parameters." +
                    "");
        }

        /// <summary>
        ///A test for Except
        ///</summary>
        public void ExceptTestHelper<TLexical>()
            where TLexical : ILexical {
            IEnumerable<TLexical> first = null; // TODO: Initialize to an appropriate value
            IEnumerable<TLexical> second = null; // TODO: Initialize to an appropriate value
            Func<TLexical, TLexical, bool> comparison = null; // TODO: Initialize to an appropriate value
            IEnumerable<TLexical> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<TLexical> actual;
            actual = LASIEnumerable.Except<TLexical>(first, second, comparison);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void ExceptTest() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of TLe" +
                    "xical. Please call ExceptTestHelper<TLexical>() with appropriate type parameters" +
                    ".");
        }

        /// <summary>
        ///A test for Except
        ///</summary>
        public void ExceptTest1Helper<TLexical>()
            where TLexical : ILexical {
            ParallelQuery<TLexical> first = null; // TODO: Initialize to an appropriate value
            ParallelQuery<TLexical> second = null; // TODO: Initialize to an appropriate value
            Func<TLexical, TLexical, bool> comparison = null; // TODO: Initialize to an appropriate value
            ParallelQuery<TLexical> expected = null; // TODO: Initialize to an appropriate value
            ParallelQuery<TLexical> actual;
            actual = LASIEnumerable.Except<TLexical>(first, second, comparison);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void ExceptTest1() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of TLe" +
                    "xical. Please call ExceptTest1Helper<TLexical>() with appropriate type parameter" +
                    "s.");
        }

        /// <summary>
        ///A test for Distinct
        ///</summary>
        public void DistinctTestHelper<TLexical>()
            where TLexical : ILexical {
            ParallelQuery<TLexical> elements = null; // TODO: Initialize to an appropriate value
            Func<TLexical, TLexical, bool> comparison = null; // TODO: Initialize to an appropriate value
            ParallelQuery<TLexical> expected = null; // TODO: Initialize to an appropriate value
            ParallelQuery<TLexical> actual;
            actual = LASIEnumerable.Distinct<TLexical>(elements, comparison);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void DistinctTest() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of TLe" +
                    "xical. Please call DistinctTestHelper<TLexical>() with appropriate type paramete" +
                    "rs.");
        }

        /// <summary>
        ///A test for Distinct
        ///</summary>
        public void DistinctTest1Helper<TLexical>()
            where TLexical : ILexical {
            IEnumerable<TLexical> elements = null; // TODO: Initialize to an appropriate value
            Func<TLexical, TLexical, bool> comparison = null; // TODO: Initialize to an appropriate value
            IEnumerable<TLexical> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<TLexical> actual;
            actual = LASIEnumerable.Distinct<TLexical>(elements, comparison);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void DistinctTest1() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of TLe" +
                    "xical. Please call DistinctTest1Helper<TLexical>() with appropriate type paramet" +
                    "ers.");
        }

        /// <summary>
        ///A test for Contains
        ///</summary>
        public void ContainsTestHelper<TLexical>()
            where TLexical : ILexical {
            IEnumerable<TLexical> elements = null; // TODO: Initialize to an appropriate value
            TLexical element = default(TLexical); // TODO: Initialize to an appropriate value
            Func<TLexical, TLexical, bool> comparison = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = LASIEnumerable.Contains<TLexical>(elements, element, comparison);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void ContainsTest() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of TLe" +
                    "xical. Please call ContainsTestHelper<TLexical>() with appropriate type paramete" +
                    "rs.");
        }

        /// <summary>
        ///A test for Contains
        ///</summary>
        public void ContainsTest1Helper<TLexical>()
            where TLexical : ILexical {
            ParallelQuery<TLexical> elements = null; // TODO: Initialize to an appropriate value
            TLexical element = default(TLexical); // TODO: Initialize to an appropriate value
            Func<TLexical, TLexical, bool> comparison = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = LASIEnumerable.Contains<TLexical>(elements, element, comparison);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void ContainsTest1() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of TLe" +
                    "xical. Please call ContainsTest1Helper<TLexical>() with appropriate type paramet" +
                    "ers.");
        }

        /// <summary>
        ///A test for Between
        ///</summary>
        [TestMethod()]
        public void BetweenTest() {
            Phrase after = null; // TODO: Initialize to an appropriate value
            Phrase before = null; // TODO: Initialize to an appropriate value
            IEnumerable<Phrase> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<Phrase> actual;
            actual = LASIEnumerable.Between(after, before);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Between
        ///</summary>
        [TestMethod()]
        public void BetweenTest1() {
            Phrase after = null; // TODO: Initialize to an appropriate value
            Func<Phrase, bool> endSelector = null; // TODO: Initialize to an appropriate value
            IEnumerable<Phrase> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<Phrase> actual;
            actual = LASIEnumerable.Between(after, endSelector);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for AsNestedEnumerable
        ///</summary>
        public void AsNestedEnumerableTestHelper<T>()
            where T : class , ILexical {
            IEnumerable<T> source = null; // TODO: Initialize to an appropriate value
            IEnumerable<T> expected = null; // TODO: Initialize to an appropriate value
            IEnumerable<T> actual;
            actual = LASIEnumerable.AsNestedEnumerable<T>(source);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        [TestMethod()]
        public void AsNestedEnumerableTest() {
            Assert.Inconclusive("No appropriate type parameter is found to satisfies the type constraint(s) of T. " +
                    "Please call AsNestedEnumerableTestHelper<T>() with appropriate type parameters.");
        }
    }
}
