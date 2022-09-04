using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Generates the legs for the spider
 * Found a method to align the legs evenly around the body here: https://forum.unity.com/threads/placing-points-along-a-circular-path-relative-to-a-rotation.427139/
 **/
public class LegHandler : MonoBehaviour
{
    [SerializeField] GameObject legPrefab;
    [SerializeField] Transform parentFolder;
    [SerializeField] Transform body;
    [SerializeField] int numberOfLegs;
    [SerializeField] float kneeDistance;
    [SerializeField] float kneeHight;
    [SerializeField] public float maxLegDistance;
    [SerializeField] public float minLegDistance;

    private List<GameObject> knees;

    // Start is called before the first frame update
    void Start()
    {
        knees = new List<GameObject>();
        InitKnees();
        PositionKnees();
    }

    // Update is called once per frame
    void Update()
    {
        if (numberOfLegs != knees.Count)
        {
            InitKnees();
            PositionKnees();
        }
    }

    //Handles the Spawning and Deleting of Legs
    private void InitKnees()
    {
        // Spawns the legPrefab
        if (numberOfLegs > knees.Count)
        {
            for (int i = knees.Count; i < numberOfLegs; i++)
            {
                // doesn't matter where it is initiated, we're updating the positions later
                GameObject knee = Instantiate(legPrefab, body.position, Quaternion.identity, parentFolder);
                knee.gameObject.GetComponent<LegDrawer>().Body = body;
                knees.Add(knee);
            }
        }
        // Deletes the legPrefab if necessary
        if (numberOfLegs < knees.Count)
        {
            while (numberOfLegs < knees.Count)
            {
                Destroy(knees[0].gameObject);
                knees.RemoveAt(0);
            }
        }
    }

    // Places the legs evenly around the body
    public void PositionKnees()
    {
        //Get angle between upper Legs
        float angle = 360f / (float) numberOfLegs;

        //Get normalized vector and multiply by kneeDistance
        Vector3 kneeDistanceVector = transform.right * kneeDistance;

        for (int index = 0; index < numberOfLegs; ++index)
        {
            knees[index].transform.position = new Vector3(body.position.x, body.position.y + kneeHight, body.position.z) + kneeDistanceVector;
            RotateAroundBody(knees[index].transform, body.position, Quaternion.Euler(0, angle * index, 0));
            knees[index].gameObject.GetComponent<LegDrawer>().ResetFootPosition();
        }
    }

    //Help method to place the legs evenly around the body
    //Took this function from here: https://stackoverflow.com/questions/67804791/rotatearound-an-object-with-using-quaternion
    private void RotateAroundBody(Transform transform, Vector3 pivotPoint, Quaternion rotation)
    {
        rotation.ToAngleAxis(out var angle, out var axis);
        transform.RotateAround(pivotPoint, axis, angle);
    }


}
