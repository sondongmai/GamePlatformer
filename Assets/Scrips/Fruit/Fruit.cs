using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{

    public enum FruitType { Cherry, apple, melon, Banana, Orange, Kiwi, Strawberry }
    private GameManger gameManager;
    private Animator animator;
    [SerializeField] private FruitType fruitType;
    [SerializeField] private GameObject pickUpVfx;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();

    }
    private void Start()
    {
        gameManager = GameManger.Instance;
        SetRandomLook();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            gameManager.AddFruit();
            Destroy(gameObject);
            AudioManager.instance.PlaySFX(8);
            GameObject vfx = Instantiate(pickUpVfx, transform.position, Quaternion.identity);
            Destroy(vfx, 0.3f);
        }
    }
    private void SetRandomLook()
    {
        if (!gameManager.HaveRandomLook())
        {
            UpdateFruitVisual();
            return;
        }
        float randomIndex = Random.RandomRange(0, 7);
        animator.SetFloat("setfloat", randomIndex);
    }

    private void UpdateFruitVisual()
    {
        animator.SetFloat("setfloat", (int)fruitType);
    }
}
