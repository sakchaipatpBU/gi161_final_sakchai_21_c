using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float walkSpeed;
    public float WalkSpeed
    {
        get
        {
            return walkSpeed;
        }
        set
        {
            walkSpeed = Mathf.Clamp(value,-10,10);
        }
    }
    [SerializeField] private float minXOffset = 1.5f, maxXOffset = 1.5f, minYOffset = 1.5f, maxYOffset = 1.5f;
    private float minX, maxX, minY, maxY;
    public float MinY => minY;
    public float MaxY => maxY;

    private Camera mainCamera;

    public static PlayerController Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        mainCamera = Camera.main;
        ReSizeBorder();
    }

    void Update()
    {
        Movement();

    }
    private void Movement()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        transform.position += transform.up * vertical * WalkSpeed * Time.deltaTime;
        transform.position += transform.right * horizontal * WalkSpeed * Time.deltaTime;
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, minX, maxX),Mathf.Clamp(transform.position.y, minY, maxY));

    }

    private void ReSizeBorder()
    {
        minX = mainCamera.ViewportToWorldPoint(Vector2.zero).x + minXOffset;
        minY = mainCamera.ViewportToWorldPoint(Vector2.zero).y + minYOffset;
        maxX = mainCamera.ViewportToWorldPoint(Vector2.right).x - maxXOffset;
        maxY = mainCamera.ViewportToWorldPoint(Vector2.up).y - maxYOffset;
    }
}

