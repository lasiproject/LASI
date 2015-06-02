using LASI.Utilities.Specialized;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Shared.Test.Attributes;
using LASI.Utilities.Specialized.Option;

namespace LASI.Utilities.Tests
{
    [TestClass]
    public class OptionTest
    {
        [TestMethod]
        public void ValueTest1()
        {
            Option<string> target = "str".ToOption();
            Assert.IsInstanceOfType(target, typeof(Option<string>));
        }
        [TestMethod]
        public void CoalescenseTest1()
        {
            Option<string> target = "str".ToOption().ToOption();
            Assert.AreEqual(target, "str".ToOption());
            Assert.AreEqual("str".ToOption(), target);
        }
        [TestMethod]
        public void CoalescenseTest()
        {
            Option<string> target = "str".ToOption();
            var iterations = new Random().Next(0, 100);
            for (var i = 0; i < iterations; ++i)
            {
                target = target.ToOption();
            }
            Assert.AreEqual(target, "str".ToOption());
            Assert.AreEqual("str".ToOption(), target);
        }
        [TestMethod]
        public void OptionFromNullTest1()
        {
            Option<object> target = FromNullFactory<object>();
            Assert.IsTrue(target.IsNone);
            Assert.IsFalse(target.IsSome);
        }

        [TestMethod]
        [ExpectedInvalidOperationException]
        public void OptionFromNullTest2()
        {
            Option<object> target = FromNullFactory<object>();
            object value = target.Value;// Must fail
        }
        [TestMethod]
        public void OptionFromValueTest1()
        {
            const string source = "str";
            Option<string> target = source.ToOption();
            string value = target.Value;
            Assert.AreEqual(value, source);
        }
        [TestMethod]
        public void OptionFromValueTest2()
        {
            const string source = "str";
            Option<string> target = source.ToOption();
            Assert.IsTrue(target.IsSome);
        }
        [TestMethod]
        public void OptionFromValueSelectTest1()
        {
            const string source = "str";
            Option<string> target = source.ToOption();
            Option<string> projected = target.Select(x => x.ToUpper());
            Assert.IsTrue(projected.IsSome);
            string value = projected.Value;
            Assert.AreEqual(value, source.ToUpper());
        }
        [TestMethod]
        public void OptionFromValueSelectTest2()
        {
            const string source = "str";
            Option<string> target = source.ToOption();
            Option<string> projected = from v in target
                                       select v.ToUpper();
            Assert.IsTrue(projected.IsSome);
            string value = projected.Value;
            Assert.AreEqual(value, source.ToUpper());
        }
        [TestMethod]
        public void OptionFromNullTestWhereTest1()
        {
            // Should always return None, never applying the predicate.
            Option<object> filtered = OptionFromNullWhereTestHelper<object>(x => true);
            Assert.IsFalse(filtered.IsSome);
        }

        [TestMethod]
        public void OptionFromNullTestWhereTest2()
        {
            Option<object> filtered = OptionFromNullWhereTestHelper<object>(x => false);
            Assert.IsFalse(filtered.IsSome);
            Assert.IsTrue(filtered.IsNone);
        }

        [TestMethod]
        [ExpectedInvalidOperationException]
        public void OptionFromNullTestWhereTest3()
        {
            Option<object> filtered = OptionFromNullWhereTestHelper<object>(x => true);
            object value = filtered.Value; // Must fail
        }

        [TestMethod]
        public void OptionFromNullSelectTest1()
        {
            Option<object> target = FromNullFactory<object>();
            Option<int> projected = target.Select(x => 1);
            Assert.IsFalse(projected.IsSome);
        }

        [TestMethod]
        [ExpectedInvalidOperationException]
        public void OptionFromNullSelectTest2()
        {
            Option<object> target = FromNullFactory<object>();
            Option<int> projected = target.Select(x => 1);
            int result = projected.Value; // Must fail
        }

        [TestMethod]
        public void OptionFromNullSelectTest3()
        {
            Option<object> target = FromNullFactory<object>();
            Option<int> result = from value in target
                                 select 1;
            Assert.IsFalse(result.IsSome);
        }

        [TestMethod]
        [ExpectedInvalidOperationException]
        public void OptionFromNullSelectTest4()
        {
            Option<object> target = FromNullFactory<object>();
            Option<int> projected = from value in target
                                    select 1;
            int result = projected.Value;// Must fail
        }

