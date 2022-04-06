using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model : MonoBehaviour
{
    public int[,] State = new int[3, 3];
    /// <summary>
    /// 0 - no marks, 1 - X, 2 - O
    /// </summary>
    public bool GameEnded { get; private set; }



    public UI ui;
    public FileWork fw;
    public Player[] players;
    public Engine engine;


    void Start()
    {

    }


    void Update()
    {
        
    }
    public void ProcessTurn()
    {
        foreach (var v in players)
        {
            if (v != null && v.IsAI)
            {
                v.ai.PlayTurn();
            }
        }
        fw.SaveState(); //saving state after the end of each turn
    }
    /// <summary>
    /// 0 - draw, 1 - X won, 2 - O won, 3 - game not ended
    /// <returns></returns>
    public int CheckWinCondition()
    {
        int WinnerSide = 0;
        for (int i = 1; i <= 2; i++)
        {
            foreach (var l in engine.Lines)
            {
                bool matches = true;
                foreach (Engine.Point p in l.points)
                {
                    if (State[p.x, p.y] != i)
                    {
                        matches = false;
                        break;
                    }
                }
                if (matches == true)
                {
                    return i;
                }
            }
        }
        //check for the draw
        foreach (int i in State)
        {
            if (i == 0)
            { //game is not ended yet, there is still some space left
                return 3;
            }
        }
        //draw
        return 0;
    }
    public void PlaceMark(Engine.Point p, int Mark)
    { //changing the state of the game
        State[p.x, p.y] = Mark;
        ui.CellsTexts[engine.GetPointID(p)].text = ui.Marks[Mark];
        int GameResult = CheckWinCondition();
        if (GameResult != 3)
        {
            GameEnded = true;
            if (GameResult == 0)
            {
                ui.AlertsWinLose[2].SetActive(true);
            }
            else
            {
                if (players[0].PlayerSide == GameResult)
                {
                    ui.AlertsWinLose[0].SetActive(true);
                }
                else
                {
                    ui.AlertsWinLose[1].SetActive(true);
                }
            }
        }
    }
    public void ResetGame()
    {
        GameEnded = false;
        State = new int[3, 3]; 
        foreach (var v in ui.AlertsWinLose)
        {
            v.SetActive(false);
        }
        foreach (var v in ui.CellsTexts)
        {
            v.text = ui.Marks[0];
        }
    }
}
