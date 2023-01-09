// See https://aka.ms/new-console-template for more information
using BankDataLibrary.Config;
using BankDataLibrary.Entities;

Console.WriteLine("Hello, World!");

try
{
    using LichvaContext db = new LichvaContext();
    //db.Banks.Add(new Bank {
    //    Name = "RandomBank",
    //    CreationDate = DateTime.Now,
    //});
    foreach (Bank dupa in db.Banks.ToList())
        Console.WriteLine($"({dupa.Id} , {dupa.Name} , {dupa.CreationDate}");
        //Console.WriteLine($"({dupa.id} , {dupa.name} , {dupa.creation_date}");

}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}
