using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Character.Player.Presenters
{
    public class HealthPresenter : MonoBehaviour
    {
        [SerializeField] private Image _healthBar;
        [SerializeField] private TextMeshProUGUI _healthText;

        [SerializeField] private PlayerController _playerController;

        private void OnEnable()
        {
            _playerController.HealthChangedEvent += HealthChanged;
        }

        private void OnDisable()
        {
            _playerController.HealthChangedEvent -= HealthChanged;
        }

        private void Start()
        {
            HealthChanged(_playerController.Health);
        }

        private void HealthChanged(int health)
        {
            _healthBar.fillAmount = (float)health / _playerController.MaxHealth;
            _healthText.text = $"{health}/{_playerController.MaxHealth}";
        }
    }
}