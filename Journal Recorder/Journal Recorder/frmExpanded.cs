using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Journal_Recorder
{
    public partial class frmExpanded : Form
    {
        public frmExpanded()
        {
            InitializeComponent();
        }
        public InvItemDB secondFile;
        public string folder;
        private List<JournalRecord> lists = null;

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FillItemListBox();
            frmExpandDetails details = new frmExpandDetails();
            details.ShowDialog();
            JournalRecord item = null;
            string fileData = "/" + details.txtJournal.Text + ".docx";
            //If you want to open a file you'd use the selected item text to find document
            FileStream file = new FileStream(folder + fileData, FileMode.Create);
            StreamWriter outputFile = new StreamWriter(file);
            outputFile.Close();
            item = details.item;
            if (!details.cancel)
            {
                lbList.Items.Add(item.displayText());
                lists.Add(item);
                secondFile.SaveItems(lists, false);
            }

        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            int i = lbList.SelectedIndex;
            if (i != -1)
            {

                // the deletion and then removes the item from the list
                string data = lbList.GetItemText(lbList.SelectedItem);
                int s;
                //getting the third /
                s = data.IndexOf("/", 3);
                //adding the date
                //Finally works
                string subString = data.Substring(i + 8);
                string fileToOpen = subString.Replace("\t", "");
                string fileData = folder + "/" + fileToOpen + ".docx";

                File.Delete(fileData);
                lbList.Items.RemoveAt(i);



                // saves the list of products, and refreshes the list box 
                lists.Remove(lists[i - 1]);

                secondFile.SaveItems(lists, false);
                FillItemListBox();

            }
            else {
                MessageBox.Show("You must select a journal to delete");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void FillItemListBox()
        {
            lbList.Items.Clear();
            lbList.Items.Add("Date" + "\t\t" + "Journal");
            // Add code here that loads the list box with the items in the list.
            lists = secondFile.GetItems(false);
            foreach (JournalRecord item in lists)
            {
                lbList.Items.Add(item.displayText());
            }
        }



        private void frmExpanded_Load_1(object sender, EventArgs e)
        {
           
            string path = secondFile.getPath();

            if (!File.Exists(path))
            {
                FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write);
                StreamWriter outputFile = new StreamWriter(file);
                outputFile.Write("<?xml version=\"1.0\" encoding=\"utf-8\"?> \n<JournalRecord />");
                outputFile.Close();
            }


            FillItemListBox();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            //Parse the data
            if (lbList.SelectedIndex > -1)
            {
                string data = lbList.GetItemText(lbList.SelectedItem);
                int i;
                //getting the third /
                i = data.IndexOf("/", 3);
                //adding the date
                //Finally works
                string subString = data.Substring(i + 6);
                string fileToOpen = subString.Replace("\t", "");
                string fileData = folder + "/" + fileToOpen + ".docx";
                string absolute = Path.GetFullPath(fileData);

                Process.Start(absolute);
            }
            else
            {
                MessageBox.Show("You must select an item to open");
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<JournalRecord> lists = new List<JournalRecord>();
            lists = secondFile.GetItems(false);
            lbList.Items.Clear();
            lbList.Items.Add("Date" + "\t\t" + "Journal");
            bool found = false;
            foreach (JournalRecord j in lists)
            {
                if (dtDate.Text == j.date)
                {
                    lbList.Items.Add(j.displayText());
                    found = true;
                }

            }
            if (!found) {
                MessageBox.Show("No journals found in that specified date");
            }
        }
     

        private void btnGetAllJournals_Click(object sender, EventArgs e)
        {
            FillItemListBox();
        }
    }
}
