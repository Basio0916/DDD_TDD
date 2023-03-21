using DDD.WinForm.Common;
using DDD.WinForm.Data;
using System.Data;
using System.Data.SQLite;

namespace DDD.WinForm
{
    public partial class WeatherLatestView : Form
    {
        public WeatherLatestView()
        {
            InitializeComponent();
        }

        private void LatestButton_Click(object sender, EventArgs e)
        {
            DataTable dt = WeatherSQLite.GetLatest(Convert.ToInt32(AreaIdTextBox.Text));
            if(dt.Rows.Count > 0)
            {
                DataDateLabel.Text = dt.Rows[0]["DataDate"].ToString();
                ConditionLabel.Text = dt.Rows[0]["Condition"].ToString();
                TermperatureLabel.Text = CommonFunc.RoundString(Convert.ToSingle( dt.Rows[0]["Temperature"]), CommonConst.TemperatureDecimalPoint) + CommonConst.TemperatureUnitName;

            }
        }

    }
}