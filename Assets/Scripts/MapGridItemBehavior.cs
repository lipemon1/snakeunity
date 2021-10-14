using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapGridItemBehavior : MonoBehaviour
{
    public Image Image;
    public Text CellValue;
    
    public int Width;
    public int Heigh;

    public void SetPosition(int width, int heigh)
    {
        Width = width;
        Heigh = heigh;

        CellValue.text = Width + "." + Heigh;
    }
    
    public void UpdateValue(MapGridBehavior.GridValue newValue)
    {
        switch (newValue)
        {
            case MapGridBehavior.GridValue.Blank:
                Image.color = Color.white;
                break;
            case MapGridBehavior.GridValue.SnakeBody:
                Image.color = Color.green;
                break;
            case MapGridBehavior.GridValue.Apple:
                Image.color = Color.red;
                break;
            case MapGridBehavior.GridValue.Wall:
                Image.color = Color.black;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newValue), newValue, null);
        }
    }
}
