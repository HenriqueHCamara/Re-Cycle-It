using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrashType { Plastic, Metal, Paper, Wood, Glass, Batteries}
[CreateAssetMenu(fileName = "new trash", menuName = "Trash/Create New Trash", order = 1)]
public class TrashSO : ScriptableObject
{

    [SerializeField] private new string name;
    [SerializeField] private string description;

    [SerializeField] private Sprite sprite;
    [SerializeField] private TrashType trashType;

    public string Name { get => name; set => name = value; }
    public string Description { get => description; set => description = value; }
    public Sprite Sprite { get => sprite; set => sprite = value; }
    public TrashType TrashType { get => trashType; set => trashType = value; }
}
