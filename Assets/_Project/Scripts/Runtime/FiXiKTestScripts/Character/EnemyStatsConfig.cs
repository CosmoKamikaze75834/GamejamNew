using UnityEngine;

namespace FiXiKTestScripts
{
    [CreateAssetMenu(fileName = nameof(EnemyStatsConfig), menuName = Constants.EditorMenuName + "/EnemyStats")]
    public class EnemyStatsConfig : ScriptableObject
    {
        [SerializeField] private EnemyStats _enemyStats;

        public EnemyStats Stats => _enemyStats;
    }
}