using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridRenderer : MonoBehaviour
{
    public static GridRenderer Instance { get; set; }
    
    public MapGridItemBehavior ItemPrefab;
    public Transform GridHolder;
    MapGridItemBehavior[,] gridRendererItems;

    [SerializeField] GridLayoutGroup gridLayoutGroup;

    int curWidth;
    int curHeight;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    
    public void CreateGrid(int width, int heigh)
    {
        curWidth = width;
        curHeight = heigh;
        
        gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayoutGroup.constraintCount = width;
        
        gridRendererItems = new MapGridItemBehavior[width, heigh];
        
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < heigh; j++)
            {
                MapGridItemBehavior mapGridItem = Instantiate(ItemPrefab, GridHolder).GetComponent<MapGridItemBehavior>();
                mapGridItem.SetPosition(i, j);
                gridRendererItems[i, j] = mapGridItem;
            }
        }
    }

    public void UpdateGrid(int[,] gridValues)
    {
        for (int i = 0; i < curWidth; i++)
        {
            for (int j = 0; j < curHeight; j++)
            {
                gridRendererItems[i, j].UpdateValue((MapGridBehavior.GridValue) gridValues[i,j]);
            }
        }
    }
}
