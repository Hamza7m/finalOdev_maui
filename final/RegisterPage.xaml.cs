using Microsoft.Maui.Controls;

namespace final
{
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

   
        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            var username = registerUsernameEntry.Text;
            var password = registerPasswordEntry.Text;

            
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                await DisplayAlert("Hata", "Kullanýcý adý ve þifre giriniz", "Tamam");
                return;
            }

 
            Application.Current.MainPage = new AppShell();
        }
    }
}
