using LASI.Core;
using System;
using System.Linq;
using System.Collections.Generic;
using Xunit;

namespace LASI.Core.Tests
{
    /// <summary>
    ///This is a test class for IDirectObjectTakerTest and is intended
    ///to contain all IDirectObjectTakerTest Unit Tests
    /// </summary>
    public class IDirectObjectTakerTest
    {
        internal virtual IDirectObjectTaker CreateIDirectObjectTaker()
        {
            IDirectObjectTaker target = new BaseVerb("slay");
            return target;
        }

        /// <summary>
        ///A test for BindDirectObject
        /// </summary>
        [Fact]
        public void BindDirectObjectTest()
        {
            var target = CreateIDirectObjectTaker();
            IEntity directObject = new PersonalPronoun("them");
            target.BindDirectObject(directObject);
            Assert.True(target.DirectObjects.Any(o => o == directObject));
        }

        /// <summary>
        ///A test for AggregateDirectObject
        /// </summary>
        [Fact]
        public void AggregateDirectObjectTest()
        {
            var target = CreateIDirectObjectTaker();
            IAggregateEntity aggregateObject = new AggregateEntity(new[]{
                new NounPhrase(new Word[]{new ProperSingularNoun("John"),new ProperSingularNoun( "Smith")}),
                new NounPhrase(new Word[]{new PossessivePronoun("his"),new CommonPluralNoun("cats")})
            });
            target.BindDirectObject(aggregateObject);
            var actual = target.AggregateDirectObject;
            Assert.False(actual.Except(aggregateObject).Any());
        }

        /// <summary>
        ///A test for DirectObjects
        /// </summary>
        [Fact]
        public void DirectObjectsTest()
        {
            var target = CreateIDirectObjectTaker();
            IEnumerable<IEntity> actual;
            actual = target.DirectObjects;
            Assert.Equal(target.DirectObjects, actual);
        }
    }
}
