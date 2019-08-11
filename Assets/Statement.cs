using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statement : MonoBehaviour
{
    public StatementType thisStatementType;
    public string speaker;
    public string stated;

    public string answer1, answer2, answer3;
    public string response1, response2, response3;
}

public enum StatementType { piece,question};