        [TestMethod]
        public void OptionFromNullSelectManyTest1()
        {
            Option<int> projected = OptionFromNullSelectManyTestHelper<object, int>(x => 1.ToOption());
            Assert.IsFalse(projected.IsSome);
        }

        [TestMethod]
        public void OptionFromNullSelectManyTest2()
        {
            Option<object> target = FromNullFactory<object>();
            Option<int> projected = from value in target
                                    from x in target
                                    select 1;
            Assert.IsFalse(projected.IsSome);
        }
        [TestMethod]
        [ExpectedInvalidOperationException]
        public void OptionFromNullSelectManyTest3()
        {
            Option<object> target = FromNullFactory<object>();
            Option<int> projected = from value in target
                                    from x in target
                                    select 1;
            int result = projected.Value; // must fail
        }
        [TestMethod]
        [ExpectedInvalidOperationException]
        public void OptionFromNullSelectManyTest4()
        {
            Option<int> projected = OptionFromNullSelectManyTestHelper<object, int>(x => 1.ToOption());
            int result = projected.Value;// must fail
        }

        [TestMethod]
        public void OptionCreateTest1()
        {
            object value = null;
            Option<object> target = Option.Create(value);
            Assert.IsFalse(target.IsSome);
            Assert.IsTrue(target.IsNone);
        }
        [TestMethod]
        [ExpectedInvalidOperationException]
        public void OptionCreateTest2()
        {
            int? value = null;
            Option<int?> target = Option.Create(value);
            object wrapped = target.Value; // must fail.
        }
        [TestMethod]
        [ExpectedInvalidOperationException()]
        public void OptionCreateTest3()
        {
            object value = null;
            Option<object> target = Option.Create(value);
            object wrapped = target.Value; // must fail.
        }

        [TestMethod]
        public void EqualityTest1()
        {
            var value = new object();
            var left = Option.Create(value);
            var right = Option.Create(value);

            Assert.IsTrue(left == right);
            Assert.IsFalse(left != right);
            Assert.IsTrue(left.Equals(right));

            Assert.IsTrue(right == left);
            Assert.IsFalse(right != left);
        }
        [TestMethod]
        public void IEquatableOfOptionEqualityTest1()
        {
            var value = new object();
            var left = Option.Create(value);
            var right = Option.Create(value);
            Assert.IsTrue(right.Equals(left));
            Assert.IsTrue(((object)right).Equals(left));
            Assert.IsTrue(left.Equals(right));
            Assert.IsTrue(((object)left).Equals(right));

        }
        [TestMethod]
        public void IEquatableOfOptionEqualityTest2()
        {
            var value = new object();
            var target = Option.Create(value);
            Assert.IsTrue(target.Equals(value));
            Assert.IsTrue(target.Equals(value));
            Assert.IsTrue(target.Equals(target.Value));
            Assert.IsTrue(((object)target).Equals(target.Value));
        }
        [TestMethod]
        public void EqualityTest2()
        {
            var leftValue = new object();
            var left = Option.Create(leftValue);
            var rightValue = new object();
            var right = Option.Create(rightValue);

            Assert.IsFalse(left == right);
            Assert.IsTrue(left != right);
            Assert.IsFalse(left.Equals(right));

            Assert.IsFalse(right == left);
            Assert.IsTrue(right != left);
            Assert.IsFalse(right.Equals(left));
        }
        [TestMethod]
        public void EqualityTest3()
        {
            var value = new object();
            var target = Option.Create(value);

            Assert.IsTrue(target == value);
            Assert.IsFalse(target != value);

            Assert.IsTrue(value == target);
            Assert.IsFalse(value != target);
        }
        [TestMethod]
        public void EqualityTest4()
        {
            var value = new object();
            var target = Option.Create(value);

            Assert.IsTrue(target == value);
            Assert.IsFalse(target != value);

            Assert.IsTrue(value == target);
            Assert.IsFalse(value != target);
        }
        [TestMethod]
        public void EqualityTest5()
        {
            var value = new object();
            var left = Option.Create(Option.Create(value));
            var right = Option.Create(value);

            Assert.IsTrue(left == right);
            Assert.IsFalse(left != right);

            Assert.IsTrue(right == left);
            Assert.IsFalse(right != left);
        }
        [TestMethod]
        public void EqualityTest6()
        {
            var value = new object();
            var left = Option.Create(Option.Create(value));
            var right = Option.Create(Option.Create(value));

            Assert.IsTrue(left == right);
            Assert.IsFalse(left != right);

            Assert.IsTrue(right == left);
            Assert.IsFalse(right != left);
        }

