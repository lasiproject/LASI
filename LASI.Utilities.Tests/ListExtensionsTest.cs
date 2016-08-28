using System;
using System.Collections.Generic;
using LASI.Utilities.Specialized.Enhanced.IList.Linq;
using System.Linq;
namespace LASI.Utilities.Tests
{
    using Fact = Xunit.FactAttribute;
    using Shared.Test.Assertions;
    using NFluent;
    public class ListExtensionsTest
    {
        [Fact]
        public void SelectTest1()
        {
            var target = List(0, 1, 2, 3);
            Func<int, string> projection = x => x.ToString();
            IList<string> expected = target.AsEnumerable().Select(projection).ToList();
            IList<string> actual = target.Select(projection);
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void SelectTest2()
        {
            IList<int> target = List(0, 1, 2, 3);
            IList<string> expected = (from x in target.AsEnumerable() select x.ToString()).ToList();
            var actual = from x in target select x.ToString();
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void WhereTest1()
        {
            IList<int> target = List(0, 1, 2, 3);
            Func<int, bool> predicate = x => x % 2 == 0;
            IList<int> expected = target.AsEnumerable().Where(predicate).ToList();
            var actual = target.Where(predicate);
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void WhereTest2()
        {
            IList<int> target = List(0, 1, 2, 3);
            IList<int> expected = (from x in target.AsEnumerable() where x % 2 == 0 select x).ToList();
            var actual = from x in target where x % 2 == 0 select x;
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void SelectManyTest1()
        {
            var target = List(Enumerable.Repeat(Enumerable.Range(0, 10), 10).ToArray());
            IList<int> expected = target.AsEnumerable().SelectMany(xs => xs).ToList();
            var actual = target.SelectMany(xs => xs);
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void SelectManyTest2()
        {
            var target = List(Enumerable.Repeat(Enumerable.Range(0, 10), 10).ToArray());
            IList<int> expected = (from xs in target.AsEnumerable()
                                   from x in xs
                                   select x).ToList();
            var actual = from xs in target
                                from x in xs
                                select x;
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void SelectManyTest3()
        {
            var target = List(Enumerable.Repeat(Enumerable.Range(0, 10), 10).ToArray());
            IList<int> expected = target.AsEnumerable().Where(xs => xs.Any(x => x % 4 == 0)).SelectMany(xs => xs).ToList();
            var actual = target.Where(xs => xs.Any(x => x % 4 == 0)).SelectMany(xs => xs);
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void SelectManyTest4()
        {
            var target = List(Enumerable.Repeat(Enumerable.Range(0, 10), 10).ToArray());
            var expected = (from xs in target.AsEnumerable()
                                  where xs.Any(x => x % 4 == 0)
                                  from x in xs
                                  select x).ToList();
            var actual = from xs in target
                                where xs.Any(x => x % 4 == 0)
                                from x in xs
                                select x;
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void SkipTest1()
        {
            var target = Range(0, 10);
            var expected = target.AsEnumerable().Skip(1).ToList();
            var actual = target.Skip(1);
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void SkipTest2()
        {
            var target = Range(0, 10);
            var expected = Range(0, 10);
            var actual = target.Skip(0);
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void SkipTest3()
        {
            var target = Range(0, 10);
            var expected = Range(0, 10);
            var actual = target.Skip(-1);
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void SkipTest4()
        {
            var target = Range(0, 10);
            var expected = Range(0, 10);
            var actual = target.Skip(-1);
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void SkipTest5()
        {
            var target = Range(0, 10);
            var expected = Range(0, 10);
            var actual = target.Skip(-140);
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void SkipTest6()
        {
            var target = Range(0, 10);
            var expected = Range(0, 0);
            var actual = target.Skip(10);
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void SkipTest7()
        {
            var target = Range(0, 10);
            var expected = Range(0, 0);
            var actual = target.Skip(11);
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void SkipTest8()
        {
            var target = Range(0, 10);
            var expected = Range(0, 0);
            var actual = target.Skip(110);
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void TakeTest1()
        {
            var target = Range(0, 10);
            var expected = Range(0, 5);
            var actual = target.Take(5);
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void TakeTest2()
        {
            var target = Range(0, 10);
            var expected = Range(0, 10);
            var actual = target.Take(10);
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void TakeTest3()
        {
            var target = Range(0, 10);
            var expected = Range(0, 10);
            var actual = target.Take(11);
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void TakeTest4()
        {
            var target = Range(0, 10);
            var expected = Range(0, 10);
            var actual = target.Take(101);
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void TakeTest5()
        {
            var target = Range(0, 10);
            var expected = Range(0, 0);
            var actual = target.Take(0);
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void TakeTest6()
        {
            var target = Range(0, 10);
            var expected = Range(0, 0);
            var actual = target.Take(-1);
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void TakeTest7()
        {
            var target = Range(0, 10);
            var expected = Range(0, 0);
            var actual = target.Take(-101);
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void TakeTest8()
        {
            var target = Range(0, 10);
            var expected = Range(0, 10);
            var actual = target.Take(10);
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void SkipTakeTest1()
        {
            var target = Range(0, 10);
            var expected = Range(2, 5);
            var actual = target.Skip(2).Take(5);
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void SkipTakeTest2()
        {
            var target = Range(0, 10);
            var expected = Range(0, 10);
            var actual = target.Skip(0).Take(10);
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void TakeSkipTest1()
        {
            var target = Range(0, 10);
            var expected = Range(4, 1);
            var actual = target.Take(5).Skip(4);
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void TakeSkipTest2()
        {
            var target = Range(0, 10);
            var expected = Range(0, 10);
            var actual = target.Take(10).Skip(0);
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void SkipWhileTest1()
        {
            var target = Range(0, 10);
            Func<int, bool> predicate = x => x < 5;
            var expected = target.SkipWhile(predicate);
            var actual = target.AsEnumerable().SkipWhile(predicate).ToList();
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void SkipWhileTest2()
        {
            var target = Range(5, 10);
            Func<int, bool> predicate = x => x < 5;
            var expected = target.SkipWhile(predicate);
            var actual = target.AsEnumerable().SkipWhile(predicate).ToList();
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void SkipWhileTest3()
        {
            var target = Range(5, 0);
            Func<int, bool> predicate = x => x < 5;
            var expected = target.SkipWhile(predicate);
            var actual = target.AsEnumerable().SkipWhile(predicate).ToList();
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void SkipWhileTest4()
        {
            var target = Range(5, 2);
            Func<int, bool> predicate = x => x < 5;
            var expected = target.SkipWhile(predicate);
            var actual = target.AsEnumerable().SkipWhile(predicate).ToList();
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void TakeWhileTest1()
        {
            var target = Range(0, 10);
            Func<int, bool> predicate = x => x < 5;
            var expected = target.TakeWhile(predicate);
            var actual = target.AsEnumerable().TakeWhile(predicate).ToList();
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void TakeWhileTest2()
        {
            var target = Range(5, 10);
            Func<int, bool> predicate = x => x < 5;
            var expected = target.TakeWhile(predicate);
            var actual = target.AsEnumerable().TakeWhile(predicate).ToList();
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void TakeWhileTest3()
        {
            var target = Range(5, 0);
            Func<int, bool> predicate = x => x < 5;
            var expected = target.TakeWhile(predicate);
            var actual = target.AsEnumerable().TakeWhile(predicate).ToList();
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void TakeWhileTest4()
        {
            var target = Range(5, 2);
            Func<int, bool> predicate = x => x < 5;
            var expected = target.TakeWhile(predicate);
            var actual = target.AsEnumerable().TakeWhile(predicate).ToList();
            Check.That(actual).ContainsExactly(expected);
        }

        private static List<T> List<T>(params T[] values) => values.ToList();
        private static List<int> Range(int start, int count) => Enumerable.Range(start, count).ToList();
    }
}
