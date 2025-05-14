using UnityEngine;

namespace Character.Player
{
    public class PlayerAttackController : MonoBehaviour
    {
        [Header("References")] [SerializeField]
        private PlayerAnimatorController _playerAnimatorController;

        [SerializeField] private PlayerController _playerController;

        public void StartAttacking()
        {
            if (_playerController.Weapon.IsAttacking) return;

            // Debug.Log("StartAttacking");
            
            _playerController.Weapon.IsAttacking = true;
            _playerAnimatorController.StartAttackAnimation();
        }

        private void StopAttacking()
        {
            if (!_playerController.Weapon.IsAttacking) return;
            
            // Debug.Log("StopAttacking");

            _playerController.Weapon.IsAttacking = false;
            _playerAnimatorController.StopAttackAnimation();
        }

        public void CanAttackChangeValue(bool state)
        {
            _playerController.Weapon.CanAttack = state;
        }
    }
}