        private Func<int> random = new Random().Next;
        [TestMethod]
        public void EqualityOfTypeWith_op_EqualsValueTest1()
        {
            var value = random();
            var target = Option.Create(value);
            Assert.IsTrue(target.Equals(value));
            Assert.IsTrue(target.Equals(value));
            Assert.IsTrue(target.Equals(target.Value));
            Assert.IsTrue(((object)target).Equals(target.Value));
        }
        [TestMethod]
        public void EqualityOfTypeWith_op_EqualsValueTest2()
        {
            var leftValue = random();
            var left = Option.Create(leftValue);
            var rightValue = leftValue + 1;
            var right = Option.Create(rightValue);

            Assert.IsFalse(left == right);
            Assert.IsTrue(left != right);
            Assert.IsFalse(left.Equals(right));

            Assert.IsFalse(right == left);
            Assert.IsTrue(right != left);
            Assert.IsFalse(right.Equals(left));
        }
        [TestMethod]
        public void EqualityOfTypeWith_op_EqualsValueTest3()
        {
            var value = random();
            var target = Option.Create(value);

            Assert.IsTrue(target == value);
            Assert.IsFalse(target != value);

            Assert.IsTrue(value == target);
            Assert.IsFalse(value != target);
        }
        [TestMethod]
        public void EqualityOfTypeWith_op_EqualsValueTest4()
        {
            var value = random();
            var target = Option.Create(value);

            Assert.IsTrue(target == value);
            Assert.IsFalse(target != value);

            Assert.IsTrue(value == target);
            Assert.IsFalse(value != target);
        }
        [TestMethod]
        public void EqualityOfTypeWith_op_EqualsValueTest5()
        {
            var value = random();
            var left = Option.Create(Option.Create(value));
            var right = Option.Create(value);

            Assert.IsTrue(left == right);
            Assert.IsFalse(left != right);

            Assert.IsTrue(right == left);
            Assert.IsFalse(right != left);
        }
        [TestMethod]
        public void EqualityOfTypeWith_op_EqualsValueTest6()
        {
            var value = random();
            var left = Option.Create(Option.Create(value));
            var right = Option.Create(Option.Create(value));

            Assert.IsTrue(left == right);
            Assert.IsFalse(left != right);

            Assert.IsTrue(right == left);
            Assert.IsFalse(right != left);
        }
        [TestMethod]
        public void IsNoneTest1()
        {
            var target = Option.Create((object)null);
            Assert.IsTrue(target.IsNone);
            Assert.IsFalse(target.IsSome);
        }
        [TestMethod]
        public void IsNoneTest2()
        {
            var target = Option.Create((int?)null);
            Assert.IsTrue(target.IsNone);
            Assert.IsFalse(target.IsSome);
        }
        [TestMethod]
        public void IsSomeTest3()
        {
            var target = Option.Create((int?)random());
            Assert.IsFalse(target.IsNone);
            Assert.IsTrue(target.IsSome);
        }
        public void IsSomeTest4()
        {
            var target = Option.Create(random());
            Assert.IsFalse(target.IsNone);
            Assert.IsTrue(target.IsSome);
        }
        [TestMethod]
        public void IsSomeTest5()
        {
            var target = Option.Create(new object());
            Assert.IsFalse(target.IsNone);
            Assert.IsTrue(target.IsSome);
        }
        private static Option<T> OptionFromNullWhereTestHelper<T>(Func<T, bool> predicate) where T : class
        {
            Option<T> target = FromNullFactory<T>();
            Option<T> filtered = target.Where(predicate);
            return filtered;
        }

        private static Option<TResult> OptionFromNullSelectManyTestHelper<T, TResult>(Func<T, Option<TResult>> selector) where T : class
        {
            Option<T> target = FromNullFactory<T>();
            Option<TResult> projected = target.SelectMany(selector);
            return projected;
        }

        static Option<T> FromNullFactory<T>() where T : class
        {
            T target = null;
            return target.ToOption();
        }
    }
}