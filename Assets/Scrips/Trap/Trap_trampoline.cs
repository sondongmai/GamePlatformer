using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap_trampoline : MonoBehaviour
{
    // Start is called before the first frame update
    protected Animator _animator;
    [SerializeField] private float pushPower;
    [SerializeField] private float duration = .5f;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            player.Push(transform.up * pushPower, duration);
            _animator.SetTrigger("isActive");
        }
    }

}
