using UnityEngine;
using System.Collections;

public class CamMove : MonoBehaviour {

    private float xMargin = 1f;
    private float yMargin = 1f;
    private float xSmooth = 8f;
    private float ySmooth = 8f;
    public Vector2 maxXAndY;
    public Vector2 minXAndY;

    public Transform Player;

    private bool CheckXMargin()
    {
        return Mathf.Abs(transform.position.x - Player.position.x) > xMargin;
    }


    private bool CheckYMargin()
    {
        return Mathf.Abs(transform.position.y - Player.position.y) > yMargin;
    }


    private void Update()
    {
        if (transform.position.x > 176 && transform.position.x < 181)
        {
            minXAndY = new Vector2(transform.position.x + 1, minXAndY.y);
        }
        TrackPlayer();
    }


    private void TrackPlayer()
    {
        float targetX = transform.position.x;
        float targetY = transform.position.y;

        if (CheckXMargin())
        {
            targetX = Mathf.Lerp(transform.position.x, Player.position.x, xSmooth * Time.deltaTime);
        }

        if (CheckYMargin())
        {
            targetY = Mathf.Lerp(transform.position.y, Player.position.y, ySmooth * Time.deltaTime);
        }

        targetX = Mathf.Clamp(targetX, minXAndY.x, maxXAndY.x);
        targetY = Mathf.Clamp(targetY, minXAndY.y, maxXAndY.y);

        transform.position = new Vector3(targetX, targetY, transform.position.z);
    }
}
