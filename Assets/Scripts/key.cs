using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class key : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Player;
    public Rigidbody2D Key;

    void Start()
    {
        Key = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
             
            
            Key.position = new Vector3(Player.transform.position.x, Player.transform.position.y + 3);


        }
    }
}
