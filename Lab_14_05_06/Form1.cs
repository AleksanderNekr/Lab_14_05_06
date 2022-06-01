using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using InputMethodsForm;
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
            IEnumerable<IGrouping<Type, Person>> persons = group.Select(pair => pair);
            foreach (IGrouping<Type, Person> grouping in persons)
            {
                List<string> personsInGroup = grouping.Select(person => person.ToString()).ToList();
                this.RequestListBox.Items.Add(grouping.Key.Name);
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
    }
}
