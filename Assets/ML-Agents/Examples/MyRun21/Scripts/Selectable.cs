using System.Collections ;
using System.Collections.Generic ;
using UnityEngine ;

public class Selectable : MonoBehaviour
{
    public string suit ;
    public int value ;
    public bool faceUp = true ;

    private string valueString ;

    public int row ;
    public int col ;

    public string faceValue ;

    // Start is called before the first frame update
    void Start()
    {
        if ( CompareTag( "Card" ) )
        {
            suit = transform.name[ 0 ].ToString() ;

            for ( int i = 1 ; i < transform.name.Length ; i++ )
            {
                char c = transform.name[ i ] ;
                valueString += c.ToString() ;
            }

            faceValue = valueString ;

            if ( valueString == "2" )
            {
                value = 2 ;
            }
            else if ( valueString == "3" )
            {
                value = 3 ;
            }
            else if ( valueString == "4" )
            {
                value = 4 ;
            }
            else if ( valueString == "5" )
            {
                value = 5 ;
            }
            else if ( valueString == "6" )
            {
                value = 6 ;
            }
            else if ( valueString == "7" )
            {
                value = 7 ;
            }
            else if ( valueString == "8" )
            {
                value = 8 ;
            }
            else if ( valueString == "9" )
            {
                value = 9 ;
            }
            else if ( valueString == "10" )
            {
                value = 10 ;
            }
            else if ( valueString == "J" )
            {
                value = 10 ;
            }
            else if ( valueString == "Q" )
            {
                value = 10 ;
            }
            else if ( valueString == "K" )
            {
                value = 10 ;
            }
            else if ( valueString == "A" )
            {
                value = 11 ;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
