using System.Text.Json.Serialization;
using PackLibrary;

// Create instances of Person
Person john = new() { Name = "John" };
Person jane = new() { Name = "Jane" };
Person sarah = new() { Name = "Sarah" };

// Marry John and Jane
john.Marry(jane);

// Output spouses
john.OutputSpouses();
jane.OutputSpouses();
sarah.OutputSpouses();

// John and Jane have children
Person baby1 = john.ProcreateWith(jane);
baby1.Name = "Mike";
baby1.Born = new DateTimeOffset(2020, 6, 15, 9, 0, 0, TimeSpan.Zero);
Console.WriteLine($"{baby1.Name} was born on {baby1.Born}");

// Create another child with John and Jane
Person baby2 = john.ProcreateWith(jane);
baby2.Name = "Anna";
baby2.Born = new DateTimeOffset(2019, 5, 20, 10, 0, 0, TimeSpan.Zero);
Console.WriteLine($"{baby2.Name} was born on {baby2.Born}");

// Check if John and Jane have kids
Person.CheckAndAdoptIfNoKids(john, jane, new Person { Name = "Adopted Child" }); // Should not adopt since they have biological kids


// Now, John and Sarah will also get married and check for adoption
john.Marry(sarah);

// Create an adopted child
Person baby3 = new() { Name = "Tom", Born = new DateTimeOffset(2022, 3, 10, 15, 30, 0, TimeSpan.Zero) };

// John and Sarah adopt baby3
Person.CheckAndAdoptIfNoKids(john, sarah, baby3);
Console.WriteLine($"{baby3.Name} was adopted on {baby3.Born}.");

// Check if baby1 is a stepchild for Jane
bool isBaby1StepChild = jane.IsStepChild(baby1);
Console.WriteLine($"{baby1.Name} is {(isBaby1StepChild ? "a stepchild" : "not a stepchild")} for Jane.");

// Check if baby2 is a stepchild for Jane
bool isBaby2StepChild = jane.IsStepChild(baby2);
Console.WriteLine($"{baby2.Name} is {(isBaby2StepChild ? "a stepchild" : "not a stepchild")} for Jane.");

// Check if baby3 is a stepchild for Jane
bool isBaby3StepChild = jane.IsStepChild(baby3);
Console.WriteLine($"{baby3.Name} is {(isBaby3StepChild ? "a stepchild" : "not a stepchild")} for Jane.");

// Show parents of baby1
john.ShowParents(baby1);

// Show parents of baby2
john.ShowParents(baby2);

// Show parents of baby3
sarah.ShowParents(baby3);

// Output children
john.WriteChildrenToConsole();
jane.WriteChildrenToConsole();
sarah.WriteChildrenToConsole();

Console.ReadLine();
