
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class InputManager : Singleton<InputManager>
{

    [SerializeField]
    public GameObject prefab;

    #region Events
    public delegate void StartTouch();
    public event StartTouch OnStartTouch;
    public delegate void EndTouch();
    public event EndTouch OnEndTouch;

    public delegate void StartTouchSecondaryE();
    public event StartTouchSecondaryE OnStartTouchSecondary;
    public delegate void EndTouchSecondaryE();
    public event EndTouchSecondaryE OnEndTouchSecondary;
    #endregion

    private TouchControl touchControl;

    private void Awake()
    {
        touchControl = new TouchControl();
    }
    private void OnEnable()
    {
        touchControl.Enable();
    }

    private void OnDisable()
    {
        touchControl.Disable();
    }

    void Start()
    {
        touchControl.Touch.PrimaryContact.started += _ => StartTouchPrimary();
        touchControl.Touch.PrimaryContact.canceled += _ => EndTouchPrimary();
        touchControl.Touch.SecondaryContact.started += _ => StartTouchSecondary();
        touchControl.Touch.SecondaryContact.canceled += _ => EndTouchSecondary();
    }

    private void StartTouchPrimary()
    {
        if (OnStartTouch != null) OnStartTouch();
    }
    private void EndTouchPrimary()
    {
        if (OnEndTouch != null) OnEndTouch();
    }

    private void StartTouchSecondary()
    {
        if (OnStartTouchSecondary != null) OnStartTouchSecondary();
    }
    private void EndTouchSecondary()
    {
        if (OnEndTouchSecondary != null) OnEndTouchSecondary();
    }

    public Vector2 PrimaryPosition()
    {
        return touchControl.Touch.PrimaryPosition.ReadValue<Vector2>();
    }
    public Vector2 SecondaryPosition()
    {
        return touchControl.Touch.SecondaryPosition.ReadValue<Vector2>();
    }
}
