﻿using LASI.Content;
using LASI.Content.Tagging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LASI.Content.Tests
{


    /// <summary>
    ///This is a test class for TaggedTextFragmentTest and is intended
    ///to contain all TaggedTextFragmentTest Unit Tests
    /// </summary>
    [TestClass]
    public class TaggedTextFragmentTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes

        #endregion


        private Tagger Tagger => new Tagger();

        /// <summary>
        ///A test for TaggedTextFragment Constructor
        /// </summary>
        [TestMethod]
        public void TaggedTextFragmentConstructorTest()
        {
            var lines = Tagger.TaggedFromRaw(new[] {
                "This is a test which i will not regret.",
                "While it may yield me, in the context of the system at large, only ",
                "a little confidence, each test makes everything else that much better."
            });
            string name = "Test Fragment";
            TaggedTextFragment target = new TaggedTextFragment(lines, name);
            Assert.AreEqual(target.Name, name);
        }

        /// <summary>
        ///A test for LoadText
        /// </summary>
        [TestMethod]
        public void LoadTextTest()
        {
            var lines = Tagger.TaggedFromRaw(new[] {
                "This is a test which i will not regret.",
                "While it may yield me, in the context of the system at large, only ",
                "a little confidence, each test makes everything else that much better."
            });
            string name = "Test Fragment";
            TaggedTextFragment target = new TaggedTextFragment(lines, name);
            string expected = string.Join("\n", lines);
            string actual;
            actual = target.LoadText();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for LoadTextAsync
        /// </summary>
        [TestMethod]
        public void LoadTextAsyncTest()
        {
            var lines = Tagger.TaggedFromRaw(new[] {
                "This is a test which i will not regret.",
                "While it may yield me, in the context of the system at large, only ",
                "a little confidence, each test makes everything else that much better."
            });
            string name = "Test Fragment";
            TaggedTextFragment target = new TaggedTextFragment(lines, name);
            string expected = string.Join("\n", lines);
            string actual = null;
            Task.WaitAll(Task.Run(async () => actual = await target.LoadTextAsync()));
            Assert.AreEqual(expected, actual);
        }
    }
}
