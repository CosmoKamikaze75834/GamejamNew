using System;
using UnityEngine;

[Serializable]
public class SavesData
{
    [SerializeField] private float _generalSoundVolume;
    [SerializeField] private float _musicVolume;
    [SerializeField] private float _sfxVolume;
    [SerializeField] private LangType _lang = LangType.En;
    [SerializeField] private DifficultyType _difficulty = DifficultyType.Normal;

    public SavesData(
        float generalSoundVolume, 
        float musicVolume, 
        float sfxVolume, 
        LangType lang, 
        DifficultyType difficulty)
    {
        _generalSoundVolume = generalSoundVolume;
        _musicVolume = musicVolume;
        _sfxVolume = sfxVolume;
        _lang = lang;
        _difficulty = difficulty;
    }

    public float GeneralSoundVolume => _generalSoundVolume;

    public float MusicVolume => _musicVolume;

    public float SfxVolume => _sfxVolume;

    public LangType Lang => _lang;

    public DifficultyType Difficulty => _difficulty;
}