using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealhSystem : MonoBehaviour
{
    [SerializeField] int health_Points = 5;

    // Particles
    [SerializeField] ParticleSystem deathParticlePrefab;
    [SerializeField] ParticleSystem hitParticlePrefab;

    // Audio 
    [Range(0f, 1f)]
    [SerializeField] float DeathVolume = 1f;
    [SerializeField] AudioClip DeathSFX;

    [Range(0f, 1f)]
    [SerializeField] float HitVolume = 1f;
    [SerializeField] AudioClip HitSFX;



    AudioSource myAudioSource;
    private void Awake()
    {
        myAudioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage(int amount) 
    {
        health_Points -= amount;
        hitParticlePrefab.Play();
        myAudioSource.volume = HitVolume;
        myAudioSource.PlayOneShot(HitSFX);
        if (health_Points <= 0)
            Die();
    }

    public void TakeDamage(int amount, ParticleSystem playParticleOnImpact) // Takes damage and instantiates particle on collision point
    {
        var vfx = Instantiate(playParticleOnImpact, transform.position + new Vector3(-3f, 3f, 0f), Quaternion.identity);
        vfx.Play();

        float destroyDelay = vfx.main.duration;
        Destroy(vfx.gameObject, destroyDelay);

        TakeDamage(amount);
    }
    public void Die() // Plays death particle and destroys object
    {
        var vfx = Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);
        vfx.Play();

        float destroyDelay = vfx.main.duration;
        Destroy(vfx.gameObject, destroyDelay);

        AudioSource.PlayClipAtPoint(DeathSFX, Camera.main.transform.position, DeathVolume);
        Destroy(gameObject);
    }
}
