using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreesDestroyed : MonoBehaviour
{
    public Text treesDestroyedText;
    public int treesDestroyed=0;
    public int maxTreesDestroyed = 10; 
    // Método para aumentar el contador
    public void IncreaseTreesDestroyedCount()
    {
        treesDestroyed++;
        TreesDestroyedCounterUI();

        if(treesDestroyed >=  maxTreesDestroyed)
        {
            // invocar al boss
        }
    }

    // Actualiza el texto en la UI
    private void TreesDestroyedCounterUI()
    {
        treesDestroyedText.text = treesDestroyed.ToString();
    }
}
