using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] Camera cam;

    public float speed;
    public float jumpForce;

    private bool isGrounded;

    public GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gm.isGameOver)
        {
            rb.velocity = Vector3.zero;
            return;
        }
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(cam.transform.forward * speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(cam.transform.forward * -speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(cam.transform.right * speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(cam.transform.right * -speed);
        }
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0));
            isGrounded = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 6)
        {
            isGrounded = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Stars")){
            Destroy(other.gameObject);
            gm.collectedStars++;
            gm.DisplayStars();
        }
    }
}
