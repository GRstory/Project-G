using DG.Tweening;
using System.Collections;
using UnityEngine;

public class AiVaccum : MonoBehaviour
{
    private enum State
    {
        Move,
        RotateLeft,
        RotateRight,
        Charge
    }

    public enum Turn
    {
        None,
        Left,
        Right,
    }


    float _detectDistance = 0.5f;
    [SerializeField] private State _state = State.Move;
    [SerializeField] private Turn _lastTurn = Turn.None;

    bool _turning = false;
    
    void Update()
    {
        SetState();
        DoState();
    }

    private void SetState()
    {
        Debug.DrawRay(transform.position, transform.forward * _detectDistance);
        if (_state == State.Move)
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, _detectDistance))
            {
                float distLeft = 0, distRight = 0;

                if (Physics.Raycast(transform.position, transform.right, out RaycastHit rhit))
                {
                    distLeft = (transform.position - rhit.point).magnitude;
                    Debug.DrawRay(transform.position, transform.right * distLeft, Color.red, 1f);
                }
                if (Physics.Raycast(transform.position, -transform.right, out RaycastHit lhit))
                {
                    distRight = (transform.position - lhit.point).magnitude;
                    Debug.DrawRay(transform.position, -transform.right * distRight, Color.blue, 1f);
                }

                if(_lastTurn == Turn.Left)
                {
                    _state = State.RotateLeft;
                    return;
                }
                if(_lastTurn == Turn.Right)
                {
                    _state = State.RotateRight;
                    return;
                }
                if (distLeft > distRight)
                {
                    _state = State.RotateLeft;
                    return;
                }
                else
                {
                    _state = State.RotateRight;
                    return;
                }
            }
            else
            {
                _state = State.Move;
            }

            _lastTurn = Turn.None;
            return;
        }
    }

    private void DoState()
    {
        switch (_state)
        {
            case State.RotateLeft:
                if(_turning == false)
                {
                    StartCoroutine(RotateLeftCoroutine());
                }
                break;
            case State.RotateRight:
                if(_turning == false)
                {
                    StartCoroutine(RotateRightCoroutine());
                }
                break;
            case State.Move:
                transform.position += transform.forward * Time.deltaTime;
                break;
        }
    }

    IEnumerator RotateLeftCoroutine()
    {
        _turning = true;
        _lastTurn = Turn.Left;

        float time = 0;
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = transform.rotation * Quaternion.Euler(0, -45, 0);
        while (time < 1f)
        {
            time += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, time / 1);

            yield return null;
        }

        transform.rotation = endRotation;
        _state = State.Move;
        _turning = false;
    }

    IEnumerator RotateRightCoroutine()
    {
        _turning = true;
        _lastTurn = Turn.Right;

        float time = 0;
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = transform.rotation * Quaternion.Euler(0, 45, 0);
        while (time < 1f)
        {
            time += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, time / 1);

            yield return null;
        }

        transform.rotation = endRotation;
        _state = State.Move;
        _turning = false;
    }
}
