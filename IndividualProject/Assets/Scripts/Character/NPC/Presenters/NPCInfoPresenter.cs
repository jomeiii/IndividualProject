using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Character.NPC.Presenters
{
    public class NPCInfoPresenter : MonoBehaviour
    {
        public event Action<int, int> HealthChangedEvent;

        [Header("References")] [SerializeField]
        private NPC _npc;

        [SerializeField] private Transform _cnavas;
        [SerializeField] private RectTransform _npcInfoImage;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private GameObject _hpBG;
        [SerializeField] private Image _hpImage;
        [SerializeField] private TextMeshProUGUI _hpText;

        private Transform _transform;

        private void OnEnable()
        {
            HealthChangedEvent += HealthChange;
        }

        private void OnDisable()
        {
            HealthChangedEvent -= HealthChange;
        }

        private void Awake()
        {
            _transform = transform;
        }

        public void SetNameText()
        {
            _nameText.text = _npc.npcName;
        }

        public void UpdateNPCInfoRotation(Transform target, bool needInvert)
        {
            _transform.LookAt(target);
            float direction = needInvert ? -1f : 1f;
            _cnavas.localScale = new Vector3(direction * Mathf.Abs(_cnavas.localScale.x), _cnavas.localScale.y, _cnavas.localScale.z);
            _transform.rotation = Quaternion.Euler(0, _transform.rotation.eulerAngles.y, 0);
        }


        public void OnHealthChanged(int health, int maxHealth)
        {
            HealthChangedEvent?.Invoke(health, maxHealth);
        }

        private void HealthChange(int health, int maxHealth)
        {
            if (health == -1 && maxHealth == -1)
            {
                CloseHPBar();
            }

            _hpImage.fillAmount = (float)health / maxHealth;
            _hpText.text = $"{health}/{maxHealth}";
        }

        private void CloseHPBar()
        {
            _hpImage.gameObject.SetActive(false);
            _hpText.gameObject.SetActive(false);
            _hpBG.SetActive(false);
            _npcInfoImage.localPosition -= new Vector3(0, _npcInfoImage.rect.height / 4, 0);

            var newSize = new Vector2(_npcInfoImage.rect.width, _npcInfoImage.rect.height / 2);
            _npcInfoImage.sizeDelta = newSize;
        }
    }
}