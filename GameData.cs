using System;
using System.Collections.Generic;

namespace MotsCroises
{
    /// <summary>
    /// Centralise toutes les données du jeu de mots croisés
    /// </summary>
    public static class GameData
    {
        public static Random Rand = new Random();

        // Configuration
        public static string Username = "";
        public static int TailleGrille;
        public static int CouleurHorizontale;
        public static int CouleurVerticale;

        // Grille de jeu
        public static string[,] Grille;

        // Mots et progression
        public static List<string> MotsDisponibles = new List<string>
        {
            "jouer", "lire", "maman", "magique", "symphonie", "pasteur", "chemise",
            "programme", "linux", "renard", "bonjour", "maison", "voiture", "arbre",
            "fleur", "paysage", "mer", "soleil", "nuage", "oiseau", "chat", "chien",
            "livre", "ecole", "professeur", "etudiant", "ordinateur", "telephone",
            "musique", "amour"
        };

        public static List<MotPlace> MotsPlaces = new List<MotPlace>();
        public static HashSet<string> MotsTrouves = new HashSet<string>();

        /// <summary>
        /// Les 8 directions possibles pour placer un mot
        /// </summary>
        public enum Direction
        {
            Horizontal,        // →
            Vertical,          // ↓
            DiagonaleBasDroite,// ↘
            DiagonaleBasGauche,// ↙
            DiagonaleHautGauche,// ↖
            AntiDiagonaleHautDroite,// ↗ (inversé)
            VerticalInverse,   // ↑
            HorizontalInverse  // ← (non utilisé dans l'original mais possible)
        }

        /// <summary>
        /// Représente un mot placé dans la grille
        /// </summary>
        public class MotPlace
        {
            public string Mot { get; set; }
            public int LigneDepart { get; set; }
            public int ColonneDepart { get; set; }
            public Direction Direction { get; set; }
            public int Couleur { get; set; }
            public List<Position> Positions { get; set; }

            public MotPlace()
            {
                Positions = new List<Position>();
            }
        }

        /// <summary>
        /// Position dans la grille
        /// </summary>
        public struct Position
        {
            public int Ligne;
            public int Colonne;

            public Position(int ligne, int colonne)
            {
                Ligne = ligne;
                Colonne = colonne;
            }

            public bool Equals(Position other)
            {
                return Ligne == other.Ligne && Colonne == other.Colonne;
            }
        }

        /// <summary>
        /// Initialise une nouvelle partie
        /// </summary>
        public static void Initialiser()
        {
            // Taille aléatoire de la grille
            TailleGrille = Rand.Next(10, 20);

            // Couleurs aléatoires
            CouleurHorizontale = Rand.Next(1, 15);
            CouleurVerticale = Rand.Next(1, 15);

            // Sélectionner les mots selon la taille
            MotsDisponibles = new List<string>(MotsDisponibles); // Copie pour ne pas modifier l'original

            int nombreMotsARetirer = TailleGrille < 15 ? 20 : 15;
            RetirerMotsAleatoires(nombreMotsARetirer);

            // Créer la grille
            Grille = new string[TailleGrille, TailleGrille];

            // Réinitialiser les collections
            MotsPlaces.Clear();
            MotsTrouves.Clear();
        }

        /// <summary>
        /// Retire un nombre spécifié de mots aléatoirement
        /// </summary>
        private static void RetirerMotsAleatoires(int nombre)
        {
            // Mélanger la liste et garder seulement les premiers éléments
            var copie = new List<string>(MotsDisponibles);
            MotsDisponibles.Clear();

            // Mélange Fisher-Yates
            for (int i = copie.Count - 1; i > 0; i--)
            {
                int j = Rand.Next(i + 1);
                var temp = copie[i];
                copie[i] = copie[j];
                copie[j] = temp;
            }

            // Garder seulement les mots nécessaires
            int nombreAGarder = Math.Min(copie.Count - nombre, copie.Count);
            for (int i = 0; i < nombreAGarder && i < copie.Count; i++)
            {
                MotsDisponibles.Add(copie[i]);
            }
        }

        /// <summary>
        /// Obtient le delta de position selon la direction
        /// </summary>
        public static (int deltaLigne, int deltaColonne) ObtenirDelta(Direction direction)
        {
            return direction switch
            {
                Direction.Horizontal => (0, 1),
                Direction.Vertical => (1, 0),
                Direction.DiagonaleBasDroite => (1, 1),
                Direction.DiagonaleBasGauche => (1, -1),
                Direction.DiagonaleHautGauche => (-1, -1),
                Direction.AntiDiagonaleHautDroite => (1, -1),
                Direction.VerticalInverse => (-1, 0),
                Direction.HorizontalInverse => (0, -1),
                _ => (0, 1)
            };
        }

        /// <summary>
        /// Vérifie si le mot est dans la liste des mots à trouver
        /// </summary>
        public static bool EstMotValide(string mot)
        {
            return MotsDisponibles.Contains(mot.ToLower());
        }

        /// <summary>
        /// Ajoute un mot trouvé (évite les doublons)
        /// </summary>
        public static bool AjouterMotTrouve(string mot)
        {
            mot = mot.ToLower();
            if (MotsTrouves.Contains(mot))
                return false;

            MotsTrouves.Add(mot);
            return true;
        }

        /// <summary>
        /// Retourne le nombre total de mots à trouver
        /// </summary>
        public static int NombreMotsTotal => MotsDisponibles.Count;

        /// <summary>
        /// Retourne le nombre de mots trouvés
        /// </summary>
        public static int NombreMotsTrouves => MotsTrouves.Count;

        /// <summary>
        /// Vérifie si tous les mots ont été trouvés
        /// </summary>
        public static bool TousMotsTrouves => MotsTrouves.Count >= MotsDisponibles.Count;
    }
}