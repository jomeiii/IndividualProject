using Weapons;

namespace Character
{
    public interface IAttackCharacter
    {
        public int Health { get; set; }
        public Weapon Weapon { get; set; }
        
        public void TakeDamage(IAttackCharacter iAttackCharacter);
        public void GetDamage(int damage);
        public void Die();

        public void CanAttackOn();
        public void CanAttackOff();
    }
}