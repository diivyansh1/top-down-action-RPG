using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class elevation_entry : MonoBehaviour
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
                mountain.enabled = false;
            }
            foreach (Collider2D boundary in boundaryCollider)
            {
                boundary.enabled = true;
            }
            collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 15; //turns the sorting order to 15
        }
    }
}
