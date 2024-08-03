using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InteractionController : MonoBehaviour
{
    [SerializeField] public List<Interactionable> _interactionableObjectList = null;
    private UI_HUD _hudUI;

    public List<Interactionable> InteractionableObjectList { get { return _interactionableObjectList; } set { _interactionableObjectList = value; } }


    private void Start()
    {
        _interactionableObjectList = new List<Interactionable>();
        _hudUI = UIManager.Instance.StaticUI as UI_HUD;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<Interactionable>() != null)
        {
            _interactionableObjectList.Add(other.transform.GetComponent<Interactionable>());
        }

        CallEventHandlerChangeInteractionaText();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.GetComponent<Interactionable>() != null)
        {
            _interactionableObjectList.Remove(other.transform.GetComponent<Interactionable>());
        }

        CallEventHandlerChangeInteractionaText();
    }

    private void SortInteractionablebject()
    {
        if (_interactionableObjectList.Count > 1)
        {
            _interactionableObjectList.Sort((a, b) =>
            {
                float distanceA = Vector3.Distance(transform.position, a.transform.position);
                float distanceB = Vector3.Distance(transform.position, b.transform.position);
                return distanceA.CompareTo(distanceB);
            });
        }
    }

    private void CheckInteractionableObjectIsActive()
    {
        foreach(Interactionable item in  _interactionableObjectList)
        {
            if (item.gameObject.activeSelf == false)
            {
                _interactionableObjectList.Remove(item);
            }
        }
    }

    /// <summary>
    /// Collider�� �浹�� ��밡���� ������ �� ���� ����� �����۰� ��ȣ�ۿ� �õ�
    /// </summary>
    public void InteractionToObject()
    {
        CheckInteractionableObjectIsActive();
        SortInteractionablebject();
        if (_interactionableObjectList.Count > 0)
        {
            _interactionableObjectList[0].TryInteraction(transform);
        }
    }

    /// <summary>
    /// ������ ���� �������� Collider ����Ʈ���� ����
    /// </summary>
    public void RemoveInteractionableObjectList(Interactionable item)
    {
        if(_interactionableObjectList.Contains(item))
        {
            _interactionableObjectList.Remove(item);
        }
    }

    private void CallEventHandlerChangeInteractionaText()
    {
        if (_interactionableObjectList.Count > 0)
        {
            EventHandler.CallChangeInteractionableText(_interactionableObjectList[0].ID);
        }
        else
        {
            EventHandler.CallChangeInteractionableText(0);
        }
    }
}
