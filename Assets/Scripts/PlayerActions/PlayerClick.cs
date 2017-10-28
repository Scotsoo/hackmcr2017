using UnityEngine;

namespace Assets.Scripts.PlayerActions
{
    public class PlayerClick : MoanymonBehaviour
    {
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
