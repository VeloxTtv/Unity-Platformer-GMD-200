using UnityEngine;
using System.Collections;

public class JumpStar : MonoBehaviour
{
    IEnumerator DisableTemporarily()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(2f);
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Collider2D>().enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector2 vector2 = transform.position;
        PlayerController thePlayer = collision.gameObject.GetComponent<PlayerController>();
        if (thePlayer != null)
        {
            StartCoroutine(DisableTemporarily());
            thePlayer._hasStar = true;
            thePlayer._numStars++;
            GetComponent<EffectSpawner>().SpawnEffectAtLocation1(vector2);
        }
    }
}
