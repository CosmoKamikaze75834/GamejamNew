using UnityEngine;

public class Sfx : MonoBehaviour
{
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip _buttonClick;
    [SerializeField] private AudioClip _popUpShow;
    [SerializeField] private AudioClip _popUpHide;
    
    public void PlayButtonClick()
    {
        Play(_buttonClick);
    }

    public void PlayPopUpShow()
    {
        Play(_popUpShow);
    }

    public void PlayPopUpHide()
    {
        Play(_popUpHide);
    }

    private void Play(AudioClip clip)
    {
        _source.PlayOneShot(clip);
    }
}