using System.Collections.Generic;
using UnityEngine;

namespace FiXiKTestScripts
{
    [CreateAssetMenu(fileName = "ColorSetConfig", menuName = Constants.EditorMenuName + "/ColorSet")]
    public class ColorSetConfig : ScriptableObject
    {
        [SerializeField] private List<Color> _colors;

        public IReadOnlyList<Color> Colors => _colors;
    }
}