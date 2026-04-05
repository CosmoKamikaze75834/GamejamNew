using UnityEngine;

public class IntroPopup : MonoBehaviour
{
    [SerializeField] private GameObject _popup;

    private void Start()
    {
        _popup.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ClosePopup()
    {
        _popup.SetActive(false);
        Time.timeScale = 1f;
    }
}