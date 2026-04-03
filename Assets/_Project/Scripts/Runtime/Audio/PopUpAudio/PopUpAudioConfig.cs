using UnityEngine;

[CreateAssetMenu(fileName = "PopUpAudioConfig", menuName = Constants.EditorMenuName + "/PopUpAudio")]
public class PopUpAudioConfig : ScriptableObject
{
    [SerializeField] private AudioClip _showing;
    [SerializeField] private AudioClip _hiding;

    public AudioClip ShowingClip => _showing;

    public AudioClip HiddingClip => _hiding;
}