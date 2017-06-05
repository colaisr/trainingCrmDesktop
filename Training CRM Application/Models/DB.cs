using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class DB
    {
        public BindingList<Account> Accounts { get; set; }
        public BindingList<Contact> Contacts { get; set; }
        public BindingList<Activitie> Activities { get; set; }
        // will be used by serializer
        public DB()
        {
           
        }
        // with predefined values
        public DB(bool initial)
        {
            fillInitialValues();
        }

        public BindingList<string> GetAllAccounts()
        {
            BindingList<string> acc = new BindingList<string>();
            foreach (Account item in Accounts)
            {
                acc.Add(item.Name);
            }

            return acc;
        }
        public BindingList<string> GetAllContacts()
        {
            BindingList<string> cont = new BindingList<string>();
            foreach (Contact item in Contacts)
            {
                cont.Add(item.FirstName+""+item.LastName);
            }

            return cont;
        }
        private void fillInitialValues()
        {
            Accounts = new BindingList<Account>() {
               new Account() {
                    Name = "Nice",
                    Source ="WEB",
                    Status =Status.New,
                    WebPage ="nice.com",
                    Street ="Zarhin 12",
                    City ="Raanana",
                    State ="Arizona",
                    Zip ="45689"},
                new Account() {
                    Name ="Google",
                    Source ="hot Net",
                    Status =Status.Closed,
                    WebPage ="google.com",
                    Street ="Smolensin 14",
                    City ="Holon",
                    State ="Cansas",
                    Zip ="654987" } };

            


            //contacts default Values
            Contacts = new BindingList<Contact>();
            Contacts.Add(new Contact()
            {
                FirstName = "Gal",
                LastName = "Tesler",
                Salutation = "Mr",
                Account = "Eaglue",
                City = "Tel Aviv",
                Email = "gal.tesler@eaglue.com",
                Phone = "0542334569",
                State = "Nebraska",
                Street = "Rotshild 12",
                Zip = "321654"
            });
            Contacts.Add(new Contact()
            {
                FirstName = "Gaby",
                LastName = "Maor",
                Salutation = "Mr",
                Account = "Nice",
                City = "London",
                Email = "Gaby.maor@somwhere.com",
                Phone = "0542334987",
                State = "Nebraska",
                Street = "Usishkin 12",
                Zip = "654654"
            });

            Activities = new BindingList<Activitie>();

        }


    }
}
