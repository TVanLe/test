using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements.Experimental;
using Quaternion = System.Numerics.Quaternion;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    
    private Transform holder;
    private List<GameObject> bullets = new List<GameObject>();
    private void Awake()
    {
        holder = transform.Find("Holder");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FireWeapon();    
        }
    }

    private void FireWeapon()
    {
        Transform bullet = GetPoolObjects().transform;
        bullet.SetPositionAndRotation(bulletSpawn.position, transform.parent.rotation);
        bullet.gameObject.SetActive(true);
    }

    private GameObject GetPoolObjects()
    {
        for (int i = 0; i < bullets.Count; i++)
        {
            if (!bullets[i].activeInHierarchy)
            {
                return bullets[i];
            }
        }

        GameObject obj = Instantiate(bulletPrefab);
        obj.gameObject.SetActive(false);
        obj.transform.parent = holder;
        bullets.Add(obj);
        return obj;
    }
        
}
