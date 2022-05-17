using System.Data;

namespace NoteTaking
{
    public partial class Form1 : Form
    {

        DataTable table;

        public Form1()
        {
            InitializeComponent();
        }

        private void clearText()
        {
            textTitle.Clear();
            textMessage.Clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            table = new DataTable();
            table.Columns.Add("Title", typeof(string));
            table.Columns.Add("Messages", typeof(string));

            dataGridView1.DataSource = table;

            dataGridView1.Columns["Messages"].Visible = false;
            dataGridView1.Columns["Title"].Width = dataGridView1.Width - 3;
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            clearText();
            dataGridView1.CurrentCell = null;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textTitle.Text) && string.IsNullOrEmpty(textMessage.Text))
            {
                MessageBox.Show("Please insert your text before you can save your note.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 
            else
            {
                table.Rows.Add(textTitle.Text, textMessage.Text);
                clearText();
            }
            dataGridView1.CurrentCell = null;
        }

        private void buttonRead_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell == null)
            {
                MessageBox.Show("Please select a note to read!!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int index; index = dataGridView1.CurrentCell.RowIndex;

                if (index > -1)
                {
                    textTitle.Text = table.Rows[index].ItemArray[0].ToString();
                    textMessage.Text = table.Rows[index].ItemArray[1].ToString();
                }
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell == null)
            {
                MessageBox.Show("Please select a note to delete!!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int index = dataGridView1.CurrentCell.RowIndex;

                string title = table.Rows[index].ItemArray[0].ToString();
                string message = table.Rows[index].ItemArray[1].ToString();

                if (MessageBox.Show("Do you really want to delete this message?", "Deleting message...", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    table.Rows[index].Delete();

                    if (textTitle.Text == title && textMessage.Text == message)
                    {
                        clearText();
                    }
                }
            }
        }
    }
}