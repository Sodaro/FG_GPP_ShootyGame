using UnityEngine;

[SelectionBase]
public class HealthPickup : MonoBehaviour
{
    [SerializeField] private float _healAmount = 20f;
    [SerializeField] private bool _canOverHeal = false;
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody.TryGetComponent(out PlayerController player))
        {
            if (player.CanHeal() || _canOverHeal && player.CanOverHeal())
            {
                player.IncreaseHealth(_healAmount, _canOverHeal);
                Destroy(gameObject);
            }
        }
    }
}
