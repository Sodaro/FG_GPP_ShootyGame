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
                float distance = Vector3.Distance(other.transform.position, enemy.transform.position);
                float delay = 0;
                if (distance <= 20)
                    delay = 0.5f;
                else if (distance <= 40)
                    delay = 1.5f;
                else if (distance <= 60)
                    delay = 2f;
                else
                    delay = 3f;

                enemy.SetHostile(other.transform, delay);
            }
        }
    }
}
