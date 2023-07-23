using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField]
	float speed = 2.5f;
	[SerializeField]
	Rigidbody2D rb;
	[SerializeField]
	Animator anim;
    [SerializeField]
    CapsuleCollider2D coll;

    private Vector2 targetPos;

    void Update()
    {
        //Moves character toward right click
        if (Input.GetMouseButtonDown(1))
        {
            anim.SetBool("walking", true);
            StopAllCoroutines();

            Vector2 sourcePos = transform.position;
            targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            UpdateFacing(targetPos);

            IEnumerator movement = MoveTowards(targetPos);
            StartCoroutine(movement);
        }

        //Rotates character animation to face mouse only if idle
        if (!anim.GetBool("walking"))
        {
            Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            UpdateFacing(direction);
        }
    }

    private void UpdateFacing(Vector2 direction)
    {
        float dXY = (Mathf.Abs(direction.x) + Mathf.Abs(direction.y));
        float dX = direction.x / dXY;
        float dY = direction.y / dXY;
        anim.SetFloat("dirX", dX);
        anim.SetFloat("dirY", dY);
    }

    IEnumerator MoveTowards(Vector2 dest)
    {
        while (Vector2.Distance(transform.position, dest) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, dest, speed * Time.deltaTime);
            yield return null;
        }

        anim.SetBool("walking", false);
        yield break;
    }

}
