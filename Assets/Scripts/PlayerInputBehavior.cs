using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputBehavior : MonoBehaviour
{
    public static PlayerInputBehavior Instance { get; set; }

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
    
    float Horizontal;
    float Vertical;

    Direction lastDir;

    void Awake()
    {
        if (Instance == null)
            Instance = this;

        lastDir = Direction.Right;
    }

    // Update is called once per frame
    void Update()
    {
        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");

        if (Horizontal != 0f)
        {
            if (Horizontal > 0f)
                lastDir = Direction.Right;
            else
                lastDir = Direction.Left;
        }
        else if(Vertical != 0f)
        {
            if (Vertical > 0f)
                lastDir = Direction.Up;
            else
                lastDir = Direction.Down;
        }
    }

    public Direction LastDirection()
    {
        return lastDir;
    }
}
