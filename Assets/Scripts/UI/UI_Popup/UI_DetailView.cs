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
        _overlayCamera = GameObject.FindGameObjectWithTag("OverlayCamera");
        _player = GameObject.FindGameObjectWithTag("Player");
        _overlayPosition = _overlayCamera.transform.position + _overlayCamera.transform.forward * 4;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        _player.GetComponent<PlayerController>().DeactiveInput();
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        _player.GetComponent<PlayerController>().ActiveInput();

        if (_currentObject != null)
        {
            _currentObject.FinishInteraction();
        }                   

        Destroy(_overlayObject);
    }

    /// <summary>
    /// 오브젝트 갱신
    /// </summary>
    public void UpdateObject(InteractionableEvidence newObject)
    {
        GameObject instance = Instantiate(newObject._prefap, _overlayPosition, Quaternion.Euler(0f, 180f, 0f));
        instance.transform.Translate(Vector3.down);

        Destroy(_overlayObject);
        _overlayObject = instance;

        _currentObject = newObject;
        _itemName.text = newObject.name;
    }

    public void OnPressButtonRight()
    {
        if(_overlayObject != null)
        {
            _overlayObject.transform.Rotate(Vector3.up * _rotateDegree);
        }
    }

    public void OnPressButtonLeft()
    {
        if(_overlayObject != null)
        {
            _overlayObject.transform.Rotate(Vector3.down * _rotateDegree);
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
