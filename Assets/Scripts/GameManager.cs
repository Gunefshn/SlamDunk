using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("LEVEL OBJECTS")]
    [SerializeField] private GameObject Platform;
    [SerializeField] private GameObject Baskett;
    [SerializeField] private GameObject BasketGrowth;
    [SerializeField] private GameObject[] PropertyPoint;
    [SerializeField] private AudioSource[] AudioSources;
    [SerializeField] private ParticleSystem[] Effects;
  
    [Header("UI OBJECTS")]
    [SerializeField] private Image[] MissionImages;//görev görselleri
    [SerializeField] private Sprite Sprite;//deðiþecek görsel
    [SerializeField] private int TargetBallNumber;//hedef top sayýsý
    [SerializeField] private GameObject[] Panels;
    [SerializeField] private TextMeshProUGUI[] LevelTexts;

    int BasketCount;
    float FingerPosX;

    private void Start()
    {
       // LevelTexts[0].text = "LEVEL : " +SceneManager.GetActiveScene().name;
         
        for(int i = 0; i < TargetBallNumber; i++)
        {
            MissionImages[i].gameObject.SetActive(true);
        }
        //Invoke("CreateProperty", 3f);
        Debug.Log("lala");
    }
    void CreateProperty()
    {
        int RandomNumber = Random.Range(0,PropertyPoint.Length-1);


        BasketGrowth.transform.position = PropertyPoint[RandomNumber].transform.position;
        BasketGrowth.SetActive(true);
    }
    private void Update()
    {
        if (Time.timeScale != 0)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Vector3 TouchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        FingerPosX = TouchPosition.x- Platform.transform.position.x;
                        break;
                    case TouchPhase.Moved:
                        if(TouchPosition.x - FingerPosX > -1.15 && TouchPosition.x - FingerPosX <1.15)
                        {
                            Platform.transform.position = Vector3.Lerp(Platform.transform.position, new Vector3(TouchPosition.x-FingerPosX, Platform.transform.position.y, Platform.transform.position.z), 5f);
                        }
                        break;
                }
            }
            /*
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (Platform.transform.position.x > -1.15)
                   
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                if (Platform.transform.position.x < 1.15)
                    Platform.transform.position = Vector3.Lerp(Platform.transform.position, new Vector3(Platform.transform.position.x + .3f, Platform.transform.position.y, Platform.transform.position.z), .050f);
            }*/
        }
    }
    public void Basket(Vector3 pos)
    {
        BasketCount++;
        MissionImages[BasketCount-1].sprite = Sprite;
        Effects[0].transform.position = pos;
        Effects[0].gameObject.SetActive(true);
        AudioSources[1].Play();
        if (BasketCount == TargetBallNumber)
        {
            Win();
           //Debug.Log("WIN");
        }
        if(BasketCount==1)
        {
            CreateProperty();
        }
    }
    public void Lose()
    {   
        AudioSources[3].Play();
        Panels[0].SetActive(false);
        Panels[3].SetActive(true);
        LevelTexts[2].text = "LEVEL : " + SceneManager.GetActiveScene().name;
        Time.timeScale = 0;
        //Debug.Log("LOSE");
    }
    void Win()
    {
        AudioSources[2].Play();
        LevelTexts[1].text = "LEVEL : " + SceneManager.GetActiveScene().name;
        Panels[0].SetActive(false);
        Panels[2].SetActive(true);
        Time.timeScale = 0;
    }
    public void GrowthBasket(Vector3 pos)
    {
        Effects[1].transform.position = pos;
        Effects[1].gameObject.SetActive(true);
        AudioSources[0].Play();
        Baskett.transform.localScale = new Vector3(55f, 55f, 55f);
    }
    public void ButtonProcess(string process)
    {
        switch(process)
        {
            case "Stop":
                Panels[0].SetActive(false);
                Panels[1].SetActive(true);
                Time.timeScale = 0;
                break;
            case "Resume":
                Panels[1].SetActive(false);
                Panels[0].SetActive(true);
                Time.timeScale = 1;
                break;
            case "Settings":
                //settings panel
                break ;
            case "Replay":
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                Time.timeScale = 1;
               // Panels[0].SetActive(true);

                break ;
            case "Quit":
                Application.Quit(); 
                break ;
            case "NextLevel":
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
                Time.timeScale = 1;
                break ;

        }
    }
}
