using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rigidbody;
    public float Speed;
    public float JumpForse;
    private float MoveInput;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveInput = Input.GetAxis("Horizontal");
        rigidbody.velocity = new Vector3(MoveInput * Speed, rigidbody.velocity.y);

    }
}
