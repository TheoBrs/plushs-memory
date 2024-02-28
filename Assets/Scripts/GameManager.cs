using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Instance statique pour accéder au GameManager depuis d'autres scripts.
    
    void Awake()
    {
        // Vérifie si instance existe déjà
        if (instance == null)
        {
            instance = this; // Assigner l'instance actuelle si elle n'existe pas
            DontDestroyOnLoad(gameObject); // Ne pas détruire cet objet lors du chargement d'une nouvelle scène
        }
        else
        {
            Destroy(gameObject); // Détruire cette instance si une autre instance existe déjà
        }
    }

    // Vous pouvez ajouter ici vos variables de jeu, comme le score, l'état du jeu, etc.
    public int score = 0;

    // Méthode pour augmenter le score
    public void IncreaseScore(int amount)
    {
        score += amount;
        Debug.Log("Score actuel: " + score);
    }

    // Ici, vous pouvez ajouter d'autres méthodes pour gérer les états de jeu, charger des niveaux, etc.
}