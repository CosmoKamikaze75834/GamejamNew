using System;
using UnityEngine;

[RequireComponent(typeof(TextLanguage))]
public class DifficultyLevelLangWrapper : MonoBehaviour
{
    [SerializeField] private LangData _easy;
    [SerializeField] private LangData _normal;
    [SerializeField] private LangData _hard;
    [SerializeField] private LangData _madness;

    private TextLanguage _textLanguage;

    private void Awake() =>
        _textLanguage = GetComponent<TextLanguage>();

    private void OnEnable()
    {
        DifficultySwitcher.Changed += OnDifficultyChanged;
        OnDifficultyChanged();
    }

    private void OnDisable() =>
        DifficultySwitcher.Changed -= OnDifficultyChanged;

    private void OnDifficultyChanged()
    {
        LangData data = DifficultySwitcher.Difficulty switch
        {
            DifficultyType.Easy => _easy,
            DifficultyType.Normal => _normal,
            DifficultyType.Hard => _hard,
            DifficultyType.Madness => _madness,
            _ => throw new Exception($"Указанный тип {nameof(DifficultyType)} {DifficultySwitcher.Difficulty} не обработан"),
        };

        _textLanguage.SetData(data);
    }
}