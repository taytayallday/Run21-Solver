using System.Collections ;
using System.Collections.Generic ;
using System.Linq;
using UnityEngine ;
using UnityEngine.SceneManagement ;
using UnityEngine.UI ;
//using MLAgentsExamples ;

public class UserInput : MonoBehaviour
{
    public int col = -1 ;
    private Run21 run21 ;
    public GameObject collectWinningsPanel ;
    public GameObject lostGamePanel ;
    UIButton uiButton ;

    // Start is called before the first frame update
    void Start()
    {
        uiButton = FindObjectOfType< UIButton >() ;
        run21 = FindObjectOfType< Run21 >() ;
    }

    // Update is called once per frame
    void Update()
    {              
        if ( ! lostGamePanel.activeSelf && ! collectWinningsPanel.activeSelf )
        {
            GetMouseClick() ;
        }
        else
        {
            if ( lostGamePanel.activeSelf )
            {
                print( "You Lose ! \nA column reached over 21" ) ;
            }
            else
            {
                collectWinningsPanel.SetActive( true ) ;
                collectWinningsPanel.transform.GetChild( 0 ).GetComponent< Text >().text
                    = "You Win ! \nFinal Score : " + run21.score ;   
                print( "You Win ! \nFinal Score : " + run21.score ) ;
            }
            //SceneManager.LoadScene( 1 ) ;
            GetComponent< Run21 >().ResetNeededStuff() ;
        }

        //GetColumnChoice() ;
    }

    public void GetMouseClick()
    {        
        if ( Input.GetMouseButtonDown( 0 ) )
        {
            Vector3 mousePosition = 
                Camera.main.ScreenToWorldPoint( 
                    new Vector3( Input.mousePosition.x , 
                                Input.mousePosition.y , 
                                Input.mousePosition.z ) ) ;

            RaycastHit2D hit = Physics2D.Raycast( 
                Camera.main.ScreenToWorldPoint( 
                    Input.mousePosition ) , Vector2.zero ) ;

            float x = mousePosition.x ;
            float y = mousePosition.y ;  
            
            if ( y < 1.5 )
            {
                if (hit.collider.CompareTag("CollectWinnings"))
                {
                    print( "clicked " ) ;
                    collectWinningsPanel.SetActive(true);
                }
                else if ( x >= -8.7 && x < -5.0 )
                {
                    col = 0 ;
                }
                else if ( x > -5.0 && x < -1.9 )
                {
                    col = 1 ;
                }
                else if ( x >= -1.9 && x < 1.9 )
                {
                    col = 2 ;
                }
                else if ( x >= 1.9 && x < 4 )
                {
                    col = 3 ;
                }
                else if ( x >= 4 && x < 8 )
                {
                    col = 4 ;
                } 

                Stack() ; 
                run21.Run21DealACard() ;

            }
            
        }        
        
    }

    public void GetColumnChoice()
    {
        if ( Input.GetKey( KeyCode.Keypad1 ) )
        {
            col = 0 ;
        }
        else if ( Input.GetKey( KeyCode.Keypad2 ) )
        {
            col = 1 ;
        }
        else if ( Input.GetKey( KeyCode.Keypad3 ) )
        {
            col = 2 ;
        }
        else if ( Input.GetKey( KeyCode.Keypad4 ) )
        {
            col = 3 ;
        }
        else if ( Input.GetKey( KeyCode.Keypad5 ) )
        {
            col = 4 ;
        }

        Stack() ; 
        run21.Run21DealACard() ;  
    }

    public void Stack()
    {
        Selectable card = run21.currentCard.transform.
            GetChild( 0 ).GetComponent<Selectable>() ;

        float yOffset = 0 ;
        int lenColumn = run21.handList[ col ].Count() ;

        if ( lenColumn > 0 )
        {
            for ( int i = 0 ; i < lenColumn ; ++i )
            {
                yOffset += 0.5f ;
            }
        }

        card.transform.position = new Vector3(
            run21.hand[ col ].transform.position.x ,
            run21.hand[ col ].transform.position.y - yOffset ,
            run21.hand[ col ].transform.position.z + 0.01f ) ;

        card.transform.parent = run21.hand[ col ].transform ;
        card.GetComponent< SpriteRenderer >().sortingOrder = lenColumn ;

        run21.hand[ col ].transform.
            GetChild( 0 ).transform.SetAsFirstSibling() ;

        run21.handList[ col ].Add( card.name ) ;

        run21.updateHandSums( 
            card.GetComponent< Selectable >().value , col ) ;

        run21.score = 0 ;

        for ( int i = 0 ; i < 5 ; i++ )
        {
            run21.score += run21.columnsSums[ i ] ;
        }

    }

}
