using System;
using UnityEngine;

[Serializable]
public class LangData
{
    [SerializeField][TextArea] private string _ru = "текст";
    [SerializeField][TextArea] private string _en = "text";

    public LangData() { }

    public LangData(string ru, string en)
    {
        _ru = ru;
        _en = en;
    }

    public string Get(LangType lang)
    {
        return lang switch
        {
            LangType.Ru => _ru,
            LangType.En => _en,
            _ => throw new Exception($"Незарегистрированный тип {nameof(LangType)}: {lang}"),
        };
    }
}