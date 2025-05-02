using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody; // �̵��� ���� ���� ������Ʈ

    [SerializeField] private SpriteRenderer characterRenderer; // �¿� ������ �̿�
    [SerializeField] private float moveSpeed = 5f; // �̵� �ӵ�

    protected AnimationHandler animationHandler;

    protected Vector2 movementDirection = Vector2.zero;
    public Vector2 MovementDirection { get { return movementDirection; } }

    protected void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        animationHandler = GetComponent<AnimationHandler>();
    }

    protected void Start()
    {
        
    }

    protected void Update()
    {
        HandleAction();
        Rotate(movementDirection);

        if(Input.GetKeyDown(KeyCode.E))
        {
            TryInteract();
        }
    }

    protected void FixedUpdate()
    {
        Movement(movementDirection);
    }

    protected void HandleAction()
    {
        float horizontal = Input.GetAxisRaw("Horizontal"); // �� �� �̵�
        float vertical = Input.GetAxisRaw("Vertical"); // �� �� �̵�

        movementDirection = new Vector2(horizontal, vertical).normalized; // ���� ���� ����ȭ(�밢�� �̵� �� �ӵ� ����)
    }

    private void Movement (Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            _rigidbody.velocity = direction * moveSpeed;
            animationHandler.Move(direction);
        }
        else
        {
            _rigidbody.velocity = Vector2.zero;
            animationHandler.Move(Vector2.zero);
        }
    }

    private void Rotate(Vector2 direction)
    {
        if (direction.x < 0)
            characterRenderer.flipX = true;
        else if (direction.x > 0)
            characterRenderer.flipX = false;
    }

    private void TryInteract()
    {
        //InteractionTrigger�� ������ ����
    }
}
