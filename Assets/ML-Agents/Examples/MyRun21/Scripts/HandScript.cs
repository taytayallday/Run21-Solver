using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;

public class HandScript : MonoBehaviour
{
    public int sum = 0 ;
    Text sumText ;
    Run21 run21 ;

    // Start is called before the first frame update
    void Start()
    {
        run21 = FindObjectOfType< Run21 >() ;
        sumText = GetComponent< Text >() ;  
    }

    // Update is called once per frame
    void Update()
    {
        sumText.text = sum.ToString() ;
    }
}
