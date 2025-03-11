using System;
using System.Linq;
using System.Windows.Forms;

namespace StudentManagementSystem
{
    public partial class LoginForm : Form
    {
        private string MockUsername = "admin"; 
        private string MockPassword = "password123"; 
        private int loginAttempts = 0;

        private Label AttemptsLbl;

        public LoginForm()
        {
            this.Text = "Login Page";
            this.Size = new System.Drawing.Size(400, 320);
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
            LoginBtn.Size = new System.Drawing.Size(100, 30);
            LoginBtn.BackColor = System.Drawing.Color.White;
            LoginBtn.ForeColor = System.Drawing.Color.Black;
            LoginBtn.Location = new System.Drawing.Point((this.ClientSize.Width - LoginBtn.Width) / 2, 110); // Centered
            LoginBtn.Click += (sender, e) => loginBtn_Click(UsernameTxt.Text, PasswordTxt.Text);
            Controls.Add(LoginBtn);

            Button ChangePassBtn = new Button();
            ChangePassBtn.Text = "Change Password";
            ChangePassBtn.Name = "ChangePassBtn";
            ChangePassBtn.Location = new System.Drawing.Point(30, 150);
            ChangePassBtn.Size = new System.Drawing.Size(135, 30);
            ChangePassBtn.Click += ChangePassBtn_Click;
            Controls.Add(ChangePassBtn);

            Button ForgotPassBtn = new Button();
            ForgotPassBtn.Text = "Forgot Password?";
            ForgotPassBtn.Name = "ForgotPassBtn";
            ForgotPassBtn.Location = new System.Drawing.Point(180, 150);
            ForgotPassBtn.Size = new System.Drawing.Size(140, 30);
            ForgotPassBtn.Click += ForgotPassBtn_Click;
            Controls.Add(ForgotPassBtn);

            Label ErrorLbl = new Label();
            ErrorLbl.Name = "ErrorLbl";
            ErrorLbl.ForeColor = System.Drawing.Color.Red;
            ErrorLbl.Location = new System.Drawing.Point(30, 190);
            ErrorLbl.Size = new System.Drawing.Size(300, 20);
            ErrorLbl.Visible = false;
            Controls.Add(ErrorLbl);

            AttemptsLbl = new Label();
            AttemptsLbl.Text = $"Login Attempts: {loginAttempts}/5";
            AttemptsLbl.Location = new System.Drawing.Point(30, 220);
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

        private void ForgotPassBtn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("A password reset link has been sent to your registered email.", "Forgot Password", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ChangePassBtn_Click(object sender, EventArgs e)
        {
            using (Form changePassForm = new Form())
            {
                changePassForm.Text = "Change Password";
                changePassForm.Size = new System.Drawing.Size(350, 280);
                changePassForm.StartPosition = FormStartPosition.CenterScreen;
                changePassForm.BackColor = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");
                changePassForm.FormBorderStyle = FormBorderStyle.FixedDialog;

                Label userLbl = new Label();
                userLbl.Text = "Confirm Username:";
                userLbl.Location = new System.Drawing.Point(20, 20);
                userLbl.AutoSize = true;
                changePassForm.Controls.Add(userLbl);

                TextBox userTxt = new TextBox();
                userTxt.Location = new System.Drawing.Point(140, 15);
                userTxt.Size = new System.Drawing.Size(150, 25);
                changePassForm.Controls.Add(userTxt);

                Label oldPassLbl = new Label();
                oldPassLbl.Text = "Old Password:";
                oldPassLbl.Location = new System.Drawing.Point(20, 60);
                oldPassLbl.AutoSize = true;
                changePassForm.Controls.Add(oldPassLbl);

                TextBox oldPassTxt = new TextBox();
                oldPassTxt.Location = new System.Drawing.Point(140, 55);
                oldPassTxt.Size = new System.Drawing.Size(150, 25);
                oldPassTxt.PasswordChar = '*';
                changePassForm.Controls.Add(oldPassTxt);

                Label confirmOldPassLbl = new Label();
                confirmOldPassLbl.Text = "Confirm Old Password:";
                confirmOldPassLbl.Location = new System.Drawing.Point(20, 100);
                confirmOldPassLbl.AutoSize = true;
                changePassForm.Controls.Add(confirmOldPassLbl);

                TextBox confirmOldPassTxt = new TextBox();
                confirmOldPassTxt.Location = new System.Drawing.Point(140, 95);
                confirmOldPassTxt.Size = new System.Drawing.Size(150, 25);
                confirmOldPassTxt.PasswordChar = '*';
                changePassForm.Controls.Add(confirmOldPassTxt);

                Label newPassLbl = new Label();
                newPassLbl.Text = "New Password:";
                newPassLbl.Location = new System.Drawing.Point(20, 140);
                newPassLbl.AutoSize = true;
                changePassForm.Controls.Add(newPassLbl);

                TextBox newPassTxt = new TextBox();
                newPassTxt.Location = new System.Drawing.Point(140, 135);
                newPassTxt.Size = new System.Drawing.Size(150, 25);
                newPassTxt.PasswordChar = '*';
                changePassForm.Controls.Add(newPassTxt);

                Button SaveBtn = new Button();
                SaveBtn.Text = "Save";
                SaveBtn.Location = new System.Drawing.Point(100, 180);
                SaveBtn.Size = new System.Drawing.Size(80, 30);
                SaveBtn.Click += (s, eArgs) =>
                {
                    if (userTxt.Text != MockUsername)
                    {
                        MessageBox.Show("Incorrect username!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (oldPassTxt.Text != MockPassword || confirmOldPassTxt.Text != MockPassword)
                    {
                        MessageBox.Show("Old passwords do not match!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (string.IsNullOrWhiteSpace(newPassTxt.Text))
                    {
                        MessageBox.Show("New password cannot be empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MockPassword = newPassTxt.Text;
                        MessageBox.Show("Password changed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        changePassForm.Close();
                    }
                };
                changePassForm.Controls.Add(SaveBtn);

                changePassForm.ShowDialog();
            }
        }
    }
}
