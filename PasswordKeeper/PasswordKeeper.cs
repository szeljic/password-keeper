using PasswordKeeper.Passwords;
using PasswordKeeper.Type;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PasswordKeeper
{
    public partial class PasswordKeeper : Form
    {
        private int idUser;
        private Form1 loginForm;
        private int logout = 0;
        private List<string> listOfPasswords = new List<string>();
        private TreeNode[] listOfNodes = new TreeNode[6];

        public PasswordKeeper(int idUser, Form1 loginForm)
        {
            this.idUser = idUser;
            this.loginForm = loginForm;
            InitializeComponent();
        }

        private void listView1_Resize(object sender, EventArgs e)
        {
            int x = this.lvPasswords.Width / 15 == 0 ? 1 : this.lvPasswords.Width / 15;
            this.lvPasswords.Columns[0].Width = x * 2;
            this.lvPasswords.Columns[1].Width = x * 3;
            this.lvPasswords.Columns[2].Width = x * 3;
            this.lvPasswords.Columns[3].Width = x * 2;
            this.lvPasswords.Columns[4].Width = x * 2;
            this.lvPasswords.Columns[5].Width = x * 3;
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PasswordKeeper_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (logout == 0)
            {
                Application.Exit();
            }            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddModifyItemForm addItemForm = new AddModifyItemForm(idUser, 0);
            addItemForm.ShowDialog();
        }

        private void addEnteryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.btnAdd_Click(sender, e);
        }

        private void PasswordKeeper_Load(object sender, EventArgs e)
        {
            this.btnModify.Enabled = false;
            this.btnRemove.Enabled = false;
            this.changeSelectedToolStripMenuItem.Enabled = false;
            this.removeSelectedToolStripMenuItem.Enabled = false;

            UnitTypeDAO types = new UnitTypeDAOImpl();
            UnitType allTypes = types.GetAllTypes();

            TreeNode ParentNode = new TreeNode();
            ParentNode.Text = "All";
            ParentNode.ForeColor = Color.Black;
            ParentNode.BackColor = Color.White;
            ParentNode.ImageIndex = 0;
            ParentNode.SelectedImageIndex = 0;
            listOfNodes[0] = ParentNode;
            this.tvTypes.Nodes.Add(ParentNode);

            int i = 1;

            foreach (string type in allTypes.AllTypes)
            {
                TreeNode ChildNode = new TreeNode();
                ChildNode.Text = type;
                ChildNode.ForeColor = Color.Black;
                ChildNode.BackColor = Color.White;
                ChildNode.ImageIndex = i;
                ChildNode.SelectedImageIndex = i;
                listOfNodes[i] = ChildNode;
                ParentNode.Nodes.Add(ChildNode);
                i++;
            }

            this.tvTypes.Nodes[0].Expand();
        }



        private void tvTypes_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.btnModify.Enabled = false;
            this.btnRemove.Enabled = false;
            this.changeSelectedToolStripMenuItem.Enabled = false;
            this.removeSelectedToolStripMenuItem.Enabled = false;
            this.lvPasswords.Items.Clear();
            List<Item> list = new List<Item>();
            this.cbShowPasswords.Checked = false;

            if (this.tvTypes.SelectedNode.Text == "All")
            {
                for (int i = 0; i < 5; i++)
                {
                    ItemDAO itemDAO = new ItemDAOImpl();
                    list.AddRange(itemDAO.SelectItemsForType(idUser, i + 1));
                }
            }
            else
            {
                ItemDAO itemDAO = new ItemDAOImpl();
                list.AddRange(itemDAO.SelectItemsForType(idUser, this.tvTypes.SelectedNode.Index + 1));
            }

            listOfPasswords.Clear();
            ListViewItem lvi;
            foreach (Item item in list)
            {
                lvi = new ListViewItem(item.Title);
                lvi.SubItems.Add(item.Username);
                lvi.SubItems.Add("");
                listOfPasswords.Add(item.Password);

                switch (item.Type)
                {
                    case 0:
                        lvi.SubItems.Add("All");
                        break;
                    case 1:
                        lvi.SubItems.Add("General");
                        break;
                    case 2:
                        lvi.SubItems.Add("Web");
                        break;
                    case 3:
                        lvi.SubItems.Add("Game");
                        break;
                    case 4:
                        lvi.SubItems.Add("Email");
                        break;
                    case 5:
                        lvi.SubItems.Add("Windows");
                        break;
                }
                lvi.SubItems.Add(item.Url);
                if(item.Expires.ToString() == DateTime.MaxValue.ToString())
                {
                    lvi.SubItems.Add("");
                }
                else
                {
                    lvi.SubItems.Add(item.Expires.ToString("dd.MM.yyyy HH:mm"));
                }
                lvi.SubItems.Add(item.IdPass.ToString());
                this.lvPasswords.Items.Add(lvi);
            }
        }

        private void cbShowPasswords_CheckedChanged(object sender, EventArgs e)
        {
            int i = 0;
            if (cbShowPasswords.Checked)
            {
                foreach (string pass in listOfPasswords)
                {
                    this.lvPasswords.Items[i].SubItems[2].Text = pass;
                    i++;
                }
            }
            else
            {
                if (this.lvPasswords.Items.Count > 0)
                {
                    foreach (string pass in listOfPasswords)
                    {
                        this.lvPasswords.Items[i].SubItems[2].Text = "";
                        i++;
                    }
                }
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            int idPass = 0;
            if (this.lvPasswords.SelectedItems.Count > 0)
            {
                ListViewItem item = this.lvPasswords.SelectedItems[0];
                idPass = Convert.ToInt32(item.SubItems[6].Text);
            }

            AddModifyItemForm addModifyForm = new AddModifyItemForm(idUser, idPass);
            addModifyForm.ShowDialog();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            int idPass = 0;
            if (this.lvPasswords.SelectedItems.Count > 0)
            {
                ListViewItem item = this.lvPasswords.SelectedItems[0];
                idPass = Convert.ToInt32(item.SubItems[6].Text);
            }

            ItemDAO itemDAO = new ItemDAOImpl();
            itemDAO.RemoveItem(idPass);
            
            this.btnRemove.Enabled = false;
        }

        private void lvPasswords_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lvPasswords.SelectedItems.Count > 0)
            {
                this.btnModify.Enabled = true;
                this.btnRemove.Enabled = true;
                this.changeSelectedToolStripMenuItem.Enabled = true;
                this.removeSelectedToolStripMenuItem.Enabled = true;
            }
            else
            {
                this.btnModify.Enabled = false;
                this.btnRemove.Enabled = false;
                this.changeSelectedToolStripMenuItem.Enabled = false;
                this.removeSelectedToolStripMenuItem.Enabled = false;
            }
        }

        private void changeSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
             btnModify_Click(sender, e);
        }

        private void removeSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnRemove_Click(sender, e);
        }

        private void allToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tvTypes.SelectedNode = listOfNodes[0];
        }

        private void generalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tvTypes.SelectedNode = listOfNodes[1];
        }

        private void webToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tvTypes.SelectedNode = listOfNodes[2];
        }

        private void gameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tvTypes.SelectedNode = listOfNodes[3];
        }

        private void emailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tvTypes.SelectedNode = listOfNodes[4];
        }

        private void windowsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tvTypes.SelectedNode = listOfNodes[5];
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.ShowDialog();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            logout = 1;
            loginForm.Show();
            this.Close();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            logout = 1;
            loginForm.Show();
            RegistryForm registryForm = new RegistryForm();
            this.Close();
            registryForm.ShowDialog();
        }
    }
}
