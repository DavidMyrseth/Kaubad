using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Andmebass_TARpv23
{
    public partial class Form2 : Form
    {
        public event Action OnLaduAdded;  // Üritus vormi1 teavitamiseks
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\opilane\Source\Repos\Andmebass_TARpv23\Andmebaas1.mdf;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter adapter;
        DataTable laotable;
        private int ID;
        public Form2()
        {
            InitializeComponent();
            NaitaLaod();  
        }

        private void NaitaLaod()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("SELECT * FROM Ladu", conn);
                adapter = new SqlDataAdapter(cmd);
                laotable = new DataTable();
                adapter.Fill(laotable);

                dataGridView1.DataSource = laotable;

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
        }

        private void Lisa_btn_Click(object sender, EventArgs e)
        {
            if (LaoNimetus_txt.Text.Trim() != string.Empty && Suurus_txt.Text.Trim() != string.Empty && Kirjeldus_txt.Text.Trim() != string.Empty)
            {
                try
                {
                    conn.Open();

                    cmd = new SqlCommand("INSERT INTO Ladu (LaoNimetus, Suurus, Kirjeldus) VALUES (@nimetus, @suurus, @kirjeldus)", conn);
                    cmd.Parameters.AddWithValue("@nimetus", LaoNimetus_txt.Text);
                    cmd.Parameters.AddWithValue("@suurus", Suurus_txt.Text);
                    cmd.Parameters.AddWithValue("@kirjeldus", Kirjeldus_txt.Text);

                    cmd.ExecuteNonQuery();

                    conn.Close();
                    // Trigger sündmus teavitada vormi1 värskendada ComboBox
                    OnLaduAdded?.Invoke();
                    Emaldamine();
                    NaitaLaod();
                }
                catch (Exception)
                {
                    MessageBox.Show("Andmebaasiga viga");
                }
            }
            else
            {
                MessageBox.Show("Sisesta andmeid");
            }
        }

        private void Kustuta_btn_Click(object sender, EventArgs e)
        {
            try
            {
                ID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value);
                if (ID != 0)
                {
                    conn.Open();
                    cmd = new SqlCommand("DELETE FROM Ladu WHERE Id=@id", conn);
                    cmd.Parameters.AddWithValue("@id", ID);
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    Emaldamine();
                    NaitaLaod();

                    MessageBox.Show("Запись успешно удалена", "Удаление");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении записи: {ex.Message}");
            }
        }

        private void Uuenda_btn_Click(object sender, EventArgs e)
        {
            if (LaoNimetus_txt.Text.Trim() != string.Empty && Suurus_txt.Text.Trim() != string.Empty && Kirjeldus_txt.Text.Trim() != string.Empty)
            {
                try
                {
                    conn.Open();
                    cmd = new SqlCommand("UPDATE Ladu SET LaoNimetus=@nimetus, Suurus=@suurus, Kirjeldus=@kirjeldus WHERE Id=@id", conn);
                    cmd.Parameters.AddWithValue("@id", ID);
                    cmd.Parameters.AddWithValue("@nimetus", LaoNimetus_txt.Text);
                    cmd.Parameters.AddWithValue("@suurus", Suurus_txt.Text);
                    cmd.Parameters.AddWithValue("@kirjeldus", Kirjeldus_txt.Text);
                    cmd.ExecuteNonQuery();

                    conn.Close();
                    Emaldamine();
                    NaitaLaod();
                    
                    MessageBox.Show("Andmed edukalt uuendatud", "Uuendamine");
                }
                catch (Exception)
                {
                    MessageBox.Show("Andmebaasiga viga");
                }
            }
            else
            {
                MessageBox.Show("Sisesta andmeid");
            }
        }


        private void Emaldamine()
        {
            LaoNimetus_txt.Text = "";
            Suurus_txt.Text = "";
            Kirjeldus_txt.Text = "";
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0) 
            {
                int ID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Id"].Value); 
                LaoNimetus_txt.Text = dataGridView1.Rows[e.RowIndex].Cells["LaoNimetus"].Value.ToString();
                Suurus_txt.Text = dataGridView1.Rows[e.RowIndex].Cells["Suurus"].Value.ToString();
                Kirjeldus_txt.Text = dataGridView1.Rows[e.RowIndex].Cells["Kirjeldus"].Value.ToString();
            }
        }
    }
}
