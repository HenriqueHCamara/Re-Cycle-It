using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;
    private void Awake()
    {
        current = this;
    }

    public event Action<int> onCorrectTrashDropped;
    public void CorrectTrashDropped(int id) 
    {
        if (onCorrectTrashDropped!=null)
        {
            onCorrectTrashDropped(id);
        }
    }

    public event Action<int> onIncorrectTrashDropped;
    public void IncorrectTrashDropped(int id)
    {
        if (onIncorrectTrashDropped != null)
        {
            onIncorrectTrashDropped(id);
        }
    }

    public event Action<bool> OnGameOver;
    public void GameOver(bool isGameOver)
    {
        if (OnGameOver != null)
        {
            OnGameOver(isGameOver);
        }
    }
}
