using UnityEngine;

public class DeathBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IHealth entity))
        {
            entity.ReduceHealth(1000);
        }
    }
}
