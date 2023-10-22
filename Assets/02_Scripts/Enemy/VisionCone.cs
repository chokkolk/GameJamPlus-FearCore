using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VisionCone : MonoBehaviour
{
    float timer = 0f;
    public bool hunt, inRange = false;
    public Material visionConeMaterial;
    public float visionRange;
    public float visionAngle;
    [SerializeField] private float TimeMultiplier;
    public LayerMask visionObstructingLayer, player;//layer with objects that obstruct the enemy view, like walls, for example
    public int visionConeResolution = 120;//the vision cone will be made up of triangles, the higher this value is the pretier the vision cone will be
    Mesh VisionConeMesh;
    MeshFilter meshFilter;
    //Create all of these variables, most of them are self explanatory, but for the ones that aren't i've added a comment to clue you in on what they do
    //for the ones that you dont understand dont worry, just follow along
    void Start()
    {
        transform.AddComponent<MeshRenderer>().material = visionConeMaterial;
        meshFilter = transform.AddComponent<MeshFilter>();
        VisionConeMesh = new Mesh();
        visionAngle *= Mathf.Deg2Rad;
    }

    private void FixedUpdate()
    {
        DrawVisionCone();//calling the vision cone function everyframe just so the cone is updated every frame
    }

    void DrawVisionCone()//this method creates the vision cone mesh
    {
        int[] triangles = new int[(visionConeResolution - 1) * 3];
        Vector3[] Vertices = new Vector3[visionConeResolution + 1];
        Vertices[0] = Vector3.zero;
        float Currentangle = -visionAngle / 2;
        float angleIcrement = visionAngle / (visionConeResolution - 1);
        float Sine;
        float Cosine;

        for (int i = 0; i < visionConeResolution; i++)
        {
            Sine = Mathf.Sin(Currentangle);
            Cosine = Mathf.Cos(Currentangle);
            Vector3 RaycastDirection = (transform.forward * Cosine) + (transform.right * Sine);
            Vector3 VertForward = (Vector3.forward * Cosine) + (Vector3.right * Sine);
            if (Physics.Raycast(transform.position, RaycastDirection, out RaycastHit hit, visionRange, visionObstructingLayer))
            {
                Vertices[i + 1] = VertForward * hit.distance;
            }
            else
            {
                Vertices[i + 1] = VertForward * visionRange;
            }
            Currentangle += angleIcrement;
            if (Physics.Raycast(transform.position, RaycastDirection, out RaycastHit character, visionRange, player))
            {
                inRange = true;
            }
        }

        if (inRange == true)
        {
            if (hunt == false) SetHunt(true);
            timer = 2;
        }
        else if (timer > 0)
        {
            timer -= Time.fixedDeltaTime;
            Debug.Log(timer);
        }
        else
        {
            if (hunt == true) SetHunt(false);
        }

        for (int i = 0, j = 0; i < triangles.Length; i += 3, j++)
        {
            triangles[i] = 0;
            triangles[i + 1] = j + 1;
            triangles[i + 2] = j + 2;
        }
        VisionConeMesh.Clear();
        VisionConeMesh.vertices = Vertices;
        VisionConeMesh.triangles = triangles;
        meshFilter.mesh = VisionConeMesh;
        inRange = false;
    }
    private void SetHunt(bool value)
    {
        hunt = value;
    }
}
