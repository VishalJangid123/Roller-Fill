using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed = 15f;
    Rigidbody rb;
    AudioSource audio_ballDragging;

    private bool isBallMoving = false;
    private Vector3 movingDirection;
    private Vector3 nextCollisionPosition;

    public int minSwipeRecognization = 500;

    private Vector2 swipePosLastFrame;
    private Vector2 swipePosCurrentFrame;
    private Vector2 currentSwipe;

    private Color solveColor;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audio_ballDragging = GetComponent<AudioSource>();
        solveColor = UnityEngine.Random.ColorHSV(0.5f, 1);
        GetComponent<MeshRenderer>().material.color = solveColor;
    }

    private void FixedUpdate()
    {

        if (isBallMoving)
        {

            rb.velocity = speed * movingDirection;
            if (!audio_ballDragging.isPlaying)
            {
                audio_ballDragging.Play();
            }
        }

        Collider[] hitColliders = Physics.OverlapSphere(transform.position - (Vector3.up / 2), 0.05f);

        int i = 0;
        while( i< hitColliders.Length)
        {
            GroundSectionController g = hitColliders[i].gameObject.GetComponent<GroundSectionController>();
            if(g && !g.isColored)
            {
                g.ChangeColor(solveColor);
            }
            i++;
        }
        if(nextCollisionPosition != Vector3.zero)
        {
            if(Vector3.Distance(transform.position, nextCollisionPosition) < 1)
            {
                isBallMoving = false;
                movingDirection = Vector3.zero;
                nextCollisionPosition = Vector3.zero;

            }
        }

        if (isBallMoving)
            return;

        if (Input.GetMouseButton(0))
        {
            swipePosCurrentFrame = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            if(swipePosLastFrame != Vector2.zero)
            {
                currentSwipe = swipePosCurrentFrame - swipePosLastFrame;
                if(currentSwipe.sqrMagnitude < minSwipeRecognization)
                {
                    return;
                }
                currentSwipe.Normalize();

                // up or down
                if(currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                {
                    SetDestination(currentSwipe.y > 0 ? Vector3.forward : Vector3.back );
                }
                
                // left or right
                if(currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                {
                    SetDestination(currentSwipe.x > 0 ? Vector3.right : Vector3.left);

                }
            }

            swipePosLastFrame = swipePosCurrentFrame;
        }

        if (Input.GetMouseButtonUp(0))
        {
            swipePosLastFrame = Vector3.zero;
            currentSwipe = Vector3.zero;
        }
    }

    private void SetDestination(Vector3 direction)
    {
        movingDirection = direction;

        RaycastHit hit;
        if(Physics.Raycast(transform.position, direction, out hit, 100f))
        {
            nextCollisionPosition = hit.point;
        }

        isBallMoving = true;
    }

}
