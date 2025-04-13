using UnityEngine;

namespace Character.AttackCharacter
{
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(Rigidbody))]
    public class AttackCharacterColliderController : MonoBehaviour
    {
        [SerializeField] private GameObject _iAttackCharacter;

        public IAttackCharacter IAttackCharacter => _iAttackCharacter.GetComponent<IAttackCharacter>();
    }
}