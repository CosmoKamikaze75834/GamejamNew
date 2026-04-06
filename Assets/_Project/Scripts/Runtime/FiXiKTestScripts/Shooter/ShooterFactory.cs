namespace FiXiKTestScripts
{
    public class ShooterFactory
    {
        private readonly Bullet _bulletPrefab;

        public ShooterFactory(Bullet bulletPrefab)
        {
            _bulletPrefab = bulletPrefab;
        }

        public Shooter Get(IAttacker attacker, float reloadTime) =>
            new(attacker, _bulletPrefab, reloadTime);
    }
}