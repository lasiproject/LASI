using NFluent;
using System;
using System.Collections.Generic;
using Xunit;

namespace LASI.Core.Analysis.Relationships.Tests
{
    public class RelationshipInferenceExtensionsTest
    {
        /// <summary>
        ///A test for SetRelationshipLookup
        /// </summary>
        [Fact]
        public void SetRelationshipLookupTest1()
        {
            IEntity entity1 = new ProperSingularNoun("John");
            IVerbal verb = new PastTenseVerb("walked");
            IEntity entity2 = new NounPhrase(new Determiner("the"), new CommonSingularNoun("store"));
            verb.BindSubject(entity1);
            verb.BindDirectObject(entity2);
            IEnumerable<IVerbal> domain = new[] { verb };
            IRelationshipLookup<IEntity, IVerbal> relationshipLookup = CreateRelationshipLookup(domain);

            // Without calling RelationShipInferenceExtensions.SetRelationshipLookup(entity1, relationshipLookup) beforehand
            Check.ThatCode(() => entity1.IsRelatedTo(entity2)).Throws<InvalidOperationException>();
        }

        private static RelationshipLookup<IEntity, IVerbal> CreateRelationshipLookup(IEnumerable<IVerbal> domain) =>
            new RelationshipLookup<IEntity, IVerbal>(domain, Equals, Equals, Equals);

        /// <summary>
        ///A test for SetRelationshipLookup
        /// </summary>
        [Fact]
        public void SetRelationshipLookupTest2()
        {
            IEntity entity1 = new ProperSingularNoun("John");
            IVerbal verb = new PastTenseVerb("walked");
            IEntity entity2 = new NounPhrase(new Determiner("the"), new CommonSingularNoun("store"));
            verb.BindSubject(entity1);
            verb.BindDirectObject(entity2);
            IEnumerable<IVerbal> domain = new[] { verb };
            IRelationshipLookup<IEntity, IVerbal> relationshipLookup = CreateRelationshipLookup(domain);

            // Without calling RelationShipInferenceExtensions.SetRelationshipLookup(entity1, relationshipLookup) beforehand
            Check.ThatCode(() => entity1.IsRelatedTo(new NounPhrase(new Determiner("the"), new CommonSingularNoun("store")))).Throws<InvalidOperationException>();
        }
        /// <summary>
        ///A test for SetRelationshipLookup
        /// </summary>
        [Fact]
        public void SetRelationshipLookupTest3()
        {
            IEntity entity1 = new ProperSingularNoun("John");
            IVerbal verb = new PastTenseVerb("walked");
            IEntity entity2 = new NounPhrase(new Determiner("the"), new CommonSingularNoun("store"));
            verb.BindSubject(entity1);
            verb.BindDirectObject(entity2);
            IEnumerable<IVerbal> domain = new[] { verb };
            IRelationshipLookup<IEntity, IVerbal> relationshipLookup = CreateRelationshipLookup(domain);
            RelationShipInferenceExtensions.SetRelationshipLookup(entity1, relationshipLookup);
            ActionsRelatedOn? expected = null;
            ActionsRelatedOn? actual;
            actual = entity1.IsRelatedTo(entity2);
            Check.That(actual).IsNotEqualTo(expected); // After calling RelationShipInferenceExtensions.SetRelationshipLookup(entity1, relationshipLookup);
        }
        /// <summary>
        ///A test for SetRelationshipLookup
        /// </summary>
        [Fact]
        public void SetRelationshipLookupTest4()
        {
            IEntity entity1 = new ProperSingularNoun("John");
            IVerbal verb = new PastTenseVerb("walked");
            IEntity entity2 = new NounPhrase(new Determiner("the"), new CommonSingularNoun("store"));
            verb.BindSubject(entity1);
            verb.BindDirectObject(entity2); IEnumerable<IVerbal> domain = new[] { verb };
            IRelationshipLookup<IEntity, IVerbal> relationshipLookup = CreateRelationshipLookup(domain);
            RelationShipInferenceExtensions.SetRelationshipLookup(entity1, relationshipLookup);
            ActionsRelatedOn? expected = null;
            ActionsRelatedOn? actual;
            actual = entity1.IsRelatedTo(new NounPhrase(new Determiner("the"), new CommonSingularNoun("store")));
            Check.That(actual).IsEqualTo(expected);// After calling RelationShipInferenceExtensions.SetRelationshipLookup(entity1, relationshipLookup);
        }

        /// <summary>
        ///A test for On
        /// </summary>
        [Fact]
        public void OnTest1()
        {
            ActionsRelatedOn? relatorSet = null;
            IVerbal relator = new PastTenseVerb("walked");
            var expected = false;
            bool actual;
            actual = RelationShipInferenceExtensions.On(relatorSet, relator);
            Check.That(actual).IsEqualTo(expected);
        }
        /// <summary>
        ///A test for On
        /// </summary>
        [Fact]
        public void OnTest2()
        {
            IVerbal relator = new PastTenseVerb("walked");
            ActionsRelatedOn? relatorSet = new ActionsRelatedOn(new[] { relator });
            var expected = true;
            bool actual;
            actual = RelationShipInferenceExtensions.On(relatorSet, relator);
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        ///A test for IsRelatedTo
        /// </summary>
        [Fact]
        public void IsRelatedToTest()
        {
            IEntity performer = new CommonPluralNoun("dogs");
            IEntity receiver = new CommonPluralNoun("cats");
            IVerbal relator = new BaseVerb("chase");
            relator.BindSubject(performer);
            relator.BindDirectObject(receiver);
            performer.SetRelationshipLookup(new RelationshipLookup<IEntity, IVerbal>(new[] { relator }, Equals, Equals, Equals));
            ActionsRelatedOn? expected = new ActionsRelatedOn(new[] { relator });
            ActionsRelatedOn? actual;
            actual = RelationShipInferenceExtensions.IsRelatedTo(performer, receiver);
            Check.That(actual).IsEqualTo(expected);
        }
        /// <summary>
        ///A test for IsRelatedTo
        /// </summary>
        [Fact]
        public void IsRelatedToOnTest1()
        {
            IEntity performer = new CommonPluralNoun("dogs");
            IEntity receiver = new CommonPluralNoun("cats");
            IVerbal relator = new BaseVerb("chase");
            relator.BindSubject(performer);
            relator.BindDirectObject(receiver);
            performer.SetRelationshipLookup(new RelationshipLookup<IEntity, IVerbal>(new[] { relator }, Equals, Equals, Equals));
            var expected = true;
            bool actual;
            actual = RelationShipInferenceExtensions.IsRelatedTo(performer, receiver).On(relator);
            Check.That(actual).IsEqualTo(expected);
        }
        /// <summary>
        ///A test for IsRelatedTo
        /// </summary>
        [Fact]
        public void IsRelatedToOnTest2()
        {
            IEntity performer = new CommonPluralNoun("dogs");
            IEntity receiver = new CommonPluralNoun("cats");
            IVerbal relator = new BaseVerb("chase");
            relator.BindSubject(performer);
            relator.BindDirectObject(receiver);
            performer.SetRelationshipLookup(new RelationshipLookup<IEntity, IVerbal>(new[] { relator }, Equals, Equals, Equals));
            var expected = true;
            bool actual;
            actual = RelationShipInferenceExtensions.IsRelatedTo(receiver, performer).On(relator);
            Check.That(actual).IsEqualTo(expected);
        }
    }
}