using System;
using UnityEngine;

namespace FiXiKTestScripts
{
    public interface IAttacker
    {
        public event Action CountChanged;

        public Color Color { get; }

        public Transform Transform { get; }

        public int RecruitsCount { get; }

        public LangData TeamName { get; }

        public void AddRecruit(Npc npc);

        public void RemoveRecruit(Npc npc);
    }
}