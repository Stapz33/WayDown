using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lamp : MonoBehaviour
{
    [SerializeField] private List<float> temps;
    private float f_timer;
    private bool isOn = true;
    private Sprite lamp;
    [SerializeField] private Sprite nullsprite;

    private void Start()
    {
        lamp = GetComponent<Image>().sprite;
        int t = Random.Range(0, temps.Count - 1);
        f_timer = temps[t];
    }

    private void Update()
    {
        if (f_timer > 0 && isOn)
        {
            f_timer -= Time.deltaTime;
            if (f_timer <= 0)
            {
                GetComponent<Animator>().SetTrigger("Blink");
                int t = Random.Range(0, temps.Count - 1);
                f_timer = temps[t];
            }
        }
    }

    public void ToggleLight()
    {
        if (isOn)
        {
            GetComponent<Image>().sprite = nullsprite;
            isOn = false;
            return;
        }
        else if (!isOn)
        {
            GetComponent<Image>().sprite = lamp;
            isOn = true;
            return;
        }
    }
}
