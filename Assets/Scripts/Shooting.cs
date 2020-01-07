using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bullet;
    public GameObject bulletSpawn;
    public float fireRate;
    private Transform _bullet;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl))
            Fire();
    }

    public void Fire()
    {
        _bullet = Instantiate(bullet.transform, bulletSpawn.transform.position, Quaternion.identity);
        _bullet.rotation = bulletSpawn.transform.rotation;
    }
}
