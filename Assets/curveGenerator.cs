using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(LineRenderer))]
public class curveGenerator : MonoBehaviour {
    public bool usingBez;
    public int numSegments;
    public GameObject obj1, obj2, obj3, obj4;
    private LineRenderer lr;
    private Vector3 pos1, pos2, pos3, pos4;
	// Use this for initialization
	void Start () {
        pos1 = obj1.transform.position;
        pos2 = obj2.transform.position;
        pos3 = obj3.transform.position;
        pos4 = obj4.transform.position;

        lr = GetComponent<LineRenderer>();
        lr.positionCount = numSegments + 1;

        if (usingBez)
        {
            lr.SetPositions(genCubicBez(numSegments, pos1, pos2, pos3, pos4));
        }
        else
        {
            lr.SetPositions(genQuadraticChris(numSegments, pos1, pos2, pos3, pos4));
        }
	}

    //for generating smooth looking curves, given exact points and a number of steps
    private Vector3[] genCubicBez(int jumpsToMake/* >= 0*/, Vector3 A, Vector3 B, Vector3 C, Vector3 D)
    {
        Vector3[] toReturn = new Vector3[jumpsToMake + 1];
        //t1 points
        Vector3 ab, bc, cd;
        //t2 points
        Vector3 abbc, bccd;
        //t3 point
        Vector3 abbcbccd;
        for (int i = 0; i <= jumpsToMake; ++i)
        {
            //t1 updates
            ab = (1.0f * i / jumpsToMake) * B + (1.0f * (jumpsToMake - i) / jumpsToMake) * A;
            bc = (1.0f * i / jumpsToMake) * C + (1.0f * (jumpsToMake - i) / jumpsToMake) * B;
            cd = (1.0f * i / jumpsToMake) * D + (1.0f * (jumpsToMake - i) / jumpsToMake) * C;
            //t2 updates
            abbc = (1.0f * i / jumpsToMake) * bc + (1.0f * (jumpsToMake - i) / jumpsToMake) * ab;
            bccd = (1.0f * i / jumpsToMake) * cd + (1.0f * (jumpsToMake - i) / jumpsToMake) * bc;
            //t3 update
            abbcbccd = (1.0f * i / jumpsToMake) * bccd + (1.0f * (jumpsToMake - i) / jumpsToMake) * abbc;
            toReturn[i] = abbcbccd;
        }
        return toReturn;
    }

    //for generating smooth looking curves, given exact points and a number of steps
    private Vector3[] genQuadraticChris(int jumpsToMake/* >= 0*/, Vector3 A, Vector3 B, Vector3 C, Vector3 D)
    {
        Vector3[] toReturn = new Vector3[jumpsToMake + 1];
        //t1 points
        Vector3 ab, cd;
        //t2 points
        Vector3 abcd;
        float distToEnd = 0f;
        for (int i = 0; i <= jumpsToMake; ++i)
        {
            distToEnd = 1.0f * i / jumpsToMake;
            //t1 updates
            ab = A + distToEnd * (B - A);
            cd = C + distToEnd * (D - C);
            //t2 updates
            abcd = ab + distToEnd * (cd - ab);
            toReturn[i] = abcd;
        }
        return toReturn;
    }

    //for generating smooth looking curves, given exact points and a number of steps
    private Vector3[] genModChris(int jumpsToMake/* >= 0*/, Vector3 A, Vector3 B, Vector3 C, Vector3 D)
    {
        Vector3[] toReturn = new Vector3[jumpsToMake + 1];
        //t1 points
        Vector3 ab, cd;
        //t2 points
        Vector3 abcd;
        float distToEnd = 0f;
        for (int i = 0; i <= jumpsToMake; ++i)
        {
            distToEnd = 1.0f * i / jumpsToMake;
            //t1 updates
            ab = A + distToEnd * (B - A);
            cd = C + distToEnd * (D - C);
            //t2 updates
            abcd = ab + distToEnd * (cd - ab);
            toReturn[i] = abcd;
        }
        return toReturn;
    }
}
