using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using System.Linq ;
using MLAgentsExamples;
using UnityEngine.SceneManagement ;
using UnityEngine.UI ;
using System ;
using UnityEditorInternal;

public class Run21GameAndAgent : Agent
{
    public Sprite[] cardFaces ;

    public GameObject cardPrefab ;
    public GameObject currentCard ;
    public GameObject[] hand ;
    public GameObject playerScore ;
    public GameObject[] handsSums ;
    public GameObject lostGamePanel ;
    public GameObject collectWinningsButton ;
    public GameObject collectWinningsPanel ;

    UserInput userInput ;
    
    public List< string > deck ;
    public static string[] suits = new string[] { "C", "D", "H", "S" } ;
    public static string[] values = new string[] { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" } ;

    //[ HideInInspector ]
    public List< string >[] handList ;   
    public List< string > hand1 = new List< string >() ;
    public List< string > hand2 = new List< string >() ;
    public List< string > hand3 = new List< string >() ;
    public List< string > hand4 = new List< string >() ;
    public List< string > hand5 = new List< string >() ;
    
    public List< int > columnsSums ;
    public int score ;   
    public int idx = 51 ;   

    int x = 1 ;

    string valueString_main = null ;
    int value_main = -1 ;

    // Start is called before the first frame update
    void Start()
    {
        userInput = FindObjectOfType< UserInput >() ;
        score = 0 ;
        handList = new List< string >[] { hand1 , hand2 , hand3 , hand4 , hand5 } ;  
        columnsSums = new List< int >() { 0 , 0 , 0 , 0 , 0 } ;
        PlayCards() ;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayCards()
    { 
        deck = GenerateDeck() ;      
        Shuffle( deck ) ;
        Run21DealACard() ;
    }

    public static List< string > GenerateDeck()
    {
        List< string > newDeck = new List< string >() ;

        foreach ( string s in suits )
        {
            foreach ( string v in values )
            {
                newDeck.Add( s + v ) ;
            }
        }

        return newDeck ;       
    }

    void Shuffle< T >( List< T > list )
    {
        System.Random random = new System.Random() ;
        int n = list.Count ;

        while ( n > 1 )
        {
            int k = random.Next( n ) ;
            --n ;
            T temp = list[ k ] ;
            list[ k ] = list[ n ] ;
            list[ n ] = temp ;
        }
    }

    void GetIntVal( string valueString_main )
    {
        if ( valueString_main == "2" )
        {
            value_main = 2 ;
        }
        else if ( valueString_main == "3" )
        {
            value_main = 3 ;
        }
        else if ( valueString_main == "4" )
        {
            value_main = 4 ;
        }
        else if ( valueString_main == "5" )
        {
            value_main = 5 ;
        }
        else if ( valueString_main == "6" )
        {
            value_main = 6 ;
        }
        else if ( valueString_main == "7" )
        {
            value_main = 7 ;
        }
        else if ( valueString_main == "8" )
        {
            value_main = 8 ;
        }
        else if ( valueString_main == "9" )
        {
            value_main = 9 ;
        }
        else if ( valueString_main == "10" )
        {
            value_main = 10 ;
        }
        else if ( valueString_main == "J" )
        {
            value_main = 10 ;
        }
        else if ( valueString_main == "Q" )
        {
            value_main = 10 ;
        }
        else if ( valueString_main == "K" )
        {
            value_main = 10 ;
        }
        else if ( valueString_main == "A" )
        {
            value_main = 11 ;
        }
    }
    
    public void Run21DealACard()
    {       
        GameObject newCard
            = Instantiate( cardPrefab ,
                        new Vector3( currentCard.transform.position.x ,
                                    currentCard.transform.position.y ,
                                    currentCard.transform.position.z ) ,
                        Quaternion.identity , currentCard.transform ) ; 

        newCard.name = deck[ idx ] ; 
        --idx ;
        newCard.GetComponent< Selectable >().faceUp = true ;

        playerScore.GetComponent< HandScript >().sum = score ;

        for ( int i = 0 ; i < 5 ; ++i )
        {
            handsSums[ i ].GetComponent< HandScript >().sum = 
                columnsSums[ i ] ;
        }

        currentCard.GetComponent< SpriteRenderer >().sortingOrder = -1 ;

        valueString_main = null ;
        value_main = -1 ;

        for ( int i = 1 ; i < newCard.name.Length ; i++ )
        {
            char c = newCard.transform.name[ i ] ;
            valueString_main += c.ToString() ;
        }

        GetIntVal( valueString_main ) ;
        //StartCoroutine( WaitForTime() ) ;
        RequestDecision() ;
    }

    public void Stack( int col ) 
    {
        Selectable card = currentCard.transform.GetChild( 0 ).GetComponent< Selectable >() ;

        float yOffset = 0.5f ;
        int lenColumn = handList[ col ].Count() ;

        yOffset *= lenColumn ;

        card.transform.position 
            = new Vector3( hand[ col ].transform.position.x,
                        hand[ col ].transform.position.y - yOffset ,
                        hand[ col ].transform.position.z ) ;

        card.transform.parent = hand[ col ].transform ;
        card.GetComponent< SpriteRenderer >().sortingOrder = lenColumn + 3 ;

        hand[ col ].transform.GetChild( 0 ).transform.SetAsFirstSibling() ;
        handList[ col ].Add( card.name ) ;

        updateHandSums( card.GetComponent< Selectable >().value , col ) ;

        score = 0 ;

        foreach ( int sum in columnsSums )
        {
            score += sum ;
        }
        
        Run21DealACard() ;
    }
 

    public void updateHandSums( int value , int col )
    {
        columnsSums[ col ] += value ;

        if ( columnsSums[ col ] <= 21 )
        {
            //return false ;
        }
        else // columns sum is over 21
        {           
            int lenColumn = hand[ col ].gameObject.transform.childCount ;
            Selectable card = hand[ col ].gameObject.transform.GetChild( 0 ).GetComponent< Selectable >() ;
            
            int i = 0 ;
            bool found = false ;

            // search for an ace
            while ( i < lenColumn & ! found )
            {
                // if the card youre on is an ace
                if ( card.value == 11 )
                {
                    found = true ;
                }
                else
                {
                    card = hand[ col ].gameObject.transform.GetChild( i ).GetComponent< Selectable >() ;
                }
                
                ++i ;
            }

            // didnt find an ace in the column & the sum is over 21
            if ( i >= lenColumn ) 
            {
                // currentCard is an ace
                if ( card.value == 11 ) 
                {     
                    card.value = 1 ;
                    columnsSums[ col ] = 0 ;                
                
                    for ( int j = 0 ; j < lenColumn ; ++j )
                    {
                        columnsSums[ col ] += hand[ col ].transform.GetChild( j ).GetComponent< Selectable >().value ;                    
                    }

                    if ( columnsSums[ col ] > 21 )
                    {
                        //return true ;            
                    }
                    else
                    {
                        //return false ;
                    }
                }
                else
                {
                    //return true ;
                }
                
            }
            else // found an ace
            {
                // change its value to 1
                card.value = 1 ;

                // resum the column 
                columnsSums[ col ] = 0 ;                
                
                for ( int j = 0 ; j < lenColumn ; ++j )
                {
                    columnsSums[ col ] += hand[ col ].transform.GetChild( j ).GetComponent< Selectable >().value ;
                }
                
                // if the sum is still over 21 , you bust 
                if ( columnsSums[ col ] > 21 )
                {
                    //return true ;                
                }
                else // youre under 21 with an ace that was playable as a 1
                {
                    //return false ;
                }
                
            }
        }  
    }

    public void ResetNeededStuff()
    {
        // destroy the cards in play
        for ( int i = 0 ; i < 5 ; ++i )
        {
            int size = hand[ i ].transform.childCount ;

            for ( int j = 0 ; j < size ; ++j )
            {
                Destroy( hand[ i ].transform.GetChild( j ).gameObject ) ;
            }
        }

        // destroy current card
        Destroy( currentCard.transform.GetChild( 0 ).gameObject ) ;

        foreach ( List< string > list in handList )
        {
            list.Clear() ;
        }

        handList = new List< string >[] { hand1 , hand2 , hand3 , hand4 , hand5 } ;  

        // reset score
        score = 0 ;

        // new column sums
        columnsSums = new List< int >() { 0 , 0 , 0 , 0 , 0 } ;

        // reset idx 
        idx = 51 ;  
        
        // reset gameobject score
        playerScore.GetComponent< HandScript >().sum = 0 ;
        
        // reset gameobject hand sums
        for ( int i = 0 ; i < 5 ; ++i )
        {
            handsSums[ i ].GetComponent< HandScript >().sum = 0 ;
        }

        // quick fix to the 2 cards dealt at once problem
        if ( x != 1 )
        { 
            PlayCards() ;
        }
        --x ; 

    }

    public override float[] Heuristic()
    {
        if ( Input.GetKeyDown( KeyCode.Keypad1 ) )
        {
           return new float[] { 0 } ;
        }
        if ( Input.GetKeyDown( KeyCode.Keypad2 ) )
        {
            return new float[] { 1 } ;
        }
        if ( Input.GetKeyDown( KeyCode.Keypad3 ) )
        {
            return new float[] { 2 } ;
        }
        if ( Input.GetKeyDown( KeyCode.Keypad4 ) )
        {
            return new float[] { 3 } ;
        }
        if ( Input.GetKeyDown( KeyCode.Keypad5 ) )
        {
            return new float[] { 4 } ;
        }
        if ( Input.GetKeyDown( KeyCode.Keypad0 ) )
        {
            return new float[] { 5 } ;
        }
        
        return new float[] { -1 } ;       
    }

    public override void AgentAction( float[] vectorAction )
    {
        int col = Mathf.FloorToInt( vectorAction[ 0 ] ) ;        

        if ( col != - 1 ) 
        {
            if ( col == 5 ) 
            {
                //print( "You Win ! \nFinal Score : " + score ) ;
                if ( score >= 0 && score < 90 )
                {
                    print( "You Win ! \nFinal Score was 0 - 89 " ) ;
                }
                else if ( score >= 90 && score < 100 )
                {
                    print( "You Win ! \nFinal Score was 90 - 99 " ) ;
                }
                else 
                {
                    print( "You Win ! \nFinal Score was 100 - 105 " ) ;
                }

                AddReward( 0.01f * score ) ;

                // penalty to hopefully increase the score ?
                AddReward( 0.001f * ( score - 105f ) ) ;

                // extra for each col == 21
                /*
                for ( int i = 0 ; i < 5 ; ++i )
                {
                    if ( columnsSums[ i ] == 21 )
                    {
                        AddReward( 0.1f ) ;
                    }
                }
                */
                Done() ;
            }
            else
            {
                Stack( col ) ;
            
                if ( columnsSums[ col ] > 21 )
                {                 
                    print( "You Lose ! \nA column reached over 21" ) ;
                    SetReward( -1f ) ;
                    Done() ;
                }

                /* */
                else if ( columnsSums[ col ] == 21 )
                {
                    AddReward( 0.1f ) ;
                }
                

            }
        }

    }

    // to call : StartCoroutine( WaitForTime() ) ;
    public IEnumerator WaitForTime() 
    {        
        yield return new WaitForSecondsRealtime( 10f ) ;
        // function call after to insure it waits
    }

    public override void CollectObservations()
    {
        // score
        AddVectorObs( score ) ;
        
        // initial face value of the card going to be played
        AddVectorObs( value_main ) ;

        for ( int i = 0 ; i < 5 ; ++i )
        {
            int size = hand[ i ].transform.childCount ; // 10 cards max in a column 
            AddVectorObs( columnsSums[ i ] ) ;

            for ( int j = 0 ; j < size ; ++j )
            {
                AddVectorObs( hand[ i ].transform.GetChild( j ).GetComponent< Selectable >().value ) ; 
            }

        }

    }

    public override void AgentReset()
    {
        ResetNeededStuff() ;
    }

}
