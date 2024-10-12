using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeState : MonoBehaviour
{
    public GameObject arbolMuertoPrefab; // Prefab del árbol muerto
    public int hitThreshold = 3; // Número de impactos necesarios
    private int currentHits = 0; // Contador de impactos recibidos

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("bulletPlay"))
        {
            currentHits++;
            Destroy(other.gameObject); // Destruir el objeto "bulletPlay"
            
            // Si el árbol alcanza el umbral de impactos, cambiar al árbol quemado
            if (currentHits >= hitThreshold)
            {
                ChangeToBurnedTree();
            }
        }
    }

    // Cambia el árbol actual por el árbol quemado
    void ChangeToBurnedTree()
    {
        // Instanciar el árbol quemado en la misma posición y rotación del árbol original
        GameObject burnedTree = Instantiate(arbolMuertoPrefab, transform.position, transform.rotation);

        // Asegurar que el árbol quemado tenga la misma escala que el original
        burnedTree.transform.localScale = transform.localScale;

        // Destruir el árbol original inmediatamente
        Destroy(gameObject);
    }
}
