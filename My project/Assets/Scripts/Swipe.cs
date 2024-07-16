using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Swipe : MonoBehaviour
{
    private Vector2 DownPosition;
    private Vector2 UpPosition;

    [SerializeField]
    private float minDistanceForSwipe = 20f;


    PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();

    }

    private void Update()
    {

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                DownPosition = touch.position;
                UpPosition = touch.position;
            }

            if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                UpPosition = touch.position;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                UpPosition = touch.position;
                CheckSwipe();

            }
        }
    }


    void CheckSwipe()
    {
        float deltaY = UpPosition.y - DownPosition.y;

        if (deltaY > minDistanceForSwipe)
        {
            if (deltaY > 0) // Up swipe
            {
                playerMovement.Jump();
            }
        }
    }
}