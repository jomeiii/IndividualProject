using Character.Player;
using UnityEngine;

namespace Controllers
{
    public class GoldenKeyContainerController : MonoBehaviour
    {
        [SerializeField] private TriggerController _triggerController;

        private void OnEnable()
        {
            _triggerController.OnTriggerEnterEvent += TriggerEnter;
        }

        private void OnDisable()
        {
            _triggerController.OnTriggerEnterEvent -= TriggerEnter;
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