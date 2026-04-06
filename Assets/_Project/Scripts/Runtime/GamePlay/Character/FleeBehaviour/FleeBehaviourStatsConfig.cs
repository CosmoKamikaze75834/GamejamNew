using UnityEngine;

namespace FiXiKTestScripts
{
    [CreateAssetMenu(fileName = nameof(FleeBehaviourStatsConfig), menuName = Constants.EditorMenuName + "/FleeBehaviourStats")]
    public class FleeBehaviourStatsConfig : ScriptableObject
    {
        [SerializeField] private FleeBehaviourStats _fleeBehaviourStats;

        public FleeBehaviourStats FleeBehaviourStats => _fleeBehaviourStats;
    }
}