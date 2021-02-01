using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trash : MonoBehaviour
{
    public TrashSO trashSO;

    public GameManager gameManager;

    public Image _image;
    [HideInInspector]
    public string trashType;
    [SerializeField]
    private Text trashName;

    private void Awake()
    {
        
        if (gameManager == null)
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }

        AssignTrash(gameManager.SendRandomTrashData());
    }

    // Start is called before the first frame update
    void Start()
    {
        if (trashSO)
        {
            _image.sprite = trashSO.Sprite;
            trashType = trashSO.TrashType.ToString();
            trashName.text = trashSO.Name;
        }
    }

    public void AssignTrash(TrashSO data) 
    {
        trashSO = data;
    }
}
