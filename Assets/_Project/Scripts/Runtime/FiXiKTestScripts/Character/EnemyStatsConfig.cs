using UnityEngine;

namespace FiXiKTestScripts
{
    [CreateAssetMenu(fileName = nameof(EnemyStatsConfig), menuName = Constants.EditorMenuName + "/EnemyStats")]
    public class EnemyStatsConfig : ScriptableObject
    {
        [SerializeField] private EnemyStats _enemyStats;
        [SerializeField] private LangData _difficultyLevel;

        public EnemyStats Stats => _enemyStats;

        public LangData DifficultyLevel => _difficultyLevel;
    }
}