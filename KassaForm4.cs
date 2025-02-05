using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Andmebass_TARpv23
{
    public partial class KassaForm4 : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Source\Repos\Kaubad\Andmebaas1.mdf;Integrated Security=True");
        SqlDataAdapter adapter;
        DataTable dt;

        public KassaForm4()
        {
            InitializeComponent();
            LoadProducts();
        }
        //Метод загрузки товаров
        private void LoadProducts()
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT Id, Nimetus, Pilt FROM Toode", conn);
                adapter = new SqlDataAdapter(cmd);
                dt = new DataTable();
                adapter.Fill(dt);
                conn.Close();
                // Добавление изображений в DataGridView
                dataGridView1.DataSource = dt;

                DataGridViewImageColumn imgColumn = new DataGridViewImageColumn();
                imgColumn.Name = "ToodePilt";
                imgColumn.HeaderText = "Pilt";
                imgColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
                dataGridView1.Columns.Add(imgColumn);
                // Заполнение изображениями
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    var imageValue = row.Cells["Pilt"].Value;
                    if (imageValue != null && !string.IsNullOrEmpty(imageValue.ToString()))
                    {
                        string imagePath = Path.Combine(Path.GetFullPath(@"..\..\Pildid"), imageValue.ToString());
                        if (File.Exists(imagePath))
                        {
                            row.Cells["ToodePilt"].Value = Image.FromFile(imagePath);
                        }
                        else
                        {
                            row.Cells["ToodePilt"].Value = Image.FromFile(Path.Combine(Path.GetFullPath(@"..\..\Pildid"), "pilt.jpg"));
                        }
                    }
                    else
                    {
                        row.Cells["ToodePilt"].Value = Image.FromFile(Path.Combine(Path.GetFullPath(@"..\..\Pildid"), "pilt.jpg"));
                    }
                }
            }
            // Обработка ошибок
            catch (Exception ex)
            {
                MessageBox.Show("Viga kaupade laadimisel: " + ex.Message);
            }
        }

    }
}
