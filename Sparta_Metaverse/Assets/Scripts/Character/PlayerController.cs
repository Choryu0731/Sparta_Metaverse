using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody; // �̵��� ���� ���� ������Ʈ

    [SerializeField] private SpriteRenderer characterRenderer; // �¿� ������ �̿�
    [SerializeField] private float moveSpeed = 5f; // �̵� �ӵ�
    [SerializeField] private float interactionRadius = 1f;
    [SerializeField] private LayerMask interactableLayer;
    public GameObject ScanObject { get; private set; }

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

        if (Input.GetKeyDown(KeyCode.E))
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

    private void Movement(Vector2 direction)
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
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, interactionRadius, interactableLayer);
        ScanObject = null;

        GameObject closestInteractable = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (var hit in hits)
        {
            InteractionTrigger trigger = hit.GetComponent<InteractionTrigger>();
            if (trigger != null)
            {
                Vector3 directionToTarget = hit.transform.position - currentPosition;
                float dSqr = directionToTarget.sqrMagnitude;
                if (dSqr < closestDistanceSqr)
                {
                    closestDistanceSqr = dSqr;
                    closestInteractable = hit.gameObject;
                }
            }
        }

        if (closestInteractable != null)
        {
            Debug.Log("Closest interactable: " + closestInteractable.name);
            ScanObject = closestInteractable;
            closestInteractable.GetComponent<InteractionTrigger>()?.Trigger();
        }
        else
        {
            Debug.Log("No interactable object found.");
        }
    }
}
