﻿using LASI.Core;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using NFluent;
using LASI.Testing.Assertions;

namespace LASI.Core.Tests
{
    /// <summary>
    ///This is a test class for IInderectObjectTakerTest and is intended
    ///to contain all IInderectObjectTakerTest Unit Tests
    /// </summary>
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
        [Fact]
        public void BindIndirectObjectTest()
        {
            var target = CreateIInderectObjectTaker();
            IEntity indirectObject = new NounPhrase(new PossessivePronoun("my"), new CommonSingularNoun("friend"));
            target.BindIndirectObject(indirectObject);
            Check.That(indirectObject).Satisfies(target.IndirectObjects.Contains).And.Satisfies(target.AggregateIndirectObject.Contains);
        }

        /// <summary>
        ///A test for AggregateIndirectObject
        /// </summary>
        [Fact]
        public void AggregateIndirectObjectTest()
        {
            var target = CreateIInderectObjectTaker();
            IAggregateEntity actual =
                new AggregateEntity(new IEntity[] {
                    new PersonalPronoun("him"),
                    new ProperSingularNoun("Patrick"),
                    new NounPhrase(new ProperSingularNoun("Brittany"))
                });
            actual = target.AggregateIndirectObject;
            Assert.True(target.AggregateIndirectObject.SequenceEqual(actual));
        }

        /// <summary>
        ///A test for IndirectObjects
        /// </summary>
        [Fact]
        public void IndirectObjectsTest()
        {
            var target = CreateIInderectObjectTaker();
            IEnumerable<IEntity> actual = new IEntity[] {
                    new PersonalPronoun("him"),
                    new ProperSingularNoun("Patrick"),
                    new NounPhrase(new ProperSingularNoun("Brittany") )
                };
            actual = target.IndirectObjects;
            Assert.True(target.IndirectObjects.SequenceEqual(actual));
        }
    }
}
