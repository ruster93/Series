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
        private int zoom;
        public Form1()
        {
            InitializeComponent();
            //zedGraphControl1.MouseWheel += zedGraphControl1_MouseWheel;
            zoom = 1;
            Show_Plot();
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
                SqlConnection myConnection = new SqlConnection("Server=localhost;" +
                                                               "database=TimeSeries;" +
                                                               "Integrated Security=True");
            try{
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
                    backDays = ts.Days-1;
                    if (backDays < 0)
                    {

                        MessageBox.Show("Данные не требуют обновления.");
                        return;
                    }
                }
                else 
                {
                    lastDBDate = Convert.ToDateTime("2008-05-18");
                    TimeSpan ts = date - lastDBDate;
                    backDays = ts.Days - 1;
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
                myConnection.Close();
                Show_Plot();
                MessageBox.Show("Данные обновлены.");
            }catch(Exception exc){
                MessageBox.Show("Не удалось обновить данные. \nException: " + exc.ToString());
            }
         }

        private void Show_Plot()
        {
            GraphPane pane = zedGraphControl1.GraphPane;
            // Создадим список точек
            PointPairList list = new PointPairList();

            SqlConnection myConnection = new SqlConnection("Server=localhost;" +
                                                               "database=TimeSeries;" +
                                                               "Integrated Security=True");
            try
            {
                myConnection.Open();
                SqlCommand myCommand = new SqlCommand("SELECT * " +
                                                      "FROM Series " +
                                                      "ORDER BY dateVisit", myConnection);
                SqlDataReader myReader = null;
                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    list.Add(new XDate(Convert.ToDateTime(myReader["dateVisit"].ToString())), Convert.ToInt32(myReader["visitors"].ToString()));
                }
                myReader.Close();
                myConnection.Close();

                // Выберем случайный цвет для графика
                Color curveColor = Color.DarkBlue;
                LineItem myCurve = pane.AddCurve("", list, curveColor, SymbolType.None);
                
                // Установим, что по оси X у нас откладываются даты,
                pane.XAxis.Type = AxisType.Date;

                // Включим сглаживание
                //myCurve.Line.IsSmooth = true;
                // Установим интервал изменения по оси X
                pane.XAxis.Min = new XDate(2008, 05, 19);
                pane.XAxis.Max = new XDate(DateTime.Today.AddDays(-1));

                // По оси Y установим автоматический подбор масштаба
                //pane.YAxis.MinAuto = true;
                //pane.YAxis.MaxAuto = true;

                // Обновим график
                zedGraphControl1.AxisChange();
                zedGraphControl1.Invalidate();
                
            }
            catch (Exception exc)
            {
                MessageBox.Show("Не удалось отобразить данные. \nException: " + exc.ToString());
            }
        }

        private void Zoom_minus_Click(object sender, EventArgs e)
        {
            if (zoom > 1) {
                GraphPane pane = zedGraphControl1.GraphPane;
                pane.
                --zoom;
            }
        }

        //void zedGraphControl1_MouseWheel(object sender, MouseEventArgs e)
        //{
        //    // throw new NotImplementedException();
        //    MessageBox.Show("sdfds");
        //}

        
    }
}
