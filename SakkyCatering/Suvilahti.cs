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
    public partial class Suvilahti : Form
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

        public int[] savilahti = new int[5];

        private string queryComboxDay = "SELECT DISTINCT WEEK(vko) FROM ilmoitusTaulu WHERE WEEK(vko);";
        private string queryComboxYear = "SELECT DISTINCT YEAR(vko) FROM ilmoitusTaulu WHERE YEAR(vko);";
        public Suvilahti()
        {
            InitializeComponent();
            ConnectionString = "SERVER=" + server + ";" + "UID=" + username + ";" + "PASSWORD=" + password + ";" + "DATABASE=" + database;
            sqlConn.ConnectionString = ConnectionString;

            //cOMBObOX
            weekCombo.DataSource = fillingCombox(queryComboxDay, "WEEK");
            vuosiCombo.DataSource = fillingCombox(queryComboxYear, "YEAR");
            weekCombo.AutoCompleteSource = AutoCompleteSource.ListItems;
            vuosiCombo.AutoCompleteSource = AutoCompleteSource.ListItems;
            weekCombo.SelectedIndex = 0;

            //Taulu
            PaivitaTaulu();
        }
       
        private void Suvilahti_Load(object sender, EventArgs e)
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
        public List<string> fillingCombox(string query, string format)//String query, ComboBox combo, string displayMember, string valueMember)
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
                saConn.Close();
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
        /*------------------------------------------------------------------*/
        public int[] PaivanOppilaat(int FkPaikkat_id)
        {

            int[] maarat = new int[5] { 0, 0, 0, 0, 0 }; //Luodaan array päivien asiakasmäärille

            if (OpenConnection())
            {
                sqlQuery = "SELECT i.ma, i.ti, i.ke, i.tor, i.per FROM ilmoitusTaulu AS i,Koulut AS k WHERE i.FkPaikkat_id= k.ID AND i.FkPaikkat_id = " + FkPaikkat_id + " AND WEEK(vko) = " + weekCombo.Texts + ";";

                MySqlCommand cmd = new MySqlCommand(sqlQuery, sqlConn);

                sqlRd = cmd.ExecuteReader();

                //Loopataan jokaisen palautetun rivin läpi ja katsotaan onko päivämäärä true.

                while (sqlRd.Read())
                {

                    if (Convert.ToBoolean(sqlRd["ma"]))
                    {
                        maarat[0]++;
                    }
                    if (Convert.ToBoolean(sqlRd["ti"]))
                    {
                        maarat[1]++;
                    }
                    if (Convert.ToBoolean(sqlRd["ke"]))
                    {
                        maarat[2]++;
                    }
                    if (Convert.ToBoolean(sqlRd["tor"]))
                    {
                        maarat[3]++;
                    }
                    if (Convert.ToBoolean(sqlRd["per"]))
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
        /*----------------------------------------*/
        private void PaivitaTaulu()
        {
            cartesianChart1.Series.Clear(); //Tyhjennetään taulukko ennen 

            savilahti = PaivanOppilaat(3);

            cartesianChart1.Series = new SeriesCollection
                {
                    new ColumnSeries
                    {
                         Title = "Savilahti",
                        Values = new ChartValues<double> { savilahti[0], savilahti[1], savilahti[2], savilahti[3], savilahti[4] },
                        MaxColumnWidth = 30,
                        Fill = System.Windows.Media.Brushes.Yellow,
                        ScalesYAt = 0

                    }
                };
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

