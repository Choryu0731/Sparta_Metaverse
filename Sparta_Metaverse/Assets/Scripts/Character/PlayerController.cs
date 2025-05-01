using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody; // 이동을 위한 물리 컴포넌트

    [SerializeField] private SpriteRenderer characterRenderer; // 좌우 반전에 이용
    [SerializeField] private float moveSpeed = 5f; // 이동 속도

    protected Vector2 movementDirection = Vector2.zero;
    public Vector2 MovementDirection { get { return movementDirection; } }

    protected void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
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
        float horizontal = Input.GetAxisRaw("Horizontal"); // 좌 우 이동
        float vertical = Input.GetAxisRaw("Vertical"); // 상 하 이동

        movementDirection = new Vector2(horizontal, vertical).normalized; // 방향 벡터 정규화(대각선 이동 시 속도 보정)
    }

    private void Movement (Vector2 direction)
    {
        _rigidbody.velocity = direction * moveSpeed;
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
        //InteractionTrigger와 연동할 예정
    }
}
