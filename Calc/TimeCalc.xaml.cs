using static System.Net.Mime.MediaTypeNames;

namespace Calc;

public partial class TimeCalc : ContentPage
{
    private readonly string _timeFormat = "hh:mm tt";

	public TimeCalc()
	{
		InitializeComponent();
        
        lblAnswer.Text = "";
        lblCurrentTz.Text = TimeZoneInfo.Local.DisplayName;
        txtTime.Text = DateTime.Now.ToString(_timeFormat);

        // update current time
        _ = Task.Run(async () =>
        {
            while (true)
            {
                await Device.InvokeOnMainThreadAsync(() =>
                {
                    UpdateCurrentTime();
                });
                await Task.Delay(30000);
            }
        });

        SemanticScreenReader.Announce(txtTime.Text);
        SemanticScreenReader.Announce(lblCurrentTz.Text);
        SemanticScreenReader.Announce(lblAnswer.Text);
    }

    private void UpdateCurrentTime()
    {
        lblCurrentTime.Text = $"Current Time - {DateTime.Now.ToString(_timeFormat)}";
        SemanticScreenReader.Announce(lblCurrentTime.Text);
    }

    private void btnCalcTime_Clicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtTimeValueHrs.Text) && !string.IsNullOrEmpty(txtTimeValueMins.Text) && !string.IsNullOrEmpty(lvTimeOperation.SelectedItem.ToString()))
        {
            string operation = lvTimeOperation.SelectedItem.ToString();
            if ((int.TryParse(txtTimeValueHrs.Text, out int calcHr) && int.TryParse(txtTimeValueMins.Text, out int calcMin)))
            {
                DateTime tempCalc = DateTime.Parse(txtTime.Text);

                if (calcHr > 0)
                {
                    tempCalc = operation.Equals("-") ? tempCalc.AddHours(-calcHr) : tempCalc.AddHours(calcHr);
                }

                if (calcMin > 0)
                {
                    tempCalc = operation.Equals("-") ? tempCalc.AddMinutes(-calcMin) : tempCalc.AddMinutes(calcMin);
                }

                lblAnswer.Text = $"Answer: {txtTime.Text} {operation} {txtTimeValueHrs.Text}:{txtTimeValueMins.Text} = {tempCalc.ToString(_timeFormat)}";
                SemanticScreenReader.Announce(lblAnswer.Text);
            }
        }
    }
}