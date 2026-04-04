using UnityEngine;
using UnityEngine.UI;

namespace FiXiK_Utilites.QuitPanel
{
    public class CustomButtonShape : MonoBehaviour
    {
        [SerializeField] private float _alpha = 0.1f;

        private void Awake() =>
            GetComponent<Image>().alphaHitTestMinimumThreshold = _alpha;
    }
}