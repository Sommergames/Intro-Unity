using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;


public class EnemyBehaviour : MonoBehaviour
{ 
    [SerializeField]
    private float _enemySpeed = 500f;

    [SerializeField]
    private Rigidbody _rb;
    
    [SerializeField]
    private LayerMask _obstacleLayer;
    
    private GameObject _player;

    [SerializeField]
    private GameObject _smoke;

    [SerializeField]
    private Canvas _ui;
    
    private Animator _anim;
    
    private new Vector3 _currentPosition;
    
    private new Vector3 _playerPosition;
    
    private new Vector3 _direction;

    private new Vector3 _normalisedDir;
    
    private float _distance;
    
    private bool _seeing;

    private bool _alreadySeen;

    private float _damageTimer;

    void Start()
    {
        // Set Player varaiable
        _player = GameObject.FindWithTag("Player");
        
        // Set animator variable and disable it
        _anim = GetComponent<Animator>();
        _anim.enabled = false;
        
        //Set enemy seeing variables to false 
        _alreadySeen = false;
        _seeing = false;
        
        _damageTimer = 0f;

    }


    void Update()
    {
        
        // Get an calculate the enemy and player position and direktion from enemy to player  
        
        _currentPosition = transform.position;
        _playerPosition = _player.transform.position;
        _direction =_playerPosition - _currentPosition;
        
        // Checik if Player is visible tto enemy or behind wall 
        if (CanSeePlayer())
        {
            _seeing = true;
        }
        
        //Run and Attack :) 
        Running();
        Attack();
        
    }
    
    // Check if enemy is hit by bullet and destroy bullet and enemy if so 
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Bullet"))
        {
            Destroy(this.gameObject);
            Destroy(other.gameObject);
            Instantiate(_smoke, transform.position, Quaternion.identity);
            // Not ready implemented but track Killcount 
            _ui.GetComponent<UI_Manager>().Killcount();

        }
    }
    
    
    bool CanSeePlayer()
    {

        // Get distance between enemy and player 
        _distance = _direction.magnitude;
        
        //Debug.DrawRay(_current_position, _direction, Color.blue, 5f, false);

        //Draw ray betweeen player and enemy and check if it hits a wall and check for minimum distance 
        if (!Physics.Raycast(transform.position, _direction, _distance, _obstacleLayer) && _distance < 20f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Run
    void Running()
    {
        // Check if player is visible to enemy or if it was ever visible 
        if (_seeing == true || _alreadySeen == true) 
        {
            // Enable animation
            _anim.enabled = true;

            // Set up down movement to zero 
            _direction.y = 0;
            
            // Rotate enemy in running direktion     
            transform.rotation = Quaternion.LookRotation(_direction);

            // normalise direction vector 
            _normalisedDir = _direction.normalized;
                
            // Set rigidbody velocity 
            _rb.velocity = _normalisedDir * _enemySpeed * Time.deltaTime  ;

            _alreadySeen = true;

        }
        
    }

    void Attack()
    {
        // Damage player every 0.3 seconds if within a certain range to player 
        _damageTimer -= Time.deltaTime;
        
        if (_distance <= 2.8f && _damageTimer <= 0f)
        {
            _player.GetComponent<Player_Script>().Damage();
            _damageTimer = 0.3f;
        }
    }


}

