using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlphaTool : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        GetComponent<Image>().alphaHitTestMinimumThreshold = 1f;
    }
}
