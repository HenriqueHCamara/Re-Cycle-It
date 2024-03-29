﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public BinSO binSO;

    private Image _image;
    [SerializeField]
    private Sprite _sprite;
    [SerializeField]
    private Text binName;
    private string binType;

    public int id;

    private GameManager _gameManager;

    //audio shots
    public AudioClip correctTrash;
    public AudioClip incorrectTrash;
    [SerializeField]
    AudioSource _audioSource;

    //SOs

    //SFX Volume
    public GameObject volume;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            if (eventData.pointerDrag.GetComponent<Trash>().trashType == binType.ToString())
            {
                GameEvents.current.CorrectTrashDropped(id);
                _gameManager.GetComponent<GameManager>().IncreaseScore();
                Destroy(eventData.pointerDrag.gameObject);
                _gameManager.InstantiateNewTrash();
                _gameManager.DestroyAllBinsAndGeneratteNewOnes();
                volume.GetComponent<Animator>().SetTrigger("CorrectTrash");
            }
            else
            {
                GameEvents.current.IncorrectTrashDropped(id);
                _gameManager.RemoveLife();
                _gameManager.DestroyAllBinsAndGeneratteNewOnes();
                Destroy(eventData.pointerDrag.gameObject);
                _gameManager.InstantiateNewTrash();
            }
        }
    }

    private void Awake()
    {
        if (_gameManager == null)
        {
            _gameManager = FindObjectOfType<GameManager>();
        }
        if (volume == null)
        {
            volume = GameObject.Find("Global Volume");
        }

        AssignBin(_gameManager.SendRandomBinData());
    }

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onCorrectTrashDropped += onCorrectTrash;
        GameEvents.current.onIncorrectTrashDropped += onIncorrectTrash;
        _image = GetComponent<Image>();


        if (binSO)
        {
            _image.sprite = binSO.Sprite;
            binType = binSO.TrashType.ToString();
            binName.text = binSO.Name;

            if (_image.sprite)
            {
                switch (binSO.TrashType)
                {
                    case TrashType.Plastic:
                        _image.color = new Color(1, 0, 0, 1);
                        break;
                    case TrashType.Metal:
                        _image.color = new Color(1, (float)0.92, (float)0.016, 1);
                        break;
                    case TrashType.Paper:
                        _image.color = new Color(0, 0, 1, 1);
                        break;
                    case TrashType.Wood:
                        _image.color = new Color((float)0.1, (float)0.1, (float)0.1, 1);
                        break;
                    case TrashType.Glass:
                        _image.color = new Color(0, 1, 0, 1);
                        break;
                    case TrashType.Batteries:
                        _image.color = new Color(1, (float)0.54, 0, 1);
                        break;
                    default:
                        break;
                }
            }
        }


        if (_audioSource == null)
        {
            _audioSource = GetComponent<AudioSource>();
        }
    }

    private void onCorrectTrash(int id)
    {
        if (id == this.id)
        {
            _audioSource.PlayOneShot(correctTrash, 1);

            _gameManager.ChangeTimer(2f, true);
        }
    }

    private void onIncorrectTrash(int id)
    {
        if (id == this.id)
        {
            _audioSource.PlayOneShot(incorrectTrash, 1);

            _gameManager.ChangeTimer(1f, false);
        }
    }

    private void OnDestroy()
    {
        GameEvents.current.onCorrectTrashDropped -= onCorrectTrash;
        GameEvents.current.onIncorrectTrashDropped -= onIncorrectTrash;
    }

    public void AssignBin(BinSO data)
    {
        binSO = data;
    }
}
