using UnityEngine;

public class PopupSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject _currentPopup;
    [SerializeField] private GameObject _nextPopup;

    public void OpenNext()
    {
        _currentPopup.SetActive(false);
        _nextPopup.SetActive(true);
    }
}