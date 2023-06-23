using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Meeple : MonoBehaviour
{
    public int turn = 0;
    public int? priority;
    public int currentSpace = -1;
    public GameObject uno;
    public GameObject dos;
    public GameObject tres;
    public GameObject cuatro;
    public GameObject cinco;
    public GameObject seis;
    public Color colour;
    GameObject info;
    Button DiceButton;
    void Start()
    {
        info = GameObject.FindGameObjectWithTag("Info");
    }

    public void ArrangeStartingPositions()
    {
        switch (priority)
        {
            case 0:
                transform.position = new Vector3(-5.5f, -4.44f, -1);
                break;
            case 1:
                transform.position = new Vector3(-6.5f, -4, -1);
                break;
            case 2:
                transform.position = new Vector3(-5.5f, -3.44f, -1);
                break;
            case 3:
                transform.position = new Vector3(-6.5f, -3, -1);
                break;
            case 4:
                transform.position = new Vector3(-5.5f, -2.44f, -1);
                break;
            case 5:
                transform.position = new Vector3(-6.5f, -2, -1);
                break;
            default:
                transform.position = new Vector3(1.5f, 6.5f, -1);
                break;
        }
    }

    public void UpdatePriority()
    {
        #region define meeple
        GameObject red = GameObject.FindGameObjectWithTag("Red");
        GameObject blue = GameObject.FindGameObjectWithTag("Blue");
        GameObject green = GameObject.FindGameObjectWithTag("Green");
        GameObject yellow = GameObject.FindGameObjectWithTag("Yellow");
        GameObject purple = GameObject.FindGameObjectWithTag("Purple");
        GameObject white = GameObject.FindGameObjectWithTag("White");
        GameObject[] meeple = new GameObject[6] { red, blue, green, yellow, purple, white };
        #endregion
        foreach (GameObject merson in meeple)
        {
            if (merson.GetComponent<Meeple>().priority != null)
            {
                switch (merson.GetComponent<Meeple>().priority)
                {
                    case 0:
                        uno.SetActive(true);
                        uno.GetComponent<SpriteRenderer>().color = merson.GetComponent<Meeple>().colour;
                        break;
                    case 1:
                        dos.SetActive(true);
                        dos.GetComponent<SpriteRenderer>().color = merson.GetComponent<Meeple>().colour;
                        break;
                    case 2:
                        tres.SetActive(true);
                        tres.GetComponent<SpriteRenderer>().color = merson.GetComponent<Meeple>().colour;
                        break;
                    case 3:
                        cuatro.SetActive(true);
                        cuatro.GetComponent<SpriteRenderer>().color = merson.GetComponent<Meeple>().colour;
                        break;
                    case 4:
                        cinco.SetActive(true);
                        cinco.GetComponent<SpriteRenderer>().color = merson.GetComponent<Meeple>().colour;
                        break;
                    case 5:
                        seis.SetActive(true);
                        seis.GetComponent<SpriteRenderer>().color = merson.GetComponent<Meeple>().colour;
                        break;
                }
            }
        }
    }

    public void Move(int amount, GameObject merson)
    {
        DiceButton = GameObject.FindGameObjectWithTag("DiceButton").GetComponent<Button>();
        GameObject[] boardArray = info.GetComponent<GameState>().boardArray;
        GameObject nextSpace = boardArray.GetValue(currentSpace + amount) as GameObject;
        StartCoroutine(MoveToPosition(merson, nextSpace, 2.4f));
        switch (nextSpace.GetComponent<BoardSpace>().action)
        {
            case 1:
                nextSpace = boardArray.GetValue(nextSpace.GetComponent<BoardSpace>().actionNum - 1) as GameObject;
                StartCoroutine(WaitABit(merson, nextSpace, boardArray));
                break;
            case 2:
                nextSpace = boardArray.GetValue(nextSpace.GetComponent<BoardSpace>().actionNum - 1) as GameObject;
                StartCoroutine(WaitABit(merson, nextSpace, boardArray));
                break;
            default:
                DiceButton.interactable = true;
                break;
        }
        currentSpace += amount;
        switch (currentSpace)
        {
            case 99:
                info.GetComponent<GameState>().WinGame(merson);
                break;
            case 100:
                currentSpace = 98;
                break;
            case 101:
                currentSpace = 97;
                break;
            case 102:
                currentSpace = 96;
                break;
            case 103:
                currentSpace = 95;
                break;
            case 104:
                currentSpace = 94;
                break;
        }
    }

    public IEnumerator MoveToPosition(GameObject merson, GameObject nextSpace, float timeToMove)
    {
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            merson.transform.position = Vector2.Lerp(merson.transform.position, nextSpace.transform.position, t);
            yield return null;
        }
    }

    public IEnumerator WaitABit(GameObject merson, GameObject nextSpace, GameObject[] boardArray)
    {
        DiceButton = GameObject.FindGameObjectWithTag("DiceButton").GetComponent<Button>();
        yield return new WaitForSeconds(1.2f);
        GameObject onSpace = boardArray[merson.GetComponent<Meeple>().currentSpace];
        nextSpace = boardArray[onSpace.GetComponent<BoardSpace>().actionNum - 1];
        merson.GetComponent<Meeple>().currentSpace = nextSpace.GetComponent<BoardSpace>().spaceNumber - 1;
        StartCoroutine(MoveToPosition(merson, nextSpace, 1.2f));
        if (merson.GetComponent<Meeple>().currentSpace == 99) { info.GetComponent<GameState>().WinGame(merson); }
        DiceButton.interactable = true;
    }
}
