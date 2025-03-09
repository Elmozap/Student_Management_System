using System;
using System.Linq;
using System.Windows.Forms;

namespace StudentManagementSystem
{
    public partial class LoginForm : Form
    {
        private const string MockUsername = "admin";
        private const string MockPassword = "password123";
        private int loginAttempts = 0;

        private Label AttemptsLbl;

        public LoginForm()
        {
            this.Text = "Login Page";
            this.Size = new System.Drawing.Size(400, 280);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.BackColor = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");

            Label UsernameLbl = new Label();
            UsernameLbl.Text = "Username:";
            UsernameLbl.Location = new System.Drawing.Point(30, 30);
            UsernameLbl.AutoSize = true;
            Controls.Add(UsernameLbl);

            TextBox UsernameTxt = new TextBox();
            UsernameTxt.Name = "UsernameTxt";
            UsernameTxt.Location = new System.Drawing.Point(120, 25);
            UsernameTxt.Size = new System.Drawing.Size(200, 25);
            Controls.Add(UsernameTxt);

            Label PasswordLbl = new Label();
            PasswordLbl.Text = "Password:";
            PasswordLbl.Location = new System.Drawing.Point(30, 70);
            PasswordLbl.AutoSize = true;
            Controls.Add(PasswordLbl);

            TextBox PasswordTxt = new TextBox();
            PasswordTxt.Name = "PasswordTxt";
            PasswordTxt.Location = new System.Drawing.Point(120, 65);
            PasswordTxt.Size = new System.Drawing.Size(200, 25);
            PasswordTxt.PasswordChar = '*';
            Controls.Add(PasswordTxt);

            Button LoginBtn = new Button();
            LoginBtn.Text = "Login";
            LoginBtn.Name = "LoginBtn";
            LoginBtn.Location = new System.Drawing.Point(120, 110);
            LoginBtn.Size = new System.Drawing.Size(80, 30);
            LoginBtn.BackColor = System.Drawing.Color.White;
            LoginBtn.ForeColor = System.Drawing.Color.Black;
            LoginBtn.Click += (sender, e) => loginBtn_Click(UsernameTxt.Text, PasswordTxt.Text);
            Controls.Add(LoginBtn);

            Label ErrorLbl = new Label();
            ErrorLbl.Name = "ErrorLbl";
            ErrorLbl.ForeColor = System.Drawing.Color.Red;
            ErrorLbl.Location = new System.Drawing.Point(30, 150);
            ErrorLbl.Size = new System.Drawing.Size(300, 20);
            ErrorLbl.Visible = false;
            Controls.Add(ErrorLbl);

            AttemptsLbl = new Label();
            AttemptsLbl.Text = $"Login Attempts: {loginAttempts}/5";
            AttemptsLbl.Location = new System.Drawing.Point(30, 180);
            AttemptsLbl.AutoSize = true;
            Controls.Add(AttemptsLbl);
        }

        private void loginBtn_Click(string username, string password)
        {
            Label? ErrorLbl = Controls.Find("ErrorLbl", true).FirstOrDefault() as Label;
            if (ErrorLbl == null) return;

            if (username == MockUsername && password == MockPassword)
            {
                MessageBox.Show("Login Successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                StudentForm studentForm = new StudentForm();
                studentForm.Show();
                this.Hide();
            }
            else
            {
                loginAttempts++;
                AttemptsLbl.Text = $"Login Attempts: {loginAttempts}/5";

                if (username != MockUsername && password != MockPassword)
                {
                    ErrorLbl.Text = "Both username and password are incorrect.";
                }
                else if (username != MockUsername)
                {
                    ErrorLbl.Text = "Username is incorrect.";
                }
                else
                {
                    ErrorLbl.Text = "Password is incorrect.";
                }

                ErrorLbl.Visible = true;

                if (loginAttempts >= 5)
                {
                    MessageBox.Show("Too many failed attempts! Click OK to reset password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    System.Diagnostics.Process.Start("https://password-reset-link.com");
                }
            }
        }
    }
}
