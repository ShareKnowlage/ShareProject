using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyWpfApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            getData();
        }

        private SqlDataAdapter sqlDataAdapter;

        private DataTable dt;



        void getData()
        {

            //init sqlconnection

            SqlConnectionStringBuilder connbuilder = new SqlConnectionStringBuilder();

            connbuilder.ConnectionString = "Data Source=MARK-PC;Initial Catalog=person;Integrated Security=SSPI;";

            //connbuilder.IntegratedSecurity = false;

            connbuilder.InitialCatalog = "person";

            //start to make sql query

            SqlConnection conn = new SqlConnection(connbuilder.ConnectionString);

            sqlDataAdapter = new SqlDataAdapter("select ContactID,FirstName,LastName,EmailAddress from person.dbo.contact where ContactID<=100;", conn);

            SqlCommandBuilder commbuilder = new SqlCommandBuilder(sqlDataAdapter);

            //sqlDataAdapter.UpdateCommand = commbuilder.GetUpdateCommand();

            dt = new DataTable();

            sqlDataAdapter.AcceptChangesDuringFill = true;

            sqlDataAdapter.Fill(dt);

            listView1.ItemsSource = dt.DefaultView;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            sqlDataAdapter.Update(dt);
            getData();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            sqlDataAdapter.Update(dt);
        }
    }
}
