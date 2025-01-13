namespace final;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}
    private async void OnLoginClicked(object sender, EventArgs e)
    {
        var username = usernameEntry.Text;
        var password = passwordEntry.Text;

        
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            await DisplayAlert("Hata", "Kullanýcý adý ve þifre giriniz", "Tamam");
            return;
        }

   
        Application.Current.MainPage = new AppShell();  
            await Shell.Current.GoToAsync("//MainPage");    
    }


    private async void OnRegisterTapped(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegisterPage());
    }
}
