using UnityEngine;

public class Sfx : MonoBehaviour
{
    [SerializeField] private AudioSource _source;

    public void PlayOneShot(AudioClip clip) =>
        _source.PlayOneShot(clip);
}