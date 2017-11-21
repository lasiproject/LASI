using LASI.Core;
using System;
using System.Linq;
using System.Collections.Generic;
using Xunit;
using NFluent;

namespace LASI.Core.Tests
{
    /// <summary>
    ///This is a test class for IDirectObjectTakerTest and is intended
    ///to contain all IDirectObjectTakerTest Unit Tests
    /// </summary>
    public class IDirectObjectTakerTest
    {
        /// <summary>
        ///A test for BindDirectObject
        /// </summary>
        [Fact]
        public void BindDirectObjectTest()
        {
            var target = new BaseVerb("slay");
            IEntity directObject = new PersonalPronoun("them");
            target.BindDirectObject(directObject);
            Check.That(target.DirectObjects).Contains(directObject);
        }

        /// <summary>
        ///A test for AggregateDirectObject
        /// </summary>
        [Fact]
        public void AggregateDirectObjectTest()
        {
            var target = new BaseVerb("slay");
            IAggregateEntity aggregateObject = new AggregateEntity(
                new NounPhrase(new ProperSingularNoun("John"), new ProperSingularNoun("Smith")),
                new NounPhrase(new PossessivePronoun("his"), new CommonPluralNoun("cats"))
            );

            target.BindDirectObject(aggregateObject);

            var actual = target.AggregateDirectObject;

            Check.That(actual).ContainsExactly(aggregateObject).And.Not.IsNull().And.Not.IsEmpty();
        }

        /// <summary>
        ///A test for DirectObjects
        /// </summary>
        [Fact]
        public void DirectObjectsTest()
        {
            var target = new BaseVerb("slay");
            IEnumerable<IEntity> actual = target.DirectObjects;
            Check.That(target.DirectObjects).IsEqualTo(actual);
        }
    }
}
