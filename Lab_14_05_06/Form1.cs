using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
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
        }

        private void PrintFactoryToListBox()
        {
            this.ListBox.Items.Clear();
            var workshopsCount = 0;
            foreach (List<Person> workshop in this._factory)
            {
                this.ListBox.Items.Add("Цех №" + ++workshopsCount);
                foreach (Person person in workshop)
                {
                    if (person is Worker worker)
                    {
                        worker.WorkshopNumber = workshopsCount;
                        this.ListBox.Items.Add(worker);
                        continue;
                    }

                    this.ListBox.Items.Add(person);
                }
            }
        }

        private void CreateMainCollectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (var workshopIndex = 0; workshopIndex < WorkshopsCount; workshopIndex++)
            {
                var persons = new List<Person>();
                for (var personIndex = 0; personIndex < PeopleCount; personIndex++)
                {
                    persons.Add(Person.GetRandomPerson());
                }

                this._factory.Enqueue(persons);
            }

            this.PrintFactoryToListBox();
        }
    }
}
