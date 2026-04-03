using System;

public class LanguageSwitcher
{
    public static LangType LangType { get; private set; }

    public static event Action Changed;

    public void SetLang(LangType lang)
    {
        if (Equals(LangType, lang) == false)
        {
            LangType = lang;
            Changed?.Invoke();
        }
    }

    public void Next()
    {
        Array values = Enum.GetValues(typeof(LangType));
        int currentIndex = Array.IndexOf(values, LangType);
        int nextIndex = (currentIndex + 1) % values.Length;
        SetLang((LangType)values.GetValue(nextIndex));
    }

    public void Previous()
    {
        Array values = Enum.GetValues(typeof(LangType));
        int currentIndex = Array.IndexOf(values, LangType);
        int previousIndex = (currentIndex - 1 + values.Length) % values.Length;
        SetLang((LangType)values.GetValue(previousIndex));
    }
}