using System;
using System.Collections.Generic;

namespace FiXiKTestScripts
{
    public class NpcRegistry : IDisposable
    {
        private readonly NpcFactory _npcFactory;

        private readonly List<Npc> _npcList = new();

        public NpcRegistry(NpcFactory npcFactory)
        {
            _npcFactory = npcFactory;

            npcFactory.NpcCreated += OnCreated;
        }

        public int Count => _npcList.Count;

        public void Dispose()
        {
            if (_npcFactory != null)
                _npcFactory.NpcCreated -= OnCreated;
        }

        private void OnCreated(Npc npc) =>
            _npcList.Add(npc);
    }
}