using System;

namespace MotsCroises
{
    /// <summary>
    /// Point d'entrée du jeu de mots croisés
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            // Initialiser le jeu
            GameData.Initialiser();

            // Générer la grille avec les mots
            GridGenerator.GenererGrille();

            // Demander le nom du joueur
            Display.DemanderNom();

            // Afficher l'écran initial
            Display.AfficherEcranJeu();

            // Lancer la boucle de jeu
            GameLogic.JouerPartie();

            // Afficher l'écran de victoire
            Display.AfficherVictoire();

            Console.WriteLine("\n\nAppuyez sur une touche pour quitter...");
            Console.ReadKey();
        }
    }
}