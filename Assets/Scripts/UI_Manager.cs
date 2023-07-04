using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    
    
    
    private float _healthbarLenght = 26.50038f;

    private float _colorChannel = 1f;

    private int _killCount = 0;
    
    private int _level = 1;
    
    private MaterialPropertyBlock _mpb;
    
    [SerializeField]
    private GameObject _healthbar;
    
    [SerializeField]
    private GameObject _attackDirektion;
    
    [SerializeField]
    private GameObject _player;
    
    [SerializeField]
    private Text _killcountText;
    
    [SerializeField]
    private Text _levelText;


    
    
    void Start()
    {
        
        if (_mpb == null)
        {
            _mpb = new MaterialPropertyBlock();
            _mpb.Clear();
        }
        

        
    }
    
    void Update()
    {

    }
    
    // Change healthbar 
    public void Healthbar()
    {
        // Shorten healthbar 
        _healthbarLenght -= 1.07f;
        
        _healthbar.transform.localScale = new Vector3( x:2.650569f , y:_healthbarLenght , z:3.975056f );
        
        // Make color less green and more red 
        _colorChannel -= 0.05f;
        
        _mpb.SetColor("_Color", new Color(1-_colorChannel, _colorChannel, 0, 1f));
        
        _healthbar.GetComponent<Renderer>().SetPropertyBlock(_mpb);

    }

    // Display attack direction 
    public void Attackdirektion(float angle)
    {
        _attackDirektion.transform.rotation = Quaternion.AngleAxis(angle, Vector3.up) * _attackDirektion.transform.rotation ;
    }

    // Not ready implemented but show Level
    public void Level()
    {
        _level += 1;
        _levelText.text = "Level" + _level;

    }

    // Not ready implemented but show Killcount 
    public void Killcount()
    {
        _killCount += 1;

        _killcountText.text = "Killcount" + _killCount ;
        
    }
    

}
