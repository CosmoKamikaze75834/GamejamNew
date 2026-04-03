using System;
using UnityEngine;

[Serializable]
public class LangData
{
    [SerializeField][TextArea] private string _ru = "текст";
    [SerializeField][TextArea] private string _en = "text";

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