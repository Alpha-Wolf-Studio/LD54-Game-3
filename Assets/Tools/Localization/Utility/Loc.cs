using System.Collections.Generic;

public class Loc
{
    public enum Language
    {
        English,
        Spanish,
        //French
    }

    public static Language currentLanguage = Language.English;

    private static Dictionary<string, string> localisedEN;
    private static Dictionary<string, string> localisedES;
    private static Dictionary<string, string> localisedFR;

    public static bool isInit;

    public static void Init()
    {
        CSVLoader tsvLoader = new CSVLoader();
        tsvLoader.LoadCSV("localization");

        localisedEN = tsvLoader.GetDictionaryValues("en");
        localisedES = tsvLoader.GetDictionaryValues("spa");

        isInit = true;
    }

    public static string ReplaceKey(string key)
    {
        if (!isInit)
            Init();

        string value = key;

        switch (currentLanguage)
        {
            case Language.English:
                localisedEN.TryGetValue(key, out value);
                break;
            case Language.Spanish:
                localisedES.TryGetValue(key, out value);
                break;
            //case Language.French:
            //    localisedFR.TryGetValue(key, out value);
            //    break;
            default:
                break;
        }

        return value;
    }
}
