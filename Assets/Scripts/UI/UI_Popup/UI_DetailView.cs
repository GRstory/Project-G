using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class UI_DetailView : UI_Popup
{
    private GameObject _overlayCamera;
    private Vector3 _overlayPosition;
    private GameObject _overlayObject;
    private GameObject _player;
    private InteractionableEvidence _currentObject = null;

    [SerializeField] private TMP_Text _itemName = null;

    public float _rotateDegree = 30f;

    private void Awake()
    {
        _overlayCamera = GameObject.FindGameObjectWithTag("OverlayCamera").transform.GetChild(0).gameObject;
        _player = GameObject.FindGameObjectWithTag("Player");
        _overlayPosition = GameObject.FindGameObjectWithTag("OverlayCamera").transform.GetChild(0).position;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        _overlayCamera.SetActive(true);
        _player.GetComponent<PlayerMovementAdvanced>().DeactiveInput();
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        _overlayCamera.SetActive(false);
        _player.GetComponent<PlayerMovementAdvanced>().ActiveInput();

        if (_currentObject != null)
        {
            _currentObject.FinishInteraction();
        }                   

        Destroy(_overlayObject);
    }

    /// <summary>
    /// 오브젝트 갱신
    /// </summary>
    public void UpdateObject(InteractionableEvidence newObject, float position)
    {
        GameObject instance = Instantiate(newObject._prefap, _overlayPosition + new Vector3(0, 0, position), Quaternion.Euler(0f, 30f, 0f));

        Destroy(_overlayObject);
        _overlayObject = instance;

        _currentObject = newObject;
        _itemName.text = newObject.name;
    }

    public void OnPressButtonRight()
    {
        if(_overlayObject != null)
        {
            _overlayObject.transform.Rotate(Vector3.forward * _rotateDegree);
        }
    }

    public void OnPressButtonLeft()
    {
        if(_overlayObject != null)
        {
            _overlayObject.transform.Rotate(Vector3.back * _rotateDegree);
        }
    }

    public void OnPressButtonUp()
    {
        if(_overlayObject != null)
        {
            _overlayObject.transform.Rotate(Vector3.right * _rotateDegree);
        }
    }

    public void OnPressButtonDown()
    {
        if(_overlayObject != null)
        {
            _overlayObject.transform.Rotate(Vector3.left * _rotateDegree);
        }
    }
}
