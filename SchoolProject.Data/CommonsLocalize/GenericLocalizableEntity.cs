using System.Globalization;

namespace SchoolProject.Data.CommonsLocalize
{
    public class GenericLocalizableEntity
    {
        public string Localize(string? textar, string? texten)
        {
            CultureInfo culture = Thread.CurrentThread.CurrentCulture;
            if (culture.TwoLetterISOLanguageName.ToLower().Equals("ar"))
                return textar;
            return texten;
        }
    }
}
