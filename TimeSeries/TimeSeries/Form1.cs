using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using ZedGraph;
using System.IO;
using System.Data.SqlClient;

namespace TimeSeries
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Update_Click(object sender, EventArgs e)
        {
            WebClient Client = new WebClient();
            DateTime date = DateTime.Today.AddDays(-1);

            DateTime lastDBDate = DateTime.Today.AddDays(-1);
            string commandInsert = "";
            int backDays = 1;
            try{
                SqlConnection myConnection = new SqlConnection("Server=localhost;" +
                                                               "database=TimeSeries;" +
                                                               "Integrated Security=True");
                myConnection.Open();
                SqlCommand myCommand = new SqlCommand("SELECT top(1) dateVisit "+
                                                      "FROM Series "+
                                                      "ORDER BY dateVisit DESC", myConnection);
                SqlDataReader myReader = null;
               
                myReader = myCommand.ExecuteReader();
                if (myReader.Read())
                {
                    lastDBDate = Convert.ToDateTime( myReader["dateVisit"].ToString());
                    TimeSpan ts = date - lastDBDate;

                    // Difference in days.
                    backDays = ts.Days-1;
                    if (backDays < 0)
                    {

                        MessageBox.Show("Данные не требуют обновления.");
                        return;
                    }
                }
                else 
                {
                    label1.Text += "dsf";
                    backDays = 6000;
                }
                myReader.Close();
                myCommand.CommandText = "INSERT INTO Series (dateVisit, visitors) " +
                                      " VALUES ";

                Client.DownloadFile("https://top.mail.ru/visits.csv?id=1351249&period=0&date=" + date.Year + "-" +
                                                                                              +date.Month + "-" +
                                                                                              +date.Day + "&back="+backDays+"&days=30", "data.csv");
                
                using (StreamReader sr = new StreamReader(File.Open("data.csv", FileMode.Open), Encoding.Default))
                {
                    string line;
                    string[] columns;
                    int count = 0;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (count < 3)
                        {
                            ++count;
                        }
                        else
                        {
                            columns = line.Split(';');
                            if (columns.Length > 2 && count<backDays+4)
                            {
                                commandInsert += "('" + date.ToShortDateString() + "'," + columns[1] + "), ";
                                date=date.AddDays(-1);
                            }
                            if (count % 100 == 0)
                            {
                                myCommand.CommandText += commandInsert.Substring(0, commandInsert.Length - 2);
                                commandInsert = "";
                                myCommand.ExecuteNonQuery();
                                myCommand.CommandText = "INSERT INTO Series (dateVisit, visitors) " +
                                      " VALUES ";
                            }
                            ++count;
                        }
                        
                        
                    }
                    myCommand.CommandText += commandInsert.Substring(0, commandInsert.Length - 2);
                    myCommand.ExecuteNonQuery();
                }

                MessageBox.Show("Данные обновлены.");
            }catch(Exception exc){
                MessageBox.Show("Не удалось обновить данне. \nException: " + exc.ToString());
            }
         }
        
    }
}
