using UnityEngine;
using UnityEngine.Events;
using Yarn.Unity;

public class EventRaiser : MonoBehaviour
{
    [SerializeField] UnityEvent Response;

    [YarnCommand("raise")]
    public void Raise()
    {
        Response.Invoke();
    }
}