namespace Observer
{
    public class EventHandler : IService
    {
        public delegate void OnPlayerDeath();
        public delegate void OnPlayerHeal();
        public delegate void OnPlayerDamageTaken();
        public delegate void OnPlayerJump();
        public delegate void OnEnemyDeath();

        public event OnPlayerDeath onPlayerDeath;
        public event OnPlayerHeal onPlayerHeal;
        public event OnPlayerDamageTaken onPlayerDamageTaken;
        public event OnPlayerJump onPlayerJump;
        public event OnEnemyDeath onEnemyDeath;

        public void RaiseOnEnemyDeath()
        {
            if (onEnemyDeath != null)
            {
                onEnemyDeath.Invoke();
            }
        }

        public void RaiseOnPlayerJump()
        {
            if (onPlayerJump != null)
            {
                onPlayerJump.Invoke();
            }
        }

        public void RaiseOnPlayerHeal()
        {
            if (onPlayerHeal != null)
            {
                onPlayerHeal.Invoke();
            }
        }

        public void RaiseOnPlayerDamageTaken()
        {
            if (onPlayerDamageTaken != null)
            {
                onPlayerDamageTaken.Invoke();
            }

        }

        public void RaiseOnPlayerDeath()
        {
            if (onPlayerDeath != null)
            {
                onPlayerDeath.Invoke();
            }
        }

        void IService.Initialize()
        {

        }
    }
}

