using System.Collections ;
using System.Collections.Generic ;
using UnityEngine ;
using UnityEngine.SceneManagement ;
using UnityEngine.UI ;

public class UIButton : MonoBehaviour
{
    public GameObject collectWinningsPanel ;
    public GameObject nextCardPanel ;
    Run21 run21 ;

    // Start is called before the first frame update
    void Start()
    {
        run21 = FindObjectOfType< Run21 >() ;
    }

    // Update is called once per frame
    void Update()
    {
    //    if (collectWinningsPanel.activeSelf)
    //    {
    //        collectWinningsPanel.SetActive(true);
    //    }
    }

    public void EndGame()
    {
        //print( "clicked" ) ;
        collectWinningsPanel.SetActive( true ) ;
        //collectWinningsPanel.transform.GetChild(0).GetComponent<Text>().text
        //    = "You Win ! \nFinal Score : " + run21.score;
        //GetComponent< Run21 >().ResetNeededStuff() ;
    }

    public void NextCard()
    {

    }
}
