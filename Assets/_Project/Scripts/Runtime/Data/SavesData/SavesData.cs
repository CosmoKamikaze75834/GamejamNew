using System;
using UnityEngine;

[Serializable]
public class SavesData
{
    [SerializeField] private float _generalSoundVolume;
    [SerializeField] private float _musicVolume;
    [SerializeField] private float _sfxVolume;
    [SerializeField] private LangType _lang = LangType.En;

    public SavesData(float generalSoundVolume, float musicVolume, float sfxVolume, LangType lang)
    {
        _generalSoundVolume = generalSoundVolume;
        _musicVolume = musicVolume;
        _sfxVolume = sfxVolume;
        _lang = lang;
    }

    public float GeneralSoundVolume => _generalSoundVolume;

    public float MusicVolume => _musicVolume;

    public float SfxVolume => _sfxVolume;

    public LangType Lang => _lang;
}