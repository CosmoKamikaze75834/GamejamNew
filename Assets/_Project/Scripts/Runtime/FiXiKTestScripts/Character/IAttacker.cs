using UnityEngine;

namespace FiXiKTestScripts
{
    public interface IAttacker
    {
        Color Color { get; }

        Transform Transform { get; }

        int RecruitsCount { get; }

        void AddRecruit(Npc npc);

        void RemoveRecruit(Npc npc);
    }
}