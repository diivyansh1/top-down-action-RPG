using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevation_exit : MonoBehaviour
{
    // Start is called before the first frame update
    public Collider2D[] mountainCollider;
    public Collider2D[] boundaryCollider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            foreach (Collider2D mountain in mountainCollider)
            {
                mountain.enabled = true;
            }
            foreach (Collider2D boundary in boundaryCollider)
            {
                boundary.enabled = false;
            }
            collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 10; //turns the sorting order to 15
        }
    }
}
