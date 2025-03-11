using System;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

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
            this.Size = new System.Drawing.Size(550, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.BackColor = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");

            Panel ScrollPanel = new Panel
            {
                AutoScroll = true,
                Dock = DockStyle.Fill,
                BackColor = System.Drawing.ColorTranslator.FromHtml("#D3D3D3")
            };
            Controls.Add(ScrollPanel);

            int yOffset = 20;

            Label WelcomeLbl = new Label
            {
                Text = "Welcome!",
                Font = new Font("Arial", 14, FontStyle.Bold),
                AutoSize = true,
                Location = new Point((ScrollPanel.ClientSize.Width - 100) / 2, yOffset),
                TextAlign = ContentAlignment.MiddleCenter
            };
            ScrollPanel.Controls.Add(WelcomeLbl);
            yOffset += 40;

            PictureBox StudentImage = new PictureBox
            {
                Name = "StudentImage",
                Size = new Size(120, 120),
                Location = new Point((ScrollPanel.ClientSize.Width - 120) / 2, yOffset),
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.White
            };
            ScrollPanel.Controls.Add(StudentImage);

            Label NoImageLbl = new Label
            {
                Text = "No Image",
                AutoSize = true,
                ForeColor = Color.Gray,
                Location = new Point(StudentImage.Left + 30, StudentImage.Top + 50),
                TextAlign = ContentAlignment.MiddleCenter
            };
            ScrollPanel.Controls.Add(NoImageLbl);
            yOffset += 130;

            Button AddImageBtn = new Button
            {
                Text = "Add Image",
                Location = new Point(120, yOffset),
                Size = new Size(100, 30)
            };
            AddImageBtn.Click += (s, e) => MessageBox.Show("Add Image functionality not implemented yet.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ScrollPanel.Controls.Add(AddImageBtn);

            Button ChangeImageBtn = new Button
            {
                Text = "Change Image",
                Location = new Point(240, yOffset),
                Size = new Size(120, 30)
            };
            ChangeImageBtn.Click += (s, e) => MessageBox.Show("Change Image functionality not implemented yet.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ScrollPanel.Controls.Add(ChangeImageBtn);

            yOffset += 40;

            ValueLabels = new Label[labels.Length];
            for (int i = 0; i < labels.Length; i++)
            {
                Label FieldLbl = new Label
                {
                    Text = labels[i] + ":",
                    Location = new Point(30, yOffset),
                    AutoSize = true
                };
                ScrollPanel.Controls.Add(FieldLbl);

                ValueLabels[i] = new Label
                {
                    Text = studentData != null && studentData.Length > i ? studentData[i] : "N/A",
                    Location = new Point(180, yOffset),
                    AutoSize = true,
                    Font = new Font("Arial", 10, FontStyle.Bold)
                };
                ScrollPanel.Controls.Add(ValueLabels[i]);

                yOffset += 30;
            }

            yOffset += 10;

            Button EditBtn = new Button
            {
                Text = "Edit/Update",
                Location = new Point(180, yOffset),
                Size = new Size(120, 30)
            };
            EditBtn.Click += EditBtn_Click;
            ScrollPanel.Controls.Add(EditBtn);

            Button LogoutBtn = new Button
            {
                Text = "Logout",
                Location = new Point(50, yOffset),
                Size = new Size(100, 30)
            };
            LogoutBtn.Click += LogoutBtn_Click;
            ScrollPanel.Controls.Add(LogoutBtn);
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            string[] currentData = ValueLabels.Select(label => label.Text ?? "N/A").ToArray();
            EditForm editForm = new EditForm(currentData, this);
            editForm.Show();
            this.Hide();
        }

        public void UpdateStudentData(string[] updatedData)
        {
            for (int i = 0; i < ValueLabels.Length; i++)
            {
                ValueLabels[i].Text = !string.IsNullOrWhiteSpace(updatedData[i]) ? updatedData[i] : "N/A";
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
