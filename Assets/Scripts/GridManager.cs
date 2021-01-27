using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField]
    private int rows = 6;
    [SerializeField]
    private int columns = 6;
    [SerializeField]
    private float tileSize = 1;
    [SerializeField]
    private float posX = 0.0f;
    [SerializeField]
    private float posY = 0.0f;
    [SerializeField]
    private bool isUI = false;

    private RectTransform _rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        if (!isUI)
        {
            GenerateGrid();
        }
        else
        {
            _rectTransform = GetComponent<RectTransform>();
            GenerateGridUI();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateGrid() 
    {
        GameObject referenceTile = (GameObject)Instantiate(Resources.Load("GridSlot"), transform);
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                GameObject tile = Instantiate(referenceTile, transform);
                tile.name = (r+1) + "x" + (c+1);
                float posX = c * tileSize;
                float posY = r * -tileSize;

                tile.transform.position = new Vector2(posX, posY);
            }
        }
        Destroy(referenceTile);

        float gridWidth = columns * tileSize;
        float gridHeight = rows * tileSize;
        //transform.position = new Vector2(-gridWidth / 2 + tileSize / 2, gridHeight / 2 + tileSize / 2);
        transform.position = new Vector2(posX, posY);
    }

    public void GenerateGridUI()
    {
        GameObject referenceTile = (GameObject)Instantiate(Resources.Load("Slot"), _rectTransform);
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                GameObject tile = Instantiate(referenceTile, _rectTransform);
                tile.name = (r + 1) + "x" + (c + 1);
                float posX = c * tileSize;
                float posY = r * -tileSize;
                
                tile.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX, posY);
            }
        }
        Destroy(referenceTile);

        float gridWidth = columns * tileSize;
        float gridHeight = rows * tileSize;
        //transform.position = new Vector2(-gridWidth / 2 + tileSize / 2, gridHeight / 2 + tileSize / 2);
        //transform.position = new Vector2(posX, posY);
    }
}
