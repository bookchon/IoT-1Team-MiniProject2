using GalaSoft.MvvmLight;
using MahApps.Metro.Controls;
using MySql.Data.MySqlClient;
using System;
using System.Data.SqlClient;
using System.Diagnostics;

namespace RiverLevelUI
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            Onchoenriver_under.Value = Onchoenriver_north.Value = 0;
        }

        private void MetroWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            MySqlConnection conn = new MySqlConnection("Server=pknuiot1team.cghin4qcf4s7.ap-northeast-2.rds.amazonaws.com;Port=3306;Database=parkseonghyeon;Uid=pknuiot1team;Pwd=2V3lhihd8gIQ3krjNMf2;");

            conn.Open();

            // SELECT 문 실행
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM riverlev", conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            // 결과 출력
            while (rdr.Read())
            {
                if (rdr["siteName"].ToString() == "온천천 하류")
                {
                    TxtCmd_under.Text = rdr["siteName"].ToString();
                    Onchoenriver_under.Value = Convert.ToDouble(rdr["waterLevel"]);

                    Txtalert1_under.Text = rdr["alertLevel1Nm"].ToString();
                    Alert1_under.Value = Convert.ToDouble(rdr["alertLevel1"]);

                    Txtalert2_under.Text = rdr["alertLevel2Nm"].ToString();
                    Alert2_under.Value = Convert.ToDouble(rdr["alertLevel2"]);

                    Txtalert3_under.Text = rdr["alertLevel3Nm"].ToString();
                    Alert3_under.Value = Convert.ToDouble(rdr["alertLevel3"]);

                    Txtalert4_under.Text = rdr["alertLevel4Nm"].ToString();
                    Alert4_under.Value = Convert.ToDouble(rdr["alertLevel4"]);
                }

                if (rdr["siteName"].ToString() == "온천장역 북측")
                {
                    TxtCmd_north.Text = rdr["siteName"].ToString();
                    Onchoenriver_north.Value = Convert.ToDouble(rdr["waterLevel"]);

                    Txtalert1_north.Text = rdr["alertLevel1Nm"].ToString();
                    Alert1_north.Value = Convert.ToDouble(rdr["alertLevel1"]);

                    Txtalert2_north.Text = rdr["alertLevel2Nm"].ToString();
                    Alert2_north.Value = Convert.ToDouble(rdr["alertLevel2"]);

                    Txtalert3_north.Text = rdr["alertLevel3Nm"].ToString();
                    Alert3_north.Value = Convert.ToDouble(rdr["alertLevel3"]);

                    Txtalert4_north.Text = rdr["alertLevel4Nm"].ToString();
                    Alert4_north.Value = Convert.ToDouble(rdr["alertLevel4"]);
                }
            }

            // 리소스 해제
            rdr.Close();
            conn.Close();
        }
    }
}
