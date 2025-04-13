using TMPro;
using UnityEngine;

namespace Character.Player
{
    public class PlayerAnimatorController : MonoBehaviour
    {
        private static readonly int Attack = Animator.StringToHash("Attack");

        [Header("Settings")] [SerializeField] private int _attackAnimationCount;

        [Header("References")] [SerializeField]
        private Animator _animator;

        public void StartAttackAnimation()
        {
            _animator.SetInteger(Attack, Random.Range(1, _attackAnimationCount + 1));
            _animator.applyRootMotion = true;
        }

        public void StopAttackAnimation()
        {
            _animator.SetInteger(Attack, 0);
            _animator.applyRootMotion = false;
        }
    }
}