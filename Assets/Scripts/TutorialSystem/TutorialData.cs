using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class TutorialStep
{
    public string instuction;
    public bool isClickable;
}

[System.Serializable]
[CreateAssetMenu(fileName = "New Tutorial Data", menuName = "Tutorial Data")]
public class TutorialData : ScriptableObject
{
    [SerializeField]
    public List<TutorialStep> tutorialSteps = null;
}
