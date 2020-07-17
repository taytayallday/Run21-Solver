using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    // Start is called before the first frame update
    public Run21GameAndAgent run21 ;
    public GameObject lostGamePanel ;
    public GameObject collectWinningsPanel ;
    void Start()
    {
        run21 = FindObjectOfType< Run21GameAndAgent >() ;
    }

    // Update is called once per frame
    void Update()
    {
        //if ( HasLost() )
        //{
        //    lostGamePanel.SetActive( true ) ;

        //}
        //if ( )
    }

    //bool HasLost()
    //{
    //    for ( int i = 0 ; i < 5 ; ++i )
    //    {
    //        if ( run21.columnsSums[ i ] > 21 ) 
    //            return true ;
    //    }
        
    //    return false ;
    //}

}
