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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private List<JournalRecord> lists = null;
        public InvItemDB file = new InvItemDB("InventoryItems");
        public string parsed;
        private string secondFilePath;
        public string folder;
        private void btnAdd_Click(object sender, EventArgs e)
        {
            FillItemListBox();
            frmAddDetails details = new frmAddDetails();
            details.ShowDialog();
            JournalRecord item = null;
            item = details.item;
            if (!details.cancel)
            {
                lbList.Items.Add(item.category);
                lists.Add(item);
                file.SaveItems(lists, true);
               
            }

        }
        private void FillItemListBox()
        {
            lbList.Items.Clear();
            lbList.Items.Add("Category");
            // Add code here that loads the list box with the items in the list.
            lists = file.GetItems(true);
            foreach (JournalRecord item in lists)
            {
                lbList.Items.Add(item.category);

            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

            const string path = @"..\..\InventoryItems.xml";
            if (!File.Exists(path))
            {
                FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write);
                StreamWriter outputFile = new StreamWriter(file);
                outputFile.Write("<?xml version=\"1.0\" encoding=\"utf-8\"?> \n<JournalRecord />");
                outputFile.Close();
            }


            FillItemListBox();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExpand_Click(object sender, EventArgs e)
        {
            int selected = lbList.SelectedIndex;
            if (selected != -1)
            {
                parsed = lbList.GetItemText(lbList.SelectedItem);
                frmExpanded expand = new frmExpanded();
                //figure a way out to sperate the date from the details
               
                expand.lblDetail.Text = parsed;
                expand.secondFile = new InvItemDB(parsed+"_Journal");
                folder = @"../../" + parsed;
                Directory.CreateDirectory(folder);
                expand.folder = folder;
                secondFilePath = expand.secondFile.getPath();
                expand.Show();
                //add directory
                
            }
            else
            {
                MessageBox.Show("You must select an item");
            }
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            int i = lbList.SelectedIndex;
            if (i != -1)
            {

                // the deletion and then removes the item from the list,
                //MessageBox.Show(parsed);
                parsed = lbList.GetItemText(lbList.SelectedItem);
                folder = @"../../" + parsed;
                File.Delete(@"..\..\" + parsed + ".xml");
                if(Directory.Exists(folder))
                    Directory.Delete(folder, true);
                lbList.Items.RemoveAt(i);
                // saves the list of products, and refreshes the list box 
                lists.Remove(lists[i - 1]);
                file.SaveItems(lists, true);
                FillItemListBox();
            }
            else
            {
                MessageBox.Show("You must select an item to delete");
            }
        }

        private void btnOpenfolder_Click(object sender, EventArgs e)
        {
            if (lbList.SelectedIndex != -1)
            {
                parsed = lbList.GetItemText(lbList.SelectedItem);
                folder = @"../../" + parsed;
                if(!Directory.Exists(folder)) 
                     Directory.CreateDirectory(folder);

                string absolute = Path.GetFullPath(folder);
                Process.Start(absolute);
            }
            else
            {
                MessageBox.Show("You must select an item to open");
            }
        }
    }
}
