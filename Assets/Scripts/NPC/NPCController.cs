using UnityEngine;

public class NPCController : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Test()
    {
        _animator.SetTrigger("itch");
    }
}
