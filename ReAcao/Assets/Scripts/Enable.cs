using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Enable : MonoBehaviour
{
    [SerializeField] List<MonoBehaviour> components;

    [YarnCommand("enable")]
    public void enableAll()
    {
        foreach(MonoBehaviour component in components)
        {
            component.enabled = true;
        }
    }
}
