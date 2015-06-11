using System.Linq;
//realiztion of method for Client Table
namespace SqlGIS.Entity
{
    public partial class SqlRepository : IRepository
    {
        public IQueryable<Client> Clients
        {
            get
            {
                return Db.Clients;
            }
        }
//save new or edit
        public void SaveClient(Client client)
        {
            if (client.Id == 0)
            {
                Db.Clients.Add(client);
            }
            else
            {
                Client dbEntry = Db.Clients.Find(client.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = client.Name;
                    dbEntry.PassportNumber = client.PassportNumber;
                    dbEntry.AbroadPassNumber = client.AbroadPassNumber;
                    dbEntry.Address = client.Address;
                    dbEntry.PhoneNumber = client.PhoneNumber;
                    dbEntry.BirthDate = client.BirthDate;

                }
            }
            Db.SaveChanges();
        }

        public Client DeleteClient(int ID)
        {
            Client dbEntry = Db.Clients.Find(ID);
            if (dbEntry != null)
            {
                Db.Clients.Remove(dbEntry);
                Db.SaveChanges();
            }
            return dbEntry;
        }
    }
}
