using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System;
using System.Net;
using System.Security.Permissions;
using Yarn.Unity;

public class EmotionVariableWritter : MonoBehaviour
{
    [SerializeField] EmotionData data;
    [SerializeField] VariableStorageBehaviour yarnStorage;
    [SerializeField] string valenceVariableName;
    [SerializeField] string arousalVariableName;

    [SerializeField] string meanValenceVariableName;
    [SerializeField] string meanArousalVariableName;

    float valenceLastValue;
    float arousalLastValue;


    void SetVariable(string name, float value)
    {
        if(yarnStorage.Contains(name))
        {
            yarnStorage.SetValue(name, value);
        }
    }

    void Update()
    {
        SetVariable(valenceVariableName, data.valence);
        SetVariable(arousalVariableName, data.valence);
        SetVariable(meanValenceVariableName, data.meanValence);
        SetVariable(meanArousalVariableName, data.meanArousal);
    }

    void OnValidate()
    {
        if(valenceVariableName[0] != '$')
        {
            valenceVariableName = "$" + valenceVariableName;
        }
        if(arousalVariableName[0] != '$')
        {
            arousalVariableName = "$" + arousalVariableName;
        }
        if(meanValenceVariableName[0] != '$')
        {
            meanValenceVariableName = "$" + meanValenceVariableName;
        }
        if(meanArousalVariableName[0] != '$')
        {
            meanArousalVariableName = "$" + meanArousalVariableName;
        }
    }
}
