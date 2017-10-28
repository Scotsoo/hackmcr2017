using UnityEngine;

namespace Assets.Scripts.PlayerActions
{
    public class PlayerClick : MoneymonBehaviour
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
                Debug.Log("Raycast hit!");
            }
        }
    }
}
