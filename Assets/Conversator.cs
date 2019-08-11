using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;
using DG.Tweening;

public class Conversator : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    private float typingSpeed = .02f;

    public string currentString;
    public GameObject cameraMainBoi;

    public string testString;
    
    public AudioClip[] a_alphabet;
    public AudioSource thisAudioSource;
    public TextMeshProUGUI continuePrompt;
    public Image textBackground;
    public TextMeshProUGUI promptText;

    public void AskQuestion()
    {
        currentString = "";
        //questionIndex++;
        //readyToAnswer = false;

        //StartCoroutine(Type(questions[questionIndex].questionString));
        //StartCoroutine(ReadyToInput(questions[questionIndex].questionString.Length * typingSpeed));
    }

    public void ClearDialogueText() {
        currentString = "";
        textDisplay.text = currentString;
    }

    IEnumerator ReadyToInput(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        //readyToAnswer = true;
        //counter = 15f;
    }

    public void StartConversation(Conversation thisConvo) {
        StartCoroutine(Convo(thisConvo));
    }

    IEnumerator WaitForPlayerAnswer(Statement currentStatement) {
        print("entering wait for player");
        continuePrompt.text = "Press 1,2,or 3 to Continue";
        bool noInput = false;
        while (noInput == false) {
            if (Input.GetKeyUp(KeyCode.Alpha1))
                break;
            if (Input.GetKeyUp(KeyCode.Alpha2))
                break;
            if (Input.GetKeyUp(KeyCode.Alpha3))
                break;
            yield return null;
        }
        print("player has broken the seal");
        continuePrompt.text = "";
        currentString = "";
        textDisplay.text = currentString;
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {          
            yield return StartCoroutine(Type(currentStatement.answer1));
            yield return new WaitForSeconds(3f);
            currentString = "";
            yield return StartCoroutine(Type(currentStatement.response1));
        }
        else if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            yield return StartCoroutine(Type(currentStatement.answer2));
            yield return new WaitForSeconds(3f);
            currentString = "";
            yield return StartCoroutine(Type(currentStatement.response2));
        }
        else if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            yield return StartCoroutine(Type(currentStatement.answer3));
            yield return new WaitForSeconds(3f);
            currentString = "";
            yield return StartCoroutine(Type(currentStatement.response3));
        }

        yield return new WaitForSeconds(1f);

        print("yeet, we done");
    }

    public IEnumerator Convo(Conversation thisConvo)
    {
        textBackground.enabled = true;
        for (int i = 0; i < thisConvo.statements.Length; i++)
        {
            currentString = "";
            textDisplay.text = currentString;

            if (thisConvo.statements[i].thisStatementType == StatementType.question)
            {
                currentString += thisConvo.statements[i].speaker + ": ";
                yield return StartCoroutine(Type(thisConvo.statements[i].stated));
                //yield return new WaitForSeconds(.5f);
                print("add the questions");
                currentString += "\n 1." + thisConvo.statements[i].answer1;
                currentString += "\n 2." + thisConvo.statements[i].answer2;
                currentString += "\n 3." + thisConvo.statements[i].answer3;
                textDisplay.text = currentString;
                print("questions added");
                yield return StartCoroutine(WaitForPlayerAnswer(thisConvo.statements[i]));
                print("wait for player answer");
            }
            else
            {
                currentString += thisConvo.statements[i].speaker + ": ";
                yield return StartCoroutine(Type(thisConvo.statements[i].stated));
                yield return StartCoroutine(WaitForPlayerContinue());
            }
            print("waiting till next turn");
            yield return new WaitForSeconds(1f);
            print("done waiting");
        }

        continuePrompt.text = "";
        textDisplay.text = "";
        textBackground.enabled = false;
    }

    IEnumerator WaitForPlayerContinue() {
        print("wait for player continue");
        continuePrompt.text = "Press 1 to Continue";
        while (!Input.GetKeyUp(KeyCode.Alpha1))
            yield return null;
        continuePrompt.text = "";
        print("player continued");
    }

    /*
    // Update is called once per frame
    void Update()
    {


        else if (thisGameState == GameState.main)
        {
            if (readyToAnswer)
            {
                if (counter > 0)
                {
                    counter -= Time.deltaTime;
                }
                else
                {
                    AnswerQuestion(4); //abstain
                }

                // Bit shift the index of the layer (8) to get a bit mask
                int layerMask = 1 << 8;

                // This would cast rays only against colliders in layer 8.
                // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
                layerMask = ~layerMask;

                RaycastHit hit;
                // Does the ray intersect any objects excluding the player layer
                if (Physics.Raycast(cameraMainBoi.transform.position, cameraMainBoi.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
                {
                    if (hit.collider.name == "Yes" || hit.collider.name == "No")
                    {
                        interactableIndicator.sprite = indicatorSprites[1];

                        if (hit.collider.name == "Yes")
                        {
                            indicatorHelper.text = "Answer Yes";
                            yesSpotlight.GetComponent<Light>().DOIntensity(200000, .2f).SetAutoKill(false);
                        }
                        else if (hit.collider.name == "No")
                        {
                            indicatorHelper.text = "Answer No";
                            noSpotlight.GetComponent<Light>().DOIntensity(200000, .2f).SetAutoKill(false);
                        }
                    }
                    else
                    {
                        interactableIndicator.sprite = indicatorSprites[0];
                        indicatorHelper.text = "";
                        yesSpotlight.GetComponent<Light>().DOIntensity(0f, .2f).SetAutoKill(false);
                        noSpotlight.GetComponent<Light>().DOIntensity(0f, .2f).SetAutoKill(false);
                    }
                }
                else
                {
                    interactableIndicator.sprite = indicatorSprites[0];
                    indicatorHelper.text = "";
                }

                if (Input.GetMouseButtonUp(0))
                {
                    if (hit.collider.name == "Yes")
                    {
                        //yesSpotlight.GetComponent<Light>().DOIntensity(30f, .5f).SetAutoKill(false);
                        AnswerQuestion(1); //yes
                    }
                    else if (hit.collider.name == "No")
                    {
                        //noSpotlight.GetComponent<Light>().DOIntensity(30f, .5f).SetAutoKill(false);
                        AnswerQuestion(2); //no               
                    }
                }
            }
        }
    }
    */

    public bool typePauser;
    public string pausedString;
    public IEnumerator Type(string questionString)
    {
        int stringIndex = 0;

        for (int dick = 0; dick < questionString.Length; dick++)
        {
            while (typePauser == true) {
                if(pausedString == "")
                    pausedString = questionString;
                yield return null;
            }

            float butt = Random.Range(0, 1f);
            currentString += questionString[dick];

            //parser
            if (dick % 3 == 0)
            {
                char thisLetter = questionString[dick];
                if (thisLetter == 'a')
                    thisAudioSource.PlayOneShot(a_alphabet[0]);
                else if (thisLetter == 'b')
                    thisAudioSource.PlayOneShot(a_alphabet[1]);
                else if (thisLetter == 'c')
                    thisAudioSource.PlayOneShot(a_alphabet[2]);
                else if (thisLetter == 'd')
                    thisAudioSource.PlayOneShot(a_alphabet[3]);
                else if (thisLetter == 'e')
                    thisAudioSource.PlayOneShot(a_alphabet[4]);
                else if (thisLetter == 'f')
                    thisAudioSource.PlayOneShot(a_alphabet[5]);
                else if (thisLetter == 'g')
                    thisAudioSource.PlayOneShot(a_alphabet[6]);
                else if (thisLetter == 'h')
                    thisAudioSource.PlayOneShot(a_alphabet[7]);
                else if (thisLetter == 'i')
                    thisAudioSource.PlayOneShot(a_alphabet[8]);
                else if (thisLetter == 'j')
                    thisAudioSource.PlayOneShot(a_alphabet[9]);
                else if (thisLetter == 'k')
                    thisAudioSource.PlayOneShot(a_alphabet[10]);
                else if (thisLetter == 'l')
                    thisAudioSource.PlayOneShot(a_alphabet[11]);
                else if (thisLetter == 'm')
                    thisAudioSource.PlayOneShot(a_alphabet[12]);
                else if (thisLetter == 'n')
                    thisAudioSource.PlayOneShot(a_alphabet[13]);
                else if (thisLetter == 'o')
                    thisAudioSource.PlayOneShot(a_alphabet[14]);
                else if (thisLetter == 'p')
                    thisAudioSource.PlayOneShot(a_alphabet[15]);
                else if (thisLetter == 'q')
                    thisAudioSource.PlayOneShot(a_alphabet[16]);
                else if (thisLetter == 'r')
                    thisAudioSource.PlayOneShot(a_alphabet[17]);
                else if (thisLetter == 's')
                    thisAudioSource.PlayOneShot(a_alphabet[18]);
                else if (thisLetter == 't')
                    thisAudioSource.PlayOneShot(a_alphabet[19]);
                else if (thisLetter == 'u')
                    thisAudioSource.PlayOneShot(a_alphabet[20]);
                else if (thisLetter == 'v')
                    thisAudioSource.PlayOneShot(a_alphabet[21]);
                else if (thisLetter == 'w')
                    thisAudioSource.PlayOneShot(a_alphabet[22]);
                else if (thisLetter == 'x')
                    thisAudioSource.PlayOneShot(a_alphabet[23]);
                else if (thisLetter == 'y')
                    thisAudioSource.PlayOneShot(a_alphabet[24]);
                else if (thisLetter == 'z')
                    thisAudioSource.PlayOneShot(a_alphabet[25]);
            }

            textDisplay.text = currentString;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    IEnumerator StartQuestioning()
    {
        yield return new WaitForSeconds(3);
        //thisGameState = GameState.main;
        AskQuestion();
    }
}

public enum GameState { intro, main, end }