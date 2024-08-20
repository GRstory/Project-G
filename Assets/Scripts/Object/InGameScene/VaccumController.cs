using UnityEngine;
using UnityEngine.AI;

public class VaccumController : MonoBehaviour
{
    private enum State
    {
        Cleaning,
        Charging
    }
    [SerializeField] private Transform[] _patrolPoint;
    [SerializeField] private Vector3 _chargePoint;
    private NavMeshAgent _agent;

    public int _index = 0;
    public int _maxIndex;
    public float _maxChargingTime = 5;
    public float _maxCleaningTime = 60f;
    public float _behaviourTime = 0f;
    [SerializeField] private State _state = State.Cleaning;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _maxIndex = _patrolPoint.Length;
        _chargePoint = transform.position;
    }

    private void Update()
    {
        if(_state == State.Cleaning)
        {
            _behaviourTime += Time.deltaTime;
            if(_behaviourTime > _maxCleaningTime)
            {
                _behaviourTime = 0f;
                _state = State.Charging;
            }
            else
            {
                Cleaning();
            }
        }
        else if(_state == State.Charging)
        {
            Charging();
        }
    }

    private void Cleaning()
    {
        _agent.SetDestination(_patrolPoint[_index].position);

        if (Vector3.Distance(transform.position, _patrolPoint[_index].position) <= 0.3f)
        {
            _index = (_index + 1) % _maxIndex;
            _agent.SetDestination(_patrolPoint[_index].position);
        }
    }

    private void Charging()
    {
        _agent.SetDestination(_chargePoint);

        if(Vector3.Distance(transform.position, _chargePoint) <= 0.3f)
        {
            _behaviourTime += Time.deltaTime;
        }
        if(_behaviourTime > _maxChargingTime)
        {
            _state = State.Cleaning;
        }
    }
}
