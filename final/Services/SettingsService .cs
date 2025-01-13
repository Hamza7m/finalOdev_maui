using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace final.Services
{
    class SettingsService
    {
        private const string BackgroundColorKey = "BackgroundColor";
        private static readonly string DefaultBackgroundColor = "White";

        public static string BackgroundColor
        {
            get => Preferences.Get(BackgroundColorKey, DefaultBackgroundColor);
            set => Preferences.Set(BackgroundColorKey, value);
        }

        public static Color GetBackgroundColor()
        {
            return BackgroundColor switch
            {
                "White" => Colors.White,
                "LightGray" => Colors.LightGray,
                "Blue" => Colors.Blue,
                "Green" => Colors.Green,
                "Yellow" => Colors.Yellow,
                _ => Colors.White,
            };
        }
    }
}

