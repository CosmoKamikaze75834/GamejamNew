using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class TextLanguage : MonoBehaviour
{
    [SerializeField] private LangData _langs;

    private TMP_Text _text;

    private void Awake() =>
        _text = GetComponent<TMP_Text>();

    private void OnEnable()
    {
        LanguageSwitcher.Changed += OnLangChanged;
        OnLangChanged();
    }

    private void OnDisable() =>
        LanguageSwitcher.Changed -= OnLangChanged;

    public void SetData(LangData data)
    {
        _langs = data;
        OnLangChanged();
    }

    private void OnLangChanged() =>
        _text.text = _langs.Get(LanguageSwitcher.LangType);
}