using System.Collections;
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
    private string binType;

    public int id;

    private GameManager _gameManager;

    //audio shots
    public AudioClip correctTrash;
    public AudioClip incorrectTrash;
    AudioSource _audioSource;

    //SOs

    //SFX Volume
    public GameObject volume;

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            if (eventData.pointerDrag.GetComponent<Trash>().trashType == binType.ToString())
            {
                GameEvents.current.CorrectTrashDropped(id);
                _gameManager.GetComponent<GameManager>().IncreaseScore();
                Destroy(eventData.pointerDrag.gameObject);
                _gameManager.InstantiateNewTrash();
                volume.GetComponent<Animator>().SetTrigger("CorrectTrash");
            }
            else
            {
                GameEvents.current.IncorrectTrashDropped(id);
            }
        }
    }

    private void Awake()
    {
        if (_gameManager == null) 
        {
            _gameManager = FindObjectOfType<GameManager>();
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
            Debug.Log("correct trash dropped");
            _audioSource.PlayOneShot(correctTrash);
            _gameManager.ChangeTimer(2f, true);
        }
    }

    private void onIncorrectTrash(int id)
    {
        if (id == this.id)
        {
            Debug.Log("incorrect trash dropped");
            _audioSource.PlayOneShot(incorrectTrash);
            _gameManager.ChangeTimer(5f, false);
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
