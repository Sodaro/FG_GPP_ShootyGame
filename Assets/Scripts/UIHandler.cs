using Observer;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField] private Image damageImage = null;
    [SerializeField] private Image deathImage = null;
    #endregion
    #region Private Fields
    private delegate void OnFadeFinished();
    private event OnFadeFinished onFadeFinished;
    private Coroutine coroutine = null;
    #endregion
    #region Private Methods
    private void OnDisable()
    {
        ServiceLocator.Current.Get<EventHandler>().onPlayerDamageTaken -= ActivateDamagePanel;
        ServiceLocator.Current.Get<EventHandler>().onPlayerDeath -= ActivateDeathPanel;
    }
    private void OnEnable()
    {
        ServiceLocator.Current.Get<EventHandler>().onPlayerDamageTaken += ActivateDamagePanel;
        ServiceLocator.Current.Get<EventHandler>().onPlayerDeath += ActivateDeathPanel;
    }

    private void ActivateDamagePanel()
    {
        if (coroutine == null)
        {
            damageImage.gameObject.SetActive(true);
            coroutine = StartCoroutine(FadeImage(damageImage, 0.5f, disableOnDone: true));
        }
    }

    private void ActivateDeathPanel()
    {
        deathImage.gameObject.SetActive(true);
        onFadeFinished += ReloadScene;
        coroutine = StartCoroutine(FadeImage(deathImage, 2f, fadeIn: true, disableOnDone: false));
    }
    private void ReloadScene()
    {
        onFadeFinished -= ReloadScene;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void SwapColors(ref Color c1, ref Color c2)
    {
        Color temp = c2;
        c2 = c1;
        c1 = temp;
    }

    private IEnumerator FadeImage(Image image, float duration, bool fadeIn = false, bool disableOnDone = false)
    {
        float f = 0;
        Color startColor, endColor;
        startColor = image.color;
        endColor = startColor;
        endColor.a = 0;

        if (fadeIn)
        {
            SwapColors(ref startColor, ref endColor);
        }

        while (f < duration)
        {
            image.color = Color.Lerp(startColor, endColor, f / duration);
            f += Time.deltaTime;
            yield return null;
        }

        if (disableOnDone)
        {
            image.gameObject.SetActive(false);
            image.color = startColor;
        }
        coroutine = null;
        if (onFadeFinished != null)
            onFadeFinished.Invoke();
    }

    #endregion
}
