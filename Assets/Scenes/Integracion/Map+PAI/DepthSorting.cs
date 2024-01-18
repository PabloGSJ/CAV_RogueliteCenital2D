using UnityEngine;
using System.Collections.Generic;

public class DepthSorting : MonoBehaviour
{
    public SpriteRenderer characterRenderer;

    // Aquí puedes definir los nombres de tus Sorting Layers
    public string sortingLayerBehind = "BehindCharacter";
    public string sortingLayerInFront = "InFrontOfCharacter";

    private List<SpriteRenderer> allParentRenderers; // Lista de todos los SpriteRenderers padres en el mapa

    void Start()
    {
        // Encuentra todos los SpriteRenderers que son padres
        allParentRenderers = new List<SpriteRenderer>();
        foreach (SpriteRenderer renderer in FindObjectsOfType<SpriteRenderer>())
        {
            if (renderer.transform.childCount > 0 && renderer.gameObject.name != "Character")
            {
                allParentRenderers.Add(renderer);
            }
        }
    }

    void Update()
    {
        if (characterRenderer == null)
        {
            Debug.LogError("CharacterRenderer no ha sido asignado en el inspector.");
            return; // No continúes si no hay un characterRenderer
        }

        // Itera sobre todos los SpriteRenderers padres encontrados
        foreach (SpriteRenderer parentRenderer in allParentRenderers)
        {
            if (parentRenderer != null)
            {
                // Encuentra el 'basePoint' que es el hijo llamado 'base'
                Transform basePoint = parentRenderer.transform.Find("Base");
                if (basePoint == null)
                {
                    Debug.LogError("BasePoint 'base' no se encontró como hijo de " + parentRenderer.gameObject.name);
                    continue; // No continúes si no hay un basePoint
                }

                // Cambia el Sorting Layer basándote en la posición Y del personaje y la base del objeto
                string newSortingLayerName = characterRenderer.transform.position.y < basePoint.position.y ? sortingLayerBehind : sortingLayerInFront;
                parentRenderer.sortingLayerName = newSortingLayerName;

                // Aplica el cambio a todos los hijos para mantener la consistencia
                foreach (Transform child in parentRenderer.transform)
                {
                    SpriteRenderer childRenderer = child.GetComponent<SpriteRenderer>();
                    if (childRenderer != null)
                    {
                        childRenderer.sortingLayerName = newSortingLayerName;
                    }
                }
            }
        }
    }
}
