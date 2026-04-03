using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;

public class ButtonAudio : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private ButtonAudioConfig _audioConfig;

    private IAudioService _audioService;

    [Inject]
    public void Construct(IAudioService audioService) =>
        _audioService = audioService;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_audioConfig != null)
            PlaySound(_audioConfig.Enter);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_audioConfig != null)
            PlaySound(_audioConfig.Exit);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_audioConfig != null)
            PlaySound(_audioConfig.Click);
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null)
            _audioService?.Sfx.PlayOneShot(clip);
    }
}