using MudBlazor;

namespace UxTracker.Web.Utils.Theme;

public class CustomTheme
{
    public static readonly MudTheme Theme = new()
    {
        PaletteDark = new PaletteDark()
        {
            Primary = Colors.Teal.Lighten1,
            Secondary = "#161B22",
            Tertiary = "#313945",

            Background = "#0D1117",
            AppbarBackground = "#0D1117",
            Surface = "#161B22",

            TextSecondary = "rgba(255,255,255, 0.60)",
            AppbarText = "#FFF",

            Success = "#2EA043",
            Info = "rgba(41,178,255, 0.60)",
            InfoDarken = "#115277",
        },
        Typography = new Typography()
        {
            Default = new Default()
            {
                FontFamily = ["Inter", "Roboto", "sans-serif"],

            },
            Button = new Button()
            {
                TextTransform = string.Empty,
                FontWeight = 700,
            },
        }
    };
}