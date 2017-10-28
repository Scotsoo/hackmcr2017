using UnityEngine;

public class LetterClick : MoanymonBehaviour
{
    //// Use this for initialization
    //void Start()
    //{
    //    base.Start();
    //}

    // Update is called once per frame
    void Update()
    {
        OnClick();
    }

    public void OnClick()
    {
        if (DetectClick())
        {
            Debug.Log("Raycast hit");
            // TODO Do add to inventory shit and remove from plane
        }
    }
}
