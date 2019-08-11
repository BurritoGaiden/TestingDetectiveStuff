using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Inventory : MonoBehaviour
{
    public List<GameObject> inventory = new List<GameObject>();
    public GameObject currentInspection;
    public Transform inspectionStarterPosition;
    public Transform inspectionPosition;

    // Update is called once per frame
    void Update()
    {
        if (currentInspection)
        {

            // Use an Update routine to change the tween's endValue each frame
            // so that it updates to the target's position if that changed
            if (targetLastPos == inspectionPosition.position) return;
            // Add a Restart in the end, so that if the tween was completed it will play again
            tween.ChangeEndValue(inspectionPosition.position, true).Restart();
            targetLastPos = inspectionPosition.position;

            //currentInspection.transform.position = inspectionPosition.transform.position;
            //currentInspection.transform.rotation = this.transform.rotation;

            if (Input.GetKeyUp(KeyCode.U)) {
                AddItem(currentInspection);
            } else if (Input.GetKeyUp(KeyCode.E))
            {
                PutDownItem(currentInspection);
            }

        }
    }

    public Transform target; // Target to follow
    Vector3 targetLastPos;
    Tweener tween;

    public void InspectItem(GameObject obj) {
        this.GetComponent<CharacterController>().enabled = false;
        this.GetComponent<FirstPersonDrifter>().enabled = false;
        currentInspection = obj;
        currentInspection.GetComponent<Collider>().enabled = false;

        GetComponent<MouseLook>().enabled = false;
        GetComponent<Conversator>().cameraMainBoi.GetComponent<MouseLook>().enabled = false;

        currentInspection.transform.position = inspectionStarterPosition.transform.position;
        // First create the "move to target" tween and store it as a Tweener.
        // In this case I'm also setting autoKill to FALSE so the tween can go on forever
        // (otherwise it will stop executing if it reaches the target)
        tween = currentInspection.transform.DOMove(inspectionPosition.position, 2).SetAutoKill(false);
        // Store the target's last position, so it can be used to know if it changes
        // (to prevent changing the tween if nothing actually changes)
        targetLastPos = inspectionPosition.position;
    }

    public void PutDownItem(GameObject obj) {
        this.GetComponent<CharacterController>().enabled = true;
        this.GetComponent<FirstPersonDrifter>().enabled = true;
        
        currentInspection.GetComponent<Collider>().enabled = true;

        obj.transform.position = obj.GetComponent<Item>().setPos;
        obj.transform.rotation = obj.GetComponent<Item>().setRot;

        currentInspection = null;

        GetComponent<MouseLook>().enabled = true;
        GetComponent<Conversator>().cameraMainBoi.GetComponent<MouseLook>().enabled = true;
    }

    public void AddItem(GameObject obj) {
        inventory.Add(obj);
        obj.SetActive(false);

        this.GetComponent<CharacterController>().enabled = true;
        this.GetComponent<FirstPersonDrifter>().enabled = true;
        currentInspection = null;

        GetComponent<MouseLook>().enabled = true;
        GetComponent<Conversator>().cameraMainBoi.GetComponent<MouseLook>().enabled = true;
    }
}
