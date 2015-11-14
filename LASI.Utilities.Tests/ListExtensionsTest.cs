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
            List<int> target = List(0, 1, 2, 3);
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
            IList<string> actual = from x in target select x.ToString();
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void WhereTest1()
        {
            IList<int> target = List(0, 1, 2, 3);
            Func<int, bool> predicate = x => x % 2 == 0;
            IList<int> expected = target.AsEnumerable().Where(predicate).ToList();
            IList<int> actual = target.Where(predicate);
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void WhereTest2()
        {
            IList<int> target = List(0, 1, 2, 3);
            IList<int> expected = (from x in target.AsEnumerable() where x % 2 == 0 select x).ToList();
            IList<int> actual = from x in target where x % 2 == 0 select x;
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void SelectManyTest1()
        {
            List<IEnumerable<int>> target = List(Enumerable.Repeat(Enumerable.Range(0, 10), 10).ToArray());
            IList<int> expected = target.AsEnumerable().SelectMany(xs => xs).ToList();
            IList<int> actual = target.SelectMany(xs => xs);
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void SelectManyTest2()
        {
            List<IEnumerable<int>> target = List(Enumerable.Repeat(Enumerable.Range(0, 10), 10).ToArray());
            IList<int> expected = (from xs in target.AsEnumerable()
                                   from x in xs
                                   select x).ToList();
            IList<int> actual = from xs in target
                                from x in xs
                                select x;
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void SelectManyTest3()
        {
            List<IEnumerable<int>> target = List(Enumerable.Repeat(Enumerable.Range(0, 10), 10).ToArray());
            IList<int> expected = target.AsEnumerable().Where(xs => xs.Any(x => x % 4 == 0)).SelectMany(xs => xs).ToList();
            IList<int> actual = target.Where(xs => xs.Any(x => x % 4 == 0)).SelectMany(xs => xs);
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void SelectManyTest4()
        {
            List<IEnumerable<int>> target = List(Enumerable.Repeat(Enumerable.Range(0, 10), 10).ToArray());
            List<int> expected = (from xs in target.AsEnumerable()
                                  where xs.Any(x => x % 4 == 0)
                                  from x in xs
                                  select x).ToList();
            IList<int> actual = from xs in target
                                where xs.Any(x => x % 4 == 0)
                                from x in xs
                                select x;
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void SkipTest1()
        {
            List<int> target = Range(0, 10);
            List<int> expected = target.AsEnumerable().Skip(1).ToList();
            List<int> actual = target.Skip(1);
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void SkipTest2()
        {
            List<int> target = Range(0, 10);
            List<int> expected = Range(0, 10);
            List<int> actual = target.Skip(0);
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void SkipTest3()
        {
            List<int> target = Range(0, 10);
            List<int> expected = Range(0, 10);
            List<int> actual = target.Skip(-1);
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void SkipTest4()
        {
            List<int> target = Range(0, 10);
            List<int> expected = Range(0, 10);
            List<int> actual = target.Skip(-1);
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void SkipTest5()
        {
            List<int> target = Range(0, 10);
            List<int> expected = Range(0, 10);
            List<int> actual = target.Skip(-140);
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void SkipTest6()
        {
            List<int> target = Range(0, 10);
            List<int> expected = Range(0, 0);
            List<int> actual = target.Skip(10);
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void SkipTest7()
        {
            List<int> target = Range(0, 10);
            List<int> expected = Range(0, 0);
            List<int> actual = target.Skip(11);
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void SkipTest8()
        {
            List<int> target = Range(0, 10);
            List<int> expected = Range(0, 0);
            List<int> actual = target.Skip(110);
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void TakeTest1()
        {
            List<int> target = Range(0, 10);
            List<int> expected = Range(0, 5);
            List<int> actual = target.Take(5);
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void TakeTest2()
        {
            List<int> target = Range(0, 10);
            List<int> expected = Range(0, 10);
            List<int> actual = target.Take(10);
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void TakeTest3()
        {
            List<int> target = Range(0, 10);
            List<int> expected = Range(0, 10);
            List<int> actual = target.Take(11);
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void TakeTest4()
        {
            List<int> target = Range(0, 10);
            List<int> expected = Range(0, 10);
            List<int> actual = target.Take(101);
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void TakeTest5()
        {
            List<int> target = Range(0, 10);
            List<int> expected = Range(0, 0);
            List<int> actual = target.Take(0);
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void TakeTest6()
        {
            List<int> target = Range(0, 10);
            List<int> expected = Range(0, 0);
            List<int> actual = target.Take(-1);
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void TakeTest7()
        {
            List<int> target = Range(0, 10);
            List<int> expected = Range(0, 0);
            List<int> actual = target.Take(-101);
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void TakeTest8()
        {
            List<int> target = Range(0, 10);
            List<int> expected = Range(0, 10);
            List<int> actual = target.Take(10);
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void SkipTakeTest1()
        {
            List<int> target = Range(0, 10);
            List<int> expected = Range(2, 5);
            List<int> actual = target.Skip(2).Take(5);
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void SkipTakeTest2()
        {
            List<int> target = Range(0, 10);
            List<int> expected = Range(0, 10);
            List<int> actual = target.Skip(0).Take(10);
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void TakeSkipTest1()
        {
            List<int> target = Range(0, 10);
            List<int> expected = Range(4, 1);
            List<int> actual = target.Take(5).Skip(4);
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void TakeSkipTest2()
        {
            List<int> target = Range(0, 10);
            List<int> expected = Range(0, 10);
            List<int> actual = target.Take(10).Skip(0);
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void SkipWhileTest1()
        {
            List<int> target = Range(0, 10);
            Func<int, bool> predicate = x => x < 5;
            List<int> expected = target.SkipWhile(predicate);
            List<int> actual = target.AsEnumerable().SkipWhile(predicate).ToList();
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void SkipWhileTest2()
        {
            List<int> target = Range(5, 10);
            Func<int, bool> predicate = x => x < 5;
            List<int> expected = target.SkipWhile(predicate);
            List<int> actual = target.AsEnumerable().SkipWhile(predicate).ToList();
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void SkipWhileTest3()
        {
            List<int> target = Range(5, 0);
            Func<int, bool> predicate = x => x < 5;
            List<int> expected = target.SkipWhile(predicate);
            List<int> actual = target.AsEnumerable().SkipWhile(predicate).ToList();
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void SkipWhileTest4()
        {
            List<int> target = Range(5, 2);
            Func<int, bool> predicate = x => x < 5;
            List<int> expected = target.SkipWhile(predicate);
            List<int> actual = target.AsEnumerable().SkipWhile(predicate).ToList();
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void TakeWhileTest1()
        {
            List<int> target = Range(0, 10);
            Func<int, bool> predicate = x => x < 5;
            List<int> expected = target.TakeWhile(predicate);
            List<int> actual = target.AsEnumerable().TakeWhile(predicate).ToList();
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void TakeWhileTest2()
        {
            List<int> target = Range(5, 10);
            Func<int, bool> predicate = x => x < 5;
            List<int> expected = target.TakeWhile(predicate);
            List<int> actual = target.AsEnumerable().TakeWhile(predicate).ToList();
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void TakeWhileTest3()
        {
            List<int> target = Range(5, 0);
            Func<int, bool> predicate = x => x < 5;
            List<int> expected = target.TakeWhile(predicate);
            List<int> actual = target.AsEnumerable().TakeWhile(predicate).ToList();
            Check.That(actual).ContainsExactly(expected);
        }
        [Fact]
        public void TakeWhileTest4()
        {
            List<int> target = Range(5, 2);
            Func<int, bool> predicate = x => x < 5;
            List<int> expected = target.TakeWhile(predicate);
            List<int> actual = target.AsEnumerable().TakeWhile(predicate).ToList();
            Check.That(actual).ContainsExactly(expected);
        }

        private static List<T> List<T>(params T[] values) => values.ToList();
        private static List<int> Range(int start, int count) => Enumerable.Range(start, count).ToList();
    }
}
