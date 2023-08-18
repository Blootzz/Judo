using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMoveDirection : MonoBehaviour
{
    [SerializeField] Vector3 direction = Vector3.zero;
    [SerializeField] [Range(0,20)] float speed = 5;
    [SerializeField] Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<Rigidbody2D>())
            rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(Vector3.MoveTowards(transform.position, transform.position + direction.normalized, speed * Time.deltaTime));
    }
}
