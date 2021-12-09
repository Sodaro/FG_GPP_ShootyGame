using UnityEngine;

public class EnemyActivator : MonoBehaviour
{
    [SerializeField] private Enemy[] _enemies;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController _))
        {
            foreach (var enemy in _enemies)
            {
                if (enemy == null)
                    continue;

                enemy.AssignTarget(other.transform);
            }
        }
    }
}
