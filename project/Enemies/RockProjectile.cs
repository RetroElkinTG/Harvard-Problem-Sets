using UnityEngine;

// Rock projectile behaviour
public class RockProjectile : ProjectileManager
{
    [Header("Audio Variables")]
    public AudioSource launchAudio;

    void Awake()
    {
        launchAudio.Play();
    }
}