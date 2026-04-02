using UnityEngine;

public class SettingsPopApp : MonoBehaviour
{
    public void Show() =>
        gameObject.SetActive(true);

    public void Hide() =>
        gameObject.SetActive(false);
}