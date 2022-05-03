using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.Wpf;
using MySql.Data.MySqlClient;
using LiveCharts.Defaults;
using System.Globalization;

namespace SakkyCatering
{
    public partial class Desktop : Form
    {
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

        //Live charts property
        //public SeriesCollection SeriesCollection { get; set; }
        //public string[] Labels { get; set; }


        //fields for round the form
        private int borderRadius = 30;
        private int borderSize= 2;
        private Color borderColor = Color.FromArgb(253, 255, 252);
        private Form currentChildForm;

        

        //Querys for comboBox
       // private string queryComboxDay = "SELECT DISTINCT WEEK(vko) FROM ilmoitusTaulu WHERE WEEK(vko);";
        //private string queryComboxYear = "SELECT DISTINCT YEAR(vko) FROM ilmoitusTaulu WHERE YEAR(vko);";

        //constructor
        public Desktop()
        {
            InitializeComponent();
            ConnectionString = "SERVER=" + server + ";" + "UID=" + username + ";" + "PASSWORD=" + password + ";" + "DATABASE=" + database;
            sqlConn.ConnectionString = ConnectionString;

            //cOMBObOX
            //weekCombo.DataSource = fillingCombox(queryComboxDay, "WEEK");
            //vuosiCombo.DataSource = fillingCombox(queryComboxYear, "YEAR");
            //weekCombo.AutoCompleteSource = AutoCompleteSource.ListItems;
            //vuosiCombo.AutoCompleteSource = AutoCompleteSource.ListItems;
            
            //weekCombo.SelectedIndex = 0;

            //Taulu
            //

            //form design
            this.FormBorderStyle = FormBorderStyle.None;
            this.Padding = new Padding(borderSize);
            this.panelTitleBar.BackColor = Color.FromArgb(253, 190, 0);

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
            kotiBtn.Hide();
            Kampukset_btn.Hide();
            textBoxTunnuns.Focus();
        }
        /*------------------------------------------------LOGing---------------------------------------------------------*/


        private void KirjauduBtn_Click(object sender, EventArgs e)
        {
            int i = 0;
            OpenConnection();
            MySqlCommand cmd = sqlConn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select *from kayttajat where username='" + textBoxTunnuns.Text + "' and password='" + textBoxSalasana.Text + "'";

            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);
            i = Convert.ToInt32(dt.Rows.Count.ToString());

            if (i == 0)
            {
                MessageBox.Show("Kirjautuminen epäonnistui");
                textBoxTunnuns.Focus();
                textBoxSalasana.Clear();
                
            }
            else
            {
                OpenChildForm(new taulutYhdessä());
                kotiBtn.Show();
                Kampukset_btn.Show();

            }
            CloseConnection();
        }
        /*-------------------------------------Styling WinForm---------------------------------------------------*/


