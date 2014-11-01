using PasswordKeeper.Passwords;
using PasswordKeeper.Type;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PasswordKeeper
{
    public partial class PasswordKeeper : Form
    {
        private int id;

        public PasswordKeeper(int id)
        {
            this.id = id;
            InitializeComponent();
        }

        private void listView1_Resize(object sender, EventArgs e)
        {
            foreach (ColumnHeader column in lvPasswords.Columns)
            {
                column.Width = -2;
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PasswordKeeper_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddItemForm addItemForm = new AddItemForm(id);
            addItemForm.ShowDialog();
        }

        private void addEnteryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.btnAdd_Click(sender, e);
        }

        private void PasswordKeeper_Load(object sender, EventArgs e)
        {
            UnitTypeDAO types = new UnitTypeDAOImpl();
            UnitType allTypes = types.GetAllTypes();

            TreeNode ParentNode = new TreeNode();
            ParentNode.Text = "All";
            ParentNode.ForeColor = Color.Black;
            ParentNode.BackColor = Color.White;
            ParentNode.ImageIndex = 0;
            ParentNode.SelectedImageIndex = 0;
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
                ParentNode.Nodes.Add(ChildNode);
                i++;
            }

            this.tvTypes.Nodes[0].Expand();
        }

        private void tvTypes_Click(object sender, EventArgs e)
        {
            this.lvPasswords.Items.Clear();

            ItemDAO itemDAO = new ItemDAOImpl();
            List<Item> list = itemDAO.SelectItemsForType(id, 3);

            ListViewItem lvi;
            foreach (Item item in list)
            {
                lvi = new ListViewItem(item.Title);
                lvi.SubItems.Add(item.Username);
                lvi.SubItems.Add(item.Password);

                switch(item.Type){
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
                lvi.SubItems.Add(item.Expires.ToString());
                this.lvPasswords.Items.Add(lvi);
            }
        }
    }
}
