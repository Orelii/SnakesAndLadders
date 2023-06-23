using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeLadder : MonoBehaviour
{
    public void Stretch(GameObject ladder, Vector3 initialPos, Vector3 finalPos, bool mirrorYAxis)
    {
        Vector3 centerPos = (initialPos + finalPos) / 2;
        ladder.transform.position = centerPos;
        Vector3 direction = finalPos - initialPos;
        ladder.transform.up = direction;
        if (mirrorYAxis) { ladder.transform.up *= -1; }
        Vector3 scale = new Vector3(1, 1, 1);
        scale.y = Vector3.Distance(initialPos, finalPos);
        ladder.transform.localScale = scale;
        ladder.GetComponent<SpriteRenderer>().forceRenderingOff = true;
    }
}
