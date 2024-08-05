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
    [SerializeField] private bool _canMovePlayer = true;

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


    private void InteractionToObject()
    {
        _interactionController.InteractionToObject();
    }
}
