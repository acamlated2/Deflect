using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    // public
    public GameObject bulletPrefab;
    
    // private
    private float _dt;
    
    private float _timer;
    private float _timerDefault = 0.25f;

    private GameObject _bullets;
    
    void Awake()
    {
        _bullets = GameObject.FindGameObjectWithTag("Bullets");
    }
    
    void Update()
    {
        // delta time
        _dt = Time.deltaTime;
        
        // shoot timer
        _timer -= 1 * _dt;
        if (_timer <= 0)
        {
             GameObject bullet = Instantiate(bulletPrefab, new Vector3(transform.localPosition.x, transform.localPosition.y - 1.5f, 0),
                quaternion.identity);
             bullet.transform.parent = _bullets.transform;
             bullet.GetComponent<BulletScript>().Shoot();
             _timer = _timerDefault;
        }
    }
}
