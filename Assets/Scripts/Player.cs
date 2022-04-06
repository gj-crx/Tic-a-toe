using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{ //Contains some information about player and describes his intents
    /// <summary>
    /// 1 - X, 2 - O
    /// </summary>
    public int PlayerSide = 2;
    public bool IsAI = false;
    [HideInInspector] public AI ai;
    private Model m;

    void Start()
    {
        m = GameObject.Find("GameController").GetComponent<Model>();
        ai = GetComponent<AI>();
    }


    void Update()
    {
        
    }
    /// <summary>
    /// players only intent in this game
    /// </summary>
    public void ButtonPlaceMark(int PointID)
    {
        if (m.GameEnded)
        {
            m.ResetGame();
            return;
        }
        Engine.Point ClickedPoint = m.engine.PointFromID(PointID);
        if (m.State[ClickedPoint.x, ClickedPoint.y] != 0)
        {
            return;
        }
        m.PlaceMark(ClickedPoint, PlayerSide);
        m.ProcessTurn();
    }
}
