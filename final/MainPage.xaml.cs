using final.Services;

namespace final
{
    public partial class MainPage : ContentPage
    {
        

        public MainPage()
        { 
            BackgroundColor = SettingsService.GetBackgroundColor();
            InitializeComponent();
           
        }

       
    }

}
