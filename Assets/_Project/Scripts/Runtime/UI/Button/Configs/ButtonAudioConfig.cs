using UnityEngine;

[CreateAssetMenu(fileName = "NewButtonAudioConfig", menuName = "Configs/New ButtonAudioConfig", order = 0)]
public class ButtonAudioConfig : ScriptableObject
{
    [SerializeField] private AudioClip _enter;
    [SerializeField] private AudioClip _exit;
    [SerializeField] private AudioClip _click;

    public AudioClip Enter => _enter;

    public AudioClip Exit => _exit;

    public AudioClip Click => _click;    
}