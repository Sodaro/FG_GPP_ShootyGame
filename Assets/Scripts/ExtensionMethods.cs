using UnityEngine;

public static class ExtensionMethods
{
    public static void StopSound(this AudioSource source)
    {
        if (source.isPlaying)
        {
            source.Stop();
        }
    }
    public static void PlaySound(this AudioSource source, bool loopSound = false)
    {
        //plays the source sound (looping optional)
        source.loop = loopSound;
        source.Play();
    }
    public static void PlaySoundRandomPitch(this AudioSource source, float lowerPitch, float upperPitch, bool loopSound = false)
    {
        //plays the source sound with random pitch between lower and upper range(inclusive), and optional looping
        float pitch = Random.Range(lowerPitch, upperPitch);
        source.pitch = pitch;
        source.loop = loopSound;
        source.Play();
    }
}
