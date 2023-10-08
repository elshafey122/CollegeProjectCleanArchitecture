using System.Globalization;

namespace SchoolProject.Infrustructure.CommonsLocalize
{
    public class GenericLocalizableEntity
    {
        public string Localize(string texten, string textar)
        {
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            if (cultureInfo.TwoLetterISOLanguageName.ToLower().Equals("ar"))
                return textar;
            return texten;
        }
    }
}