        /*For dragging the form*/

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        //Get minimize the form
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style |= 0x20000; // <--- Minimize borderless form from taskbar
                return cp;
            }
        }

        //Move from the desktop Panel
       
        private void panelDesktop_MouseDown_1(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        //move form from the Title bar
        private void panelTitleBar_MouseDown_1(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        //Private methods
        private void FormRegionAndBorder(Form form, float radius, Graphics graph, Color borderColor, float borderSize)
        {
            if (this.WindowState != FormWindowState.Minimized)
            {
                using (GraphicsPath roundPath = GetRoundedPath(form.ClientRectangle, radius))
                using (Pen penBorder = new Pen(borderColor, borderSize))
                using (Matrix transform = new Matrix())
                {
                    graph.SmoothingMode = SmoothingMode.AntiAlias;
                    form.Region = new Region(roundPath);
                    if (borderSize >= 1)
                    {
                        Rectangle rect = form.ClientRectangle;
                        float scaleX = 1.0F - ((borderSize + 1) / rect.Width);
                        float scaleY = 1.0F - ((borderSize + 1) / rect.Height);
                        transform.Scale(scaleX, scaleY);
                        transform.Translate(borderSize / 1.6F, borderSize / 1.6F);
                        graph.Transform = transform;
                        graph.DrawPath(penBorder, roundPath);
                    }
                }
            }
        }
        private GraphicsPath GetRoundedPath(Rectangle rect, float radius)
        {
            GraphicsPath path = new GraphicsPath();
            float curveSize = radius * 2F;
            path.StartFigure();
            path.AddArc(rect.X, rect.Y, curveSize, curveSize, 180, 90);
            path.AddArc(rect.Right - curveSize, rect.Y, curveSize, curveSize, 270, 90);
            path.AddArc(rect.Right - curveSize, rect.Bottom - curveSize, curveSize, curveSize, 0, 90);
            path.AddArc(rect.X, rect.Bottom - curveSize, curveSize, curveSize, 90, 90);
            path.CloseFigure();
            return path;
        }
        private void DrawPath(Rectangle rect, Graphics graph, Color color)
        {
            using (GraphicsPath roundPath = GetRoundedPath(rect, borderRadius))
            using (Pen penBorder = new Pen(color, 3))
            {
                graph.DrawPath(penBorder, roundPath);
            }
        }
        private struct FormBoundsColors
        {
            public Color TopLeftColor;
            public Color TopRightColor;
            public Color BottomLeftColor;
            public Color BottomRightColor;
        }
       private FormBoundsColors GetFormBoundsColors()
        {
            var fbColor = new FormBoundsColors();
            using (var bmp = new Bitmap(1, 1))
            using (Graphics graph = Graphics.FromImage(bmp))
            {
                Rectangle rectBmp = new Rectangle(0, 0, 1, 1);

                //Top Left
                rectBmp.X = this.Bounds.X - 1;
                rectBmp.Y = this.Bounds.Y;
                graph.CopyFromScreen(rectBmp.Location, Point.Empty, rectBmp.Size);
                fbColor.TopLeftColor = bmp.GetPixel(0, 0);

                //Top Right
                rectBmp.X = this.Bounds.Right;
                rectBmp.Y = this.Bounds.Y;
                graph.CopyFromScreen(rectBmp.Location, Point.Empty, rectBmp.Size);
                fbColor.TopRightColor = bmp.GetPixel(0, 0);

                //Bottom Left
                rectBmp.X = this.Bounds.X;
                rectBmp.Y = this.Bounds.Bottom;
                graph.CopyFromScreen(rectBmp.Location, Point.Empty, rectBmp.Size);
                fbColor.BottomLeftColor = bmp.GetPixel(0, 0);

                //Bottom Right
                rectBmp.X = this.Bounds.Right;
                rectBmp.Y = this.Bounds.Bottom;
                graph.CopyFromScreen(rectBmp.Location, Point.Empty, rectBmp.Size);
                fbColor.BottomRightColor = bmp.GetPixel(0, 0);
            }
            return fbColor;
        }

        //Event Methods
     
        private void Desktop_Paint(object sender, PaintEventArgs e)
        {
            //-> SMOOTH OUTER BORDER
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rectForm = this.ClientRectangle;
            int mWidht = rectForm.Width / 2;
            int mHeight = rectForm.Height / 2;
            var fbColors = GetFormBoundsColors();
            //Top Left
            DrawPath(rectForm, e.Graphics, fbColors.TopLeftColor);
            //Top Right
            Rectangle rectTopRight = new Rectangle(mWidht, rectForm.Y, mWidht, mHeight);
            DrawPath(rectTopRight, e.Graphics, fbColors.TopRightColor);
            //Bottom Left
            Rectangle rectBottomLeft = new Rectangle(rectForm.X, rectForm.X + mHeight, mWidht, mHeight);
            DrawPath(rectBottomLeft, e.Graphics, fbColors.BottomLeftColor);
            //Bottom Right
            Rectangle rectBottomRight = new Rectangle(mWidht, rectForm.Y + mHeight, mWidht, mHeight);
            DrawPath(rectBottomRight, e.Graphics, fbColors.BottomRightColor);
            //-> SET ROUNDED REGION AND BORDER
            FormRegionAndBorder(this, borderRadius, e.Graphics, borderColor, borderSize);
        }
        private void ControlRegionAndBorder(Control control, float radius, Graphics graph, Color borderColor)
        {
            using (GraphicsPath roundPath = GetRoundedPath(control.ClientRectangle, radius))
            using (Pen penBorder = new Pen(borderColor, 1))
            {
                graph.SmoothingMode = SmoothingMode.AntiAlias;
                control.Region = new Region(roundPath);
                graph.DrawPath(penBorder, roundPath);
            }
        }
        private void panelDesktop_Paint(object sender, PaintEventArgs e)
        {
            ControlRegionAndBorder(panelDesktop, borderRadius - (borderSize / 2), e.Graphics, borderColor);
        }

        private void Desktop_Activated(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void Desktop_Resize(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void Desktop_SizeChanged(object sender, EventArgs e)
        {
            this.Invalidate();
        }


        /*----------------------------------- Open new form in the current main form--------------------------*/
        private void OpenChildForm(Form childForm)
        {
            //open only form
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }
            currentChildForm = childForm;
            //End
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelDesktop.Controls.Add(childForm);
            panelDesktop.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }


        /*---------------------------------------MENU BUTTONS-------------------------------------*/
        private void kotiBtn_Click_1(object sender, EventArgs e)
        {
            OpenChildForm(new taulutYhdessä());
        }
        private void Kampukset_btn_Click_1(object sender, EventArgs e)
        {
            OpenChildForm(new Kampukset());
        }
      

        private void ulosBtn_Click_1(object sender, EventArgs e)
        {
            DialogResult iExit;
            try
            {
                iExit = MessageBox.Show("Confirm if you want to exit", "MySQL Connect", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (iExit == DialogResult.Yes)
                {
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClosed_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

       private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    

    /*----------------Making connection-----------------------------*/

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
