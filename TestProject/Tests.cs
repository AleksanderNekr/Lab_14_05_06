using System;
using System.Collections.Generic;
using System.Linq;
using Lab_13_15_05;
using NUnit.Framework;
using static Lab_14_05_06.Program;

namespace TestProject
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void TestWhere()
        {
            var table = new MyNewHashTable<int, int>
                        {
                            new KeyValuePair<int, int>(1, 1),
                            new KeyValuePair<int, int>(2, 2),
                            new KeyValuePair<int, int>(3, 3),
                            new KeyValuePair<int, int>(4, 4),
                            new KeyValuePair<int, int>(5, 5)
                        };

            IEnumerable<KeyValuePair<int, int>> result1 = table.Where(x => (x.Value % 2) == 0)
                                                               .ToList();
            IEnumerable<KeyValuePair<int, int>> result2 = table.Filter(x => (x.Value % 2) == 0)
                                                               .ToList();

            for (var i = 0; i < result1.Count(); i++)
            {
                Assert.AreEqual(result1.ElementAt(i).Value, result2.ElementAt(i).Value);
            }
        }

        [Test]
        public void TestAverage()
        {
            var table = new MyNewHashTable<int, int>
                        {
                            new KeyValuePair<int, int>(1, 1),
                            new KeyValuePair<int, int>(2, 2),
                            new KeyValuePair<int, int>(3, 3),
                            new KeyValuePair<int, int>(4, 4),
                            new KeyValuePair<int, int>(5, 5)
                        };

            Assert.AreEqual(table.Average(x => x.Value), 3);
        }

        [Test]
        public void TestSortDescending()
        {
            var table = new MyNewHashTable<int, int>
                        {
                            new KeyValuePair<int, int>(1, 1),
                            new KeyValuePair<int, int>(2, 2),
                            new KeyValuePair<int, int>(3, 3),
                            new KeyValuePair<int, int>(4, 4),
                            new KeyValuePair<int, int>(5, 5)
                        };

            IEnumerable<KeyValuePair<int, int>> result1 = table.OrderByDescending(x => x.Value)
                                                               .ToList();

            int Comparison(KeyValuePair<int, int> pair1, KeyValuePair<int, int> pair2)
            {
                return pair1.Value.CompareTo(pair2.Value);
            }

            IEnumerable<KeyValuePair<int, int>> result2 = table.SortDescending(Comparison)
                                                               .ToList();

            for (var i = 0; i < result1.Count(); i++)
            {
                Assert.AreEqual(result1.ElementAt(i).Value, result2.ElementAt(i).Value);
            }
        }
    }
}
