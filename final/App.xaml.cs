using final.Services;

namespace final
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
            ApplyGlobalSettings();
        }
        private void ApplyGlobalSettings()
        {
         
            MainPage.BackgroundColor = SettingsService.GetBackgroundColor();
        }
    }
}
