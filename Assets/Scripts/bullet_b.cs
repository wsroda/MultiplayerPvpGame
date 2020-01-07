using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_b : MonoBehaviour
{
    public float aliveTime;
    public float damage;
    public float movSpeed;

    public GameObject bulletSpawn;
    private GameObject enemyTriggered;

    void Start()
    {
        //bulletSpawn = GameObject.Find("spawn");
        this.transform.rotation = bulletSpawn.transform.rotation;
    }



    void Update()
    {
        aliveTime -= 1 * Time.deltaTime;
        if (aliveTime <= 0)
            Destroy(this.gameObject);
        this.transform.Translate(Vector3.forward * Time.deltaTime * movSpeed);
    }

    void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }
}
