using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA : MonoBehaviour
{
    /// <summary>
    /// The gene sequence
    /// </summary>
    List<int> genes = new List<int>();

    /// <summary>
    /// Length of the DNA
    /// </summary>
    int DNALength = 0;
    /// <summary>
    /// Max value of the gene int
    /// </summary>
    int MaxValues = 0;

    /// <summary>
    /// Class constructor for init basic values
    /// </summary>
    /// <param name="len"></param>
    /// <param name="val"></param>
    public DNA(int len,int val)
    {
        DNALength = len;
        MaxValues = val;
        SetRandom();
    }

    /// <summary>
    /// Sets the gene with random values
    /// </summary>
    public void SetRandom()
    {
        genes.Clear();

        for (int i = 0; i < DNALength; i++)
        {
            genes.Add(Random.Range(0, MaxValues));
        }
    }

    /// <summary>
    /// Sets the gene using the position
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="val"></param>
    public void SetInt(int pos, int val)
    {
        genes[pos] = val;
    }

    /// <summary>
    /// Combines parent's DNA into one by splitting into one DNA
    /// </summary>
    /// <param name="parent1"></param>
    /// <param name="parent2"></param>
    public void Combine(DNA parent1,DNA parent2)
    {
        for (int i = 0; i < DNALength; i++)
        {
            
            if (i < DNALength / 2)
            {
                int c = parent1.genes[i];
                genes[i] = c;
            }
            else
            {

                int c = parent2.genes[i];
                genes[i] = c;

            }
        }
    }

    /// <summary>
    /// Used for Mutation, Adds a random gene into the sequence
    /// </summary>
    public void Mutate()
    {
        genes[Random.Range(0, DNALength)] = Random.Range(0, MaxValues);
    }

    /// <summary>
    /// Gets the gene from the pos
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public int GetGene(int pos)
    {
        return genes[pos];
    }
}
