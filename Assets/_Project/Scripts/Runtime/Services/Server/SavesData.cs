using System;
using UnityEngine;

[Serializable]
public class SavesData
{
    [SerializeField] private float _generalSoundVolume;
    [SerializeField] private float _musicVolume;
    [SerializeField] private float _sfxVolume;

    public SavesData(float generalSoundVolume, float musicVolume, float sfxVolume)
    {
        _generalSoundVolume = generalSoundVolume;
        _musicVolume = musicVolume;
        _sfxVolume = sfxVolume;
    }

    public float GeneralSoundVolume => _generalSoundVolume;

    public float MusicVolume => _musicVolume;

    public float SfxVolume => _sfxVolume;
}