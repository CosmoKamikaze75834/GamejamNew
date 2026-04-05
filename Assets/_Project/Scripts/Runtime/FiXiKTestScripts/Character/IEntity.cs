using UnityEngine;

namespace FiXiKTestScripts
{
    public interface IEntity 
    {
        Transform Transform { get; }

        Color Color { get; }
    }
}