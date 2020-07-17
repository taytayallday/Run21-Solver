using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using System.Linq ;
using MLAgentsExamples;
using UnityEngine.SceneManagement ;
using UnityEngine.UI ;
public class Run21Agent : Agent
{
    //public Run21 run21 ;
    //public GameObject currentCard ;
    //public GameObject[] hand ;
    //public GameObject[] handsSums ;
    //public GameObject score ;
    //public int col ;

    //public override void InitializeAgent()
    //{
    //    //run21 = GetComponent<Run21>();
    //    //currentCard = GetComponent<GameObject>();
    //    //hand = GetComponent<GameObject[]>();
    //    //playerScore = GetComponent<GameObject>();
    //    //handsSums = GetComponent<GameObject[]>();
    //    //score = GetComponent<GameObject>();
    //}

    public override void AgentAction(float[] vectorAction)
    {
        // Move the agent using the action.
        int col = Mathf.FloorToInt( vectorAction[ 0 ] ) ;
        print( col ) ;
        //if ( col != -1 )
            //run21.Stack( col ) ;
        //if ( col >= 0 && col <= 4 )
        //{
        //    //print( col ) ;
        //    //GetComponentInParent< Run21 >().Stack( col ) ;
        //    score.GetComponent< HandScript >().sum = col ;
        //}
    }

    public override float[] Heuristic()
    {


        if (Input.GetKey(KeyCode.Keypad1))
        {
            //print( 1 ) ;
            return new float[] { 0 };
        }
        if (Input.GetKey(KeyCode.Keypad2))
        {
            //print( 2 ) ;
            return new float[] { 1 };
        }
        if (Input.GetKey(KeyCode.Keypad3))
        {
            //print( 3 ) ;
            return new float[] { 2 };
        }
        if (Input.GetKey(KeyCode.Keypad4))
        {
            //print( 4 ) ;
            return new float[] { 3 };
        }
        if (Input.GetKey(KeyCode.Keypad5))
        {
            //print( 5 ) ;
            return new float[] { 4 };
        }
        if (Input.GetKey(KeyCode.Keypad6))
        {
            //print( 6 ) ;
            return new float[] { 5 };
        }
        else
        {
            //print( -1 ) ;
            return new float[] { -1 };
        }
    }

    //private void Update()
    //{
    //    Heuristic() ;
    //}

    //public override void AgentReset()
    //{
    //    //SceneManager.LoadScene( 1 ) ;
    //}

    //public void FixedUpdate()
    //{
    //    if (GetStepCount() % 5 == 0)
    //    {
    //        RequestDecision();
    //    }
    //    else
    //    {
    //        RequestAction();
    //    }
    //}

}
