using System.Data;
using System.Data.SQLite;

namespace DDD.WinForm
{
    public partial class WeatherLatestView : Form
    {
        private readonly string ConnectionString = @"Data Source=D:\dev\Repos\DDD_TDD\database\DDD.db;Version=3;";
        public WeatherLatestView()
        {
            InitializeComponent();
        }

        private void LatestButton_Click(object sender, EventArgs e)
        {
            string sql = @"
                SELECT DataDate, Condition, Temperature
                FROM Weather
                WHERE AreaId = @AreaId
                ORDER BY DataDate DESC
                LIMIT 1
            ";

            DataTable dt = new DataTable();
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                using (var command = new SQLiteCommand(sql, connection))
                {
                    connection.Open();

                    command.Parameters.AddWithValue("@AreaId", this.AreaIdTextBox.Text);
                    using(var adapter = new SQLiteDataAdapter(command))
                    {
                        adapter.Fill(dt);
                    }
                }
            }

            if(dt.Rows.Count > 0)
            {
                DataDateLabel.Text = dt.Rows[0]["DataDate"].ToString();
                ConditionLabel.Text = dt.Rows[0]["Condition"].ToString();
                TermperatureLabel.Text = RoundString(Convert.ToSingle( dt.Rows[0]["Temperature"]), 2) + "Åé";

            }
        }

        private string RoundString(float value, int decimalPoint)
        {
            var temp = Convert.ToSingle(Math.Round(value, decimalPoint));
            return temp.ToString("F" + decimalPoint);
        }
    }
}