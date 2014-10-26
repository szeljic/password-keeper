using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PasswordKeeper.User;

namespace PasswordKeeper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (tbUsername.Text == "" || tbPassword.Text == "")
            {
                lblInfo.Text = "Please enter username and password";
                tbPassword.Text = "";
                return;
            }

            UserDAO user = new UserDAOImpl();
            User.User newUser = user.getUser(this.tbUsername.Text, this.tbPassword.Text);

            if (newUser != null)
            {
                PasswordKeeper passwordKeeper = new PasswordKeeper();
                passwordKeeper.Show();
                this.Hide();
            }
            else
            {
                lblInfo.Text = "Wrong username or password!";
                this.tbPassword.Text = "";
                return;
            }
        }

        private void tbUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.btnLogin_Click(sender, e);
            }
        }

        private void tbPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.btnLogin_Click(sender, e);
            }
        }

        private void btnRegistry_Click(object sender, EventArgs e)
        {
            RegistryForm registryForm = new RegistryForm();
            registryForm.ShowDialog();
        }

    }
}
