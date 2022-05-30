using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;



namespace week_14_praktikum
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        MySqlConnection sqlConnect = new MySqlConnection("server=localhost;uid=root;pwd=;database=premier_league");
        MySqlCommand sqlCommand;
        MySqlDataAdapter sqlAdapter;
        String sqlQuery;
        DataTable dtData = new DataTable();
        int posisiSekarang = 0;

        public void IsiDataPemain(int Posisi)
        {
            //MessageBox.Show(Posisi.ToString());
            labelTeamName.Text = dtData.Rows[Posisi][0].ToString();
            labelManager.Text = dtData.Rows[Posisi][1].ToString();
            labelStadium.Text = dtData.Rows[Posisi][2].ToString();
            //lbl_IsiTopScorer.Text = dtData.Rows[Posisi][3].ToString();
            //lbl_IsiWorstDiscipline.Text = dtData.Rows[Posisi][8].ToString();
            posisiSekarang = Posisi;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            sqlQuery = "select t.team_name, concat(m.manager_name, ' (' ,n.nation, ')'), concat(t.home_stadium, ', ',  t.city, ' (',t.capacity,')') from team t, manager m, nationality n where t.manager_id = m.manager_id and n.nationality_id = m.nationality_id; ";
            sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
            sqlAdapter = new MySqlDataAdapter(sqlCommand);
            sqlAdapter.Fill(dtData);
            IsiDataPemain(0);

            DataTable dtMatch = new DataTable();
            sqlQuery = "select match_date from `match`";
            sqlCommand = new MySqlCommand(sqlQuery, sqlConnect);
            sqlAdapter = new MySqlDataAdapter(sqlCommand);
            sqlAdapter.Fill(dtMatch);
            dataGridView_last5match.DataSource = dtMatch;


        }

        private void buttonPrev2_Click(object sender, EventArgs e)
        {
            IsiDataPemain(0);
        }

        private void buttonPrev_Click(object sender, EventArgs e)
        {
            if (posisiSekarang > 0)
            {
                posisiSekarang--;
                IsiDataPemain(posisiSekarang);
            }
            else
            {
                MessageBox.Show("Data Sudah Data Pertama");
            }
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (posisiSekarang < dtData.Rows.Count - 1)
            {
                posisiSekarang++;
                IsiDataPemain(posisiSekarang);
            }
            else
            {
                MessageBox.Show("Data Sudah Data Terakhir");
            }
        }

        private void buttonNext2_Click(object sender, EventArgs e)
        {
            IsiDataPemain(dtData.Rows.Count - 1);
        }
    }
}
