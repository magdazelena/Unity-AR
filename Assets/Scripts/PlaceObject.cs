using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager), typeof(ARPlaneManager))]
public class PlaceObject : MonoBehaviour
{
    private ARRaycastManager aRRaycastManager;
    private ARPlaneManager aRPlaneManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private bool object_spawned = false;

    private InputManager inputManager;

    
   private void Awake() {
    aRRaycastManager = GetComponent<ARRaycastManager>();
    aRPlaneManager = GetComponent<ARPlaneManager>();
    inputManager = InputManager.Instance;
   }

   private void OnEnable() {
    inputManager.OnStartTouch += FingerDown;
   }

   private void OnDisable() {
        inputManager.OnStartTouch -= FingerDown;
    }
    private void FingerDown(Vector2 position, float time) {
        if (object_spawned) return;

        if (aRRaycastManager.Raycast(position, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose pose = hits[0].pose;
            inputManager.prefab = Instantiate(inputManager.prefab, pose.position, pose.rotation) as GameObject;
            object_spawned = true;
        }
    }
}
