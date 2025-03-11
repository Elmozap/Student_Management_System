using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace StudentManagementSystem
{
    public partial class LoginForm : Form
    {
        private readonly string MockUsername = "admin";
        private string MockPassword = "password123"; 
        private int loginAttempts = 0;

        private Label AttemptsLbl;
        private TextBox UsernameTxt, PasswordTxt;
        private Label ErrorLbl;

        public LoginForm()
        {
            this.Text = "Login Page";
            this.Size = new System.Drawing.Size(400, 320);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.BackColor = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");

            Label UsernameLbl = new Label
            {
                Text = "Username:",
                Location = new System.Drawing.Point(30, 30),
                AutoSize = true
            };
            Controls.Add(UsernameLbl);

            UsernameTxt = new TextBox
            {
                Name = "UsernameTxt",
                Location = new System.Drawing.Point(120, 25),
                Size = new System.Drawing.Size(200, 25)
            };
            Controls.Add(UsernameTxt);

            Label PasswordLbl = new Label
            {
                Text = "Password:",
                Location = new System.Drawing.Point(30, 70),
                AutoSize = true
            };
            Controls.Add(PasswordLbl);

            PasswordTxt = new TextBox
            {
                Name = "PasswordTxt",
                Location = new System.Drawing.Point(120, 65),
                Size = new System.Drawing.Size(200, 25),
                PasswordChar = '*'
            };
            Controls.Add(PasswordTxt);

            Button LoginBtn = new Button
            {
                Text = "Login",
                Name = "LoginBtn",
                Size = new System.Drawing.Size(100, 30),
                BackColor = System.Drawing.Color.White,
                ForeColor = System.Drawing.Color.Black,
                Location = new System.Drawing.Point((this.ClientSize.Width - 100) / 2, 110)
            };
            LoginBtn.Click += LoginBtn_Click;
            Controls.Add(LoginBtn);

            Button ChangePassBtn = new Button
            {
                Text = "Change Password",
                Name = "ChangePassBtn",
                Location = new System.Drawing.Point(30, 150),
                Size = new System.Drawing.Size(135, 30)
            };
            ChangePassBtn.Click += ChangePassBtn_Click;
            Controls.Add(ChangePassBtn);

            Button ForgotPassBtn = new Button
            {
                Text = "Forgot Password?",
                Name = "ForgotPassBtn",
                Location = new System.Drawing.Point(180, 150),
                Size = new System.Drawing.Size(140, 30)
            };
            ForgotPassBtn.Click += ForgotPassBtn_Click;
            Controls.Add(ForgotPassBtn);

            ErrorLbl = new Label
            {
                Name = "ErrorLbl",
                ForeColor = System.Drawing.Color.Red,
                Location = new System.Drawing.Point(30, 190),
                Size = new System.Drawing.Size(300, 20),
                Visible = false
            };
            Controls.Add(ErrorLbl);

            AttemptsLbl = new Label
            {
                Text = $"Login Attempts: {loginAttempts}/5",
                Location = new System.Drawing.Point(30, 220),
                AutoSize = true
            };
            Controls.Add(AttemptsLbl);
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            if (UsernameTxt == null || PasswordTxt == null || ErrorLbl == null) return;

            string username = UsernameTxt.Text.Trim();
            string password = PasswordTxt.Text.Trim();

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
                    try
                    {
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = "https://password-reset-link.com",
                            UseShellExecute = true
                        });
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Unable to open reset link.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
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

                Label userLbl = new Label { Text = "Confirm Username:", Location = new System.Drawing.Point(20, 20), AutoSize = true };
                TextBox userTxt = new TextBox { Location = new System.Drawing.Point(140, 15), Size = new System.Drawing.Size(150, 25) };

                Label oldPassLbl = new Label { Text = "Old Password:", Location = new System.Drawing.Point(20, 60), AutoSize = true };
                TextBox oldPassTxt = new TextBox { Location = new System.Drawing.Point(140, 55), Size = new System.Drawing.Size(150, 25), PasswordChar = '*' };

                Label newPassLbl = new Label { Text = "New Password:", Location = new System.Drawing.Point(20, 100), AutoSize = true };
                TextBox newPassTxt = new TextBox { Location = new System.Drawing.Point(140, 95), Size = new System.Drawing.Size(150, 25), PasswordChar = '*' };

                Button SaveBtn = new Button { Text = "Save", Location = new System.Drawing.Point(100, 140), Size = new System.Drawing.Size(80, 30) };
                SaveBtn.Click += (s, eArgs) =>
                {
                    if (userTxt.Text != MockUsername)
                    {
                        MessageBox.Show("Incorrect username!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (oldPassTxt.Text != MockPassword)
                    {
                        MessageBox.Show("Old password is incorrect!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                changePassForm.Controls.AddRange(new Control[] { userLbl, userTxt, oldPassLbl, oldPassTxt, newPassLbl, newPassTxt, SaveBtn });
                changePassForm.ShowDialog();
            }
        }
    }
}
