using System;
using UnityEngine;

namespace FiXiK_Utilites.QuitPanel
{
    [Serializable]
    public class ButtonAnimator
    {
        private static readonly int IsEnterHash = Animator.StringToHash("IsEnter");

        [SerializeField] private Animator _animator;

        public void SetEnter() =>
            _animator.SetBool(IsEnterHash, true);

        public void SetExit() =>
            _animator.SetBool(IsEnterHash, false);
    }
}