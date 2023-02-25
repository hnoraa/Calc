
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

    private void btnCalcTimeAdd_Clicked(object sender, EventArgs e)
    {
        Calc("+");
    }

    private void btnCalcTimeSub_Clicked(object sender, EventArgs e)
    {
        Calc("-");
    }

    private void Calc(string operand)
    {
        if(string.IsNullOrEmpty(txtTimeValueMins.Text))
        {
            txtTimeValueMins.Text = "00";
        }

        if(string.IsNullOrEmpty(txtTimeValueHrs.Text))
        {
            txtTimeValueHrs.Text = "00";
        }

        if ((int.TryParse(txtTimeValueHrs.Text, out int calcHr) && int.TryParse(txtTimeValueMins.Text, out int calcMin)))
        {
            DateTime tempCalc = DateTime.Parse(txtTime.Text);

            if (calcHr > 0)
            {
                tempCalc = operand.Equals("-") ? tempCalc.AddHours(-calcHr) : tempCalc.AddHours(calcHr);
            }

            if (calcMin > 0)
            {
                tempCalc = operand.Equals("-") ? tempCalc.AddMinutes(-calcMin) : tempCalc.AddMinutes(calcMin);
            }

            txtTimeValueHrs.Text = txtTimeValueHrs.Text.Length == 1 ? "0" + txtTimeValueHrs.Text : txtTimeValueHrs.Text;
            txtTimeValueMins.Text = txtTimeValueMins.Text.Length == 1 ? "0" + txtTimeValueMins.Text : txtTimeValueMins.Text;
            lblAnswer.Text = $"Answer: {txtTime.Text} {operand} {txtTimeValueHrs.Text}:{txtTimeValueMins.Text} = {tempCalc.ToString(_timeFormat)}";
            SemanticScreenReader.Announce(lblAnswer.Text);
        }
    }

    private void btnSetTime_Clicked(object sender, EventArgs e)
    {
        txtTime.Text = DateTime.Now.ToString(_timeFormat);
        SemanticScreenReader.Announce(txtTime.Text);
    }
}