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

    [SerializeField]
    private GameObject Footsteps;
    private bool isPlaying = false;



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
            Debug.Log(velocity.magnitude);
            Debug.Log(isPlaying);
            if(velocity.magnitude > .01f && !isPlaying)
            {
                Debug.Log("Move");
                Footsteps.GetComponent<AudioSource>().loop = true;
                Footsteps.GetComponent<AudioSource>().Play();
                isPlaying = true;
            }
            else if (velocity.magnitude == 0)
            {
                isPlaying = false;
                Footsteps.GetComponent<AudioSource>().Stop();
            }
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
