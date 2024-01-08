using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletVeclocity = 150f;
    [SerializeField] private float buleltLiftTime = 3f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        rb.velocity = transform.forward * bulletVeclocity;
        StartCoroutine(DestroyAfterTime(buleltLiftTime));
    }

    private IEnumerator DestroyAfterTime(float delay)
    {
        yield return new WaitForSeconds(delay);
        this.gameObject.SetActive(false);
    }
    
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("hit " + other.gameObject.name + " !");
        this.gameObject.SetActive(false);
    }
}
