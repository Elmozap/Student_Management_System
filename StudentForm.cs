using System;
using System.Linq;
using System.Windows.Forms;

namespace StudentManagementSystem
{
    public partial class StudentForm : Form
    {
        private Label[] ValueLabels;
        private string[] labels = { "Name", "Age", "Address", "Contact Number", "Email Address",
                                    "Course and Year", "Guardian Name", "Guardian Contact", "Hobbies", "Nickname" };

        public StudentForm(string[] studentData = null)
        {
            this.Text = "Student Page";
            this.Size = new System.Drawing.Size(500, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.BackColor = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");

            int yOffset = 20;

            Label WelcomeLbl = new Label();
            WelcomeLbl.Text = "Welcome!";
            WelcomeLbl.Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold);
            WelcomeLbl.AutoSize = true;
            WelcomeLbl.Location = new System.Drawing.Point((this.ClientSize.Width - WelcomeLbl.Width) / 2, yOffset);
            WelcomeLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            Controls.Add(WelcomeLbl);

            yOffset += 50;

            ValueLabels = new Label[labels.Length];

            for (int i = 0; i < labels.Length; i++)
            {
                Label FieldLbl = new Label();
                FieldLbl.Text = labels[i] + ":";
                FieldLbl.Location = new System.Drawing.Point(30, yOffset);
                FieldLbl.AutoSize = true;
                Controls.Add(FieldLbl);

                ValueLabels[i] = new Label();
                ValueLabels[i].Text = studentData != null && studentData.Length > i ? studentData[i] : " ";
                ValueLabels[i].Location = new System.Drawing.Point(180, yOffset);
                ValueLabels[i].AutoSize = true;
                ValueLabels[i].Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);
                Controls.Add(ValueLabels[i]);

                yOffset += 30;
            }

            Button EditBtn = new Button();
            EditBtn.Text = "Edit/Update";
            EditBtn.Location = new System.Drawing.Point(180, yOffset);
            EditBtn.Size = new System.Drawing.Size(120, 30);
            EditBtn.Click += EditBtn_Click;
            Controls.Add(EditBtn);

            Button LogoutBtn = new Button();
            LogoutBtn.Text = "Logout";
            LogoutBtn.Location = new System.Drawing.Point(50, yOffset);
            LogoutBtn.Size = new System.Drawing.Size(100, 30);
            LogoutBtn.Click += LogoutBtn_Click;
            Controls.Add(LogoutBtn);
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            string[] currentData = ValueLabels.Select(label => label.Text ?? "").ToArray();
            EditForm editForm = new EditForm(currentData, this);
            editForm.Show();
            this.Hide();
        }

        public void UpdateStudentData(string[] updatedData)
        {
            for (int i = 0; i < ValueLabels.Length; i++)
            {
                ValueLabels[i].Text = updatedData[i];
            }
        }

        private void LogoutBtn_Click(object sender, EventArgs e)
        {
            DialogResult confirmLogout = MessageBox.Show("Are you sure you want to log out?", "Logout Confirmation",
                                                         MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmLogout == DialogResult.Yes)
            {
                LoginForm loginForm = new LoginForm();
                loginForm.Show();
                this.Close();
            }
        }
    }
}
