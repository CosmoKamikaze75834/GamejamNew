using UnityEngine;

namespace FiXiKTestScripts
{
    [CreateAssetMenu(fileName = nameof(WandererStatsConfig), menuName = Constants.EditorMenuName + "/WandererStats")]
    public class WandererStatsConfig : ScriptableObject
    {
        [SerializeField] private WandererStats _wandererStats;

        public WandererStats WandererStats => _wandererStats;
    }
}