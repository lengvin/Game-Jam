using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dead : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject player;
    public GameObject Effect;
    public GameObject respawn;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        

        if (collision.gameObject.tag == "Player")
        {
            Instantiate(Effect, player.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            Instantiate(player, respawn.transform.position, Quaternion.identity);
        }
    }
}
