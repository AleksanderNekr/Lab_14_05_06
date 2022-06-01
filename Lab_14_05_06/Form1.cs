using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using InputMethodsForm;
using Lab_13_15_05;
using WorkersLib;

namespace Lab_14_05_06
{
    internal sealed partial class Form1 : Form
    {
        private       Queue<List<Person>> _factory       = new Queue<List<Person>>();
        private const int                 WorkshopsCount = 10;
        private const int                 PeopleCount    = 5;

        internal Form1()
        {
            this.InitializeComponent();
            InputForm.GetMessage += this.OnInputFormOnGetMessage;
        }

        private void CreateMainCollectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this._factory.Clear();
            for (var workshopIndex = 0; workshopIndex < WorkshopsCount; workshopIndex++)
            {
                var persons = new List<Person>();
                for (var personIndex = 0; personIndex < PeopleCount; personIndex++)
                {
                    var person = Person.GetRandomPerson();
                    if (person is Worker worker)
                    {
                        worker.WorkshopNumber = workshopIndex + 1;
                        persons.Add(worker);
                        continue;
                    }

                    persons.Add(person);
                }

                this._factory.Enqueue(persons);
            }

            this.PrintFactoryToListBox();
            this.JournalListBox.Items.Add("Успешно создана главная коллекция");
            this.lINQQueriesToolStripMenuItem.Visible          = true;
            this.lINQExtensionMethodsToolStripMenuItem.Visible = true;
        }

        #region LINQ запросы

        private void WorkersNamesOfWorkshopQueryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Worker> workers = this.Get<Worker>();

            bool IsCorrectNumber(object input)
            {
                return workers.Any(worker => worker.WorkshopNumber == (int)input);
            }

            int workshopNumber = InputForm.ReadInt("номер цеха", IsCorrectNumber);
            if (this.GetLastMessage() == InputForm.CancelMessage)
            {
                return;
            }

