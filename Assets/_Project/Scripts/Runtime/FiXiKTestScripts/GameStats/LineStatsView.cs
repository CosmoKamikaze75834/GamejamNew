using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FiXiKTestScripts
{
    public class LineStatsView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _number;
        [SerializeField] private TMP_Text _count;
        [SerializeField] private TextLanguage _teamName;
        [SerializeField] private Image _fillImage;
        [SerializeField] private int _lenghtName;

        public void UpdateStats(
            int number,
            LangData teamName,
            int count,
            int totalCount,
            Color color)
        {
            _number.text = $"{number}.";
            _teamName.SetData(TruncateWithDots(teamName));
            _count.text = count.ToString();
            _fillImage.fillAmount = CalculateFill(count, totalCount);
            _fillImage.color = color;
        }

        private LangData TruncateWithDots(LangData text)
        {
            string ru = text.Get(LangType.Ru);
            string en = text.Get(LangType.En);

            string truncatedRu = TruncateWithDots(ru);
            string truncatedEn = TruncateWithDots(en);

            return new LangData(truncatedRu, truncatedEn);
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