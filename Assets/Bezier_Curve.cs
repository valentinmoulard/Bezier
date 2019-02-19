using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode, RequireComponent(typeof(LineRenderer))]
public class Bezier_Curve : MonoBehaviour {

    [SerializeField]
    List<GameObject> checkpoints = new List<GameObject>();
    
    public Color color = Color.white;
    public float width = 0.2f;
    public int numberOfPoints = 20;

    LineRenderer lineRenderer;
    private float t = 0;

    // Use this for initialization
    void Start () {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = true;
        //lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
    }
	
	// Update is called once per frame
	void Update () {
        UpdateLineRenderer();

        if (numberOfPoints > 0)
        {
            lineRenderer.positionCount = numberOfPoints;
        }
        
        CalculatePos(checkpoints[0], checkpoints[1], checkpoints[2], checkpoints[3]);

        AfficheDistance();


    }



    void AfficheDistance()
    {
        for (int i = 0; i < numberOfPoints -1 ; i++)
        {
            Debug.Log(Vector3.Distance(lineRenderer.GetPosition(i), lineRenderer.GetPosition(i+1)));
        }
    }


    void CalculatePos(GameObject GOp1, GameObject GOp2, GameObject GOp3, GameObject GOp4)
    {
        Vector3 position = new Vector3();
        for (int i = 0; i < numberOfPoints; i++)
        {
            t = i / ((float)numberOfPoints - 1.0f);

            for (int j = 0; j < checkpoints.Count; j++)
            {
                position += Bernstein(j, checkpoints.Count, t) * checkpoints[j].transform.position;
            }

            lineRenderer.SetPosition(i, position);
            position = Vector3.zero;
        }
    }

    void UpdateLineRenderer()
    {
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;
    }

    float Bernstein(int j, int n, float t)
    {
        float result = ((Factorial(n)) / ((Factorial(j)) * Factorial(n - j))) * Mathf.Pow(t, j) * Mathf.Pow(1 - t, n - j);
        return result;
    }


    int Factorial(int i)
    {
        if (i <= 1)
            return 1;
        return i * Factorial(i - 1);
    }




}
