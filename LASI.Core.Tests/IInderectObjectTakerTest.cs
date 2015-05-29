﻿using LASI.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LASI.Core.Tests
{


    /// <summary>
    ///This is a test class for IInderectObjectTakerTest and is intended
    ///to contain all IInderectObjectTakerTest Unit Tests
    /// </summary>
    [TestClass]
    public class IInderectObjectTakerTest
    {
        internal virtual IInderectObjectTaker CreateIInderectObjectTaker()
        {
            IInderectObjectTaker target = new BaseVerb("walk");
            return target;
        }

        /// <summary>
        ///A test for BindIndirectObject
        /// </summary>
        [TestMethod]
        public void BindIndirectObjectTest()
        {
            IInderectObjectTaker target = CreateIInderectObjectTaker();
            IEntity indirectObject = new NounPhrase(new Word[] { new PossessivePronoun("my"), new CommonSingularNoun("friend") });
            target.BindIndirectObject(indirectObject);
            Assert.IsTrue(target.IndirectObjects.Contains(indirectObject));
            Assert.IsTrue(target.AggregateIndirectObject.Contains(indirectObject));
        }

        /// <summary>
        ///A test for AggregateIndirectObject
        /// </summary>
        [TestMethod]
        public void AggregateIndirectObjectTest()
        {
            IInderectObjectTaker target = CreateIInderectObjectTaker();
            IAggregateEntity actual =
                new AggregateEntity(new IEntity[] {
                    new PersonalPronoun("him"),
                    new ProperSingularNoun("Patrick"),
                    new NounPhrase(new Word[] { new ProperSingularNoun("Brittany") })
                });
            actual = target.AggregateIndirectObject;
            Assert.IsTrue(target.AggregateIndirectObject.SequenceEqual(actual));
        }

        /// <summary>
        ///A test for IndirectObjects
        /// </summary>
        [TestMethod]
        public void IndirectObjectsTest()
        {
            IInderectObjectTaker target = CreateIInderectObjectTaker();
            IEnumerable<IEntity> actual = new IEntity[] {
                    new PersonalPronoun("him"),
                    new ProperSingularNoun("Patrick"),
                    new NounPhrase(new Word[] { new ProperSingularNoun("Brittany") })
                };
            actual = target.IndirectObjects;
            Assert.IsTrue(target.IndirectObjects.SequenceEqual(actual));
        }
    }
}
