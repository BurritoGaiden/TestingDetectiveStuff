using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question : MonoBehaviour
{
    public string questionString;
    public bool bigQuestion;

    public float yesResult;
    public float noResult;
    public float abstainResult;

    public string yesResponse;
    public string noResponse;
    public string abstainResponse;

    public bool questionAchieved;
    public float questionResultingChange;
}
