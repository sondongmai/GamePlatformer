using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static SkinManager instance;
    public int skinid;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }    
    }
    public void setSkinID(int skinid )
    {
        this.skinid = skinid;
    }   
    public int getSkinID( ) 
    {
        return skinid;
    }
}
