using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;

public class Interactor : MonoBehaviour
{
    public GameObject cameraMainBoi;
    public TextMeshProUGUI interactorText;
    public RaycastHit hit;
    // Update is called once per frame
    void Update()
    {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        //RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(cameraMainBoi.transform.position, cameraMainBoi.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            if (hit.collider.name == "Seat")
            {
                if (GetComponent<Sitter>().currrentSeat != null)
                    interactorText.text = "Get up";
                else
                    interactorText.text = "Take a Seat";
            }
            else if (hit.collider.name == "Cup") {

            }
            else
            {
                interactorText.text = "";
            }
        }
        else
        {
            interactorText.text = "";
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            if (hit.collider.name == "Seat")
            {
                if (GetComponent<Sitter>().currrentSeat != null)
                    GetComponent<Sitter>().Stand();
                else
                    GetComponent<Sitter>().Sit(hit.collider.GetComponent<Seat>());
            }
        }
    }
}
