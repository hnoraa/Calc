namespace Calc;

public partial class TimeCalc : ContentPage
{
    private readonly string _timeFormat = "hh:mm tt";

	public TimeCalc()
	{
		InitializeComponent();
        lblCurrentTime.Text = $"Current Time - {DateTime.Now.ToString(_timeFormat)}";
		lblAnswer.Text = "";

        SemanticScreenReader.Announce(lblCurrentTime.Text);
        SemanticScreenReader.Announce(lblAnswer.Text);
    }

    private void btnCalcTime_Clicked(object sender, EventArgs e)
    {
        if(!string.IsNullOrEmpty(txtTimeValue.Text) && txtTimeValue.Text.Contains(':') && !string.IsNullOrEmpty(lvTimeOperation.SelectedItem.ToString()))
        {
            string[] val = txtTimeValue.Text.Split(':');
            string operation = lvTimeOperation.SelectedItem.ToString();
            string currentTime = DateTime.Now.ToString(_timeFormat);
            if (val.Length == 2 && (int.TryParse(val[0], out int calcHr) && int.TryParse(val[1], out int calcMin)))
            {
                DateTime tempCalc = DateTime.Parse(currentTime);

                if (calcHr > 0)
                {
                    tempCalc = operation.Equals("-") ? tempCalc.AddHours(-calcHr) : tempCalc.AddHours(calcHr);
                }

                if (calcMin > 0)
                {
                    tempCalc = operation.Equals("-") ? tempCalc.AddMinutes(-calcMin) : tempCalc.AddMinutes(calcMin);
                }

                lblCurrentTime.Text = $"Current Time - {DateTime.Now.ToString(_timeFormat)}";
                lblAnswer.Text = $"Answer: {currentTime} {operation} {txtTimeValue.Text} = {tempCalc.ToString(_timeFormat)}";
                SemanticScreenReader.Announce(lblCurrentTime.Text);
                SemanticScreenReader.Announce(lblAnswer.Text);
            }
        }
    }
}