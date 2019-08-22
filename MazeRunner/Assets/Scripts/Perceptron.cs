using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TrainingSet
{
    public string ItemName;
    public double[] input;
    public double output;
}


public class Perceptron : MonoBehaviour
{
    [TextArea]
    public string Description;

    public TrainingSet[] Set;

    /// <summary>
    /// Depends on the number of inputs of the training set
    /// </summary>
    public double[] weights = {0,0};

    double bias = 0;

    double totalError = 0;

    public SimpleGrapher SG;

    void DrawAllPoints()
    {
        for (int i = 0; i < Set.Length; i++)
        {
            if (Set[i].output == 0)
            { //weapon
                SG.DrawPoint((float)Set[i].input[0], (float)Set[i].input[1], Color.magenta);
            }
            else
            { //food
                SG.DrawPoint((float)Set[i].input[0], (float)Set[i].input[1], Color.green);
            }
        }
    }

    private void Start()
    {
        DrawAllPoints();
        Train(200);

        //slope, intercept, color
        SG.DrawRay((float)(-(bias / weights[1]) / (bias / weights[0])), (float)(-bias / weights[1]), Color.red);

        //Test the graph
        if (CalcOutput(0.3, 0.9) == 0)
        {
            SG.DrawPoint(0.3f, 0.9f, Color.red);
            
        }
        else
        {
            SG.DrawPoint(0.3f, 0.9f, Color.yellow);
        }
    }

    void Train(int epochs)
    {
        InitialiseWeights();

        for (int e = 0; e < epochs; e++)
        {
            totalError = 0;
            for (int t = 0; t < Set.Length; t++)
            {
                UpdateWeights(t);
                Debug.Log("W1: " + (weights[0]) + "W2: " + (weights[1]) + " B: " + bias);
            }
            Debug.Log("Total Error: " + totalError);
        }
    }

    void InitialiseWeights()
    {
        for (int i = 0; i < weights.Length; i++)
        {
            weights[i] = Random.Range(-1f, 1f);
        }
        bias = Random.Range(-1f, 1f);
    }      

    void UpdateWeights(int _lineNum)
    {
        double error = Set[_lineNum].output - CalcOutput(_lineNum);

        totalError += Mathf.Abs((float)error);

        for (int i = 0; i < weights.Length; i++)
        {
            weights[i] = weights[i] + error * Set[_lineNum].input[i];
        }

        bias += error;
    }

    double CalcOutput(int i)
    {
        double dp = DotProductBias(weights, Set[i].input);

        if (dp > 0)
            return 1;

        return 0;
    }

    /// <summary>
    /// For outside calculation of a set
    /// </summary>
    /// <param name="i1"></param>
    /// <param name="i2"></param>
    /// <returns></returns>
    double CalcOutput(double i1, double i2)
    {
        double[] inp = new double[] { i1, i2 };

        double dp = DotProductBias(weights, inp);

        if (dp > 0)
            return 1;

        return 0;
    }

    /// <summary>
    /// Calculates the dot product for the current training set
    /// </summary>
    /// <param name="v1">  These are the weights that are coming in for the current training set </param>
    /// <param name="v2"> These are the inputs that are coming in for the current training set </param>
    /// <returns></returns>
    double DotProductBias(double[] v1, double[] v2)
    {
        if (v1 == null || v2 == null)
        {
            return -1;
        }

        if (v1.Length != v2.Length)
        {
            return -1;
        }

        double d = 0;

        for (int i = 0; i < v1.Length; i++)
        {
            d += v1[i] * v2[i];
        }

        d += bias;

        return d;
    }
}
