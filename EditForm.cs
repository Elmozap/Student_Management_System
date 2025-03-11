using System;
using System.Linq;
using System.Windows.Forms;

namespace StudentManagementSystem
{
    public partial class EditForm : Form
    {
        private TextBox[] InputFields;
        private string[] labels = { "Name", "Age", "Address", "Contact Number", "Email Address",
                                    "Course", "Year", "Guardian/Parent", "Guardian Contact", "Hobbies", "Nickname" };
        private StudentForm parentForm;
        private ComboBox CourseCmb;
        private ComboBox YearCmb;

        public EditForm(string[] studentData, StudentForm parent)
        {
            parentForm = parent;

            if (studentData == null || studentData.Length < labels.Length)
            {
                studentData = new string[labels.Length];
            }

            this.Text = "Edit Student Details";
            this.Size = new System.Drawing.Size(600, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.BackColor = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");

            Panel scrollPanel = new Panel();
            scrollPanel.AutoScroll = true;
            scrollPanel.Dock = DockStyle.Fill;
            scrollPanel.AutoSize = true;
            this.Controls.Add(scrollPanel);

            int leftColumnX = 30;
            int rightColumnX = 300;
            int yOffset = 20;
            InputFields = new TextBox[labels.Length];

            for (int i = 0; i < labels.Length; i++)
            {
                int currentX = i % 2 == 0 ? leftColumnX : rightColumnX;

                Label InfoLbl = new Label
                {
                    Text = labels[i] + ":",
                    Location = new System.Drawing.Point(currentX, yOffset),
                    AutoSize = true
                };
                scrollPanel.Controls.Add(InfoLbl);

                if (labels[i] == "Course")
                {
                    CourseCmb = new ComboBox
                    {
                        Name = "CourseCmb",
                        Location = new System.Drawing.Point(currentX + 140, yOffset - 3),
                        Size = new System.Drawing.Size(130, 25)
                    };
                    CourseCmb.Items.AddRange(new string[] { "ABEL", "BSBA", "BSIT", "BPA" });
                    CourseCmb.SelectedItem = studentData[i] != "" ? studentData[i] : "BSIT";
                    scrollPanel.Controls.Add(CourseCmb);
                }
                else if (labels[i] == "Year")
                {
                    YearCmb = new ComboBox
                    {
                        Name = "YearCmb",
                        Location = new System.Drawing.Point(currentX + 140, yOffset - 3),
                        Size = new System.Drawing.Size(130, 25)
                    };
                    YearCmb.Items.AddRange(new string[] { "First", "Second", "Third", "Fourth" });
                    YearCmb.SelectedItem = studentData[i] != "" ? studentData[i] : "First";
                    scrollPanel.Controls.Add(YearCmb);
                }
                else
                {
                    InputFields[i] = new TextBox
                    {
                        Name = labels[i].Replace(" ", "") + "Txt",
                        Location = new System.Drawing.Point(currentX + 140, yOffset - 3),
                        Size = new System.Drawing.Size(130, 25),
                        Text = studentData[i] ?? ""
                    };

                    if (labels[i] == "Age" || labels[i].Contains("Contact"))
                    {
                        InputFields[i].KeyPress += (sender, e) =>
                        {
                            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                            {
                                e.Handled = true;
                                MessageBox.Show("Only numbers are allowed!", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        };
                    }
                    scrollPanel.Controls.Add(InputFields[i]);
                }

                if (i % 2 == 1)
                {
                    yOffset += 35;
                }
            }

            yOffset += 20;

            Button SaveBtn = new Button
            {
                Text = "Save Changes",
                Location = new System.Drawing.Point(200, yOffset),
                Size = new System.Drawing.Size(120, 30)
            };
            SaveBtn.Click += SaveBtn_Click;
            scrollPanel.Controls.Add(SaveBtn);

            Button BackBtn = new Button
            {
                Text = "Back",
                Location = new System.Drawing.Point(50, yOffset),
                Size = new System.Drawing.Size(100, 30)
            };
            BackBtn.Click += BackBtn_Click;
            scrollPanel.Controls.Add(BackBtn);
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            string[] updatedData = new string[labels.Length];

            for (int i = 0; i < labels.Length; i++)
            {
                if (labels[i] == "Course")
                {
                    updatedData[i] = CourseCmb?.SelectedItem?.ToString() ?? "BSIT";
                }
                else if (labels[i] == "Year")
                {
                    updatedData[i] = YearCmb?.SelectedItem?.ToString() ?? "First";
                }
                else
                {
                    updatedData[i] = InputFields[i]?.Text.Trim() ?? "";
                }
            }

            string[] requiredFields = { "Name", "Age", "Address", "Contact Number", "Email Address", "Course", "Year", "Guardian/Parent", "Guardian Contact" };
            foreach (string field in requiredFields)
            {
                int index = Array.IndexOf(labels, field);
                if (index >= 0 && string.IsNullOrWhiteSpace(updatedData[index]))
                {
                    MessageBox.Show($"The field '{field}' is required.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            parentForm.UpdateStudentData(updatedData);
            MessageBox.Show("Profile successfully updated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
            parentForm.Show();
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            this.Close();
            parentForm.Show();
        }
    }
}
