using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    // public
    
    // private
    private Camera _camera;
    private Vector2 _cameraExtent;
    
    private GameObject _player;

    private Vector2 _dir;
    private float _speed = 25f;
    private float _dt;

    private Vector2 _deltaPos;

    private float _exitLimit = 3f;

    void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _camera = Camera.main;

        _cameraExtent = new Vector2(_camera.orthographicSize * _camera.aspect, _camera.orthographicSize);
    }

    void Update()
    {
        // delta time
        _dt = Time.deltaTime;
        
        // delete object
        Delete();
    }

    void FixedUpdate()
    {
        // movements
        transform.position += new Vector3(_dir.x * _speed * _dt, _dir.y * _speed * _dt, 0);
    }

    public void Shoot()
    {
        _deltaPos =  transform.InverseTransformPoint(_player.transform.position);

        if (Math.Abs(_deltaPos.x) > Math.Abs(_deltaPos.y))
        {
            _dir = _deltaPos / Math.Abs(_deltaPos.x);
        }

        else
        {
            _dir = _deltaPos / Math.Abs(_deltaPos.y);
        }
    }

    private void Delete()
    {
        if ((transform.position.x > _cameraExtent.x + _exitLimit) ||
            (transform.position.x < -_cameraExtent.x - _exitLimit) ||
            (transform.position.y > _cameraExtent.y + _exitLimit) ||
            (transform.position.y < -_cameraExtent.y - _exitLimit))
        {
            Destroy(transform.gameObject);
        }
    }

    public void Deflect(float xDir)
    {
        _dir.x = xDir;
        _dir.y = 1;
    }
}
