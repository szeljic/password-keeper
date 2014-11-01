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

            ImageList myImageList = new ImageList();
            myImageList.Images.Add(Image.FromFile("..\\..\\resources\\icons\\1414350971_11142.ico"));
            myImageList.Images.Add(Image.FromFile("..\\..\\resources\\icons\\1414350974_11092.ico"));
            myImageList.Images.Add(Image.FromFile("..\\..\\resources\\icons\\1414350959_11079.ico"));
            myImageList.Images.Add(Image.FromFile("..\\..\\resources\\icons\\1414351050_59253.ico"));
            myImageList.Images.Add(Image.FromFile("..\\..\\resources\\icons\\dice-icon.png"));
            myImageList.Images.Add(Image.FromFile("..\\..\\resources\\icons\\Apps-preferences-system-login-icon.png"));

            this.tvTypes.ImageList = myImageList;

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
            ItemDAO itemDAO = new ItemDAOImpl();
            List<Item> list = itemDAO.SelectItemsForType(id, 3);

            ListViewItem lvi;
            foreach (Item item in list)
            {
                lvi = new ListViewItem(item.Title);
                lvi.SubItems.Add(item.Username);
                lvi.SubItems.Add(item.Password);
                lvi.SubItems.Add(item.Type.ToString());
                lvi.SubItems.Add(item.Url);
                lvi.SubItems.Add(item.Expires.ToString());
                this.lvPasswords.Items.Add(lvi);
            }
        }
    }
}
