using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterMove : MonoBehaviour
{
    public static HelicopterMove instance;
    public GameObject[] Route;
    public GameObject PlaneStartPosition;
    public GameObject RouteStartPosition;
    public bool onRouteStart;
    public bool inRoute;
    public float timer;
    public int previousNode = 0;
    public int nextNode = 0;
    public int fren = 3;
    public bool play = false;
    public float beforeCicleBoxTime = 10f;
    public float betweenCicleBoxTime = 2f;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!onRouteStart && play)
        {
            //timer += Time.deltaTime*0.05f;
            LerpPosition(PlaneStartPosition, RouteStartPosition, beforeCicleBoxTime);
            if (timer - beforeCicleBoxTime <= 0.1f && beforeCicleBoxTime - timer <= 0.1f)
            {
                transform.position = RouteStartPosition.transform.position;
                onRouteStart = true;
                timer = 0f;
            }
        }
        else if (onRouteStart && !inRoute)
        {
            //timer += Time.deltaTime * 0.05f;
            LerpPosition(RouteStartPosition, Route[nextNode], betweenCicleBoxTime);
            if (timer - betweenCicleBoxTime <= 0.1f && betweenCicleBoxTime - timer <= 0.1f)
            {
                transform.position = Route[nextNode].transform.position;
                previousNode = nextNode;
                nextNode++;
                inRoute = true;
                timer = 0f;
            }   
        }
        else if (inRoute && fren>0)
        {
            //timer += Time.deltaTime * 0.05f;
            LerpPosition(Route[previousNode], Route[nextNode], betweenCicleBoxTime);
            if (timer - betweenCicleBoxTime <= 0.1f && betweenCicleBoxTime - timer <= 0.1f)
            {
                transform.position = Route[nextNode].transform.position;
                previousNode = nextNode;
                nextNode++;
                if(nextNode == 30)
                {
                    nextNode -= 30;
                    fren--;
                }
                inRoute = true;
                timer = 0f;
            }
        }

    }
    public void LerpPosition(GameObject start, GameObject end,  float totalTime)
    {        
        if (timer < totalTime)
        {
            transform.position = Vector3.Lerp(start.transform.position, end.transform.position, timer / totalTime);
            //ClampAngles(start.transform.eulerAngles);
            //ClampAngles(end.transform.eulerAngles);
            transform.eulerAngles = Vector3.Lerp(start.transform.eulerAngles, end.transform.eulerAngles, timer / totalTime);
        }
        timer += Time.deltaTime;
    }
    public void ClampAngles(Vector3 eulerAngle)
    {
        eulerAngle.x = ClampAngle(eulerAngle.x);
        eulerAngle.y = ClampAngle(eulerAngle.y);
        eulerAngle.z = ClampAngle(eulerAngle.z);
        
    }
    public float ClampAngle(float angle)
    {
        float a = angle;
        if (angle < -180) { a = angle + 360; }
        else if (angle > 180) { a = angle - 360; }
        return a;
    }
}
