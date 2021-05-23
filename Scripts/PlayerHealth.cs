using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int playerHealth = 10;         // Max player hp
    [SerializeField] int healthDecrease = 1;        // Decrease per enemy 
    [SerializeField] TMP_Text healthText;         
    [SerializeField] AudioClip enemyReachedEnd;

    void Update()
    {
        healthText.text = playerHealth.ToString();
    }
    private void OnTriggerEnter(Collider other)
    {
        GetComponent<AudioSource>().PlayOneShot(enemyReachedEnd); // PlayOneShot instantiates a object that is active until the audio has finished playing
        if(playerHealth != 0)
        {
            playerHealth = playerHealth - healthDecrease; // decrease hp
        }
        else
        {
            healthText.text = "You have died";
        }
    }
}
