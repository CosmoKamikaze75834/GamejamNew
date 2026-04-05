using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FiXiKTestScripts
{
    public class LineStatsView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _number;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _count;
        [SerializeField] private Image _fillImage;
        [SerializeField] private int _lenghtName;

        public void UpdateStats(
            int number,
            string name,
            int count,
            int totalCount,
            Color color)
        {
            _number.text = $"{number}.";
            _name.text = TruncateWithDots(name);
            _count.text = count.ToString();
            _fillImage.fillAmount = CalculateFill(count, totalCount);
            _fillImage.color = color;
        }

        private string TruncateWithDots(string text)
        {
            if (text.Length <= _lenghtName)
                return text;

            return text[..(_lenghtName - 3)] + "...";
        }

        private float CalculateFill(int count, int totalCount) =>
            Mathf.Clamp01((float)count / totalCount);
    }
}