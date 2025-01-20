using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadGrid();
        }
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-AB39CRL;Initial Catalog=PPD;Integrated Security=True");

        public void ClearBtn()
        {
            Textbox1.Clear();
            Textbox2.Clear();
            Textbox3.Clear();
            Textbox4.Clear();
            Textbox5.Clear();
            Textbox7.Clear();
            Textbox6.Clear();
        }

        public void LoadGrid()
        {
            SqlCommand cmd = new SqlCommand("select * from violator", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            datagrid.ItemsSource = dt.DefaultView;
        }

        private void ClrBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearBtn();
        }

        public bool isValid()
        {
            if (Textbox1.Text == String.Empty)
            {
                MessageBox.Show("Введите Id", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (Textbox2.Text == String.Empty)
            {
                MessageBox.Show("Введите Passport Series", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (Textbox3.Text == String.Empty)
            {
                MessageBox.Show("Введите Surname", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (Textbox4.Text == String.Empty)
            {
                MessageBox.Show("Введите Name", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (Textbox5.Text == String.Empty)
            {
                MessageBox.Show("Введите Patronymic", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (Textbox6.Text == String.Empty)
            {
                MessageBox.Show("Введите Passport Number", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private void InsBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isValid())
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO violator Values (@IdViolator,@Surname,@Name,@Patronymic,@Passport_Series,@Passport_Numbers )", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@IdViolator", Textbox1.Text);
                    cmd.Parameters.AddWithValue("@Surname", Textbox3.Text);
                    cmd.Parameters.AddWithValue("@Name", Textbox4.Text);
                    cmd.Parameters.AddWithValue("@Patronymic", Textbox5.Text);
                    cmd.Parameters.AddWithValue("@Passport_Series", Textbox2.Text);
                    cmd.Parameters.AddWithValue("@Passport_Numbers", Textbox6.Text);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    LoadGrid();
                    MessageBox.Show("Подтверждено", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                    ClearBtn();
                }
            }

            catch (SqlException ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void DelBtn_Click(object sender, RoutedEventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from violator where IdViolator =" + Textbox7.Text+ "", con);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Запись удалена", "Удаление", MessageBoxButton.OK, MessageBoxImage.Information);
                con.Close();
                ClearBtn();
                LoadGrid();
                con.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Не удалена"+ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void UpdBtn_Click(object sender, RoutedEventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Update violator set IdViolator = '" + Textbox1.Text+ "',  Surname = '" + Textbox3.Text + "', Name = '" + Textbox4.Text + "', Patronymic = '" + Textbox5.Text + "', Passport_Series = '" + Textbox2.Text + "',  Passport_Numbers = '" + Textbox6.Text + "' Where IdViolator = '" + Textbox7.Text+"' ", con);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Записи обновлены успешно", "Обновление", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
                ClearBtn();
                LoadGrid();
            }
        }
    }
}
  
