using System;

namespace LASI.Utilities.Tests
{
    using NFluent;
    using Xunit;
    public class OptionTest
    {
        [Fact]
        public void ValueTest1()
        {
            Option<string> target = "str".ToOption();
            Check.That(target).InheritsFrom<Option<string>>();
        }
        [Fact]
        public void CoalescenseTest1()
        {
            Option<string> target = "str".ToOption().ToOption();
            Assert.Equal(target, "str".ToOption());
            Assert.Equal("str".ToOption(), target);
        }
        [Fact]
        public void CoalescenseTest()
        {
            Option<string> target = "str".ToOption();
            var iterations = new Random().Next(0, 100);
            for (var i = 0; i < iterations; ++i)
            {
                target = target.ToOption();
            }
            Assert.Equal(target, "str".ToOption());
            Assert.Equal("str".ToOption(), target);
        }
        [Fact]
        public void OptionFromNullTest1()
        {
            Option<object> target = FromNullFactory<object>();
            Assert.True(target.IsNone);
            Assert.False(target.IsSome);
        }

        [Fact]
        public void OptionFromNullTest2()
        {
            Option<object> target = FromNullFactory<object>();
            Check.ThatCode(() => target.Value).Throws<InvalidOperationException>();
        }
        [Fact]
        public void OptionFromValueTest1()
        {
            const string source = "str";
            Option<string> target = source.ToOption();
            string value = target.Value;
            Assert.Equal(value, source);
        }
        [Fact]
        public void OptionFromValueTest2()
        {
            const string source = "str";
            Option<string> target = source.ToOption();
            Assert.True(target.IsSome);
        }
        [Fact]
        public void OptionFromValueSelectTest1()
        {
            const string source = "str";
            Option<string> target = source.ToOption();
            Option<string> projected = target.Select(x => x.ToUpper());
            Assert.True(projected.IsSome);
            string value = projected.Value;
            Assert.Equal(value, source.ToUpper());
        }
        [Fact]
        public void OptionFromValueSelectTest2()
        {
            const string source = "str";
            Option<string> target = source.ToOption();
            Option<string> projected = from v in target
                                       select v.ToUpper();
            Assert.True(projected.IsSome);
            string value = projected.Value;
            Assert.Equal(value, source.ToUpper());
        }
        [Fact]
        public void OptionFromNullTestWhereTest1()
        {
            // Should always return None, never applying the predicate.
            Option<object> filtered = OptionFromNullWhereTestHelper<object>(x => true);
            Assert.False(filtered.IsSome);
        }

        [Fact]
        public void OptionFromNullTestWhereTest2()
        {
            Option<object> filtered = OptionFromNullWhereTestHelper<object>(x => false);
            Assert.False(filtered.IsSome);
            Assert.True(filtered.IsNone);
        }

        [Fact]
        public void OptionFromNullTestWhereTest3()
        {
            Option<object> filtered = OptionFromNullWhereTestHelper<object>(x => true);
            Check.ThatCode(() => filtered.Value).Throws<InvalidOperationException>();
        }

        [Fact]
        public void OptionFromNullSelectTest1()
        {
            Option<object> target = FromNullFactory<object>();
            Option<int> projected = target.Select(x => 1);
            Assert.False(projected.IsSome);
        }

        [Fact]
        public void OptionFromNullSelectTest2()
        {
            Option<object> target = FromNullFactory<object>();
            Option<int> projected = target.Select(x => 1);
            Check.ThatCode(() => projected.Value).Throws<InvalidOperationException>();
        }

        [Fact]
        public void OptionFromNullSelectTest3()
        {
            Option<object> target = FromNullFactory<object>();
            Option<int> result = from value in target
                                 select 1;
            Assert.False(result.IsSome);
        }

        [Fact]
        public void OptionFromNullSelectTest4()
        {
            Option<object> target = FromNullFactory<object>();
            Option<int> projected = from value in target
                                    select 1;
            Check.ThatCode(() => projected.Value).Throws<InvalidOperationException>(); // Must fail
        }

        [Fact]
        public void OptionFromNullSelectManyTest1()
        {
            Option<int> projected = OptionFromNullSelectManyTestHelper<object, int>(x => 1.ToOption());
            Assert.False(projected.IsSome);
        }

        [Fact]
        public void OptionFromNullSelectManyTest2()
        {
            Option<object> target = FromNullFactory<object>();
            Option<int> projected = from value in target
                                    from x in target
                                    select 1;
            Assert.False(projected.IsSome);
        }
        [Fact]
        public void OptionFromNullSelectManyTest3()
        {
            Option<object> target = FromNullFactory<object>();
            Option<int> projected = from value in target
                                    from x in target
                                    select 1;
            Check.ThatCode(() => projected.Value).Throws<InvalidOperationException>();
        }
        [Fact]
        public void OptionFromNullSelectManyTest4()
        {
            Option<int> projected = OptionFromNullSelectManyTestHelper<object, int>(x => 1.ToOption());
            Check.ThatCode(() => projected.Value).Throws<InvalidOperationException>();
        }

        [Fact]
        public void OptionCreateTest1()
        {
            object value = null;
            Option<object> target = Option.Create(value);
            Assert.False(target.IsSome);
            Assert.True(target.IsNone);
        }
        [Fact]
        public void OptionCreateTest2()
        {
            int? value = null;
            Option<int?> target = Option.Create(value);
            Check.ThatCode(() => target.Value).Throws<InvalidOperationException>();
        }
        [Fact]
        public void OptionCreateTest3()
        {
            object value = null;
            Option<object> target = Option.Create(value);
            Check.ThatCode(() => target.Value).Throws<InvalidOperationException>();
        }

