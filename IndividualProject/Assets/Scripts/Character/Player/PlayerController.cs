using System;
using Character.NPC.NPCWithDialogue;
using StarterAssets;
using Systems;
using Systems.DialogueSystem;
using UnityEngine;
using Weapons;

namespace Character.Player
{
    public class PlayerController : Character, IAttackCharacter
    {
        public event Action<int> HealthChangedEvent;

        [Header("Settings")] [SerializeField] private int _health;
        [SerializeField] private Weapon _weapon;

        [Header("References")] [SerializeField]
        private ThirdPersonController _thirdPersonController;

        [SerializeField] private PlayerAttackController _playerAttackController;
        [SerializeField] private Transform _spawnPosition;
        [SerializeField] private GameObject _visuals;

        private int _maxHealth;

        public int Health
        {
            get => _health;
            set => _health = value;
        }

        public Weapon Weapon
        {
            get => _weapon;
            set => _weapon = value;
        }

        public ThirdPersonController ThirdPersonController => _thirdPersonController;
        public int MaxHealth => _maxHealth;

        private InputManager InputManager => InputManager.Instance;
        private DialogueManager DialogueManager => DialogueManager.Instance;

        protected override void OnEnable()
        {
            base.OnEnable();

            InputManager.AttackButtonPressedEvent += OnAttackButton;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            InputManager.AttackButtonPressedEvent -= OnAttackButton;
        }

        protected override void Awake()
        {
            base.Awake();

            _maxHealth = Health;
        }

        public void DialogueStart()
        {
            _thirdPersonController.CanMove = false;
            _visuals.SetActive(false);
        }

        public void DialogueEnd()
        {
            _thirdPersonController.CanMove = true;
            _visuals.SetActive(true);
        }

        public void TriggerEnterNPCWithDialogue(NPCWithDialogue npcWithDialogue)
        {
            DialogueManager.TriggerEnterNPCWithDialogue(npcWithDialogue);
        }

        public void TriggerExitNPCWithDialogue()
        {
            DialogueManager.TriggerExitNPCWithDialogue();
        }

        public void TakeDamage(IAttackCharacter iAttackCharacter)
        {
            iAttackCharacter.GetDamage(_weapon.Damage);
        }

        public void GetDamage(int damage)
        {
            _health -= damage;
            if (_health <= 0)
            {
                Die();
            }
            else
            {
                HealthChangedEvent?.Invoke(_health);
            }
        }

        public void Die()
        {
            _thirdPersonController.Teleport(_spawnPosition.position);
        }

        private void OnAttackButton()
        {
            _playerAttackController.StartAttacking();
        }
    }
}