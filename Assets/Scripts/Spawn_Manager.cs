using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Manager : MonoBehaviour
{
    
    
    [SerializeField] 
    private GameObject _enemy ;

    private float _enmemySpacing = 2f;
    
    private bool _alive = true;
    
    private GameObject[] _spawnPoints;
    
    private Vector3 _spawnPosition;

    private GameObject[] _enemys;
    
    [SerializeField]
    private Transform _spawnManager;


    void Start()
    {
        EnemySpawn();
        
    }

    void Update()
    {
        
    }
    

    void EnemySpawn()
    {
        
        // Find enemy and spawn points
        _enemy = GameObject.FindWithTag("Enemy");
        _spawnPoints = GameObject.FindGameObjectsWithTag("Enemyspawn");

        // Loop through each spawn point
        foreach (GameObject spawnPoint in _spawnPoints)
        {
            // Get the spawn point's position
            _spawnPosition = spawnPoint.transform.position;

            // Instantiate 3 enemies right beside each  other  at position
            for (int i = 0; i < 3; i++)
            {
                // Add distance between enemys to position vector and instantiate as child 
                Vector3 _enemy_position = _spawnPosition + new Vector3(i * _enmemySpacing, 0f, 0f);
                Instantiate(_enemy, _enemy_position, Quaternion.identity,_spawnManager);
            }
        }
    }

    // Despawn all enemys 
    public void EnemyDespawn()
    {
        _enemys = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject _enemy in _enemys)
        {
            Destroy(_enemy);
        }
        
    }

}