            this.PrintToRequestListBox(GetWorkersNamesByNumberExt(workers, workshopNumber));
            this.JournalListBox.Items.Add("Успешно выведены имена рабочих в цехе №" + workshopNumber);
        }

        private void EngineersCountWithMinWorkExpQueryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool ExperienceRange(object input)
            {
                return ((int)input > 0) && ((int)input <= 40);
            }

            int                   experience = InputForm.ReadInt("стаж, отсчитываемый от 25 лет", ExperienceRange);
            IEnumerable<Engineer> engineers  = this.Get<Engineer>();
            int count = (from engineer in engineers
                         where (engineer.Age - 25) >= experience
                         select engineer).Count();

            this.PrintToRequestListBox(new List<string> { count + " инженеров в списке" });
            this.JournalListBox.Items
                .Add($"Успешно посчитано количество инженеров со стажем не менее {experience} лет!");
        }

        private void AverageWorkerSalaryQueryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Worker> workers = this.Get<Worker>();
            double averageSalary = (double)(from worker in workers
                                            select worker.Salary).Sum()
                                 / workers.Count;
            this.PrintToRequestListBox(new List<string>
                                       { Math.Round(averageSalary, 2) + " – средняя зарплата всех рабочих" });
            this.JournalListBox.Items.Add("Успешно посчитана средняя зарплата всех рабочих!");
        }

        private void GroupByProfessionQueryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.RequestListBox.Items.Clear();
            IEnumerable<IGrouping<Type, Person>> personsGroup = from person in this.Get<Person>()
                                                                group person by person.GetType();
            foreach (IGrouping<Type, Person> pair in personsGroup)
            {
                IEnumerable<string> personsInGroup = from person in pair select person.ToString();
                this.RequestListBox.Items.Add(pair.Key.Name);
                foreach (string personInfo in personsInGroup)
                {
                    this.RequestListBox.Items.Add("  " + personInfo);
                }
            }

            this.JournalListBox.Items.Add("Успешно выведены группы по типу людей");
        }

        #endregion LINQ запросы

        #region Методы расширения LINQ

        private void WorkersNamesOfWorkshopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Worker> workers = this.GetExtension<Worker>();

            bool IsCorrectNumber(object input)
            {
                return workers.Any(worker => worker.WorkshopNumber == (int)input);
            }

            int workshopNumber = InputForm.ReadInt("номер цеха", IsCorrectNumber);
            if (this.GetLastMessage() == InputForm.CancelMessage)
            {
                return;
            }

            this.PrintToRequestListBox(GetWorkersNamesByNumberExt(workers, workshopNumber));
            this.JournalListBox.Items.Add("Успешно выведены имена рабочих в цехе №" + workshopNumber);
        }

        private void EngineersCountWithMinWorkExpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool ExperienceRange(object input)
            {
                return ((int)input > 0) && ((int)input <= 40);
            }

            int                   experience = InputForm.ReadInt("стаж, отсчитываемый от 25 лет", ExperienceRange);
            IEnumerable<Engineer> engineers  = this.Get<Engineer>();
            int                   count      = engineers.Count(engineer => (engineer.Age - 25) >= experience);

            this.PrintToRequestListBox(new List<string> { count + " инженеров в списке" });
            this.JournalListBox.Items
                .Add($"Успешно посчитано количество инженеров со стажем не менее {experience} лет!");
        }

        private void AverageWorkerSalaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            double averageSalary = this.GetExtension<Worker>().Average(worker => worker.Salary);
            this.PrintToRequestListBox(new List<string>
                                       { Math.Round(averageSalary, 2) + " – средняя зарплата всех рабочих" });
            this.JournalListBox.Items.Add("Успешно посчитана средняя зарплата всех рабочих!");
        }

        private void GroupByProfessionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.RequestListBox.Items.Clear();
            IEnumerable<IGrouping<Type, Person>>
                group = this.GetExtension<Person>().GroupBy(person => person.GetType());
            foreach (IGrouping<Type, Person> pair in group)
            {
                IEnumerable<string> personsInGroup = pair.Select(person => person.ToString());
                this.RequestListBox.Items.Add(pair.Key.Name);
                foreach (string personInfo in personsInGroup)
                {
                    this.RequestListBox.Items.Add("  " + personInfo);
                }
            }

            this.JournalListBox.Items.Add("Успешно выведены группы по профессиям");
        }

        #endregion Методы расширения LINQ

        #region Дополнительные методы

        private string GetLastMessage()
        {
            return (string)this.JournalListBox.Items[this.JournalListBox.Items.Count - 1];
        }

        private void OnInputFormOnGetMessage(object sender, ResultMessageEventArgs args)
        {
            this.JournalListBox.Items.Add(args.Message);
        }

        private List<T> GetExtension<T>() where T : Person
        {
            IEnumerable<T> people = this._factory
                                        .SelectMany(workshop => workshop)
                                        .OfType<T>();

            return people.ToList();
        }

        private List<T> Get<T>() where T : Person
        {
            IEnumerable<T> people = from workshop in this._factory
                                    from person in workshop
                                    where person is T select person as T;
            return people.ToList();
        }

        private static IEnumerable<string> GetWorkersNamesByNumberExt(IEnumerable<Worker> workers, int workshopNumber)
        {
            return workers.Where(worker => worker.WorkshopNumber == workshopNumber)
                          .Select(worker => worker.FullName);
        }

        private static IEnumerable<string> GetWorkersNamesByNumber(IEnumerable<Worker> workers, int workshopNumber)
        {
            return from worker in workers
                   where worker.WorkshopNumber == workshopNumber
                   select worker.FullName;
        }

        private void PrintToRequestListBox<T>(IEnumerable<T> collection)
        {
            this.RequestListBox.Items.Clear();
            foreach (T item in collection)
            {
                this.RequestListBox.Items.Add(item);
            }
        }

        private void PrintFactoryToListBox()
        {
            this.ListBox.Items.Clear();
            var workshopIndex = 0;
            foreach (List<Person> workshop in this._factory)
            {
                this.ListBox.Items.Add("Цех №" + ++workshopIndex);
                foreach (Person person in workshop)
                {
                    this.ListBox.Items.Add(person);
                }

                this.ListBox.Items.Add(Environment.NewLine);
            }
        }

        #endregion Дополнительные методы

        #region Расширение MyCollection

        private MyNewHashTable<int, Person> _myNewHashTable;

        private void PrintMyNewHashTable()
        {
            this.ListBox.Items.Clear();
            foreach (KeyValuePair<int, Person> pair in this._myNewHashTable)
            {
                this.ListBox.Items.Add(pair.Key + ": " + pair.Value);
            }
        }

        private void CreateMyNewCollectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this._myNewHashTable = new MyNewHashTable<int, Person>();
            for (var i = 0; i < 30; i++)
            {
                this._myNewHashTable.Add(i + 1, Person.GetRandomPerson());
            }

            this.PrintMyNewHashTable();
            this.JournalListBox.Items.Add("Успешно создана новая коллекция MyNewHashTable");
            this.PrintPersonsOnlyToolStripMenuItem.Visible    = true;
            this.AverageEngineersAgeToolStripMenuItem.Visible = true;
            this.SortDescByAgeToolStripMenuItem.Visible       = true;
        }

        private void PrintPersonsOnlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.RequestListBox.Items.Clear();
            MyNewHashTable<int, Person> persons = this._myNewHashTable.Where(pair => pair.Value.IsPersonOnly());
            foreach (KeyValuePair<int, Person> pair in persons)
            {
                this.RequestListBox.Items.Add(pair.Value);
            }

            this.JournalListBox.Items.Add("Успешно выведены все люди без профессии");
        }

        private void AverageEngineersAgeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.RequestListBox.Items.Clear();
            double averageAge = this._myNewHashTable
                                    .Where(pair => pair.Value is Engineer)
                                    .Average(pair => pair.Value.Age);
            this.RequestListBox.Items.Add(Math.Round(averageAge, 2) + " – средний возраст всех инженеров");
            this.JournalListBox.Items.Add("Успешно выведен средний возраст всех инженеров");
        }


        private void SortDescByAgeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int Comparison(KeyValuePair<int, Person> pair1, KeyValuePair<int, Person> pair2)
            {
                return pair1.Value.Age.CompareTo(pair2.Value.Age);
            }

            List<KeyValuePair<int, Person>> sortedPairs = this._myNewHashTable.SortDescending(Comparison);
            this.JournalListBox.Items
                .Add("Успешно отсортирована коллекция MyNewHashTable по возрасту в порядке убывания возраста");
            this.ListBox.Items.Clear();
            sortedPairs.ForEach(pair => this.ListBox.Items.Add(pair.Value));
        }

        #endregion Расширение MyCollection
    }
}
