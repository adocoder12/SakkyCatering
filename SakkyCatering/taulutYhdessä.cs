using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.Wpf;
using MySql.Data.MySqlClient;

namespace SakkyCatering
{
    public partial class taulutYhdessä : Form
    {
        //Property
        MySqlCommand sqlCmd = new MySqlCommand();
        String sqlQuery;
        MySqlDataReader sqlRd;
        DataSet DS = new DataSet();
        MySqlConnection sqlConn = new MySqlConnection();

        string server = "linuxedu.koulutus.kynet.fi";
        string username = "tommi_nuutinen";
        string password = "12345";
        string database = "P166049";
        string ConnectionString = null;

      

        private string queryComboxDay = "SELECT DISTINCT WEEK(vko) FROM ilmoitusTaulu WHERE WEEK(vko);";
        private string queryComboxYear = "SELECT DISTINCT YEAR(vko) FROM ilmoitusTaulu WHERE YEAR(vko);";
        public taulutYhdessä()
        {
            InitializeComponent();
            ConnectionString = "SERVER=" + server + ";" + "UID=" + username + ";" + "PASSWORD=" + password + ";" + "DATABASE=" + database;
            sqlConn.ConnectionString = ConnectionString;
            //cOMBObOX
            weekCombo.DataSource = fillingCombox(queryComboxDay, "WEEK");
            vuosiCombo.DataSource = fillingCombox(queryComboxYear, "YEAR");
            weekCombo.AutoCompleteSource = AutoCompleteSource.ListItems;
            vuosiCombo.AutoCompleteSource = AutoCompleteSource.ListItems;
            //Taulu
            PaivitaTaulu();
        }
        private void taulutYhdessä_Load(object sender, EventArgs e)
        {
            cartesianChart1.AxisX.Add(new Axis
            {
                Title = "",
                FontSize = 20,
                Labels = new[] { "Maanantai", "Tiistai", "Keskiviikko", "Torstai", "Perjantai" }

            });
            cartesianChart1.AxisY.Add(new Axis //Lisätään Y akseli
            {
                Title = "Opiskelijat",
                FontSize = 20,
                LabelFormatter = value => value.ToString("N0"), //Muutetaan kokonaisluvuiksi.
                Position = AxisPosition.LeftBottom,

            });
        }
        /*------------FILLLING COMBOX-----------------*/
        public List<string> fillingCombox(string query, string format)
        {
            List<string> dateList = new List<string>();
            ConnectionString = "server=" + server + ";" + "user id=" + username + ";" + "password=" + password + ";" + "database=" + database;
            using (MySqlConnection saConn = new MySqlConnection(ConnectionString))
            {
                MySqlCommand cmd = new MySqlCommand(query, saConn);
                saConn.Open();
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    dateList.Add(dataReader[format + "(vko)"].ToString());
                }
                CloseConnection();
                return dateList;
            }
        }
        private void weekCombo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            PaivitaTaulu();
        }

        private void vuosiCombo_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            weekCombo.DataSource = fillingCombox(queryComboxDay, "WEEK");
            weekCombo.AutoCompleteSource = AutoCompleteSource.ListItems;
        }
        /*---------------------------------------------------------------------------------*/
        public int[] PaivanOppilaat(int FkPaikkat_id)
        {

            int[] maarat = new int[5] { 0, 0, 0, 0, 0 }; //Luodaan array päivien asiakasmäärille

            if (OpenConnection())
            {
                sqlQuery = "SELECT i.ma, i.ti, i.ke, i.tor, i.per FROM ilmoitusTaulu AS i,Koulut AS k WHERE i.FkPaikkat_id= k.ID AND i.FkPaikkat_id = " + FkPaikkat_id + " AND WEEK(vko) = " + weekCombo.Texts + " AND YEAR(vko) = " + vuosiCombo.Texts + ";";

                MySqlCommand cmd = new MySqlCommand(sqlQuery, sqlConn);

                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Loopataan jokaisen palautetun rivin läpi ja katsotaan onko päivämäärä true.

                while (dataReader.Read())
                {

                    if (Convert.ToBoolean(dataReader["ma"]))
                    {
                        maarat[0]++;
                    }
                    if (Convert.ToBoolean(dataReader["ti"]))
                    {
                        maarat[1]++;
                    }
                    if (Convert.ToBoolean(dataReader["ke"]))
                    {
                        maarat[2]++;
                    }
                    if (Convert.ToBoolean(dataReader["tor"]))
                    {
                        maarat[3]++;
                    }
                    if (Convert.ToBoolean(dataReader["per"]))
                    {
                        maarat[4]++;
                    }

                }
                CloseConnection(); //Suljetaan yhteys.

                return maarat;
            }
            else
            {
                MessageBox.Show("Connection fail");
                return maarat;
            }
        }
        /*--------------------------------------------------------------------------------*/

        //Array where to store the days from the table
        public int[] pressa = new int[5];
        public int[] sammakko = new int[5];
        public int[] savilahti = new int[5];
        public int[] toivala = new int[5];
        public int[] varkaus = new int[5];
        private void PaivitaTaulu()
        {

            //Tyhjennetään taulukko ennen 
            cartesianChart1.Series.Clear();

            //Arrays where are stored the days of the week
            pressa = PaivanOppilaat(1);
            sammakko = PaivanOppilaat(2);
            savilahti = PaivanOppilaat(3);
            toivala = PaivanOppilaat(4);
            varkaus = PaivanOppilaat(5);

            //Chart Collection
            cartesianChart1.Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Presidentinkatu",
                    MaxColumnWidth = 30,
                    Values = new ChartValues<double> { pressa[0], pressa[1], pressa[2], pressa[3], pressa[4] }
                }
            };

            cartesianChart1.Series.Add(new ColumnSeries
            {

                Title = "Sammakkolampi",
                Values = new ChartValues<double> { sammakko[0], sammakko[1], sammakko[2], sammakko[3], sammakko[4] },
                MaxColumnWidth = 30

            });
            cartesianChart1.Series.Add(new ColumnSeries

            {
                Title = "Savilahti",
                Values = new ChartValues<double> { savilahti[0], savilahti[1], savilahti[2], savilahti[3], savilahti[4] },
                MaxColumnWidth = 30
            });



            cartesianChart1.Series.Add(new ColumnSeries
            {
                Title = "Toivala",
                Values = new ChartValues<double> { toivala[0], toivala[1], toivala[2], toivala[3], toivala[4] },
                MaxColumnWidth = 30

            });

            cartesianChart1.Series.Add(new ColumnSeries
            {
                Title = "Varkaus",
                Values = new ChartValues<double> { varkaus[0], varkaus[1], varkaus[2], varkaus[3], varkaus[4] },
                MaxColumnWidth = 30
            });

        }

        private void cartesianChart1_DataClick(object sender, ChartPoint chartPoint)
        {
            MessageBox.Show("You clicked (" + chartPoint.X + "," + chartPoint.Y + ")");
        }
        /*-------------------Making connection-----------------*/

        bool OpenConnection() // Yritetään avata yhteys.
        {
            try
            {
                sqlConn.Open();
                return true;
            }
            catch (MySqlException ex)
            {

                try
                {
                    MessageBox.Show("Virhe!\n" + ex.Message.Substring(136) + "\n\nJos ongelma jatkuu, ota yhteyttä ylläpitäjään.");
                }
                catch
                {
                    MessageBox.Show("Virhe yhdistäessä palvelimelle! \n" + "Tarkista verkkoyhteys. \n\nJos ongelma jatkuu, ota yhteyttä ylläpitäjään.");
                }
                return false;
            }
        }

        bool CloseConnection() //Yritetään sulkea yhteys.
        {
            try
            {
                sqlConn.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                try
                {
                    MessageBox.Show("Virhe!\n" + ex.Message.Substring(136) + "\n\nJos ongelma jatkuu, ota yhteyttä ylläpitäjään.");
                }
                catch
                {
                    MessageBox.Show("Virhe yhdistäessä palvelimelle! \n" + "Tarkista verkkoyhteys. \n\nJos ongelma jatkuu, ota yhteyttä ylläpitäjään.");
                }
                return false;
            }
        }

       
    }
}
