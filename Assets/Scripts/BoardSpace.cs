using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardSpace : MonoBehaviour
{
    public int spaceNumber = 1;
    public Sprite evenSprite;
    public Sprite oddSprite;
    public Sprite endSprite;
    public Sprite ladderSprite;
    public Sprite snakeSprite;
    public int action = 0;
    public int actionNum;
    public Font displayFont;
    GameObject[] boardArray;

    void Start()
    {
        boardArray = new GameObject[105] { GameObject.Find("BoardSpace"), GameObject.Find("BoardSpace (1)"), GameObject.Find("BoardSpace (2)"), GameObject.Find("BoardSpace (3)"), GameObject.Find("BoardSpace (4)"), GameObject.Find("BoardSpace (5)"), GameObject.Find("BoardSpace (6)"), GameObject.Find("BoardSpace (7)"), GameObject.Find("BoardSpace (8)"), GameObject.Find("BoardSpace (9)"), GameObject.Find("BoardSpace (10)"), GameObject.Find("BoardSpace (11)"), GameObject.Find("BoardSpace (12)"), GameObject.Find("BoardSpace (13)"), GameObject.Find("BoardSpace (14)"), GameObject.Find("BoardSpace (15)"), GameObject.Find("BoardSpace (16)"), GameObject.Find("BoardSpace (17)"), GameObject.Find("BoardSpace (18)"), GameObject.Find("BoardSpace (19)"), GameObject.Find("BoardSpace (20)"), GameObject.Find("BoardSpace (21)"), GameObject.Find("BoardSpace (22)"), GameObject.Find("BoardSpace (23)"), GameObject.Find("BoardSpace (24)"), GameObject.Find("BoardSpace (25)"), GameObject.Find("BoardSpace (26)"), GameObject.Find("BoardSpace (27)"), GameObject.Find("BoardSpace (28)"), GameObject.Find("BoardSpace (29)"), GameObject.Find("BoardSpace (30)"), GameObject.Find("BoardSpace (31)"), GameObject.Find("BoardSpace (32)"), GameObject.Find("BoardSpace (33)"), GameObject.Find("BoardSpace (34)"), GameObject.Find("BoardSpace (35)"), GameObject.Find("BoardSpace (36)"), GameObject.Find("BoardSpace (37)"), GameObject.Find("BoardSpace (38)"), GameObject.Find("BoardSpace (39)"), GameObject.Find("BoardSpace (40)"), GameObject.Find("BoardSpace (41)"), GameObject.Find("BoardSpace (42)"), GameObject.Find("BoardSpace (43)"), GameObject.Find("BoardSpace (44)"), GameObject.Find("BoardSpace (45)"), GameObject.Find("BoardSpace (46)"), GameObject.Find("BoardSpace (47)"), GameObject.Find("BoardSpace (48)"), GameObject.Find("BoardSpace (49)"), GameObject.Find("BoardSpace (50)"), GameObject.Find("BoardSpace (51)"), GameObject.Find("BoardSpace (52)"), GameObject.Find("BoardSpace (53)"), GameObject.Find("BoardSpace (54)"), GameObject.Find("BoardSpace (55)"), GameObject.Find("BoardSpace (56)"), GameObject.Find("BoardSpace (57)"), GameObject.Find("BoardSpace (58)"), GameObject.Find("BoardSpace (59)"), GameObject.Find("BoardSpace (60)"), GameObject.Find("BoardSpace (61)"), GameObject.Find("BoardSpace (62)"), GameObject.Find("BoardSpace (63)"), GameObject.Find("BoardSpace (64)"), GameObject.Find("BoardSpace (65)"), GameObject.Find("BoardSpace (66)"), GameObject.Find("BoardSpace (67)"), GameObject.Find("BoardSpace (68)"), GameObject.Find("BoardSpace (69)"), GameObject.Find("BoardSpace (70)"), GameObject.Find("BoardSpace (71)"), GameObject.Find("BoardSpace (72)"), GameObject.Find("BoardSpace (73)"), GameObject.Find("BoardSpace (74)"), GameObject.Find("BoardSpace (75)"), GameObject.Find("BoardSpace (76)"), GameObject.Find("BoardSpace (77)"), GameObject.Find("BoardSpace (78)"), GameObject.Find("BoardSpace (79)"), GameObject.Find("BoardSpace (80)"), GameObject.Find("BoardSpace (81)"), GameObject.Find("BoardSpace (82)"), GameObject.Find("BoardSpace (83)"), GameObject.Find("BoardSpace (84)"), GameObject.Find("BoardSpace (85)"), GameObject.Find("BoardSpace (86)"), GameObject.Find("BoardSpace (87)"), GameObject.Find("BoardSpace (88)"), GameObject.Find("BoardSpace (89)"), GameObject.Find("BoardSpace (90)"), GameObject.Find("BoardSpace (91)"), GameObject.Find("BoardSpace (92)"), GameObject.Find("BoardSpace (93)"), GameObject.Find("BoardSpace (94)"), GameObject.Find("BoardSpace (95)"), GameObject.Find("BoardSpace (96)"), GameObject.Find("BoardSpace (97)"), GameObject.Find("BoardSpace (98)"), GameObject.Find("BoardSpace (99)"), GameObject.Find("BoardSpace (98)"), GameObject.Find("BoardSpace (97)"), GameObject.Find("BoardSpace (96)"), GameObject.Find("BoardSpace (95)"), GameObject.Find("BoardSpace (94)") };
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        if (spaceNumber == 100)
        {
            GetComponent<SpriteRenderer>().sprite = endSprite;
        }
        else if (spaceNumber % 2 == 0)
        {
            GetComponent<SpriteRenderer>().sprite = evenSprite;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = oddSprite;
        }
        DrawHazards();
        if (spaceNumber != 100) { DrawSpaceNum(); }
    }

    void DrawHazards()
    {
        if (action == 1)
        {
            GameObject endPoint = boardArray[actionNum - 1];
            GameObject ladder = new GameObject();
            ladder.name = "Ladder " + spaceNumber + "->" + actionNum;
            ladder.AddComponent<SpriteRenderer>();
            ladder.SetActive(true);
            ladder.transform.position = transform.position;
            ladder.GetComponent<SpriteRenderer>().sprite = ladderSprite;
            ladder.transform.localScale = new Vector3(1f, 1f, 1f);
            ladder.transform.parent = GameObject.Find("-----Ladders-----").transform;
            ladder.GetComponent<SpriteRenderer>().sortingLayerID = -515641929;
            ladder.AddComponent<SnakeLadder>();
            ladder.tag = "LadderSnake";
            ladder.GetComponent<SnakeLadder>().Stretch(ladder, transform.position, endPoint.transform.position, false);
        }
        else if (action == 2)
        {
            GameObject endPoint = boardArray[actionNum - 1];
            GameObject snake = new GameObject();
            snake.AddComponent<SnakeLadder>();
            snake.AddComponent<SpriteRenderer>();
            snake.name = "Snake " + spaceNumber + "->" + actionNum;
            snake.SetActive(true);
            snake.transform.position = transform.position;
            snake.GetComponent<SpriteRenderer>().sprite = snakeSprite;
            snake.transform.localScale = new Vector3(1f, 1f, 1f);
            snake.transform.parent = GameObject.Find("-----Snakes-----").transform;
            snake.GetComponent<SpriteRenderer>().sortingLayerID = -515641929;
            snake.tag = "LadderSnake";
            snake.GetComponent<SnakeLadder>().Stretch(snake, transform.position, endPoint.transform.position, true);
        }
    }
    void DrawSpaceNum()
    {
        GameObject numText = new GameObject();
        numText.AddComponent<RectTransform>();
        numText.AddComponent<CanvasRenderer>();
        numText.AddComponent<Text>();
        numText.GetComponent<RectTransform>().SetParent(GameObject.Find("BoardCanvas").transform, false);
        numText.GetComponent<RectTransform>().SetPositionAndRotation(transform.position, numText.GetComponent<RectTransform>().rotation);
        numText.GetComponent<CanvasRenderer>().cullTransparentMesh = true;
        numText.GetComponent<Text>().text = spaceNumber.ToString();
        numText.SetActive(true);
        numText.name = spaceNumber.ToString() + "indicator";
        numText.GetComponent<Text>().fontSize = 25;
        numText.GetComponent<Text>().font = displayFont;
        Color colorTemp;
        colorTemp.g = 0; colorTemp.r = 0; colorTemp.b = 0; colorTemp.a = 0;
        numText.GetComponent<Text>().color = colorTemp;
        numText.tag = "NumInfo";
        numText.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
    }
}