using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace DataBasewithDatagriew
{
    public partial class Form1 : Form
    {
        private SqlConnection sqlConnection = null;
        private SqlCommandBuilder  sqlBuilder = null;
        private SqlDataAdapter sqlDataAdapter = null;
        private DataSet dataSet = null;
        private bool newRowAdding = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void LoadData() 
        {

            try
            {
                sqlDataAdapter = new SqlDataAdapter("SELECT *, 'Delete' AS [Delete] FROM Users", sqlConnection);
                sqlBuilder = new SqlCommandBuilder(sqlDataAdapter);
                sqlBuilder.GetInsertCommand();
                sqlBuilder.GetUpdateCommand();
                sqlBuilder.GetDeleteCommand();

                dataSet = new DataSet();

                sqlDataAdapter.Fill(dataSet, "Users");
                dataGridView1.DataSource = dataSet.Tables["Users"];
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                    dataGridView1[15, i] = linkCell;
                }
            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void ReloadData()
        {

            try
            {
                dataSet.Tables.Clear();

                sqlDataAdapter.Fill(dataSet, "Users");
                dataGridView1.DataSource = dataSet.Tables["Users"];
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();

                    dataGridView1[15, i] = linkCell;
                }
            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""D:\project\c# project\DataBasewithDatagriew\DataBasewithDatagriew\Database.mdf"";Integrated Security=True");
            sqlConnection.Open();
            LoadData();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            ReloadData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)//******
        {
            try
            {
                if (e.ColumnIndex == 15)
                {
                    string task = dataGridView1.Rows[e.RowIndex].Cells[15].Value.ToString();
                    if (task == "Delete")
                    {
                        if (MessageBox.Show("Delete that rows?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int rowIndex = e.RowIndex;
                            dataGridView1.Rows.RemoveAt(rowIndex);
                            dataSet.Tables["Users"].Rows[rowIndex].Delete();
                            sqlDataAdapter.Update(dataSet, "Users");

                        }
                    }
                    else if (task == "Insert")
                    {
                        int rowIndex = dataGridView1.Rows.Count - 2;
                        DataRow row = dataSet.Tables["Users"].NewRow();

                        row["Date"] = dataGridView1.Rows[rowIndex].Cells["Date"].Value;
                        row["Partner"] = dataGridView1.Rows[rowIndex].Cells["Partner"].Value;
                        row["time"] = dataGridView1.Rows[rowIndex].Cells["time"].Value;
                        row["type"] = dataGridView1.Rows[rowIndex].Cells["type"].Value;
                        row["comments"] = dataGridView1.Rows[rowIndex].Cells["comments"].Value;
                        row["adult"] = dataGridView1.Rows[rowIndex].Cells["adult"].Value;
                        row["teenager"] = dataGridView1.Rows[rowIndex].Cells["teenager"].Value;
                        row["Child"] = dataGridView1.Rows[rowIndex].Cells["Child"].Value;
                        row["studnet"] = dataGridView1.Rows[rowIndex].Cells["studnet"].Value;
                        row["Pensioner"] = dataGridView1.Rows[rowIndex].Cells["Pensioner"].Value;
                        row["Name Surname"] = dataGridView1.Rows[rowIndex].Cells["Name Surname"].Value;
                        row["phone"] = dataGridView1.Rows[rowIndex].Cells["phone"].Value;
                        row["sum"] = dataGridView1.Rows[rowIndex].Cells["sum"].Value;
                        row["total number"] = dataGridView1.Rows[rowIndex].Cells["total number"].Value;

                        dataSet.Tables["Users"].Rows.Add(row);
                        dataSet.Tables["Users"].Rows.RemoveAt(dataSet.Tables["Users"].Rows.Count - 1);
                        dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count - 2);
                        dataGridView1.Rows[e.RowIndex].Cells[15].Value = "Delete";

                        sqlDataAdapter.Update(dataSet, "Users");
                        newRowAdding = false;

                    }
                    ReloadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            try 
            {
                if (newRowAdding == false)
                {
                    newRowAdding = true;

                    int lastRow = dataGridView1.Rows.Count - 15;
                    DataGridViewRow row = dataGridView1.Rows[lastRow];
                    DataGridViewLinkCell linkCell = new DataGridViewLinkCell();
                    row.Cells["Delete"].Value = "Insert";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
