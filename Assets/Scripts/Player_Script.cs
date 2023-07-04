using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player_Script : MonoBehaviour
{

    [SerializeField]
    private GameObject _spawnManager;
    
    [SerializeField] 
    private GameObject _bullet;
    
    [SerializeField] 
    private Rigidbody _rb;

    [SerializeField]
    private GameObject _ui;

    [SerializeField]
    private GameObject _sceneEnd;

    private Animator _anim;
    
    [SerializeField] 
    private float _speed = 100f;
    
    [SerializeField] 
    private float _jumpingSpeed = 9f;
    
    [SerializeField]
    private float _jumpingDelay = 2f;
    private float _jumpingCooldown;

    [SerializeField] 
    private float _bulletDelay = 1f;
    private float _bulletCooldown = 0f;

    public int _lives = 25;

    private new Vector3 _currentPosition;
    
    private new Vector3 _boxPosition;
    
    private new Vector3 _direction;

    private new Vector3 _lookDirektion;

    public new Vector3 _shootingDirektion;
    

    
    
    void Start()
    {
        _sceneEnd = GameObject.FindWithTag("SceneEnd");
        
        // Set shooting direction vector 
        _shootingDirektion = new Vector3(0, 0, 1);
        
        // Get animator of player overlay and disable 
        _anim = GetComponentInChildren<Animator>();
        
        _anim.enabled = false;

    }
    
    void Update()
    {

        // Move
        Playermovement();

        // Shoot 
        InstantiateBullet();

    }

    void Playermovement()
    {
        //Moving in Plane 
        
        // Get input 
        float x_Input = Input.GetAxis("Horizontal");
        float z_Input = Input.GetAxis("Vertical");


        // Disable / enable animation depending on movement 
        if (Mathf.Abs(_rb.velocity.x) >= 0.1f && Mathf.Abs(_rb.velocity.x) >= 0.1f)
        {
            _anim.enabled = true;
        }
        else
        {
            _anim.enabled = false;
        }
        
        // Change player velocity if maximum speed isnt exceeded
       if(Mathf.Abs(_rb.velocity.x) < 7 && Mathf.Abs(_rb.velocity.z) < 7)
       {
           _rb.velocity += (Vector3.right * Time.deltaTime * _speed* x_Input);
           _rb.velocity += (Vector3.forward * Time.deltaTime * _speed* z_Input);

       }

       // Only change orientation of player if its actually moving 
       if (_rb.velocity.x != 0f && _rb.velocity.z != 0f)
       {
           // Set orientation to movement direction 
           _lookDirektion = _rb.velocity;
           _lookDirektion.y = 0f;
           transform.rotation = Quaternion.LookRotation(_lookDirektion);
           
       }
       
        
        // Jump if cooldown is over and space is pressed 
        _jumpingCooldown -= Time.deltaTime;

        if (Input.GetKeyDown("space") && _jumpingCooldown <= 0f)
        {
            _rb.velocity += new Vector3(x: 0f, y: _jumpingSpeed, z: 0f);
            _jumpingCooldown = _jumpingDelay;
        }
        
        
    }
    

    // Instantiate bullet on mouse click
    void InstantiateBullet()
    {
        _bulletCooldown -= Time.deltaTime;
        
        if (Input.GetKeyDown(KeyCode.Mouse0) && _bulletCooldown < 0)
        {
            Instantiate(_bullet, transform.position, Quaternion.identity);

            _bulletCooldown = _bulletDelay;
        }
        
        // Change shooting direction depending on keyboard input and make it visible in the UI
        
        if (Input.GetKeyDown("q"))
        {
            _shootingDirektion = Quaternion.AngleAxis(-10, Vector3.up) * _shootingDirektion;
            _ui.GetComponent<UI_Manager>().Attackdirektion(-10);
        }
        if (Input.GetKeyDown("e"))
        {
            _shootingDirektion = Quaternion.AngleAxis(10, Vector3.up) * _shootingDirektion;
            _ui.GetComponent<UI_Manager>().Attackdirektion(10);
        }

    }

    // Damage function 
    public void Damage()
    {
        
        _lives -= 1 ;
        // change healthbar 
        _ui.GetComponent<UI_Manager>().Healthbar();

        // Destroy all enemys on player death 
        if (_lives == 0)
        {
            _spawnManager.GetComponent<Spawn_Manager>().EnemyDespawn();
            
            Destroy(this.gameObject);
            // Restart scene 
            _sceneEnd.GetComponent<Scene_Manager>().ReloadScene();
            
        }
    }
    
    // Would have implemented simple collectable like increase speed, slow enemys, increase health or reduce bulletcooldown 
    // But time was rare 
    
}
