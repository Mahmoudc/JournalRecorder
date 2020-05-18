using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Journal_Recorder
{
    public partial class frmExpandDetails : Form
    {
        public frmExpandDetails()
        {
            InitializeComponent();
        }
        public JournalRecord item = new JournalRecord();
        public bool cancel = false;

        private void button1_Click(object sender, EventArgs e)
        {
            cancel = true;
            this.Close();
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            if (txtJournal.Text != "")
            {
                item.date = dateTimePicker1.Text;
                item.journal = txtJournal.Text;
                
                this.Close();
            }
            else
            {
                MessageBox.Show("You can leave details empty");
            }
        }

       
    }
}
