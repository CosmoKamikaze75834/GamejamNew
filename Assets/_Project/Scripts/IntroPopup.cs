using UnityEngine;

public class IntroPopup : MonoBehaviour
{
    public void Show() =>
        gameObject.SetActive(true);

    public void Hide() =>
        gameObject.SetActive(false);
}