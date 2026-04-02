using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip _menuClip;
    [SerializeField] private AudioClip _gameClip;

    public void Stop()
    {
        _source.Stop();
    }

    public void PlayMenuMusic()
    {
        Play(_menuClip);
    }

    public void PlayGameMusic()
    {
        Play(_gameClip);
    }

    private void Play(AudioClip clip)
    {
        _source.clip = clip;
        _source.Play();
    }
}