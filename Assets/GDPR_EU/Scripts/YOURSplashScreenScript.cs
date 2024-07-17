using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YOURSplashScreenScrip : MonoBehaviour
{
    public UmpManagerz _umpManager;
    // Start is called before the first frame update
    void Start()
    {
        if (_umpManager != null)
            _umpManager.ConsentCall();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
