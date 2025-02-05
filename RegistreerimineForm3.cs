using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Andmebass_TARpv23
{
    public partial class RegistreerimineForm3 : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Source\Repos\Kaubad\Andmebaas1.mdf;Integrated Security=True");
        SqlCommand cmd;
        public RegistreerimineForm3()
        {
            InitializeComponent();
            // Добавляем элементы в ComboBox
            Rolli_comboBox.Items.Add("müüja");
            Rolli_comboBox.Items.Add("omanik");
            // Устанавливаем первый элемент как выбранный
            Rolli_comboBox.SelectedIndex = 0;
        }
        // Метод регистрации
        private void RegisterButton_Click_1(object sender, EventArgs e)
        {
            string nimi = Nimi_txt.Text;
            string parool = parool_txt.Text;
            string rolli = Rolli_comboBox.SelectedItem.ToString();
            // Проверка на пустые поля
            if (!string.IsNullOrEmpty(nimi) && !string.IsNullOrEmpty(parool))
            {
                // Запись данных в базу
                try
                {
                    conn.Open();
                    cmd = new SqlCommand("INSERT INTO  Registreerimini(Nimi, Parool, Rolli) VALUES (@nimi, @parool, @rolli)", conn);
                    cmd.Parameters.AddWithValue("@nimi", nimi);
                    cmd.Parameters.AddWithValue("@parool", parool);
                    cmd.Parameters.AddWithValue("@rolli", rolli);
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    MessageBox.Show("Edukalt");

                    // Открытие новой формы в зависимости от роли
                    if (rolli == "müüja")
                    {
                        // Открываем форму Кассы для "müüja"
                        KassaForm4 kassaForm = new KassaForm4();
                        kassaForm.Show();
                    }
                    else if (rolli == "omanik")
                    {
                        // Открываем основную форму для "omanik" (Form1)
                        Form1 form1 = new Form1();
                        form1.Show();
                        //Открываем также форму Кассы для "omanik"
                        KassaForm4 kassaForm = new KassaForm4();
                        kassaForm.Show();
                    }
                }
                // Обработка ошибок
                catch (Exception ex)
                {
                    MessageBox.Show("Viga: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Palun täitke kõik väljad.");
            }
        }
    }

}
