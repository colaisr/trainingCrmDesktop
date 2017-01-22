using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Training_CRM_Application
{
    public partial class NewContact : Form
    {

        private DataGridView dgvContacts;
        private BindingList<Contact> listOfContacts;

        public NewContact()
        {
            InitializeComponent();
        }

        public NewContact(BindingList<Contact> listContacts, DataGridView dgvontacts)
        {
            listOfContacts = listContacts;
            this.dgvContacts = dgvontacts;
        }
    }
}
