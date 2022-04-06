using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public Player p;
    private Model m;
    private Engine e;
    void Start()
    {
        m = GameObject.Find("GameController").GetComponent<Model>();
        e = m.engine;
        p = GetComponent<Player>();
    }

    public void PlayTurn()
    {
        int EnemyPlayerSide = 1;
        if (p.PlayerSide == 1)
        {
            EnemyPlayerSide = 2;
        }

        int EnemyMarksInTheLane = CalculateBestMoveValue(EnemyPlayerSide);
        int AIMarksInTheLane = CalculateBestMoveValue(p.PlayerSide);

        if (EnemyMarksInTheLane == 2 && AIMarksInTheLane < 2)
        { //if enemy can win within 1 move AI should stop him
            PlaceMarkInLine(FindBestMoveLine(EnemyPlayerSide), p.PlayerSide); //find the best possible enemy move and block it
        }
        else
        {
            PlaceMarkInLine(FindBestMoveLine(p.PlayerSide), p.PlayerSide);
        }
    }
    /// <summary>
    /// returns the amount of marks of certain side
    /// </summary>
    private int CheckLine(Engine.Line line, int CheckForWhat = 1)
    {
        int CountMarksOnLine = 0;
        foreach (Engine.Point p in line.points)
        {
            if (m.State[p.x, p.y] == CheckForWhat)
            {
                CountMarksOnLine++;
            }
            else
            {
                if (m.State[p.x, p.y] != 0) //if line already has a mark of enemy
                {
                    return -1;
                }
            }
        }
        return CountMarksOnLine;
    }
    /// <summary>
    /// Return the number of line with a best possible move
    /// </summary>
    private int CalculateBestMoveValue(int PlayerSide)
    {
        int max = -2;
        for (int i = 0; i < e.Lines.Length; i++)
        {
            int CurrentNumber = CheckLine(e.Lines[i], PlayerSide);
            if (CurrentNumber > max)
            {
                max = CurrentNumber;
            }
        }
        return max;
    }
    /// <summary>
    /// returns line with the best possible move for player
    /// </summary>
    private Engine.Line FindBestMoveLine(int PlayerSide)
    {
        int max = -2;
        Engine.Line BestMoveLine = new Engine.Line();
        for (int i = 0; i < e.Lines.Length; i++)
        {
            int CurrentNumber = CheckLine(e.Lines[i], PlayerSide);
            if (CurrentNumber > max)
            {
                max = CurrentNumber;
                BestMoveLine = e.Lines[i];
            }
        }
        return BestMoveLine;
    }
    private void PlaceMarkInLine(Engine.Line line, int Side)
    {
        for (int i = 0; i < line.points.Length; i++)
        {
            if (m.State[line.points[i].x, line.points[i].y] == 0)
            {
                m.PlaceMark(line.points[i], Side);
                break;
            }
        }
    }

}
