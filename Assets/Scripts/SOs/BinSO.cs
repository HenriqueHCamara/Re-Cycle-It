using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Bin", menuName = "Trash/Create New Bin", order = 2)]
public class BinSO : ScriptableObject
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