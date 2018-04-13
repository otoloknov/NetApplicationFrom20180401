using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using ConsoleTableExt;
using System.Threading;

namespace Attraction
{
    // Internal class for Kid generation (Initialization)
    // Singelton is better? Maybe not in this case?
    class RandomKidGenerator
    {
        // List of Names for ( M + F )
        private enum ListOfWonamNames { Ardath = 1, Alyce, Fermina, Emily, Ronna, Erminia, Sheryl, Shanita, Lecia, Leonor }
        private enum ListOfManNames { Dexter = 1, Cortez, Rocco, Winston, Corey, Coy, Dirk, Sammie, Levi, Caleb }

        private readonly List<Kid> _kidList = new List<Kid>();

        // public constructor for Kid generation 
        public RandomKidGenerator()
        {
            var rand = new Random();
            
            for (int i = 0; i < 2; i++)
            {
                _kidList.Add(new Kid(((ListOfManNames)rand.Next(1, 11)).ToString(), rand.Next(50,250), GenderType.M, rand.Next(5,18), rand.Next(5, 10)));
                _kidList.Add(new Kid(((ListOfWonamNames)rand.Next(1, 11)).ToString(), rand.Next(50, 250), GenderType.F, rand.Next(5, 18), rand.Next(5, 10)));
            }
            for (int i = 0; i < 3; i++)
            {
                _kidList.Add(new Kid(((ListOfManNames)rand.Next(1, 11)).ToString(), rand.Next(50, 250), GenderType.M, rand.Next(5, 18), rand.Next(84, 151), rand.Next(60, 100)));
                _kidList.Add(new Kid(((ListOfWonamNames)rand.Next(1, 11)).ToString(), rand.Next(50, 250), GenderType.F, rand.Next(5, 18), rand.Next(84, 151), rand.Next(60, 100)));
            }
            
            _kidList.Add(new Kid(((ListOfManNames)rand.Next(1, 11)).ToString(), rand.Next(50, 65), GenderType.M, rand.Next(5, 18), rand.Next(84, 151), rand.Next(60, 100)));
            _kidList.Add(new Kid(((ListOfWonamNames)rand.Next(1, 11)).ToString(), rand.Next(50, 65), GenderType.F, rand.Next(5, 18), rand.Next(84, 151), rand.Next(60, 100)));

        }

        // return list of generated Kids
        // out type: List<Kid>
        // how to use Singelton here?
        public List<Kid> GetListOfGeneratedKids()
        {
            return _kidList;
        }

        // fucntion that return DataTable type based on structure below
        private DataTable GetSampleTableData()
        {
            var table = new DataTable();
            // adding list of columns
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Height", typeof(string));
            table.Columns.Add("Gender", typeof(string));
            table.Columns.Add("Age", typeof(int));
            table.Columns.Add("Money", typeof(int));
            table.Columns.Add("Satisfaction", typeof(int));
            table.Columns.Add("Action", typeof(string));
            table.Columns.Add("IsBusy", typeof(bool));

            // addded listof value into columns
            foreach (var kids in _kidList)
            {
                table.Rows.Add(kids.Name,kids.Height, kids.Gender, kids.Age,kids.GetCurrentMoneyAmount(),kids.GetSatisfactionDegree(),kids.GetKidAction(),kids.GetIsBusyStatus());
            }
            // return table
            return table;
        }

        // Print information run in separete thread until cash box will be not full
        public void PrintInformationAboutKids()
        {
            do
            {
                // Clean console
                Console.Clear();
                Console.WriteLine("CashBox = {0}", Attraction.CashBox);
                //ConsoleTableBuilder was loaded from NuGet
                ConsoleTableBuilder
                    .From(GetSampleTableData())
                    .ExportAndWriteLine();
                // need to have some kind on delay
                Thread.Sleep(100);
            } while (Attraction.CashBox < AttractionManager.MAX_VALUE_FOR_CASH_BOX);
        }
    }
}
