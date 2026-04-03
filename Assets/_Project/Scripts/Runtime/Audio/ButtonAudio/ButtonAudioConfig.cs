using UnityEngine;

[CreateAssetMenu(fileName = "ButtonAudioConfig", menuName = Constants.EditorMenuName + "/ButtonAudio")]
public class ButtonAudioConfig : ScriptableObject
{
    [SerializeField] private AudioClip _enter;
    [SerializeField] private AudioClip _exit;
    [SerializeField] private AudioClip _click;

    public AudioClip Enter => _enter;

    public AudioClip Exit => _exit;

    public AudioClip Click => _click;    
}