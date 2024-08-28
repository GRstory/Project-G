using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class Interactionable : MonoBehaviour
{
    [SerializeField] private string _guid = "";
    [SerializeField] protected int _id = 0;
    [SerializeField] protected GameEnum.InteractionType _interactionType = GameEnum.InteractionType.None;
    protected float _coolDownTime = 2f;
    protected int _interactionCount = 0;
    protected bool _canInteraction = true;

    private WaitForSeconds _waitForSeconds = null;
    private Outline _outline;

    public string GUID { get { return _guid; } }
    public int ID { get { return _id; } }
    public GameEnum.InteractionType interactionType { get { return _interactionType; } }
    public int InteractionCount { get { return _interactionCount; } }
    public bool CanInteraction { get { return _canInteraction; } }

    protected virtual void Awake()
    {
        if (!Application.IsPlaying(gameObject))
        {
            if (_guid == "")
            {
                _guid = System.Guid.NewGuid().ToString();
            }
        }
        _guid = System.Guid.NewGuid().ToString();
        _outline = GetComponent<Outline>();
    }

    protected virtual void Start()
    {
        Init();
    }

    protected virtual void Init()
    {
        _waitForSeconds = new WaitForSeconds(_coolDownTime);
        _outline.outlineMode = Outline.Mode.OutlineVisible;
        _outline.outlineColor = Color.black;
        _outline.outlineWidth = 3;

        gameObject.layer = 8;

    }

    public void TryInteraction(Transform fromTransform)
    {
        if(_canInteraction)
        {
            _interactionCount++;
            StartCoroutine(InteractionCoolDownCoroutine());

            Interaction(fromTransform);
        }
        else
        {
            CantInteraction(fromTransform);
        }
    }

    protected virtual void Interaction(Transform fromTransform)
    {

    }

    protected virtual void CantInteraction(Transform fromTransform)
    {
        
    }

    public virtual void FinishInteraction()
    {

    }

    IEnumerator InteractionCoolDownCoroutine()
    {
        _canInteraction = false;
        yield return _waitForSeconds;
        _canInteraction = true;
    }
}
