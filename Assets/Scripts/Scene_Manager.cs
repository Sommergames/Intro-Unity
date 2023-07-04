using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class Scene_Manager : MonoBehaviour
{

    [SerializeField]
    private GameObject _ui;

    private int _level = 0;
    
    // SWITCH TO LEVEL 2
    private void OnTriggerEnter(Collider other)
    {

        _level += 1;
        
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(_level);
            _ui.GetComponent<UI_Manager>().Level();
            
        }
        
    }

    // Restart level
    public void ReloadScene()
    {
        SceneManager.LoadScene(_level);
    }
}

