using System.Collections;
using UnityEngine;

public class PinchDetection : MonoBehaviour
{
      private InputManager inputManager;
      private GameObject obj;
      private void Awake() {
        inputManager = InputManager.Instance;
        obj = inputManager.prefab;
      }

      private void OnEnable() {
        inputManager.OnStartTouchSecondary += ZoomStart;
        inputManager.OnEndTouchSecondary += ZoomEnd;
      }
      private void OnDisable() {
        inputManager.OnStartTouchSecondary -= ZoomStart;
        inputManager.OnEndTouchSecondary -= ZoomEnd;
      }

      private void ZoomStart() { 
        StartCoroutine(DetectZoom());
      }

      private void ZoomEnd() {    
        StopCoroutine(DetectZoom());
      }

      IEnumerator DetectZoom() {
        float prevDistance = 0f, distance = 0f;
        while(true) {
          distance = Vector2.Distance(inputManager.PrimaryPosition(), inputManager.SecondaryPosition());
          if (prevDistance < distance) {
            Vector3 targetPosition = inputManager.prefab.transform.localScale;
            targetPosition -= new Vector3(1,1,1);
            inputManager.prefab.transform.localScale = Vector3.Lerp(inputManager.prefab.transform.localScale, targetPosition, Time.deltaTime);
          } else if (prevDistance > distance) {
            Debug.Log("zoom in");
            Vector3 targetPosition = inputManager.prefab.transform.localScale;
            targetPosition += new Vector3(1,1,1);
            inputManager.prefab.transform.localScale = Vector3.Lerp(inputManager.prefab.transform.localScale, targetPosition, Time.deltaTime);
          }
          prevDistance = distance;
          yield return null;
        }
      }
}
