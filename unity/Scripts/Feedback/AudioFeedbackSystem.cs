using UnityEngine;

public class AudioFeedbackSystem : MonoBehaviour
{
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip levelUpClip;
    [SerializeField] private AudioClip chestOpenClip;
    [SerializeField] private AudioClip nearDeathClip;

    public void PlayLevelUp()
    {
        Play(levelUpClip);
    }

    public void PlayChestOpen()
    {
        Play(chestOpenClip);
    }

    public void PlayNearDeath()
    {
        Play(nearDeathClip);
    }

    private void Play(AudioClip clip)
    {
        if (sfxSource != null && clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
}
