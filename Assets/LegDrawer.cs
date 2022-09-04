using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Uses the LineRenderer to draw the lines (actual legs) of the spider
 **/
public class LegDrawer : MonoBehaviour
{
    public Transform Body { get; set; }
    private Vector3 Foot;
    private LegHandler legHandler;

    private LineRenderer lineRenderer;

    private void Start()
    {
        //Get LegHandler
        legHandler = GetComponentInParent<LegHandler>();
        //Get LindeRenderer
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 3;
        //Set Foot directly underneath Knee
        Foot = new Vector3(transform.position.x, 0, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLegPosition();
        lineRenderer.SetPosition(0, Body.position);
        lineRenderer.SetPosition(1, transform.position);
        lineRenderer.SetPosition(2, Foot);
    }

    //Changes the position of the foot relative to the body
    public void UpdateLegPosition()
    {
        //Get Input Direction as Vector
        Vector3 inputDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

        //Check if input is valid
        if (inputDirection.magnitude > 0f)
        {
            //Get WorldPlane
            Plane planeWorld = new Plane(new Vector3(0, 1, 0), new Vector3(0, 0, 0));

            //Get PositionOf Foot on Plane
            Vector3 currentKneePosition = planeWorld.ClosestPointOnPlane(transform.position);
            //Get Position of Body on Plane
            Vector3 currentBodyPosition = planeWorld.ClosestPointOnPlane(Body.position);

            if (Vector3.Distance(Foot, currentKneePosition) > legHandler.maxLegDistance || Vector3.Distance(Foot, currentBodyPosition) < legHandler.minLegDistance)
            {
                //Get Plane orthogonal to inputDirection with knee Position as reference point
                Plane planeInputFromKnee = new Plane(inputDirection, currentKneePosition);

                //Get Mirror Point for Foot Position
                Vector3 mirrorPoint = planeInputFromKnee.ClosestPointOnPlane(Foot);

                //Get Vector From Foot to mirrorPoint
                Vector3 legPlaneVector = mirrorPoint - Foot;

                //Calculate newPosition
                Vector3 newPosition = currentKneePosition + legPlaneVector.normalized * legHandler.maxLegDistance * 0.5f;

                //Check if newPosition is to close to body, if so increase distance
                if (Vector3.Distance(currentBodyPosition, newPosition) < legHandler.minLegDistance)
                {
                    //Get Vector from center to point
                    newPosition = currentBodyPosition + (newPosition - currentBodyPosition).normalized * legHandler.minLegDistance * 1.1f;
                }

                //Randomize the exact new Position of the leg
                float randomFloat = Random.Range(-1f, 1f);

                //Change Position of Foot
                Foot = new Vector3(newPosition.x + randomFloat, 0, newPosition.z + randomFloat);
            }
        }
    }

    public void ResetFootPosition()
    {
        Foot = new Vector3(transform.position.x, 0, transform.position.z);
    }

}
