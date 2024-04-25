using UnityEngine;

public class TopDownMovement : MonoBehaviour
{
    public bool fishingState;
    public float speed;
    [SerializeField] private GameObject casino;
    [SerializeField] private GameObject pier;
    private Vector3 target;
    private Rigidbody2D rb;
    private bool moveTo;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!fishingState)
        {
            // Move automaticly to target position when moveTo is true
            if (moveTo)
            {
                Vector3 currentPos = transform.position;
                Vector3 moveTowards = Vector2.MoveTowards(currentPos, target, speed * Time.deltaTime);
                transform.position = moveTowards;
                Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, moveTowards);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 10);
                if (transform.position == target)
                {
                    moveTo = false;
                }
            }
            // Manual movement
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");
            Vector3 moveDirection = new Vector3(horizontalInput, verticalInput, 0);
            moveDirection.Normalize();
            if (moveDirection != new Vector3(0, 0, 0))
            {
                moveTo = false;
            }
            rb.velocity = moveDirection * speed;
            if (moveDirection != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, moveDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 10);
            }
        }
    }
    // Button functions
    public void GoToPier()
    {
        target = pier.transform.position;
        moveTo = true;
    }

    public void GoToCasino()
    {
        target = casino.transform.position;
        moveTo = true;
    }
}
