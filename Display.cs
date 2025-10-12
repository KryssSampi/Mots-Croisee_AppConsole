using System;
using System.Collections.Generic;
using static MotsCroises.GameData;

namespace MotsCroises
{
    /// <summary>
    /// Gère tout l'affichage du jeu
    /// </summary>
    public static class Display
    {
        /// <summary>
        /// Affiche du texte en couleur
        /// </summary>
        public static void AfficherCouleur(string texte, int couleur)
        {
            Console.ForegroundColor = (ConsoleColor)couleur;
            Console.Write(texte);
            Console.ResetColor();
        }

        /// <summary>
        /// Affiche l'en-tête du jeu
        /// </summary>
        private static void AfficherEntete()
        {
            AfficherCouleur("========================================================================================================================", CouleurHorizontale);
            AfficherCouleur("|| ", CouleurVerticale);
            AfficherCouleur("\t \t \t \t \t \t  | LES MOTS-CROISÉES |\t\t\t\t\t\t      ", 15);
            AfficherCouleur("||\n", CouleurVerticale);
            AfficherCouleur("========================================================================================================================", CouleurHorizontale);
            AfficherCouleur("|| ", CouleurVerticale);
            AfficherCouleur("\t \t \t \t \t \t BIENVENUE MON CHER AMI \t\t\t\t\t      ", 15);
            AfficherCouleur("||\n", CouleurVerticale);
            AfficherCouleur("|| ", CouleurVerticale);
            AfficherCouleur("\t \t \t\t \t    CECI EST UN PETIT JEU AMUSANT\t\t\t\t\t      ", 15);
            AfficherCouleur("||", CouleurVerticale);
            AfficherCouleur("========================================================================================================================", CouleurHorizontale);
            AfficherCouleur("|| ", CouleurVerticale);
            AfficherCouleur($"\t\t\t      Voici une grille contenant {NombreMotsTotal} mots caches essaies de les trouver tous\t\t      ", 15);
            AfficherCouleur("||\n", CouleurVerticale);
            AfficherCouleur("|| ", CouleurVerticale);
            AfficherCouleur($" \t\t\t\t\t\t!!!BONNE CHANCE {Username.ToUpper()}!!!\t\t\t\t\t      ", 15);
            AfficherCouleur("||\n", CouleurVerticale);
            AfficherCouleur("========================================================================================================================\n", CouleurHorizontale);
        }

        /// <summary>
        /// Affiche les statistiques de progression
        /// </summary>
        private static void AfficherStatistiques()
        {
            AfficherCouleur($"\t\t\t\t\t\t   MOT TROUVÉ(S) : {NombreMotsTrouves}/{NombreMotsTotal}\n", CouleurVerticale);
        }

        /// <summary>
        /// Affiche la grille avec coloration selon les mots trouvés
        /// </summary>
        private static void AfficherGrille(bool toutColorer = false, bool messageSucces = false)
        {
            if (messageSucces)
            {
                AfficherCouleur("\n\t\t\t\t\t\t!!!! BIEN JOUÉ !!!!\n", 2);
            }

            for (int ligne = 0; ligne < TailleGrille; ligne++)
            {
                Console.Write("\n\t\t\t\t  ");

                for (int colonne = 0; colonne < TailleGrille; colonne++)
                {
                    bool estColore = false;

                    // Chercher si cette position fait partie d'un mot trouvé
                    foreach (var motPlace in MotsPlaces)
                    {
                        // Si on veut tout colorier OU si le mot a été trouvé
                        if (toutColorer || MotsTrouves.Contains(motPlace.Mot.ToLower()))
                        {
                            foreach (var pos in motPlace.Positions)
                            {
                                if (pos.Ligne == ligne && pos.Colonne == colonne)
                                {
                                    AfficherCouleur("  " + Grille[ligne, colonne], motPlace.Couleur);
                                    estColore = true;
                                    break;
                                }
                            }
                        }

                        if (estColore) break;
                    }

                    // Afficher sans couleur si pas trouvé
                    if (!estColore)
                    {
                        Console.Write("  " + Grille[ligne, colonne]);
                    }
                }
            }
        }

        /// <summary>
        /// Affiche l'écran complet du jeu
        /// </summary>
        public static void AfficherEcranJeu(bool messageSucces = false)
        {
            Console.Clear();
            AfficherEntete();
            AfficherStatistiques();
            AfficherGrille(false, messageSucces);
        }

        /// <summary>
        /// Affiche la grille complète avec tous les mots colorés (Easter egg)
        /// </summary>
        public static void AfficherSolution()
        {
            Console.Clear();
            AfficherEntete();
            AfficherCouleur("\n\t\t\t\t\t    🎯 MODE TRICHE ACTIVÉ 🎯\n", 14);
            AfficherCouleur("\t\t\t\t\t   Tous les mots sont révélés !\n", 14);
            AfficherGrille(true, false);
        }

        /// <summary>
        /// Affiche l'écran de victoire
        /// </summary>
        public static void AfficherVictoire()
        {
            Console.WriteLine();
            AfficherCouleur("========================================================================================================================", CouleurHorizontale);
            AfficherCouleur("|| ", CouleurVerticale);
            AfficherCouleur($"\t\t\t\t\tFELICITATION {Username.ToUpper()} VOUS AVEZ GAGNÉ\t\t\t\t\t", 2);
            AfficherCouleur("      ||\n", CouleurVerticale);
            AfficherCouleur("========================================================================================================================", CouleurHorizontale);
        }

        /// <summary>
        /// Demande le nom de l'utilisateur avec validation
        /// </summary>
        public static void DemanderNom()
        {
            AfficherEntete();
            AfficherCouleur("||", CouleurVerticale);
            AfficherCouleur("  \t\t\t\t\tQuel est votre nom svp : ", 15);

            Username = Console.ReadLine()?.Trim() ?? "";

            while (string.IsNullOrWhiteSpace(Username) || int.TryParse(Username, out _))
            {
                Console.Write("\t\t !!!!!Veuillez saisir un nom valide svp : ");
                Username = Console.ReadLine()?.Trim() ?? "";
            }
        }

        /// <summary>
        /// Affiche un message d'erreur
        /// </summary>
        public static void AfficherErreur(string message)
        {
            AfficherCouleur($"\t\t\t    {message}", 4);
        }

        /// <summary>
        /// Demande un mot avec validation
        /// </summary>
        public static string DemanderMot(string invite)
        {
            Console.Write(invite);
            string mot = Console.ReadLine()?.Trim() ?? "";

            while (string.IsNullOrWhiteSpace(mot) || int.TryParse(mot, out _))
            {
                AfficherCouleur("\t\t\t\t !!!!!Veuillez saisir un mot svp : ", 4);
                mot = Console.ReadLine()?.Trim() ?? "";
            }

            return mot.ToLower();
        }
    }
}