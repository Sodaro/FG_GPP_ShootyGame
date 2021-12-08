using Observer;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField] private Image _effectImage = null;
    [SerializeField] private Image _deathImage = null;
    [SerializeField] private TMP_Text _healthText = null;
    #endregion
    #region Private Fields
    private delegate void OnFadeFinished();
    private event OnFadeFinished _onFadeFinished;
    private Coroutine _coroutine = null;
    #endregion
    #region Private Methods
    private void OnDisable()
    {
        ServiceLocator.Instance.Get<EventHandler>().onPlayerDamageTaken -= ActivateEffectPanel;
        ServiceLocator.Instance.Get<EventHandler>().onPlayerHeal -= ActivateEffectPanel;
        ServiceLocator.Instance.Get<EventHandler>().onPlayerDeath -= ActivateDeathPanel;
    }

    private void OnEnable()
    {
        ServiceLocator.Instance.Get<EventHandler>().onPlayerDamageTaken += ActivateEffectPanel;
        ServiceLocator.Instance.Get<EventHandler>().onPlayerHeal += ActivateEffectPanel;
        ServiceLocator.Instance.Get<EventHandler>().onPlayerDeath += ActivateDeathPanel;
    }

    private void UpdateHealthText(int newValue)
    {
        _healthText.text = newValue.ToString();
    }

    private void ActivateEffectPanel(int newHealth)
    {
        if (_coroutine == null)
        {
            Color color = Color.black;
            bool success = int.TryParse(_healthText.text, out int oldHealth);
            if (success)
            {
                color = newHealth > oldHealth ? Color.cyan : Color.red;
            }
            UpdateHealthText(newHealth);
            _effectImage.gameObject.SetActive(true);
            _coroutine = StartCoroutine(FadeImage(_effectImage, color, 0.5f, disableOnDone: true));
        }
    }

    private void ActivateDeathPanel()
    {
        _healthText.text = "0";
        _deathImage.gameObject.SetActive(true);
        _onFadeFinished += ReloadScene;
        _coroutine = StartCoroutine(FadeImage(_deathImage, Color.black, 2f, fadeIn: true, disableOnDone: false));
    }
    private void ReloadScene()
    {
        _onFadeFinished -= ReloadScene;
        ServiceLocator.Instance.Get<SceneHandler>().RestartActiveScene();
    }
    private void SwapColors(ref Color c1, ref Color c2)
    {
        Color temp = c2;
        c2 = c1;
        c1 = temp;
    }

    private IEnumerator FadeImage(Image image, Color startColor, float duration, bool fadeIn = false, bool disableOnDone = false)
    {
        float f = 0;
        Color endColor;
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
        _coroutine = null;
        if (_onFadeFinished != null)
            _onFadeFinished.Invoke();
    }

    #endregion
}
