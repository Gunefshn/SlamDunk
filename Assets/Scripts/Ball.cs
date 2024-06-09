using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private GameManager _GameManager;
    [SerializeField] private AudioSource Audio;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Basket"))
        {
            _GameManager.Basket(transform.position);
        }
        else if (other.gameObject.CompareTag("GameOver"))
        {
            _GameManager.Lose();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Audio.Play(); // topun çarptýðý her yerde sekme sesinin oynatýlmasý
    }
}

