using UnityEngine;

public class PlayerPinOnScreenMovement : MoanymonBehaviour
{
    public float Speed = 0.1f;

    // Use this for initialization
    void Start()
    {
        base.Start();

    }

    //// Update is called once per frame
    //void Update () {

    //}

    public void Up_OnClick()
    {
        Vector3 velocity = Vector3.zero;

        velocity += new Vector3(Speed, 0, 0);

        OnClickUpdate(velocity);
    }

    public void Down_OnClick()
    {
        Vector3 velocity = Vector3.zero;

        velocity -= new Vector3(Speed, 0, 0);

        OnClickUpdate(velocity);
    }

    public void Left_OnClick()
    {
        Vector3 velocity = Vector3.zero;

        velocity += new Vector3(0, 0, Speed);

        OnClickUpdate(velocity);
    }

    public void Right_OnClick()
    {
        Vector3 velocity = Vector3.zero;

        velocity -= new Vector3(0, 0, Speed);

        OnClickUpdate(velocity);
    }

    private void OnClickUpdate(Vector3 velocity)
    {
        transform.position += velocity;
        Camera.transform.position += velocity;
    }
}
