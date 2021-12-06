namespace Observer
{
    public class EventHandler : IService
    {
        public delegate void OnEnemyDeath();
        public event OnEnemyDeath onEnemyDeath;
        public void RaiseOnEnemyDeath()
        {
            if (onEnemyDeath != null)
                onEnemyDeath.Invoke();
        }

        public delegate void OnPlayerJump();
        public event OnPlayerJump onPlayerJump;
        public void RaiseOnPlayerJump()
        {
            if (onEnemyDeath != null)
                onPlayerJump.Invoke();
        }

        void IService.Initialize()
        {

        }
    }
}

