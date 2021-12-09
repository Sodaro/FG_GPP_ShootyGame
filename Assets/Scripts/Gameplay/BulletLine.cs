using System.Collections;
using UnityEngine;

public class BulletLine : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    #region Public Methods
    public void SetLinePositions(Vector3 startPos, Vector3 endPos)
    {
        _lineRenderer.SetPositions(new Vector3[2] { startPos, endPos });
        StartCoroutine(DisableAfterTime(0.5f));
    }
    #endregion
    private IEnumerator DisableAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        LinePool.Instance.AddToPool(this);
    }
}
