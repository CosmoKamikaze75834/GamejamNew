using System.Collections.Generic;
using UnityEngine;

namespace FiXiKTestScripts
{
    [CreateAssetMenu(fileName = "ConspiracyTheoryConfig", menuName = Constants.EditorMenuName + "/ConspiracyTheory")]
    public class ConspiracyTheoryConfig : ScriptableObject
    {
        [SerializeField] private List<LangData> _theories;

        public IReadOnlyList<LangData> Theories => _theories;
    }
}