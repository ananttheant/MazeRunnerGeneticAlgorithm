using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour
{
    /// <summary>
    /// Why Two?
    /// Needs to make two decisions
    /// 1. What to do when i see the Wall
    /// 2. What to do when i dont see the Wall
    /// </summary>
    int DNALength = 2;

    /// <summary>
    /// Link to DNA
    /// </summary>
    [HideInInspector]
    public DNA dna;
    /// <summary>
    /// Link to our Eyes
    /// </summary>
    public GameObject eyes;

    /// <summary>
    /// If we are seeing a wal in front of us
    /// </summary>
    bool seeWall = true;

    /// <summary>
    /// For starting position
    /// </summary>
    /// <remarks>
    /// we need this cuz we our fitness algo depends on how far we have reached 
    /// </remarks>
    Vector3 startPosition;

    /// <summary>
    /// For the amount of distance we have travelled for the current agent
    /// </summary>
    public float distaceTravelled = 0;

    /// <summary>
    /// If the agent is still alive
    /// </summary>
    bool alive = false;

    /// <summary>
    /// If we end up hitting the dead zone which is the black part, we die
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "dead")
        {
            distaceTravelled = 0;
            alive = false;
        }
    }


    public void Init()
    {
        ///DNA
        ///of Length Two <see cref="Brain.DNALength"/>
        ///dna[0] -> corresponds to when we don't see the wall what to do
        ///dna[1] -> corresponds to when we see the wall what to do

        //DNA Actions
        // 0 forward
        // 1 Angle Turn

        dna = new DNA(DNALength, 360);
        alive = true;
        distaceTravelled = 0;

        startPosition = transform.position;
    }

    private void Update()
    {
        if (!alive)
            return;

        seeWall = false;

        RaycastHit hit;

        Debug.DrawRay(eyes.transform.position, eyes.transform.forward * 0.5f, Color.red);
        if (Physics.SphereCast(eyes.transform.position, 0.2f, eyes.transform.forward, out hit, 0.7f))
        {

            if (hit.collider.gameObject.tag == "wall")
            {

                seeWall = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (!alive)
            return;

        //Read DNA 
        float h = 0; // rotate
        float v = dna.GetGene(0); // steps

        if (seeWall)
        {
            h = dna.GetGene(1);
        }

        this.transform.Translate(0, 0, v * 0.001f);
        transform.Rotate(0, h, 0);
        distaceTravelled = Vector3.Distance(startPosition, transform.position);
    }

}
