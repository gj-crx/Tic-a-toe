using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    Model m;
    public Text[] CellsTexts = new Text[9];
    /// <summary>
    /// 0 - win, 1 - lose
    /// </summary>
    public GameObject[] AlertsWinLose = new GameObject[3];
    public string[] Marks = new string[3];

    private void Awake()
    {
        m = GetComponent<Model>();
    }

    public void ShowMarks()
    {
        int a = 0;
        foreach (Engine.Point p in m.engine.Points)
        { //visualizing all points value
            CellsTexts[a].text = Marks[m.State[p.x, p.y]];
            a++;
        }
    }
}
