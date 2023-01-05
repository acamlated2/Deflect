using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // public
    
    // private
    public int _dirX;
    public float _speed = 20f;
    private bool _goingLeft;

    private float _playerHorizontalExtent;
    private float _playerVerticalExtent;

    private float _dt;

    private GameObject _bullets;

    private float _deflectZoneLimitUp = 1f;
    private float _deflectZoneNoMoveExtent = 0.1f;
    
    void Awake()
    {
        // find gameobjects
        _bullets = GameObject.FindGameObjectWithTag("Bullets");
        
        // get numbers
        _playerHorizontalExtent = GetComponent<Collider2D>().bounds.extents.x;
        _playerVerticalExtent = GetComponent<Collider2D>().bounds.extents.y;
    }
    
    void Update()
    {
        KeyPressed();
        KeyReleased();
        
        // delta time
        _dt = Time.deltaTime;
    }

    void FixedUpdate()
    {
        // player movements
        transform.position += new Vector3(_dirX * _speed * _dt, 0, 0);
    }

    void KeyPressed()
    {
        // movements
        if (Input.GetKeyDown("a"))
        {
            GoLeft();
        }

        if (Input.GetKeyDown("d"))
        {
            GoRight();
        }
        
        // gameplay
        if (Input.GetKeyDown("j") ||
            Input.GetKeyDown("k"))
        {
            DeflectBullet();
        }
    }

    void KeyReleased()
    {
        // movements
        if (_goingLeft)
        {
            if (Input.GetKeyUp("a"))
            {
                _dirX = 0;
            }
        }

        else
        {
            if (Input.GetKeyUp("d"))
            {
                _dirX = 0;
            }
        }
    }

    void GoLeft()
    {
        _dirX = -1;
        _goingLeft = true;
    }

    void GoRight()
    {
        _dirX = 1;
        _goingLeft = false;
    }

    void DeflectBullet()
    {
        for (int i = 0; i < _bullets.transform.childCount; i++)
        {
            // make sure player can deflect
            if ((_bullets.transform.GetChild(i).transform.position.y <
                 transform.position.y + _playerVerticalExtent + _deflectZoneLimitUp) &&
                (_bullets.transform.GetChild(i).transform.position.y) > transform.position.y)
            {
                // make sure it's inside the player x bounds
                if ((_bullets.transform.GetChild(i).transform.position.x <
                     transform.position.x - _playerHorizontalExtent) ||
                    (_bullets.transform.GetChild(i).transform.position.x > 
                     transform.position.x + _playerHorizontalExtent))
                {
                    continue;
                }

                // deflect
                Vector3 bulletRelativePos = transform.InverseTransformPoint(_bullets.transform.GetChild(i).transform.position);
                float newBulletDir = bulletRelativePos.x / _playerHorizontalExtent;
                _bullets.transform.GetChild(i).GetComponent<BulletScript>().Deflect(newBulletDir);
            }
        }
    }
}
