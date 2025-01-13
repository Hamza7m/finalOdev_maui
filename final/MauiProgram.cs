using Firebase.Database;
using Microsoft.Extensions.Logging;

namespace final
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
            builder.Services.AddSingleton(new FirebaseClient("https://todo-aef6a-default-rtdb.firebaseio.com/"));
            builder.Services.AddSingleton<yapilacaklar>(); 
#endif      

            return builder.Build();
        }
    }
}
