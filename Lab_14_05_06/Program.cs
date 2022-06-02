using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Lab_13_15_05;

namespace Lab_14_05_06
{
    public static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        /// <summary>
        /// Выборка данных по условию.
        /// </summary>
        /// <param name="table">Хеш-таблица.</param>
        /// <param name="predicate">Условие.</param>
        /// <typeparam name="TKey">Тип ключа.</typeparam>
        /// <typeparam name="TValue">Тип значения.</typeparam>
        /// <returns>Отфильтрованная таблица.</returns>
        public static MyNewHashTable<TKey, TValue> Filter<TKey, TValue>(this MyNewHashTable<TKey, TValue> table,
                                                                       Predicate<KeyValuePair<TKey, TValue>> predicate)
        {
            var newTable = new MyNewHashTable<TKey, TValue>();
            foreach (KeyValuePair<TKey, TValue> item in table)
            {
                if (predicate(item))
                {
                    newTable.Add(item.Key, item.Value);
                }
            }

            return newTable;
        }

        /// <summary>
        /// Среднее арифметическое последовательности, преобразованной функцией к числу типа int.
        /// </summary>
        /// <param name="table">Хеш-таблица.</param>
        /// <param name="func">Функция.</param>
        /// <typeparam name="TKey">Тип ключа.</typeparam>
        /// <typeparam name="TValue">Тип значения.</typeparam>
        /// <returns>Среднее арифметическое.</returns>
        public static double Average<TKey, TValue>(this MyNewHashTable<TKey, TValue> table,
                                                   Func<KeyValuePair<TKey, TValue>, int> func)
        {
            return table.Select(func).Average();
        }

        /// <summary>
        /// Сортировка последовательности по убыванию с заданной функцией сравнения.
        /// </summary>
        /// <param name="table">Хеш-таблица.</param>
        /// <param name="comparison">Функция сравнения.</param>
        /// <typeparam name="TKey">Тип ключа.</typeparam>
        /// <typeparam name="TValue">Тип значения.</typeparam>
        /// <returns>Отсортированная таблица.</returns>
        public static List<KeyValuePair<TKey, TValue>> SortDescending<TKey, TValue>(
            this MyNewHashTable<TKey, TValue> table,
            Comparison<KeyValuePair<TKey, TValue>> comparison)
        {
            List<KeyValuePair<TKey, TValue>> newTable = table.ToList();
            newTable.Sort(comparison);
            newTable.Reverse();

            return newTable;
        }
    }
}
