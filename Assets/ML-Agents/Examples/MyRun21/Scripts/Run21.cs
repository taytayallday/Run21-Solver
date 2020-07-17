using System.Collections ;
using System.Collections.Generic ;
using UnityEngine ;
using System.Linq ;
using UnityEngine.UI ;
using UnityEngine.SceneManagement ;
using MLAgents ;

public class Run21 : MonoBehaviour
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
    
    public void Run21DealACard()
    {
        GameObject newCard
            = Instantiate( cardPrefab, 
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

        //run21Agent.Heuristic() ;
        //GetComponentInChildren< Run21Agent >().RequestDecision() ;
    }

    // Stack()
    /*   */
    public void Stack(int col) 
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

        // returns true if game should end 
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
            int i = 0 ;

            int lenColumn = 
                hand[ col ].gameObject.transform.childCount ;

            Selectable card = hand[ col ].gameObject.transform.
                GetChild( i ).GetComponent< Selectable >() ;
            
            // search for an ace
            while ( i < lenColumn )
            {
                // if the card youre on is an ace
                if ( card.value == 11 ) 
                {
                    break ;
                }
                else // keep looking
                {                     
                    card = hand[ col ].gameObject.transform.
                        GetChild( i ).GetComponent< Selectable >() ;
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
                        columnsSums[ col ] += hand[ col ].transform.
                            GetChild( j ).GetComponent< Selectable >().value ;                    
                    }

                    if ( columnsSums[ col ] > 21 )
                    {
                        lostGamePanel.SetActive( true ) ;
                        //return true ;
                        //ResetNeededStuff() ;
                    }
                    else
                    {
                        //return false ;
                    }
                }
                else 
                {
                    lostGamePanel.SetActive( true ) ;
                    //return true ;
                    //ResetNeededStuff() ;
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
                    columnsSums[ col ] += hand[ col ].transform.
                            GetChild( j ).GetComponent< Selectable >().value ;
                }

                // if the sum is still over 21 , you bust 
                if ( columnsSums[ col ] > 21 )
                {
                    lostGamePanel.SetActive( true ) ;
                    //return true ;
                    //ResetNeededStuff() ;
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
        // reset deck and current card
        UpdateSprite[] cards = FindObjectsOfType< UpdateSprite >() ;

        foreach ( UpdateSprite card in cards )
        {
            Destroy( card.gameObject ) ;
        }

        PlayCards() ;

        // hand list
        hand1 = new List< string >() ;
        hand2 = new List< string >() ;
        hand3 = new List< string >() ;
        hand4 = new List< string >() ;
        hand5 = new List< string >() ;

        handList = new List< string >[] { 
            hand1 , hand2 , hand3 , hand4 , hand5 } ;  

        // score
        score = 0 ;

        // column sums
        columnsSums = new List< int >() { 0 , 0 , 0 , 0 , 0 } ;

        idx = 51 ;  
        
        playerScore.GetComponent< HandScript >().sum = 0 ;
        
        for ( int i = 0 ; i < 5 ; ++i )
        {
            handsSums[ i ].GetComponent< HandScript >().sum = 0 ;
        }

        lostGamePanel.SetActive( false ) ;
        collectWinningsPanel.SetActive( false ) ;
    }
}
