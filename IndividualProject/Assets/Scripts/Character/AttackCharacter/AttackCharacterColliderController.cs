using UnityEngine;

namespace Character.AttackCharacter
{
    public class AttackCharacterColliderController : MonoBehaviour
    {
        [SerializeField] private GameObject _iAttackCharacter;

        public IAttackCharacter IAttackCharacter => _iAttackCharacter.GetComponent<IAttackCharacter>();
    }
}