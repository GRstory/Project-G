using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InteractionController : MonoBehaviour
{
    [SerializeField] private Material _highlightMaterial;
    [SerializeField] public List<Interactionable> _interactionableObjectList = null;
    private Dictionary<string, Material> _materialDict = new Dictionary<string, Material>();


    public List<Interactionable> InteractionableObjectList { get { return _interactionableObjectList; } set { _interactionableObjectList = value; } }


    private void Start()
    {
        _interactionableObjectList = new List<Interactionable>();
        SceneManager.sceneLoaded += CallEventSceneChange;
    }

    private void OnTriggerEnter(Collider other)
    {
        Interactionable interactionable = other.transform.GetComponent<Interactionable>();
        if (interactionable != null)
        {
            _interactionableObjectList.Add(interactionable);
            /*if(_materialDict.TryGetValue(interactionable.GUID, out Material mat))
            {
                
            }
            else
            {
                MeshRenderer meshRenderer = other.GetComponent<MeshRenderer>();
                if (meshRenderer != null)
                {
                    Material originalMaterial = other.GetComponent<MeshRenderer>().material;
                    _materialDict.Add(interactionable.GUID, originalMaterial);
                }
            }

            SortInteractionablebject();
            ChangeObjectMaterialToHighlight(_interactionableObjectList[0]);*/
            CallEventHandlerChangeInteractionaText();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.GetComponent<Interactionable>() != null)
        {
            _interactionableObjectList.Remove(other.transform.GetComponent<Interactionable>());

            //ChangeObjectMaterialToOriginal(other.transform.GetComponent<Interactionable>());
            CallEventHandlerChangeInteractionaText();
        }
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
            if(item != null)
            {
                if (item.gameObject.activeSelf == false)
                {
                    _interactionableObjectList.Remove(item);
                }
            }
            else
            {
                _interactionableObjectList.Remove(item);
            }
        }
    }

    private void ChangeObjectMaterialToHighlight(Interactionable interactionable)
    {
        MeshRenderer meshRenderer = interactionable.GetComponent<MeshRenderer>();
        if(meshRenderer != null)
        {
            meshRenderer.material = _highlightMaterial;
        }
        for(int i = 1; i < _interactionableObjectList.Count; i++)
        {
            ChangeObjectMaterialToHighlight(_interactionableObjectList[i]);
        }
    }

    private void ChangeObjectMaterialToOriginal(Interactionable interactionable)
    {
        if(_materialDict.TryGetValue(interactionable.GUID, out Material originalMaterial))
        {
            interactionable.GetComponent<MeshRenderer>().material = originalMaterial;
        }
        
    }

    /// <summary>
    /// Collider에 충돌한 사용가능한 아이템 중 제일 가까운 아이템과 상호작용 시도
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
    /// 변수로 넣은 아이템을 Collider 리스트에서 제거
    /// </summary>
    public void RemoveInteractionableObjectList(Interactionable item)
    {
        if(_interactionableObjectList.Contains(item))
        {
            _interactionableObjectList.Remove(item);
        }
    }

    private void CallEventSceneChange(Scene scene, LoadSceneMode mode)
    {
        _interactionableObjectList.Clear();
        CallEventHandlerChangeInteractionaText();
    }

    private void CallEventHandlerChangeInteractionaText()
    {
        if (_interactionableObjectList.Count > 0)
        {
            while (_interactionableObjectList[0] == null)
            {
                RemoveInteractionableObjectList(_interactionableObjectList[0]);
            }
            if(_interactionableObjectList.Count > 0)
            {
                EventHandler.CallChangeInteractionableText(_interactionableObjectList[0].ID);
            }
            else
            {
                EventHandler.CallChangeInteractionableText(0);
            }
        }
        else
        {
            EventHandler.CallChangeInteractionableText(0);
        }
    }
}
