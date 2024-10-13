using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackLibrary
{
    public class Person
    {
        #region Properties

        public String? Name { get; set; }
        public DateTimeOffset Born { get; set; }
        public List<Person> Children = new();

        //Allow multiple spouses to be stored for a person
        public List<Person> Spouses = new();

        public Person()
        {
        }

        //A readonly field to show is a person is married to anyone
        public bool Married => Spouses.Count > 0;

        #endregion

        #region Methods

        public void WriteToConsole()
        {
            Console.WriteLine($"{Name} was born on a {Born:yyyy-M-dddd}");
        }

        public void WriteChildrenToConsole()
        {
            string term = Children.Count == 1 ? "child" : "children";
            Console.WriteLine($"{Name} has {Children.Count} {term}.");
        }

        //static method to marry two people
        public static void Marry(Person p1, Person p2)
        {
            ArgumentNullException.ThrowIfNull(p1);
            ArgumentNullException.ThrowIfNull(p2);

            if (p1.Spouses.Contains(p2) || p2.Spouses.Contains(p1))
            {
                throw new ArgumentException(string.Format("{0} is already married to {1}.", arg0: p1.Name, arg1: p2.Name));
            }

            p1.Spouses.Add(p2);
            p2.Spouses.Add(p1);
        }

        public void Marry(Person partner)
        {
            Marry(this, partner); //this; --> current person
        }

        public void OutputSpouses()
        {
            if (Married)
            {
                string term = Spouses.Count == 1 ? "person" : "people";
                Console.WriteLine($"{Name} is married to {Spouses.Count} {term}:");

                foreach (var spouse in Spouses)
                {
                    Console.WriteLine($"{spouse.Name}");
                }
            }
            else
            {
                Console.WriteLine($"{Name} is single.");
            }
        }

        //procreate
        public static Person Procreate(Person p1, Person p2)
        {
            ArgumentNullException.ThrowIfNull(p1);
            ArgumentNullException.ThrowIfNull(p2);
            if (!p1.Spouses.Contains(p2) && p2.Spouses.Contains(p1))
            {
                throw new ArgumentException(string.Format("{0} must be married to {1} to procreate with them.",
                    arg0: p1.Name, arg1: p2.Name));
            }

            Person baby = new()
            {
                Name = $"Baby of {p1.Name} and {p2.Name}",
                Born = DateTimeOffset.Now
            };

            p1.Children.Add(baby);
            p2.Children.Add(baby);

            return baby;
        }

        //instance method for procreate
        public Person ProcreateWith(Person partner)
        {
            return Procreate(this, partner);
        }

         // Check if a married couple has kids and let them adopt if not
        public static void CheckAndAdoptIfNoKids(Person p1, Person p2, Person adoptableChild)
        {
            // Check if both spouses have no biological children together
            bool haveCommonChildren = p1.Children.Intersect(p2.Children).Any();

            if (!haveCommonChildren)
            {
                p1.AdoptKid(adoptableChild);
                p2.AdoptKid(adoptableChild);
                Console.WriteLine($"{p1.Name} and {p2.Name} have adopted {adoptableChild.Name}.");
            }
            else
            {
                Console.WriteLine($"{p1.Name} and {p2.Name} already have kids.");
            }
        }
        // Check if person has kids
        public bool HasKids()
        {
            return Children.Count > 0;
        }

        // Adopt a kid
        public void AdoptKid(Person child)
        {
            Children.Add(child);
            Console.WriteLine($"{Name} has adopted {child.Name}.");
        }

        // Check if a baby is a stepchild
        public bool IsStepChild(Person baby)
        {
            // A stepchild is defined as a child of a spouse but not a biological child
            foreach (var spouse in Spouses)
            {
                if (spouse.Children.Contains(baby) && !Children.Contains(baby))
                {
                    return true;
                }
            }
            return false;
        }

        // Show parents of a baby
        public void ShowParents(Person baby)
        {
            if (Children.Contains(baby))
            {
                foreach (var spouse in Spouses)
                {
                    if (spouse.Children.Contains(baby))
                    {
                        Console.WriteLine($"Parents of {baby.Name}: {Name} and {spouse.Name}");
                        return;
                    }
                }
            }
            else
            {
                Console.WriteLine($"{baby.Name} is not one of the children of {Name}.");
            }
        }

        #endregion
    }
}
