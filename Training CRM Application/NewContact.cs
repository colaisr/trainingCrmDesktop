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
            this.listOfContacts = listContacts;
            this.dgvContacts = dgvontacts;
        }

        private void NewContact_LocationChanged(object sender, EventArgs e)
        {
            if ((this.Left + this.Width) > Screen.AllScreens[0].Bounds.Width)
                this.Left = Screen.AllScreens[0].Bounds.Width - this.Width;

            if (this.Left < Screen.AllScreens[0].Bounds.Left)
                this.Left = Screen.AllScreens[0].Bounds.Left;

            if ((this.Top + this.Height) > Screen.AllScreens[0].Bounds.Height)
                this.Top = Screen.AllScreens[0].Bounds.Height - this.Height;

            if (this.Top < Screen.AllScreens[0].Bounds.Top)
                this.Top = Screen.AllScreens[0].Bounds.Top;
        }
    }
}
