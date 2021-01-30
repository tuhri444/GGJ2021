using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    private float speed = 5;

    [SerializeField]
    private float walkSpeed = 5;

    [SerializeField]
    private float runSpeed = 10;

    Vector2 velocity;
    private bool canMove = true;



    private void Start()
    {
        GrabHand.OnGrab += OnGrab;
        GrabHand.OnLetGo += OnLetGo;
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = runSpeed;
            }
            else
            {
                speed = walkSpeed;
            }

            velocity.y = Input.GetAxis("Vertical") * speed * Time.deltaTime;
            velocity.x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            transform.Translate(velocity.x, 0, velocity.y);
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
