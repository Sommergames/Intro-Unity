using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletBehaviour : MonoBehaviour
{
    [SerializeField]
    private float _bulletSpeed = 4f;

    private GameObject _player;

    [SerializeField]
    private GameObject _smoke;

    private Vector3 _shootingDirektion2;
    
    [SerializeField]
    private float _angle = 3;
 


    void Start()
    {
        // Set Player varaiable
        _player = GameObject.FindWithTag("Player");

    }


    void Update()
    {
        // Get shooting direktion from Player Script 
        
        _shootingDirektion2 = _player.GetComponent<Player_Script>()._shootingDirektion;
        
        // Set shooting Angle 
        _shootingDirektion2.y = _angle;
       
        // Shoot Bullet
        transform.Translate(_shootingDirektion2 * Time.deltaTime * _bulletSpeed);

    }
    
    private void OnTriggerEnter(Collider other)
    {
        // Destroy bullet if hit Ground or Wall and insantiate smoke animation
        
        if (other.CompareTag("Ground"))
        {
            Destroy(this.gameObject);
            Instantiate(_smoke, transform.position, Quaternion.identity);

        }
        if (other.CompareTag("Walls"))
        {
            Destroy(this.gameObject);
            Instantiate(_smoke, transform.position, Quaternion.identity);
            
        }
        
    }
}
