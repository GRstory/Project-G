using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //조작
    private CharacterController _controller;
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private InteractionController _interactionController;
    [SerializeField] private Transform _cameraTransform;
    private Vector2 _moveInput;
    private bool _canMovePlayer = true;

    //일시정지
    public bool _isPaused = false;
    public bool _isInventory = false;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Start()
    {
        FlowManager.Instance.Player = gameObject;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (_canMovePlayer) MovePlayer();
    }

    private void FixedUpdate()
    {

    }

    public void ActiveInput()
    {
        _canMovePlayer = true;
        _cameraTransform.GetComponent<Unity.Cinemachine.CinemachineInputAxisController>().enabled = true;
    }

    public void DeactiveInput()
    {
        _canMovePlayer = false;
        _cameraTransform.GetComponent<Unity.Cinemachine.CinemachineInputAxisController>().enabled = false;
    }

    public void ActionMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    public void ActionInteraction()
    {
        if (_canMovePlayer && _canMovePlayer) InteractionToObject();
    }

    public void ActionEscape(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            UIManager.Instance.OnDeactivePopupUI();
        }
    }

    public void ActionInventory(InputAction.CallbackContext context)
    {
        if (context.performed && _isInventory == false && _canMovePlayer)
        {
            UIManager.Instance.OnActivePopupUI(UIManager.Instance.InventoryUI);
            _isInventory = true;
        }
    }

    private void MovePlayer()
    {
        Vector3 moveDir = _cameraTransform.forward * _moveInput.y + _cameraTransform.right * _moveInput.x;
        moveDir.y = 0;
        
        _controller.Move(moveDir * Time.deltaTime * 10);
    }

    //플레이어 조작시 Ray가 오브젝트를 인식 못하는 버그가 있음
    /*private void ShootRayToTarget()
    {
        RaycastHit hit;
        Vector3 rayOrigin = _playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
        Vector3 rayDir = _playerCamera.transform.forward * 3f;
        Debug.DrawRay(rayOrigin, rayDir, Color.red, 0.1f);
        if (Physics.Raycast(rayOrigin, rayDir, out hit, 3f))
        {
            GameObject target = hit.collider.gameObject;
            InteractionableObject targetComponent = target.GetComponent<InteractionableObject>();

            if (targetComponent == null)
            {
                if (_hudUI != null)
                {
                    _hudUI.SetInteractionText(0);
                }
            }
            else
            {
                if (_hudUI != null)
                {
                    _hudUI.SetInteractionText(targetComponent.ID);
                }
            }
        }
    }*/

    private void InteractionToObject()
    {
        _interactionController.InteractionToObject();
    }
}
