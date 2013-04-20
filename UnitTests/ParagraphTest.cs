using LASI.Algorithm;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace AlgorithmAssemblyUnitTestProject
{


    /// <summary>
    ///This is a test class for ParagraphTest and is intended
    ///to contain all ParagraphTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ParagraphTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
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
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void ToStringTest()
        {
            Phrase[] phrases1 = new Phrase[] { 
                new NounPhrase(new Word[] { new ProperSingularNoun("LASI") }), 
                new VerbPhrase(new Word[] { new PastTenseVerb("found") }),
                new NounPhrase(new Word[] { new ProperPluralNoun("TIMIS") })
            };
            Sentence[] sentences = new Sentence[3];
            sentences[0] = new Sentence(phrases1, new SentencePunctuation('.'));
            Phrase[] phrases2 = new Phrase[] { 
                new NounPhrase(new Word[] { new ProperSingularNoun("LASI") }),
                new VerbPhrase(new Word[] { new PastTenseVerb("SNIFd") }),
                new NounPhrase(new Word[] { new ProperPluralNoun("them") })
            };
            sentences[1] = new Sentence(phrases2, new SentencePunctuation('.'));
            Phrase[] phrases3 = new Phrase[] {
                new NounPhrase(new Word[] { new ProperSingularNoun("Richard") }),
                new VerbPhrase(new Word[] { new PastTenseVerb("did") }), 
                new NounPhrase(new Word[] { new ProperPluralNoun("awesome") }) 
            };
            sentences[2] = new Sentence(phrases3, new SentencePunctuation('.'));

            Paragraph target = new Paragraph(sentences);

            string expected = String.Format("LASI.Algorithm.Paragraph {0} sentences\n\"LASI found TIMIS. LASI SNIFd them. Richard did awesome.\"", sentences.Length);
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Text
        ///</summary>
        [TestMethod()]
        public void TextTest()
        {
            Phrase[] phrases1 = new Phrase[] { 
                new NounPhrase(new Word[] { new ProperSingularNoun("LASI") }),
                new VerbPhrase(new Word[] { new PastTenseVerb("found")}),
                new NounPhrase(new Word[] { new ProperPluralNoun("TIMIS")})
            };
            Sentence[] sentences = new Sentence[3];
            sentences[0] = new Sentence(phrases1, new SentencePunctuation('.'));
            Phrase[] phrases2 = new Phrase[] { 
                new NounPhrase(new Word[] { new ProperSingularNoun("LASI") }), 
                new VerbPhrase(new Word[] { new PastTenseVerb("SNIFd") }), 
                new NounPhrase(new Word[] { new ProperPluralNoun("them") })
            };
            sentences[1] = new Sentence(phrases2, new SentencePunctuation('.'));
            Phrase[] phrases3 = new Phrase[] {
                new NounPhrase(new Word[] { new ProperSingularNoun("Richard") }),
                new VerbPhrase(new Word[] { new PastTenseVerb("did") }),
                new NounPhrase(new Word[] { new ProperPluralNoun("awesome") }) 
            };
            sentences[2] = new Sentence(phrases3, new SentencePunctuation('.'));

            Paragraph target = new Paragraph(sentences);

            string expected = "LASI found TIMIS. LASI SNIFd them. Richard did awesome.";
            string actual;
            actual = target.Text;
            Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for EstablishParent
        ///</summary>
        //[TestMethod()]
        /*   public void EstablishParentTest()
           {
               Phrase[] phrases1 = new Phrase[] { 
                   new NounPhrase(new Word[] { new ProperSingularNoun("LASI") }),
                   new VerbPhrase(new Word[] { new PastTenseVerb("found")}),
                   new NounPhrase(new Word[] { new ProperPluralNoun("TIMIS")})
               };
               Sentence[] sentences = new Sentence[3];
               sentences[0] = new Sentence(phrases1, new SentencePunctuation('.'));
               Phrase[] phrases2 = new Phrase[] { 
                   new NounPhrase(new Word[] { new ProperSingularNoun("LASI") }), 
                   new VerbPhrase(new Word[] { new PastTenseVerb("SNIFd") }), 
                   new NounPhrase(new Word[] { new ProperPluralNoun("them") })
               };
               sentences[1] = new Sentence(phrases2, new SentencePunctuation('.'));
               Phrase[] phrases3 = new Phrase[] {
                   new NounPhrase(new Word[] { new ProperSingularNoun("Richard") }),
                   new VerbPhrase(new Word[] { new PastTenseVerb("did") }),
                   new NounPhrase(new Word[] { new ProperPluralNoun("awesome") }) 
               };
               sentences[2] = new Sentence(phrases3, new SentencePunctuation('.'));

               Paragraph target = new Paragraph(sentences);
               Document parentDoc = new Document(target) ; // TODO: Initialize to an appropriate value
               target.EstablishParent(parentDoc);
               Assert.AreEqual(target.ParentDocument, parentDoc);
           }*/
       }

    }
