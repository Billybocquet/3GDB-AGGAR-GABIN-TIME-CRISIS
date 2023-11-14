using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public UnityEvent EventReloaded;
    public UnityEvent EventOutOfAmmo;
    public UnityEvent EventShoot;
    public UnityEvent EventHitTaken;
    public UnityEvent EventCover;

    public GameObject bulletPrefab;
    public int maxAmmo = 10;
    int currentAmmo;
    public int CurrentAmmo { get { return currentAmmo; } }

    public int startLife = 3;
    int currentLife;
    public int CurrentLife { get { return currentLife; } }


    Transform coverPosition;
    Transform uncoverPosition;
    bool onCover = false;

    private bool onReload;

    public KeyCode shootKey;
    public KeyCode reloadKey;
    public KeyCode coverKey;

    public string tagToComparePlayer;

    private void Awake()
    {
        currentAmmo = maxAmmo;
        currentLife = startLife;
    }

    public void Update()
    {
        if (Input.GetKeyDown(shootKey))
        {
            EventShoot.Invoke();
        }

        if (Input.GetKeyDown(reloadKey))
        {
            EventReloaded.Invoke();
        }

        if (Input.GetKeyDown(coverKey))
        {
            EventCover.Invoke();
        }
    }

    public void TakeHit()
    {
        currentLife--;
        
        //Debug.Log(currentLife);
    }

    public void SetAreaPositions(Transform playerPos, Transform coverPos)
    {
        uncoverPosition = playerPos;
        coverPosition = coverPos;
        transform.position = uncoverPosition.position;
    }

    public void ToggleCover()
    {
        onCover = !onCover;
        if (onCover) transform.position = coverPosition.position;
        else transform.position = uncoverPosition.position;
    }

    public void Shoot()
    {
        if(currentAmmo > 0)
        {
            
            currentAmmo--;
            
            //Debug.Log(currentAmmo);

            var  bullet = GameObject.Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Ray r = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            bullet.GetComponent<Rigidbody>().AddForce(r.direction * 750);
            
            if(currentAmmo <= 0)
            {
                EventOutOfAmmo.Invoke();
            }
        }
    }

    public void Reload()
    {
        currentAmmo = maxAmmo;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("PlayerHit");
        
        if (other.gameObject.CompareTag(tagToComparePlayer))
        {
            EventHitTaken.Invoke();            
        }

    }
}
