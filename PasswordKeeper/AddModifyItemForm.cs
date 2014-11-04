using PasswordKeeper.DB;
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
    public partial class AddModifyItemForm : Form
    {
        private int idUser, idPass;

        public AddModifyItemForm(int idUser, int idPass)
        {
            this.idUser = idUser;
            this.idPass = idPass;
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddItemForm_Load(object sender, EventArgs e)
        {
            UnitTypeDAO unitType = new UnitTypeDAOImpl();
            List<string> allTypes = unitType.GetAllTypes().AllTypes;

            foreach (string type in allTypes)
            {
                this.cbType.Items.Add(type);
            }
            this.cbType.SelectedIndex = 0;

            dtpExpires.Format = DateTimePickerFormat.Custom;
            dtpExpires.CustomFormat = "Da't'e: dd. MM. yyyy. Ti'm'e: HH:mm:ss";
            dtpExpires.MinDate = DateTime.Now;

            this.dtpExpires.Enabled = false;

            if (idPass != 0)
            {
                this.pictureBox1.Image = this.ilBanners.Images[1];
                this.Text = "Modify Entery";
                this.btnAdd.Text = " Modify";
                this.btnAdd.Image = this.iLModify.Images[0];

                ItemDAO itemDAO = new ItemDAOImpl();
                Item item = itemDAO.SelectItem(idPass);
                this.tbTitle.Text = item.Title;
                this.tbUsername.Text = item.Username;
                this.tbPassword.Text = item.Password;
                this.tbRepeatPassword.Text = item.Password;
                this.cbType.SelectedIndex = item.Type - 1;
                this.tbURL.Text = item.Url;
                this.tbDescription.Text = item.Description;
                if (item.Expires.ToString() == DateTime.MaxValue.ToString())
                {
                    this.cbExpires.Checked = false;
                    this.dtpExpires.Enabled = false;
                }
                else
                {
                    this.cbExpires.Checked = true;
                    this.dtpExpires.Value = item.Expires;
                    this.dtpExpires.Enabled = true;
                }
            }     
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.tbTitle.Text == "" || this.tbUsername.Text == "" || this.tbPassword.Text == "")
            {
                this.lblInfo.Text = "Missing information.";
                this.lblInfo.ForeColor = Color.Red;
                this.tbPassword.Text = "";
                this.tbRepeatPassword.Text = "";
                if (this.tbTitle.Text == "")
                {
                    this.lblTitleMissing.ForeColor = Color.FromArgb(0x00FF0000);
                }
                else
                {
                    this.lblTitleMissing.ForeColor = Color.FromArgb(0x00000000);
                }
                if (this.tbUsername.Text == "")
                {
                    this.lblUsernameMissing.ForeColor = Color.FromArgb(0x00FF0000);
                }
                else
                {
                    this.lblUsernameMissing.ForeColor = Color.FromArgb(0x00000000);
                }
                if (this.tbPassword.Text == "")
                {
                    this.lblPasswordMissing.ForeColor = Color.FromArgb(0x00FF0000);
                }
                else
                {
                    this.lblPasswordMissing.ForeColor = Color.FromArgb(0x00000000);
                }
                if (this.tbRepeatPassword.Text == "")
                {
                    this.lblPasswordRepeatMissing.ForeColor = Color.FromArgb(0x00FF0000);
                }
                else
                {
                    this.lblPasswordRepeatMissing.ForeColor = Color.FromArgb(0x00000000);
                }
                return;
            }

            if (this.tbPassword.Text != this.tbRepeatPassword.Text)
            {
                this.lblInfo.Text = "Entered passwords are not matching.";
                this.tbPassword.Text = "";
                this.tbRepeatPassword.Text = "";
                return;
            }

            string title = this.tbTitle.Text;
            string username = this.tbUsername.Text;
            string password = this.tbPassword.Text;
            int type = this.cbType.SelectedIndex + 1;
            string url = this.tbURL.Text;
            string description = this.tbDescription.Text;
            DateTime expires;
            if (this.cbExpires.Checked)
            {
                expires = this.dtpExpires.Value;
            }
            else
            {
                expires = DateTime.MaxValue;
            }

            if (idPass == 0)
            {
                Item item = new Item(idPass, idUser, title, username, password, type, url, description, expires);
                ItemDAO itemDAO = new ItemDAOImpl();
                itemDAO.AddItem(item);
            }
            else
            {
                Item item = new Item(idPass, idUser, title, username, password, type, url, description, expires);
                ItemDAO itemDAO = new ItemDAOImpl();
                itemDAO.UpdateItem(item);
            }
            

            this.Close();
        }

        private void cbExpires_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbExpires.Checked)
            {
                this.dtpExpires.Enabled = true;
            }
            else
            {
                this.dtpExpires.Enabled = false;
            }
        }

        private void cbShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbShowPassword.Checked)
            {
                this.tbPassword.PasswordChar = '\0';
                this.tbRepeatPassword.PasswordChar = '\0';
            }
            else
            {
                this.tbPassword.PasswordChar = '*';
                this.tbRepeatPassword.PasswordChar = '*';
            }
        }
    }
}
