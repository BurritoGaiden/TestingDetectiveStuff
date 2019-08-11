using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sitter : MonoBehaviour
{
    public GameObject cameraMainBoi;
    public Seat currrentSeat;

    public void Sit(Seat destSeat) {
        currrentSeat = destSeat;

        this.transform.position = currrentSeat.seatPosition;
        this.GetComponent<CharacterController>().enabled = false;
        this.GetComponent<FirstPersonDrifter>().enabled = false;
        cameraMainBoi.GetComponent<HeadBob>().enabled = false;
    }

    public void Stand()
    {
        this.GetComponent<CharacterController>().enabled = true;
        this.GetComponent<FirstPersonDrifter>().enabled = true;
        cameraMainBoi.GetComponent<HeadBob>().enabled = true;

        currrentSeat = null;
    }
}
