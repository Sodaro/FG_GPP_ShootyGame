using UnityEngine;

public class EnemyActivator : MonoBehaviour
{
    [SerializeField] private Enemy[] enemies;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController controller))
        {
            foreach (var enemy in enemies)
            {
                if (enemy == null)
                    continue;
                enemy.AssignTarget(other.transform);
            }
        }
    }
}
