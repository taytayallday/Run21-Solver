using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateSpritesV2 : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite cardFace ;
    public Sprite cardBack ;
    private SpriteRenderer spriteRenderer ;
    private Selectable selectable ;
    private Run21GameAndAgent run21 ;

    // Start is called before the first frame update
    void Start()
    {
        List< string > deck = Run21GameAndAgent.GenerateDeck() ;
        run21 = FindObjectOfType< Run21GameAndAgent >() ;

        int i = 0 ;

        foreach ( string card in deck )
        {
            if ( this.name == card )
            {
                cardFace = run21.cardFaces[ i ] ;
                break;
            }

            i++ ;
        }

        spriteRenderer = GetComponent< SpriteRenderer >() ;
        selectable = GetComponent< Selectable >() ;
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.sprite = cardFace ;
    }
}
