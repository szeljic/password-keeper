using PasswordKeeper.User;
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
    public partial class RegistryForm : Form
    {
        public RegistryForm()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            this.tbUsername.Text = "";
            this.tbPassword.Text = "";
            this.tbRepeatPassword.Text = "";
            this.lblInfo.Text = "";
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (this.tbUsername.Text == "" || this.tbPassword.Text == "" || this.tbRepeatPassword.Text == "")
            {
                this.lblInfo.Text = "Missing information.";
                this.tbPassword.Text = "";
                this.tbRepeatPassword.Text = "";
                return;
            }
            if (this.tbPassword.Text != this.tbRepeatPassword.Text)
            {
                this.lblInfo.Text = @"The passwords you 
entered did not match.";
                this.tbPassword.Text = "";
                this.tbRepeatPassword.Text = "";
                return;
            }
            if (this.tbPassword.Text.Length < 8)
            {
                this.lblInfo.Text = @"The password must have
at least 8 characters.";
                this.tbPassword.Text = "";
                this.tbRepeatPassword.Text = "";
                return;
            }
            if (!this.tbPassword.Text.Any(char.IsDigit))
            {
                this.lblInfo.Text = @"The password must contain
at least one digit.";
                this.tbPassword.Text = "";
                this.tbRepeatPassword.Text = "";
                return;
            }

            UserDAO user = new UserDAOImpl();
            if (user.isExisting(this.tbUsername.Text))
            {
                this.lblInfo.Text = "User already existing.";
                return;
            }
            user.addUser(this.tbUsername.Text, this.tbPassword.Text);
            this.Close();
        }

        private void tbUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.btnRegister_Click(sender, e);
            }
        }

        private void tbPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.btnRegister_Click(sender, e);
            }
        }

        private void tbRepeatPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.btnRegister_Click(sender, e);
            }
        }
    }
}
