using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    #region Serialized Fields

    #endregion
    #region Private Fields
    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody.TryGetComponent(out PlayerController player))
        {
            player.IncreaseHealth(20f);
            Destroy(gameObject);
        }
    }
    #endregion
    #region Private Methods

    #endregion
    #region Public Methods

    #endregion
}
