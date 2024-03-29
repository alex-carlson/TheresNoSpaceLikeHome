using UnityEngine;
using UnityEngine.Events;

public class EventListener : MonoBehaviour {
    public GameEvent Event;
    public UnityEvent Response;

    void OnEnable() { Event.Register(this); }

    void OnDisable() { Event.DeRegister(this); }

    public void OnEventRaised()  { Response.Invoke(); }
}