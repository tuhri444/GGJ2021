using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField]
    GroundCheck groundCheck;
    Rigidbody rigidbody;
    public float jumpStrength = 2;
    public event System.Action Jumped;
    private bool canMove = true;

    void Reset()
    {
        groundCheck = GetComponentInChildren<GroundCheck>();
        if (!groundCheck)
            groundCheck = GroundCheck.Create(transform);
    }

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        GrabHand.OnGrab += OnGrab;
        GrabHand.OnLetGo += OnLetGo;
    }

    void LateUpdate()
    {
        if (Input.GetButtonDown("Jump") && groundCheck.isGrounded && canMove)
        {
            rigidbody.AddForce(Vector3.up * 100 * jumpStrength);
            Jumped?.Invoke();
        }
    }

    void OnGrab(Collider other)
    {
        canMove = false;
    }

    void OnLetGo(Collider other)
    {
        canMove = true;
    }

    private void OnDestroy()
    {
        GrabHand.OnGrab -= OnGrab;
        GrabHand.OnLetGo -= OnLetGo;
    }
}
