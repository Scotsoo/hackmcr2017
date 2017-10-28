using UnityEngine;

namespace Assets.Scripts
{
    public class MoanymonBehaviour: MonoBehaviour
    {
        public GameObject Camera;

        public void Start()
        {
            Camera = GameObject.FindGameObjectWithTag("MainCamera");
        }

        public bool DetectClick()
        {
            // if left button pressed...
            if (!Input.GetMouseButtonDown(0))
            {
                return false;
            }

            Ray ray = Camera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            return Physics.Raycast(ray, out hit);
        }
    }
}
