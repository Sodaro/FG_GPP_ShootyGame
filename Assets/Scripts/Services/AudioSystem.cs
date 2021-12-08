using UnityEngine;

public class AudioSystem : IService
{
    #region Private Methods
    void IService.Initialize()
    {

    }
    #endregion
    #region Public Methods
    public void StopSound(AudioSource source)
    {
        if (source.isPlaying)
        {
            source.Stop();
        }
    }
    public void PlaySound(AudioSource source, bool loopSound = false)
    {
        //plays the source sound (looping optional)
        source.loop = loopSound;
        source.Play();
    }
    public void PlaySoundRandomPitch(AudioSource source, float lowerPitch, float upperPitch, bool loopSound = false)
    {
        //plays the source sound with random pitch between lower and upper range(inclusive), and optional looping
        float pitch = Random.Range(lowerPitch, upperPitch);
        source.pitch = pitch;
        source.loop = loopSound;
        source.Play();
    }
    #endregion

}
