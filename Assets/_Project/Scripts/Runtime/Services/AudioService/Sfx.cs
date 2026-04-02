using UnityEngine;

public class Sfx : MonoBehaviour
{
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip _buttonClick;
    [SerializeField] private AudioClip _popUpShow;
    [SerializeField] private AudioClip _popUpHide;
    
    public void PlayButtonClick()
    {
        PlayOneShot(_buttonClick);
    }

    public void PlayPopUpShow()
    {
        PlayOneShot(_popUpShow);
    }

    public void PlayPopUpHide()
    {
        PlayOneShot(_popUpHide);
    }

    public void PlayOneShot(AudioClip clip)
    {
        _source.PlayOneShot(clip);
    }
}