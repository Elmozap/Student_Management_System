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
            this.Size = new System.Drawing.Size(500, 550);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.BackColor = System.Drawing.ColorTranslator.FromHtml("#D3D3D3");

            int yOffset = 20;
            InputFields = new TextBox[labels.Length];

            for (int i = 0; i < labels.Length; i++)
            {
                Label InfoLbl = new Label();
                InfoLbl.Text = labels[i] + ":";
                InfoLbl.Location = new System.Drawing.Point(30, yOffset);
                InfoLbl.AutoSize = true;
                Controls.Add(InfoLbl);

                if (labels[i] == "Course")
                {
                    CourseCmb = new ComboBox();
                    CourseCmb.Name = "CourseCmb";
                    CourseCmb.Location = new System.Drawing.Point(180, yOffset - 3);
                    CourseCmb.Size = new System.Drawing.Size(200, 25);
                    CourseCmb.Items.AddRange(new string[] { "ABEL", "BSBA", "BSIT", "BPA" });
                    CourseCmb.SelectedItem = studentData[i];
                    Controls.Add(CourseCmb);
                }
                else if (labels[i] == "Year")
                {
                    YearCmb = new ComboBox();
                    YearCmb.Name = "YearCmb";
                    YearCmb.Location = new System.Drawing.Point(180, yOffset - 3);
                    YearCmb.Size = new System.Drawing.Size(200, 25);
                    YearCmb.Items.AddRange(new string[] { "First", "Second", "Third", "Fourth" });
                    YearCmb.SelectedItem = studentData[i];
                    Controls.Add(YearCmb);
                }
                else
                {
                    InputFields[i] = new TextBox();
                    InputFields[i].Name = labels[i].Replace(" ", "") + "Txt";
                    InputFields[i].Location = new System.Drawing.Point(180, yOffset - 3);
                    InputFields[i].Size = new System.Drawing.Size(200, 25);
                    InputFields[i].Text = studentData[i] ?? "";

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

                    Controls.Add(InputFields[i]);
                }
                yOffset += 35;
            }

            Button SaveBtn = new Button();
            SaveBtn.Text = "Save Changes";
            SaveBtn.Location = new System.Drawing.Point(180, yOffset);
            SaveBtn.Size = new System.Drawing.Size(120, 30);
            SaveBtn.Click += SaveBtn_Click;
            Controls.Add(SaveBtn);

            Button BackBtn = new Button();
            BackBtn.Text = "Back";
            BackBtn.Location = new System.Drawing.Point(50, yOffset);
            BackBtn.Size = new System.Drawing.Size(100, 30);
            BackBtn.Click += BackBtn_Click;
            Controls.Add(BackBtn);
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            string[] updatedData = new string[labels.Length];

            for (int i = 0; i < labels.Length; i++)
            {
                if (labels[i] == "Course")
                {
                    updatedData[i] = CourseCmb?.SelectedItem?.ToString() ?? "";
                }
                else if (labels[i] == "Year")
                {
                    updatedData[i] = YearCmb?.SelectedItem?.ToString() ?? "";
                }
                else
                {
                    updatedData[i] = InputFields[i]?.Text.Trim() ?? "";
                }
            }

            string[] requiredFields = { "Name", "Age", "Address", "Contact Number", "Email Address", "Course", "Year", "Guardian/Parent", "Guardian Contact" };
            for (int i = 0; i < labels.Length; i++)
            {
                if (requiredFields.Contains(labels[i]) && string.IsNullOrWhiteSpace(updatedData[i]))
                {
                    MessageBox.Show($"The field '{labels[i]}' is required.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
