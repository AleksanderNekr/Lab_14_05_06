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

        private void WorkersNamesOfWorkshopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<Worker> workers = this.GetWorkers();

            bool IsCorrectNumber(object input)
            {
                return workers.Any(worker => worker.WorkshopNumber == (int)input);
            }

            int workshopNumber = InputForm.ReadInt("номер цеха", IsCorrectNumber);
            if (this.GetLastMessage() == InputForm.CancelMessage)
            {
                return;
            }

            this.PrintToRequestListBox(GetWorkersNamesByNumber(workers, workshopNumber));
            this.JournalListBox.Items.Add("Успешно выведены имена рабочих в цехе №" + workshopNumber);
        }

        private string GetLastMessage()
        {
            return (string)this.JournalListBox.Items[this.JournalListBox.Items.Count - 1];
        }

        private void OnInputFormOnGetMessage(object o, ResultMessageEventArgs args)
        {
            this.JournalListBox.Items.Add(args.Message);
        }

        private List<Worker> GetWorkers()
        {
            IEnumerable<Worker> workers = this._factory.SelectMany(workshop => workshop)
                                              .OfType<Worker>();

            return workers.ToList();
        }

        private static IEnumerable<string> GetWorkersNamesByNumber(IEnumerable<Worker> workers, int workshopNumber)
        {
            return workers.Where(worker => worker.WorkshopNumber == workshopNumber)
                          .Select(worker => worker.FullName);
        }

        private void PrintToRequestListBox<T>(IEnumerable<T> collection)
        {
            this.RequestListBox.Items.Clear();
            foreach (T item in collection)
            {
                this.RequestListBox.Items.Add(item);
            }
        }
    }
}
