
using BankDataLibrary.Entities;

namespace UnitTests
{
    [TestClass]
    public class LichvaContextTests
    {
        [TestMethod]
        public void BankTest()
        {
            try
            {
                LichvaContext db = new LichvaContext();
                db.Banks.Add(new Bank
                {
                    creation_date = DateTime.Now,
                    name = "random2",
                });

                db.SaveChanges();

                foreach(Bank bank in db.Banks)
                {
                    Console.WriteLine($"{bank.id}, {bank.name}, {bank.creation_date}");
                }

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        [TestMethod]
        public void InquiriesTest()
        {
            using LichvaContext db = new();
            db.Inquiries.Add(new Inquiry
            {
                creation_date = DateTime.Now,
                user_id = 1,
                ammount = 150,
                installments = 10,
            });

            db.SaveChanges();

            foreach (Inquiry inq in db.Inquiries)
            {
                Console.WriteLine($"{inq.id}, {inq.user_id}, {inq.creation_date}, {inq.ammount}, {inq.installments}");
            }

        }
    }
}