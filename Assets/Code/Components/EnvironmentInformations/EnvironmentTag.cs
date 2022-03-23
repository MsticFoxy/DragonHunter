using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnvironmentTag : MonoBehaviour
{
    [Flags]
    public enum EnvironmentTags
    {
        Unclimbable = 1,
        Sticky = 2
    }
    public EnvironmentTags tags;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
