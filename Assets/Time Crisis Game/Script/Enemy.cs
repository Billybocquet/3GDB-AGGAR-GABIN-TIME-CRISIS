using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public GameObject bulletPrefab;

    public UnityEvent EventHit;

    public string tagToCompare;
    
    void Start()
    {
        Invoke("Shoot", Random.Range(1, 10));
    }

    void Shoot()
    {
        var bullet = GameObject.Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Vector3 dir = GameObject.FindWithTag("Player").transform.position - transform.position;
        bullet.GetComponent<Rigidbody>().AddForce(dir.normalized * 750);

        Invoke("Shoot", Random.Range(1, 10));
    }

    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("EnemyHit");
        
        if(other.gameObject.CompareTag(tagToCompare))
            EventHit.Invoke();
    }
}
