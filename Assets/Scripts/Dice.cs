using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
    bool spinDiceActive = false;
    bool enableAlpha = false;
    float xspin;
    float yspin;
    float zspin;
    public GameObject rollResult;
    public Sprite roru1;
    public Sprite roru2;
    public Sprite roru3;
    public Sprite roru4;
    public Sprite roru5;
    public Sprite roru6;

    private void Update()
    {
        if (spinDiceActive)
        {
            transform.Rotate(xspin * 6, yspin * 6, zspin * 6);
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.06f, transform.position.z);
            transform.localScale = new Vector3(transform.localScale.x - 0.0008f * 3, transform.localScale.y - 0.0008f * 3, transform.localScale.z - 0.0008f * 3);
        }
        if (enableAlpha)
        {
            rollResult.transform.localScale = new Vector3(rollResult.transform.localScale.x + 0.009f * 8, rollResult.transform.localScale.y + 0.009f * 8);
            Color colalp = rollResult.GetComponent<SpriteRenderer>().color;
            colalp.a = colalp.a + 0.03f * 4;
            rollResult.GetComponent<SpriteRenderer>().color = colalp;
            rollResult.transform.position = new Vector3(transform.position.x, transform.position.y, -2);
        }
    }
    IEnumerator SpinDice(GameObject merson, int resultant)
    {
        Button DiceButton = GameObject.FindGameObjectWithTag("DiceButton").GetComponent<Button>();
        transform.position = new Vector3(8, -2f, -1.5f);
        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        DiceButton.interactable = false;
        while (spinDiceActive)
        {
            yield return new WaitForSeconds(1);
            enableAlpha = true;
            yield return new WaitForSeconds(0.2f);
            enableAlpha = false;
            spinDiceActive = false;
            transform.position = new Vector3(8f, 10.66f, -1.5f);
            yield return new WaitForSeconds(0.4f);
            rollResult.transform.position = new Vector3(20, 20, -2);
            rollResult.transform.localScale = new Vector3(0.2f, 0.2f);
            GameObject info = GameObject.FindGameObjectWithTag("Info");
            merson.GetComponent<Meeple>().Move(resultant, merson);
            //if (resultant == 6)
            //{
            //    resultant = 0;
            //    DiceRoll(merson, true);
            //}
            //else
            //{
                merson.GetComponent<Meeple>().turn++;
                info.GetComponent<GameState>().CyclePriority();
            //}
        }
    }
    public void DiceRoll(GameObject merson, bool alreadyRolled)
    {
        int diceresult = Random.Range(1, 7);
        spinDiceActive = true;
        xspin = Random.Range(-0.8f, 0.8f);
        yspin = Random.Range(-0.8f, 0.8f);
        zspin = Random.Range(-0.8f, 0.8f);
        Color colalp = rollResult.GetComponent<SpriteRenderer>().color;
        colalp.a = 0;
        rollResult.GetComponent<SpriteRenderer>().color = colalp;
        switch (diceresult)
        {
            case 1:
                rollResult.GetComponent<SpriteRenderer>().sprite = roru1;
                break;
            case 2:
                rollResult.GetComponent<SpriteRenderer>().sprite = roru2;
                break;
            case 3:
                rollResult.GetComponent<SpriteRenderer>().sprite = roru3;
                break;
            case 4:
                rollResult.GetComponent<SpriteRenderer>().sprite = roru4;
                break;
            case 5:
                rollResult.GetComponent<SpriteRenderer>().sprite = roru5;
                break;
            case 6:
                rollResult.GetComponent<SpriteRenderer>().sprite = roru6;
                break;
        }
        if (alreadyRolled)
        {
       //     StartCoroutine(ExtraDice(merson, diceresult));
        }
        else
        {
            StartCoroutine(SpinDice(merson, diceresult));
        }
    }
}
