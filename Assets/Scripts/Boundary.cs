using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    private BoxCollider2D bounderCollider;
    void Start()
    {
        bounderCollider = GetComponent<BoxCollider2D>();
        ReSizeCollider();
    }

    private void ReSizeCollider()
    {
        Vector2 viewportSize = Camera.main.ViewportToWorldPoint(new Vector2(1, 1) * 2);
        viewportSize.x *= 1.5f;
        viewportSize.y *= 1.5f;
        bounderCollider.size = viewportSize;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Bullet" || collision.tag == "Egg")
        {
            Destroy(collision.gameObject);
        }
    }

}
