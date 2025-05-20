using Character.Player;
using UnityEngine;

namespace Controllers
{
    public class GoldenKeyContainerController : MonoBehaviour
    {
        [SerializeField] private GoldenKeyController _goldenKeyController;
        [SerializeField] private TriggerController _triggerController;
        [SerializeField] private GameObject _visuals;

        private void OnEnable()
        {
            _triggerController.OnTriggerEnterEvent += TriggerEnter;
            _goldenKeyController.GoldenKeyPressedEvent += OnGoldenKeyPress;
        }

        private void OnDisable()
        {
            _triggerController.OnTriggerEnterEvent -= TriggerEnter;
            _goldenKeyController.GoldenKeyPressedEvent -= OnGoldenKeyPress;
        }

        private void OnGoldenKeyPress()
        {
            _visuals.SetActive(true);
        }

        private void TriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerController playerController))
            {
                if (playerController.hasGoldenKey)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}