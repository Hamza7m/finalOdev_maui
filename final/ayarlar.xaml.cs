using final.Services;

namespace final;

public partial class ayarlar : ContentPage
{
	public ayarlar()
	{
        InitializeComponent();
        LoadSettings();
    }

    private void LoadSettings()
    {
        
        string savedColor = SettingsService.BackgroundColor;
        BackgroundColorPicker.SelectedItem = savedColor;
    }

    private void OnApplySettingsClicked(object sender, EventArgs e)
    {
        DisplayAlert(title: "Background color change ", message: "if you change the color to see the change you must restart the applaction  " ,cancel:"cancel");
        string selectedColor = BackgroundColorPicker.SelectedItem?.ToString() ?? "White";

 
        SettingsService.BackgroundColor = selectedColor;

    
        Application.Current.MainPage.BackgroundColor = SettingsService.GetBackgroundColor();
    }
}