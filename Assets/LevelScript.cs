using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour
{
    public GameObject playerCharacter;
    public Seat seat001;
    public Conversation testConvo;
    public Color col;
    public GameObject cup;
    public GameObject visitor;
    public Conversation convoTwo;

    // Start is called before the first frame update
    void Start()
    {
        StartLevel();
    }

    void StartLevel() {
        StartCoroutine(StartScript());
    }

    IEnumerator StartScript() {
        playerCharacter.GetComponent<Sitter>().Sit(seat001);

        yield return StartCoroutine(playerCharacter.GetComponent<Conversator>().Convo(testConvo));
        yield return StartCoroutine(BringUpCup());

        yield return new WaitForSeconds(2f);

        //player has to look at the cup
        yield return StartCoroutine(MoveCup01());
        visitorTalking = true;
        //print("finished cup");

       // while (visitorTalking)
        //    yield return null;
        //yield return StartCoroutine(VisitorInterview());
        yield return StartCoroutine(playerCharacter.GetComponent<Conversator>().Convo(convoTwo));
        //yield return StartCoroutine(CupPuzzleRoutine());
        print("oof we didn't make");
        //CameraFade.StartAlphaFade(col, true, 5f);
    }

    public float cupTimer;
    public float cupPuzzleTimer;
    public bool activated;

    public bool visitorTalking;
    public int visitorConvoInt;

    IEnumerator VisitorInterruption() {
        playerCharacter.GetComponent<Conversator>().ClearDialogueText();
        yield return StartCoroutine(playerCharacter.GetComponent<Conversator>().Type("Uhh..."));
        yield return new WaitForSeconds(2f);
        playerCharacter.GetComponent<Conversator>().ClearDialogueText();
        yield return StartCoroutine(playerCharacter.GetComponent<Conversator>().Type("Don't look at me"));
        yield return new WaitForSeconds(3f);
        playerCharacter.GetComponent<Conversator>().ClearDialogueText();
        yield return StartCoroutine(playerCharacter.GetComponent<Conversator>().Type("They're watching"));
        yield return new WaitForSeconds(6f);
        playerCharacter.GetComponent<Conversator>().ClearDialogueText();
        yield return StartCoroutine(playerCharacter.GetComponent<Conversator>().Type("....."));
        yield return new WaitForSeconds(7f);
        playerCharacter.GetComponent<Conversator>().ClearDialogueText();
        yield return StartCoroutine(playerCharacter.GetComponent<Conversator>().Type("really?"));
        yield return new WaitForSeconds(6f);
        playerCharacter.GetComponent<Conversator>().ClearDialogueText();
        yield return StartCoroutine(playerCharacter.GetComponent<Conversator>().Type("You're going to blow my cover. My client does not want this to be known."));
        yield return new WaitForSeconds(14f);
        playerCharacter.GetComponent<Conversator>().ClearDialogueText();
        yield return StartCoroutine(playerCharacter.GetComponent<Conversator>().Type("If you want to hear more, just look away"));
        yield return new WaitForSeconds(6f);
    }

    IEnumerator EndVisitorInterruption() {
        yield return null;

    }

    public Coroutine visitLog;

    void Update() {

        if (visitorTalking) {
            if (playerCharacter)
            {
                if (playerCharacter.GetComponent<Interactor>().hit.collider.name == "VisitorFace")
                {
                    if (playerCharacter.GetComponent<Conversator>().typePauser == false)
                    {
                        playerCharacter.GetComponent<Conversator>().typePauser = true;
                        visitLog = StartCoroutine(VisitorInterruption());
                    }
                    if (playerCharacter.GetComponent<Conversator>().typePauser == true)
                    {
                        playerCharacter.GetComponent<Conversator>().textDisplay.text = playerCharacter.GetComponent<Conversator>().currentString;
                    }
                }
                else {
                    if (playerCharacter.GetComponent<Conversator>().typePauser == true) {
                        //stop the coroutine
                        StopCoroutine(visitLog);

                        //add a "as I was saying"

                        //reset the current string
                        playerCharacter.GetComponent<Conversator>().currentString = playerCharacter.GetComponent<Conversator>().pausedString;
                        playerCharacter.GetComponent<Conversator>().pausedString = "";

                        //unpause the pauser
                        playerCharacter.GetComponent<Conversator>().typePauser = false;
                    }                   
                }
            }
        }
    }

    IEnumerator VisitorInterview() {
        yield return new WaitForSeconds(5f); //{

        //}
    }

    IEnumerator MoveCup01()
    {
        yield return new WaitForSeconds(2f);
        cup.GetComponent<Animator>().SetBool("MoveAcrossTable", true);

        playerCharacter.GetComponent<Conversator>().textBackground.enabled = true;
        playerCharacter.GetComponent<Conversator>().ClearDialogueText();

        StartCoroutine(playerCharacter.GetComponent<Conversator>().Type("Look at the Cup"));

        while (playerCharacter.GetComponent<Interactor>().hit.collider.name != "Drink") {
            yield return null;
        }

        visitor.SetActive(true);

        playerCharacter.GetComponent<Conversator>().ClearDialogueText();

        yield return StartCoroutine(playerCharacter.GetComponent<Conversator>().Type("Hah, thought so."));

        yield return new WaitForSeconds(2f);

        playerCharacter.GetComponent<Conversator>().ClearDialogueText();

        cup.GetComponent<Animator>().SetBool("MoveAcrossTable01", true);
        yield return StartCoroutine(playerCharacter.GetComponent<Conversator>().Type("Okay look back over here."));

        while (playerCharacter.GetComponent<Interactor>().hit.collider.name != "Drink")
        {
            yield return null;
        }

        yield return new WaitForSeconds(3f);

        playerCharacter.GetComponent<Conversator>().ClearDialogueText();

        yield return StartCoroutine(playerCharacter.GetComponent<Conversator>().Type("Hmmm. Guess you're ok. Take it."));

        yield return new WaitForSeconds(2f);
        playerCharacter.GetComponent<Conversator>().ClearDialogueText();

        //prompt the player to take the drink
        playerCharacter.GetComponent<Conversator>().promptText.text = "Press E to take the Drink";

        while (!Input.GetKeyUp(KeyCode.E))
        {   
            yield return null;
        }

        playerCharacter.GetComponent<Conversator>().ClearDialogueText();
        yield return StartCoroutine(playerCharacter.GetComponent<Conversator>().Type("Don't mind if I do!"));
        cup.GetComponent<Animator>().SetBool("VisitorTakesDrink", true);
        
        yield return new WaitForSeconds(2f);
    }

    IEnumerator BringUpCup() {
        playerCharacter.GetComponent<Conversator>().textBackground.enabled = true;
        playerCharacter.GetComponent<Conversator>().ClearDialogueText();

        cup.GetComponent<Animator>().SetBool("RestOnTable", true);
        yield return StartCoroutine(playerCharacter.GetComponent<Conversator>().Type("OK. So I'll give you this drink. But you have to prove you're sober enough."));

        yield return new WaitForSeconds(3f);

        playerCharacter.GetComponent<Conversator>().textBackground.enabled = false;
        playerCharacter.GetComponent<Conversator>().ClearDialogueText();
    }

    IEnumerator CupPuzzleRoutine() {
        print("look at the cup");
        while (cupPuzzleTimer < 10f)
        {
            cupPuzzleTimer += Time.deltaTime;
            if (playerCharacter)
                if (playerCharacter.GetComponent<Interactor>().hit.collider != null)
                    if (playerCharacter.GetComponent<Interactor>().hit.collider.name == "Drink") {
                        cupTimer += Time.deltaTime;
                    }
            yield return null;
        }

        print("done with puzzle");
        if (cupTimer > 5f)
            print("you looked for long enough");
        else
            print("you did not look for long enough");
    }
}
