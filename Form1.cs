using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Andmebass_TARpv23
{
    public partial class Form1 : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\opilane\Source\Repos\Andmebass_TARpv23\Andmebaas1.mdf;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter adapter;
        OpenFileDialog open;
        SaveFileDialog save;
        Form popupForm;
        DataTable laotable;
        string extension;
        private byte[] imageData;
        public Form1()
        {
            InitializeComponent();
            NaitaAndmed();
            NaitaLaod();
        }

        private void NaitaLaod()
        {
            conn.Open();
            cmd = new SqlCommand("SELECT Id, LaoNimetus FROM Ladu", conn);
            adapter = new SqlDataAdapter(cmd);
            laotable = new DataTable();
            adapter.Fill(laotable);
            foreach (DataRow item in laotable.Rows)
            {
                Ladu_cb.Items.Add(item["LaoNimetus"]);
            }
            conn.Close();
        }

        public void NaitaAndmed()
        {
            conn.Open();
            DataTable dt = new DataTable();
            cmd = new SqlCommand("SELECT * FROM Toode", conn);
            adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }

        private void Lisa_btn_Click(object sender, EventArgs e)
        {
            if (Nimetus_txt.Text.Trim() != string.Empty && Kogus_txt.Text.Trim() != string.Empty && Hind_txt.Text.Trim() != string.Empty)
            {
                try
                {
                    conn.Open();

                    cmd = new SqlCommand("SELECT Id FROM Ladu WHERE LaoNimetus=@ladu", conn);
                    cmd.Parameters.AddWithValue("@ladu", Ladu_cb.Text);
                    cmd.ExecuteNonQuery();
                    ID = Convert.ToInt32(cmd.ExecuteScalar());

                    cmd = new SqlCommand("Insert into Toode(Nimetus, Kogus, Hind, Pilt, LaoID) Values (@toode,@kogus,@hind,@pilt,@ladu)", conn);
                    cmd.Parameters.AddWithValue("@toode", Nimetus_txt.Text);
                    cmd.Parameters.AddWithValue("@kogus", Kogus_txt.Text);
                    cmd.Parameters.AddWithValue("@hind", Hind_txt.Text);
                    cmd.Parameters.AddWithValue("@pilt", Nimetus_txt.Text + extension);

                    //imageData = File.ReadAllBytes(open.FileName);
                    cmd.Parameters.AddWithValue("@ladu", ID);

                    cmd.ExecuteNonQuery();

                    conn.Close();
                    Emaldamine();
                    NaitaAndmed();
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
                    cmd = new SqlCommand("DELETE FROM Toode WHERE Id=@id", conn);
                    cmd.Parameters.AddWithValue("@id", ID);
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    // Удаляем файл
                    Kustuta_fail(dataGridView1.SelectedRows[0].Cells["Pilt"].Value.ToString());

                    Emaldamine();
                    NaitaAndmed();

                    MessageBox.Show("Salvestus on edukalt kustutatud", "Kustutamine");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Viga kirje kustutamisel: {ex.Message}");
            }
        }

        private void Kustuta_fail(string file)
        {
            try
            {
                // Полный путь к файлу
                string filePath = Path.Combine(Path.GetFullPath(@"..\..\Pildid"), file);

                // Проверяем, существует ли файл
                if (File.Exists(filePath))
                {
                    // Сбрасываем картинку в PictureBox
                    pictureBox1.Image?.Dispose();
                    pictureBox1.Image = null;

                    // Удаляем файл
                    File.Delete(filePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Viga faili kustutamisel: {ex.Message}");
            }
        }

        //Исправь проблемы с картинками при запуске запроса на обновление
        private void Uuenda_btn_Click(object sender, EventArgs e)
        {
            try
            {
                // Получаем путь для нового изображения
                string imagePath = Path.Combine(Path.GetFullPath(@"..\..\Pildid"), Nimetus_txt.Text + extension);

                if (File.Exists(imagePath))
                {
                    File.Delete(imagePath); // Удаляем старое изображение, если оно существует
                }

               
                File.Copy(open.FileName, imagePath);

                // Обновляем запись в базе данных
                conn.Open();
                cmd = new SqlCommand("UPDATE Toode SET Nimetus=@toode, Kogus=@kogus, Hind=@hind, Pilt=@pilt, LaoID=@laoid WHERE Id=@id", conn);
                cmd.Parameters.AddWithValue("@id", ID);
                cmd.Parameters.AddWithValue("@toode", Nimetus_txt.Text);
                cmd.Parameters.AddWithValue("@kogus", Kogus_txt.Text);
                cmd.Parameters.AddWithValue("@hind", Hind_txt.Text);
                cmd.Parameters.AddWithValue("@pilt", Nimetus_txt.Text + extension); // Обновляем имя изображения
                cmd.Parameters.AddWithValue("@laoid", Ladu_cb.Text); // Обновляем категорию
                cmd.ExecuteNonQuery();

                conn.Close();
                NaitaAndmed();
                Emaldamine();
                MessageBox.Show("Andmed on edukalt uuendatud", "Uuendamine");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Viga andmebaasiga: {ex.Message}");
            }
        }



        private void Emaldamine()
        {
            Nimetus_txt.Text = "";
            Kogus_txt.Text = "";
            Hind_txt.Text = "";
            pictureBox1.Image = Image.FromFile(Path.Combine(Path.GetFullPath(@"..\..\Pildid"), "pilt.jpg"));
        }

        int ID = 0;
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ID = (int)dataGridView1.Rows[e.RowIndex].Cells["Id"].Value; 
            Nimetus_txt.Text = dataGridView1.Rows[e.RowIndex].Cells["Nimetus"].Value.ToString();
            Kogus_txt.Text = dataGridView1.Rows[e.RowIndex].Cells["Kogus"].Value.ToString();
            Hind_txt.Text = dataGridView1.Rows[e.RowIndex].Cells["Hind"].Value.ToString();
            try
            {
                pictureBox1.Image = Image.FromFile(Path.Combine(Path.GetFullPath(@"..\..\Pildid"),
                    dataGridView1.Rows[e.RowIndex].Cells["Pilt"].Value.ToString()));
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
            catch (Exception)
            {
                pictureBox1.Image = Image.FromFile(Path.Combine(Path.GetFullPath(@"..\..\Pildid"), "pilt.jpg"));
            }
        }

        private void Pildi_otsing_btn_Click(object sender, EventArgs e)
        {
            open = new OpenFileDialog();
            open.InitialDirectory = @"C:\Users\opilane\Pictures\";
            open.Multiselect = false;
            open.Filter = "Images Files(*.jpeg;*.png;*.bmp;*.jpg)|*.jpeg;*.png;*.bmp;*.jpg";

            if (open.ShowDialog() == DialogResult.OK && Nimetus_txt.Text != null)
            {
                save = new SaveFileDialog();
                save.InitialDirectory = Path.GetFullPath(@"..\..\Pildid");
                extension = Path.GetExtension(open.FileName);
                save.FileName = Nimetus_txt.Text + extension;
                save.Filter = "Images" + Path.GetExtension(open.FileName) + "|" + Path.GetExtension(open.FileName);

                if (save.ShowDialog() == DialogResult.OK && Nimetus_txt.Text != null)
                {
                    // If there's an old image, delete it first
                    string oldImagePath = Path.Combine(Path.GetFullPath(@"..\..\Pildid"), Nimetus_txt.Text + extension);
                    if (File.Exists(oldImagePath))
                    {
                        File.Delete(oldImagePath);
                    }

                    // Copy the new image
                    File.Copy(open.FileName, save.FileName);
                    pictureBox1.Image = Image.FromFile(save.FileName);
                }
            }
            else
            {
                MessageBox.Show("Puudub toode nimetus või ole Cancel vajutatud");
            }
        }

        private void dataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 4)
            {
                imageData = dataGridView1.Rows[e.RowIndex].Cells["FusPilt"].Value as byte[];
                if (imageData != null)
                {
                    using (MemoryStream ms = new MemoryStream(imageData))
                    {
                        Image image = Image.FromStream(ms);
                        LoopIt(image, e.RowIndex);
                    }
                }
            }
        }
        private void LoopIt(Image image, int r)
        {
            popupForm = new Form();
            popupForm.FormBorderStyle = FormBorderStyle.None;
            popupForm.StartPosition = FormStartPosition.Manual;
            popupForm.Size = image.Size;


            PictureBox pictureBox = new PictureBox();
            pictureBox.Image = image;
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

            popupForm.Controls.Add(pictureBox);

            Rectangle cellRectangle = dataGridView1.GetCellDisplayRectangle(4, r, true);
            Point popupLocation = dataGridView1.PointToScreen(cellRectangle.Location);

            popupForm.Location = new Point(popupLocation.X + cellRectangle.Width, popupLocation.Y);

            popupForm.Show();
        }

        private void dataGridView1_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (popupForm != null && !popupForm.IsDisposed)
            {
                popupForm.Close();
            }
        }

        private void ladu_btn_Click(object sender, EventArgs e)
        {
            Form2 addLaduForm = new Form2();

            addLaduForm.OnLaduAdded += AddLaduForm_OnLaduAdded; ;

            addLaduForm.ShowDialog();
        }

        private void AddLaduForm_OnLaduAdded()
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT Id, LaoNimetus FROM Ladu", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                Ladu_cb.Items.Clear();

                while (reader.Read())
                {
                    Ladu_cb.Items.Add(new KeyValuePair<int, string>(
                        (int)reader["Id"],
                        reader["LaoNimetus"].ToString()
                    ));
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Viga: {ex.Message}");
            }
        }

        //Метод KeyPress обрабатывает каждый символ, который вводится в текстовое поле, до того, как этот символ будет добавлен в поле.
        // Проверки для полей Kogus_txt 
        private void Kogus_txt_KeyPress(object sender, KeyPressEventArgs e) 
        {
            // Разрешаем только цифры и точку
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        //проверки  на отрицательные значения
        private void Kogus_txt_Leave(object sender, EventArgs e)
        {
            if (int.TryParse(Kogus_txt.Text, out int kogus) && kogus < 0)
            {
                MessageBox.Show("Kogus ei saa olla negatiivne!");
                Kogus_txt.Text = "0";
            }
        }

        // Проверки для полей Hind_txt
        private void Hind_txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Разрешаем вводить только цифры, одну точку
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            // Запрещаем несколько точек в числе
            if (e.KeyChar == '.' && Hind_txt.Text.Contains("."))
            {
                e.Handled = true;
            }
        }

        //проверки  на отрицательные значения
        private void Hind_txt_Leave(object sender, EventArgs e)
        {
            if (decimal.TryParse(Hind_txt.Text, out decimal hind) && hind < 0)
            {
                MessageBox.Show("Hind ei saa olla negatiivne!");
                Hind_txt.Text = "0";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Kogus_txt.KeyPress += Kogus_txt_KeyPress;
            Hind_txt.KeyPress += Hind_txt_KeyPress;
            Kogus_txt.Leave += Kogus_txt_Leave;
            Hind_txt.Leave += Hind_txt_Leave;
        }
        private void SearchProducts(string searchTerm)
        {
            conn.Open();

            string query = "SELECT * FROM Toode WHERE Nimetus LIKE @searchTerm";
            cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%");

            adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;

            conn.Close();
        }

        private void OtsidaButton_Click_1(object sender, EventArgs e)
        {
            string searchTerm = otsida_txt.Text.Trim();
            if (!string.IsNullOrEmpty(searchTerm))
            {

                SearchProducts(searchTerm);
            }
            else
            {
                MessageBox.Show("Palun sisesta otsimiseks toote nimi!");
            }
        }
    }
} 