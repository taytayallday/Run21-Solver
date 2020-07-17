using System.Collections ;
using System.Collections.Generic ;
using UnityEngine ;

public class UpdateSprite : MonoBehaviour
{
    public Sprite cardFace ;
    public Sprite cardBack ;
    private SpriteRenderer spriteRenderer ;
    private Selectable selectable ;
    private Run21 run21 ;

    // Start is called before the first frame update
    void Start()
    {
        List<string> deck = Run21.GenerateDeck() ;
        run21 = FindObjectOfType< Run21 >() ;

        int i = 0 ;

        foreach ( string card in deck )
        {
            if ( this.name == card )
            {
                cardFace = run21.cardFaces[ i ] ;
                break ;
            }

            i++ ;
        }

        spriteRenderer = GetComponent< SpriteRenderer >() ;
        selectable = GetComponent< Selectable >() ;
    }

    // Update is called once per frame
    void Update()
    {
        if ( selectable.faceUp == true )
        {
            spriteRenderer.sprite = cardFace ;
        }
        else
        {
            spriteRenderer.sprite = cardBack ;
        }
    }
}
