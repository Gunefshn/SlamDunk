using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class GrowthBasket : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI CountDown;
    [SerializeField] private int CountStart;
    [SerializeField] private GameManager _GameManager;
    IEnumerator Start()
    {
        CountDown.text = CountStart.ToString();
        while (true)
        {
            yield return new WaitForSeconds(1f);
            CountStart--;
            CountDown.text = CountStart.ToString();
            if (CountStart == 0)
            {
                gameObject.SetActive(false);
                break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //çarpabilecek baþka obje olmadýðý için tagdan yakalama yapmaya gerek yok 
        gameObject.SetActive(false);
        _GameManager.GrowthBasket(transform.position);
    }
}
