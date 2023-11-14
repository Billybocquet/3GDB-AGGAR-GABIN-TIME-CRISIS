using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    public Player player;
    public GameManager gameManager;

    public TMPro.TMP_Text ammo;
    public TMPro.TMP_Text time;
    public TMPro.TMP_Text hp;
    public GameObject reload;

    public float blinkInterval;
    public void Init()
    {
        UpdateAmmo();
        UpdateTime();
        UpdateLife();
    }

    private void Update()
    {
        UpdateTime();
        UpdateLife();
        UpdateAmmo();
    }

    public void UpdateAmmo()
    {
        ammo.text = player.CurrentAmmo.ToString();
    }

    public void UpdateTime()
    {
        time.text = gameManager.RemainingTime.ToString("0.00");
    }

    public void UpdateLife()
    {
        hp.text = player.CurrentLife.ToString();
    }

    public void StartReloadBlink()
    {
        StartCoroutine(ReloadBlink());  
    }    
    
    public void StopReloadBlink()
    {
        StopCoroutine(ReloadBlink());  
    }
    
    
    
    IEnumerator ReloadBlink()
    {
        while (true)
        {
            reload.SetActive(true);
            
            yield return new WaitForSeconds(blinkInterval);
            
            reload.SetActive(false);
            
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}
