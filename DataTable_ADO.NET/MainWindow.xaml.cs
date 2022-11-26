using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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

namespace DataTable_ADO.NET
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection conn;
        string cs = "";
        DataTable table;
        SqlDataReader reader;
        public MainWindow()
        {
            InitializeComponent();
            conn = new SqlConnection();
            cs = ConfigurationManager.ConnectionStrings["MyConnString"].ConnectionString;
        }
        DataSet set;
        SqlDataAdapter da;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            #region DataTable


            //using (var conn = new SqlConnection())
            //{
            //    conn.ConnectionString = cs;
            //    conn.Open();
            //    SqlCommand command = new SqlCommand();
            //    command.CommandText = "SELECT * FROM Authors";
            //    command.Connection = conn;


            //    table = new DataTable();
            //    bool hasColumnAdded = false;

            //    using (reader = command.ExecuteReader())
            //    {
            //        while (reader.Read())
            //        {
            //            if(hasColumnAdded == false)
            //            {
            //                for (int i = 0; i < reader.FieldCount; i++)
            //                {
            //                    table.Columns.Add(reader.GetName(i));
            //                }
            //                hasColumnAdded = true;
            //            }
            //            DataRow row = table.NewRow();
            //            for (int i = 0; i < reader.FieldCount; i++)
            //            {
            //                row[i] = reader[i];
            //            }
            //            table.Rows.Add(row);
            //        }

            //        MyDataGrid1.ItemsSource = table.DefaultView;
            //    }

            //}


            #endregion



            #region DataSet And SqlDataAdapter


            //using (conn = new SqlConnection())
            //{
            //    conn.ConnectionString = cs;
            //    conn.Open();
            //    set = new DataSet();

            //    da = new SqlDataAdapter("SELECT * FROM Authors ; SELECT * FROM Books",conn);

            //    da.Fill(set,"mybook");

            //    MyDataGrid1.ItemsSource = set.Tables[0].DefaultView;
            //    MyDataGrid2.ItemsSource = set.Tables[1].DefaultView;



            //}


            #endregion



            using (conn = new SqlConnection())
            {
                da = new SqlDataAdapter();
                conn.ConnectionString = cs;
                conn.Open();
                set = new DataSet();

                SqlCommand command = new SqlCommand("SELECT * FROM Authors", conn);

                //command.Parameters.Add(new SqlParameter
                //{
                //    DbType = DbType.Int32,
                //    ParameterName = "@id",
                //    Value = 1
                //});

                da.SelectCommand = command;

                da.Fill(set, "firstAuthor");

                //    myDataGrid1.ItemsSource = set.Tables[0].DefaultView;




                //da = new SqlDataAdapter();
                //conn.ConnectionString = cs;
                //conn.Open();
                //set = new DataSet();

                command = new SqlCommand("UPDATE Authors SET Firstname=@firstName WHERE Id=@id", conn);

                command.Parameters.Add(new SqlParameter
                {
                    DbType = DbType.Int32,
                    ParameterName = "@id",
                    Value = 1
                });

                command.Parameters.Add(new SqlParameter
                {
                    SqlDbType = SqlDbType.NVarChar,
                    ParameterName = "@firstName",
                    Value = "DDDDD"
                });

                da.UpdateCommand = command;
                da.UpdateCommand.ExecuteNonQuery();

                da.Update(set, "firstAuthor");
                set.Clear();
                da.Fill(set, "firstAuthor");

                // myDataGrid1.ItemsSource = null;
                MyDataGrid1.ItemsSource = set.Tables[0].DefaultView;

            }



        }
    }
}
