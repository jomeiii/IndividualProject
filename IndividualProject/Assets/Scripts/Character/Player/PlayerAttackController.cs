using UnityEngine;

namespace Character.Player
{
    public class PlayerAttackController : MonoBehaviour
    {
        [Header("Settings")] [SerializeField] private bool _isAttacking;

        [Header("References")] [SerializeField]
        private PlayerAnimatorController _playerAnimatorController;

        [SerializeField] private PlayerController _playerController;

        public void StartAttacking()
        {
            if (_isAttacking) return;

            _isAttacking = true;
            _playerController.Weapon.IsAttacking = true;
            _playerAnimatorController.StartAttackAnimation();
        }

        private void StopAttacking()
        {
            _isAttacking = false;
            _playerAnimatorController.StopAttackAnimation();
        }
    }
}