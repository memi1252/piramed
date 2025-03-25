using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] public float PlayerHealth = 100;
    [SerializeField] public float PlayerOxygen = 100;
    [SerializeField] public float OxygenDecrease = 1;
    [SerializeField] public int price = 0;
    [SerializeField] public bool isMove = true;
    [SerializeField] public bool GameStart = true;
    [SerializeField] public bool NotLookEnemy = false;
    [SerializeField] public bool Coin2X;
    
    [Header("박스")]
    [SerializeField] public List<Box> Boxs;
    [SerializeField] public List<Box> openBoxs;
    [SerializeField] public List<Box> NowOpenBoxs;

    [Header("플레이어")] [SerializeField] public Player Player;
    public float maxPlayerHealth = 100;
    public float maxPlayerOxygen = 100;

    [SerializeField]
    public GameObject GlobalLight;
    
    public bool boxWhere = false;
    

    private void Update()
    {
        hp();
        oxygen();
        boxOpen();
        BoxWhere();
        SpeedUpTimerUpdate();
    }
    
    public void InGameStart()
    {
        PlayerOxygen = maxPlayerOxygen;
        PlayerHealth = maxPlayerHealth;
        StartCoroutine(GameStartCou());
    }

    private IEnumerator GameStartCou()
    {
        UIManager.Instance.gameMenuUI.Hide();
        GlobalLight.SetActive(false);
        Light2D playerLight2D = Player.GetComponent<Light2D>();
        playerLight2D.enabled = true;

        
        while (playerLight2D.intensity >= 0)
        {
            playerLight2D.intensity = Mathf.Lerp(playerLight2D.intensity, -1f, 2f * Time.deltaTime);
            yield return null; 
        }

        yield return new WaitForSeconds(0.8f);

        playerLight2D.intensity = 0;
        playerLight2D.pointLightOuterRadius = 4;

        Player.transform.position = new Vector3(0, 0, 0);
        while (playerLight2D.intensity <= 1f)
        {
            playerLight2D.intensity = Mathf.Lerp(playerLight2D.intensity, 2f, 2f * Time.deltaTime);
            yield return null; 
        }
        playerLight2D.intensity = 1f;
        
        GameStart = true;
        isMove = true;
        PlayerHealth = maxPlayerHealth;
        PlayerOxygen = maxPlayerOxygen;
        UIManager.Instance.statusUI.Show();
    }
    
    public void ISDungenExit(Vector3 pos)
    {
        StartCoroutine(DungenExit(pos));
    }
    
    
    private IEnumerator DungenExit(Vector3 pos)
    {
        
        Light2D playerLight2D = Player.GetComponent<Light2D>();
        
        while (playerLight2D.intensity >= 0)
        {
            playerLight2D.intensity = Mathf.Lerp(playerLight2D.intensity, -1f, 2f * Time.deltaTime);
            yield return null; 
        }

        yield return new WaitForSeconds(0.8f);

        playerLight2D.intensity = 0;
        playerLight2D.pointLightOuterRadius = 14;

        Player.transform.position = new Vector3(-88, 14, 0);
        while (playerLight2D.intensity <= 1f)
        {
            playerLight2D.intensity = Mathf.Lerp(playerLight2D.intensity, 2f, 2f * Time.deltaTime);
            yield return null; 
        }
        playerLight2D.intensity = 1f;
        
        playerLight2D.enabled = false;
        GlobalLight.SetActive(true);
        UIManager.Instance.gameMenuUI.Show();
        if(UIManager.Instance.inventoryUI.gameObject.activeSelf)
        {
            UIManager.Instance.inventoryUI.Hide();
        }
    }
    
    private void boxOpen()
    {
        
        for (int i = Boxs.Count - 1; i >= 0; i--)
        {
            var box = Boxs[i];
            if (box.isOpen && !box.buffItem)
            {
                openBoxs.Add(box);
                NowOpenBoxs.Add(box);
                Boxs.RemoveAt(i);
            }
        }
    }

    private void hp()
    {
        
        ItemManager itemManager = ItemManager.Instance;
        if (PlayerHealth > maxPlayerHealth)
        {
            PlayerHealth = maxPlayerHealth;
        }
        
        if (PlayerHealth <= 0)
        {
            UIManager.Instance.statusUI.Hide();
            GameStart = false;
            isMove = false;
            PlayerHealth = maxPlayerHealth;
            PlayerOxygen = maxPlayerOxygen;
            ISDungenExit(new Vector3(-88, 14, 0));
            itemManager.Inventoryitmes.Clear();
            foreach (Item item in itemManager.NowGetItemList)
            {
                itemManager.ItemList.Add(item);
            }
            itemManager.NowGetItemList.Clear();
            for (int i = NowOpenBoxs.Count - 1; i >= 0; i--)
            {
                NowOpenBoxs[i].isOpen = false;
                NowOpenBoxs[i].GetComponent<Animator>().SetBool("BoxOpen", false);
                Boxs.Add(NowOpenBoxs[i]);
                for (int j = openBoxs.Count -1; j >=0; j--)
                {
                    if(openBoxs[j] == NowOpenBoxs[i])
                    {
                        openBoxs.RemoveAt(j);
                    }
                }
            }
            NowOpenBoxs.Clear();
        }
    }
    
    private void oxygen()
    {
        ItemManager itemManager = ItemManager.Instance;
        if (PlayerOxygen > maxPlayerOxygen)
        {
            PlayerOxygen = maxPlayerOxygen;
        }
        
        if (GameStart)
        {
            PlayerOxygen -= OxygenDecrease * Time.deltaTime;
        }

        if (PlayerOxygen <= 0)
        {
            UIManager.Instance.statusUI.Hide();
            GameStart = false;
            isMove = false;
            PlayerHealth = maxPlayerHealth;
            PlayerOxygen = maxPlayerOxygen;
            ISDungenExit(new Vector3(-88, 14, 0));
            itemManager.Inventoryitmes.Clear();
            foreach (Item item in itemManager.NowGetItemList)
            {
                itemManager.ItemList.Add(item);
            }
            itemManager.NowGetItemList.Clear();
            for (int i = NowOpenBoxs.Count - 1; i >= 0; i--)
            {
                NowOpenBoxs[i].isOpen = false;
                NowOpenBoxs[i].GetComponent<Animator>().SetBool("BoxOpen", false);
                Boxs.Add(NowOpenBoxs[i]);
                for (int j = openBoxs.Count -1; j >=0; j--)
                {
                    if(openBoxs[j] == NowOpenBoxs[i])
                    {
                        openBoxs.RemoveAt(j);
                    }
                }
            }
            NowOpenBoxs.Clear();
        }
    }
    private int Boxindex;
    private float boxdistance;
    private void BoxWhere()
    {
        if(boxWhere)
        {
            UIManager.Instance.statusUI.BoxWhereText.gameObject.SetActive(true);
            if (Boxindex == -1)
            {
                Boxindex = UnityEngine.Random.Range(0, Boxs.Count);
                while (Boxs[Boxindex].buffItem)
                {
                    Boxindex = UnityEngine.Random.Range(0, Boxs.Count);
                }
            }
            boxdistance = Vector3.Distance(Player.transform.position, Boxs[Boxindex].transform.position);
            UIManager.Instance.statusUI.BoxWhereText.text = $"상자까지 거리 {(int) boxdistance}m";
        }
        else
        {
            UIManager.Instance.statusUI.BoxWhereText.gameObject.SetActive(false);
            boxdistance = 0;
            Boxindex = -1;
        }
    }

    private float SpeedUpmaxTimeer = 0;
    private float SpeedUTimer = 0;
    public float SpeedUpSpeed = 0;
    public bool SpeedUpOn = false;
    public void SpeedUp(float speed, float maxTime)
    {
        SpeedUpmaxTimeer = maxTime;
        SpeedUpSpeed = speed;
        Player.Movespeed += speed;
        SpeedUpOn = true;
    }

    private void SpeedUpTimerUpdate()
    {
        if (SpeedUpOn)
        {
            SpeedUTimer += Time.deltaTime;
            if (SpeedUTimer >= SpeedUpmaxTimeer)
            {
                Player.Movespeed -= SpeedUpSpeed;
                SpeedUTimer = 0;
                SpeedUpSpeed = 0;
                SpeedUpOn = false;
            }
        }
    }
    
}
