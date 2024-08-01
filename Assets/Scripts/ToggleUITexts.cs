using UnityEngine;
using UnityEngine.UI;

public class ToggleUITexts: MonoBehaviour
{
    public Text text1;
    public Text text2;
    private bool isText1Active = true;
    private float timer = 0f;
    public float switchInterval = 2f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= switchInterval)
        {
            isText1Active = !isText1Active;
            text1.gameObject.SetActive(isText1Active);
            text2.gameObject.SetActive(!isText1Active);
            timer = 0f;
        }
    }
}
