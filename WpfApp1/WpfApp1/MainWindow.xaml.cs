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
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection sqlCon = new SqlConnection(@"Data Source=TOSHIBA;Initial Catalog=DB;Integrated Security=True");
        int ContactId;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sqlCon.State == System.Data.ConnectionState.Closed)
                    sqlCon.Open();
                SqlCommand sqlCmd = new SqlCommand("ContactAddOrEdit", sqlCon);
                sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@mode", "Add");
                sqlCmd.Parameters.AddWithValue("@ContactId", 0);
                sqlCmd.Parameters.AddWithValue("@Imie", txtImie.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@Nazwisko", txtNazwisko.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@NumerTelefonu", txtNrTel.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@Adres", txtAdres.Text.Trim());
                sqlCmd.ExecuteNonQuery();
                MessageBox.Show("Zapisano!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message");
            }
            finally
            {
                sqlCon.Close();
            }

        }
        void FillDataGridView()
        {
            if (sqlCon.State == System.Data.ConnectionState.Closed)
                sqlCon.Open();
            SqlDataAdapter sqlDa = new SqlDataAdapter("ContactViewOrSearch", sqlCon);
            sqlDa.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlDa.SelectCommand.Parameters.AddWithValue("@ContactName", txtSzukaj.Text.Trim());
            DataTable dtbl = new DataTable();
            sqlDa.Fill(dtbl);
            dgDane.ItemsSource = dtbl.DefaultView;
            dgDane.Columns[0].Visibility = Visibility.Collapsed;
            sqlCon.Close();


        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {

        }

        private void txtImie_TextCahnged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtNazwisko_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtNrTel_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtAdres_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtSzukaj_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btn_Szukaj(object sender, RoutedEventArgs e)
        {
            try
            {
                FillDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message");
            }
        }

        private void dgDane_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            
        
        }

        private void dgDane_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            DataRowView row_selected = dg.SelectedItem as DataRowView;
            if(row_selected != null)
            {
                ContactId = Convert.ToInt32(row_selected[0].ToString());
                txtImie.Text = row_selected[1].ToString();
                txtNazwisko.Text = row_selected[2].ToString();
                txtAdres.Text = row_selected[3].ToString();
                txtNrTel.Text = row_selected[4].ToString();
                btnSave.Content = "Zaaktualizuj";
                btnDelete.IsEnabled = true;



            }
        }
    }
}

