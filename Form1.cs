using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Baze_LV7_predlozak
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            btnDelete.Enabled = false;
        }

        private void btnSve_Click(object sender, EventArgs e)
        {
            // OVDJE PIŠETE KOD ZA ZADATAK 1. a):
            string statement = "SELECT * FROM osobe ORDER BY prezime ASC";
            SqlConnection conn = new SqlConnection("Server = 192.168.1.111; Initial Catalog = student; User ID = user; Password = 1234");    //postavljeni username i password
            conn.Open();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(statement, conn);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            dgvPodaci.DataSource = dt;
            conn.Close();
        }

        private void btnSpremi_Click(object sender, EventArgs e)
        {
            string ime, prezime, oib, spol, visina, brCip, datum;
            ime = txtIme.Text;
            prezime = txtPrezime.Text;
            oib = txtOIB.Text;
            datum = txtDatum.Value.ToShortDateString();
            visina = txtVisina.Text;
            brCip = txtBrCip.Text;
            if (rbM.Checked)
            {
                spol = "M";
            }
            else
            {
                spol = "Ž";
            }
            // OVDJE PIŠETE KOD ZA ZADATAK 1. b) i ZADATAK 2.:
            SqlConnection conn = new SqlConnection("Server = 192.168.1.111; Initial Catalog = student; User ID = user; Password = 1234");    //postavljeni username i password
            conn.Open();

            if (txtOIB.ReadOnly.Equals(false))
            {
                string insert = "INSERT INTO osobe(ime, prezime, jmbg, datum_rodenja, spol, visina, broj_cipela)" +
                                "VALUES('" + ime + "', '" + prezime + "', '" + oib + "', '" + datum + "', '" + spol + "', '" + visina + "', '" + brCip + "');";

                SqlCommand dodaj = new SqlCommand(insert, conn);
                dodaj.ExecuteNonQuery();

            }
            else
            {
                string update = "UPDATE osobe SET " +
                                "ime = '" + ime + "', prezime = '" + prezime + "', datum_rodenja = '" + datum + "', spol = '" + spol + "', visina = '" + visina + "', broj_cipela = '" + brCip +"' WHERE jmbg = " + oib + ";";
                SqlCommand promjeni = new SqlCommand(update, conn);
                promjeni.ExecuteNonQuery();
            }
            conn.Close();
        }

        public void obrisiSve()
        {
            txtOIB.Text = "";
            txtIme.Text = "";
            txtPrezime.Text = "";
            txtDatum.Text = "";
            txtBrCip.Text = "";
            txtVisina.Text = "";
            dgvPodaci.ClearSelection();
            txtOIB.ReadOnly = false;
            btnDelete.Enabled = false;
        }

        private void btnObrisi_Click(object sender, EventArgs e)
        {
            obrisiSve();
        }

        private void dgvPodaci_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //OVDJE JE DODATAK POTREBAN ZA 2. ZADATAK
            txtIme.Text = dgvPodaci.SelectedRows[0].Cells[0].Value.ToString();
            txtPrezime.Text = dgvPodaci.SelectedRows[0].Cells[1].Value.ToString();
            txtOIB.Text = dgvPodaci.SelectedRows[0].Cells[2].Value.ToString();
            txtDatum.Text = dgvPodaci.SelectedRows[0].Cells[3].Value.ToString();
            if (dgvPodaci.SelectedRows[0].Cells[4].Value.ToString() == "M") 
                rbM.Checked = true;
            else 
                rbZ.Checked = true;
            txtVisina.Text = dgvPodaci.SelectedRows[0].Cells[5].Value.ToString();
            txtBrCip.Text = dgvPodaci.SelectedRows[0].Cells[6].Value.ToString();
            txtOIB.ReadOnly = true;
            btnDelete.Enabled = true;
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // OVDJE PIŠETE KOD ZA 3. ZADATAK:
            if (btnDelete.Enabled)
            {
                SqlConnection conn = new SqlConnection("Server = 192.168.1.111; Initial Catalog = student; User ID = user; Password = 1234");    //postavljeni username i password
                conn.Open();
                string delete = "DELETE FROM osobe WHERE jmbg = "+txtOIB.Text;
                SqlCommand brisanje = new SqlCommand(delete, conn);
                brisanje.ExecuteNonQuery();
                conn.Close();
            }
            
            // ovu naredbu ispod ostavite
            btnDelete.Enabled = false;
        }
    }
}