        [Fact]
        public void EqualityTest1()
        {
            var value = new object();
            var left = Option.Create(value);
            var right = Option.Create(value);

            Assert.True(left == right);
            Assert.False(left != right);
            Assert.True(left.Equals(right));

            Assert.True(right == left);
            Assert.False(right != left);
        }
        [Fact]
        public void IEquatableOfOptionEqualityTest1()
        {
            var value = new object();
            var left = Option.Create(value);
            var right = Option.Create(value);
            Assert.True(right.Equals(left));
            Assert.True(((object)right).Equals(left));
            Assert.True(left.Equals(right));
            Assert.True(((object)left).Equals(right));

        }
        [Fact]
        public void IEquatableOfOptionEqualityTest2()
        {
            var value = new object();
            var target = Option.Create(value);
            Assert.True(target.Equals(value));
            Assert.True(target.Equals(value));
            Assert.True(target.Equals(target.Value));
            Assert.True(((object)target).Equals(target.Value));
        }
        [Fact]
        public void EqualityTest2()
        {
            var leftValue = new object();
            var left = Option.Create(leftValue);
            var rightValue = new object();
            var right = Option.Create(rightValue);

            Assert.False(left == right);
            Assert.True(left != right);
            Assert.False(left.Equals(right));

            Assert.False(right == left);
            Assert.True(right != left);
            Assert.False(right.Equals(left));
        }
        [Fact]
        public void EqualityTest3()
        {
            var value = new object();
            var target = Option.Create(value);

            Assert.True(target == value);
            Assert.False(target != value);

            Assert.True(value == target);
            Assert.False(value != target);
        }
        [Fact]
        public void EqualityTest4()
        {
            var value = new object();
            var target = Option.Create(value);

            Assert.True(target == value);
            Assert.False(target != value);

            Assert.True(value == target);
            Assert.False(value != target);
        }
        [Fact]
        public void EqualityTest5()
        {
            var value = new object();
            var left = Option.Create(Option.Create(value));
            var right = Option.Create(value);

            Assert.True(left == right);
            Assert.False(left != right);

            Assert.True(right == left);
            Assert.False(right != left);
        }
        [Fact]
        public void EqualityTest6()
        {
            var value = new object();
            var left = Option.Create(Option.Create(value));
            var right = Option.Create(Option.Create(value));

            Assert.True(left == right);
            Assert.False(left != right);

            Assert.True(right == left);
            Assert.False(right != left);
        }

        private Func<int> random = new Random().Next;
        [Fact]
        public void EqualityOfTypeWith_op_EqualsValueTest1()
        {
            var value = random();
            var target = Option.Create(value);
            Assert.True(target.Equals(value));
            Assert.True(target.Equals(value));
            Assert.True(target.Equals(target.Value));
            Assert.True(((object)target).Equals(target.Value));
        }
        [Fact]
        public void EqualityOfTypeWith_op_EqualsValueTest2()
        {
            var leftValue = random();
            var left = Option.Create(leftValue);
            var rightValue = leftValue + 1;
            var right = Option.Create(rightValue);

            Assert.False(left == right);
            Assert.True(left != right);
            Assert.False(left.Equals(right));

            Assert.False(right == left);
            Assert.True(right != left);
            Assert.False(right.Equals(left));
        }
        [Fact]
        public void EqualityOfTypeWith_op_EqualsValueTest3()
        {
            var value = random();
            var target = Option.Create(value);

            Assert.True(target == value);
            Assert.False(target != value);

            Assert.True(value == target);
            Assert.False(value != target);
        }
        [Fact]
        public void EqualityOfTypeWith_op_EqualsValueTest4()
        {
            var value = random();
            var target = Option.Create(value);

            Assert.True(target == value);
            Assert.False(target != value);

            Assert.True(value == target);
            Assert.False(value != target);
        }
        [Fact]
        public void EqualityOfTypeWith_op_EqualsValueTest5()
        {
            var value = random();
            var left = Option.Create(Option.Create(value));
            var right = Option.Create(value);

            Assert.True(left == right);
            Assert.False(left != right);

            Assert.True(right == left);
            Assert.False(right != left);
        }
        [Fact]
        public void EqualityOfTypeWith_op_EqualsValueTest6()
        {
            var value = random();
            var left = Option.Create(Option.Create(value));
            var right = Option.Create(Option.Create(value));

            Assert.True(left == right);
            Assert.False(left != right);

            Assert.True(right == left);
            Assert.False(right != left);
        }
        [Fact]
        public void IsNoneTest1()
        {
            var target = Option.Create((object)null);
            Assert.True(target.IsNone);
            Assert.False(target.IsSome);
        }
        [Fact]
        public void IsNoneTest2()
        {
            var target = Option.Create((int?)null);
            Assert.True(target.IsNone);
            Assert.False(target.IsSome);
        }
        [Fact]
        public void IsSomeTest3()
        {
            var target = Option.Create((int?)random());
            Assert.False(target.IsNone);
            Assert.True(target.IsSome);
        }
        public void IsSomeTest4()
        {
            var target = Option.Create(random());
            Assert.False(target.IsNone);
            Assert.True(target.IsSome);
        }
        [Fact]
        public void IsSomeTest5()
        {
            var target = Option.Create(new object());
            Assert.False(target.IsNone);
            Assert.True(target.IsSome);
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