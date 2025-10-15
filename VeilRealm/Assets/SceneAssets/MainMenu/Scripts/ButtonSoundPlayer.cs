using UnityEngine;

public class ButtonSoundPlayer : MonoBehaviour
{
    // public AudioClip clickClip;
    // [Range(0f, 1f)]
    // public float clickVolume = 1f;

    public void PlayClip(AudioClip clickClip)
    {
        if (clickClip == null)
        {
            Debug.LogWarning("ButtonSoundPlayer: clickClip is null");
            return;
        }

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayOneShot(clickClip);
        }
        else
        {
            Debug.LogWarning("ButtonSoundPlayer: AudioManager.Instance is null. Did you include the AudioManager in a boot scene?");
        }
    }
}