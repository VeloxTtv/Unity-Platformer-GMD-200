using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 5.0f;
    //Member Variables
    float m_xInput;
    float m_yInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Hello World");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Shoot");
        }
        m_xInput = Input.GetAxisRaw("Horizontal");
        m_yInput = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 force = new Vector2(m_xInput, m_yInput) * moveSpeed;
        rb.AddForce(force);
    }
}
