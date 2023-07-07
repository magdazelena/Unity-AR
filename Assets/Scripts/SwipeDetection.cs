using UnityEngine;
using System.Collections;

public class SwipeDetection : MonoBehaviour
{
    [SerializeField]
    private float maximumTime = 1f;

    private InputManager inputManager;

    private bool secondFinger = false;
    private Coroutine swipeCoroutine;

    private void Awake()
    {
        inputManager = InputManager.Instance;
    }
    private void OnEnable()
    {
        inputManager.OnStartTouch += SwipeStart;
        inputManager.OnStartTouchSecondary += SwipeCancelled;
        inputManager.OnEndTouch += SwipeEnd;
    }
    private void OnDisable()
    {
        inputManager.OnStartTouch -= SwipeStart;
        inputManager.OnStartTouchSecondary -= SwipeCancelled;
        inputManager.OnEndTouch -= SwipeEnd;
    }

    private void SwipeStart()
    {
        if (inputManager.prefab.gameObject != null && !secondFinger) {
            swipeCoroutine = StartCoroutine(SwipeDetect());
        }
    }
    private void SwipeEnd()
    {
    
        StopCoroutine(swipeCoroutine);
        secondFinger = false;
    }

    private void SwipeCancelled()
    {
        secondFinger = true;
        StopCoroutine(swipeCoroutine);
    }

    private IEnumerator SwipeDetect() {
        yield return new WaitForSeconds(maximumTime);
        if (secondFinger) yield return null;
        Vector2 prevPosition = new Vector2(0,0), position = new Vector2(0,0);
        while(true) {
            position = inputManager.PrimaryPosition();
            Vector2 direction = position - prevPosition;
            Debug.Log(direction);
            inputManager.prefab.transform.Rotate(Vector3.down, direction.x, Space.World);
            inputManager.prefab.transform.Rotate(Vector3.left, direction.y, Space.World);
            prevPosition = position;
            yield return null;
        }
    }
}