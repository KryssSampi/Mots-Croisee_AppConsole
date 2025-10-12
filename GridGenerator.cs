using System;
using System.Collections.Generic;
using static MotsCroises.GameData;

namespace MotsCroises
{
    /// <summary>
    /// Génère la grille de mots croisés avec algorithme robuste
    /// </summary>
    public static class GridGenerator
    {
        private const int MAX_TENTATIVES_PAR_MOT = 100;
        private const int MAX_TENTATIVES_POSITION = 50;

        /// <summary>
        /// Génère la grille complète avec tous les mots
        /// </summary>
        public static void GenererGrille()
        {
            // Initialiser la grille vide
            for (int i = 0; i < TailleGrille; i++)
            {
                for (int j = 0; j < TailleGrille; j++)
                {
                    Grille[i, j] = null;
                }
            }

            // Placer chaque mot
            foreach (var mot in MotsDisponibles)
            {
                PlacerMot(mot.ToUpper());
            }

            // Remplir les cases vides avec des lettres aléatoires
            RemplirCasesVides();
        }

        /// <summary>
        /// Place un mot dans la grille avec gestion intelligente des tentatives
        /// </summary>
        private static void PlacerMot(string mot)
        {
            bool place = false;
            int tentatives = 0;

            while (!place && tentatives < MAX_TENTATIVES_PAR_MOT)
            {
                tentatives++;

                // Choisir une direction aléatoire parmi les 7 possibles (comme l'original)
                Direction direction = (Direction)Rand.Next(0, 7);

                // Trouver une position valide
                if (TrouverEtPlacerPosition(mot, direction))
                {
                    place = true;
                }
            }

            // Si échec après toutes les tentatives, forcer un placement simple
            if (!place)
            {
                ForcerPlacement(mot);
            }
        }

        /// <summary>
        /// Trouve et place le mot dans une direction donnée
        /// </summary>
        private static bool TrouverEtPlacerPosition(string mot, Direction direction)
        {
            for (int tentative = 0; tentative < MAX_TENTATIVES_POSITION; tentative++)
            {
                // Générer une position de départ aléatoire
                int ligne = Rand.Next(0, TailleGrille);
                int colonne = Rand.Next(0, TailleGrille);

                // Vérifier si le mot peut être placé à cette position
                if (PeutPlacerMot(mot, ligne, colonne, direction))
                {
                    PlacerMotDansGrille(mot, ligne, colonne, direction);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Vérifie si un mot peut être placé à une position donnée
        /// </summary>
        private static bool PeutPlacerMot(string mot, int ligneDep, int colDep, Direction direction)
        {
            var (deltaL, deltaC) = ObtenirDelta(direction);

            // Vérifier chaque lettre
            for (int i = 0; i < mot.Length; i++)
            {
                int ligne = ligneDep + (i * deltaL);
                int colonne = colDep + (i * deltaC);

                // Hors limites
                if (ligne < 0 || ligne >= TailleGrille || colonne < 0 || colonne >= TailleGrille)
                {
                    return false;
                }

                // Case occupée par une lettre différente
                if (Grille[ligne, colonne] != null && Grille[ligne, colonne] != mot[i].ToString())
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Place effectivement le mot dans la grille et enregistre les positions
        /// </summary>
        private static void PlacerMotDansGrille(string mot, int ligneDep, int colDep, Direction direction)
        {
            var (deltaL, deltaC) = ObtenirDelta(direction);

            MotPlace motPlace = new MotPlace
            {
                Mot = mot,
                LigneDepart = ligneDep,
                ColonneDepart = colDep,
                Direction = direction,
                Couleur = Rand.Next(1, 7)
            };

            // Placer chaque lettre
            for (int i = 0; i < mot.Length; i++)
            {
                int ligne = ligneDep + (i * deltaL);
                int colonne = colDep + (i * deltaC);

                Grille[ligne, colonne] = mot[i].ToString();
                motPlace.Positions.Add(new Position(ligne, colonne));
            }

            MotsPlaces.Add(motPlace);
        }

        /// <summary>
        /// Force le placement d'un mot en mode simple (horizontal au centre)
        /// </summary>
        private static void ForcerPlacement(string mot)
        {
            // Chercher une ligne avec assez d'espace
            for (int ligne = 0; ligne < TailleGrille; ligne++)
            {
                for (int colonne = 0; colonne <= TailleGrille - mot.Length; colonne++)
                {
                    bool espaceDispo = true;

                    // Vérifier l'espace horizontal
                    for (int i = 0; i < mot.Length; i++)
                    {
                        if (Grille[ligne, colonne + i] != null &&
                            Grille[ligne, colonne + i] != mot[i].ToString())
                        {
                            espaceDispo = false;
                            break;
                        }
                    }

                    if (espaceDispo)
                    {
                        PlacerMotDansGrille(mot, ligne, colonne, Direction.Horizontal);
                        return;
                    }
                }
            }

            // Dernier recours : écraser et placer en première ligne disponible
            int col = Rand.Next(0, Math.Max(1, TailleGrille - mot.Length + 1));
            PlacerMotDansGrille(mot, 0, col, Direction.Horizontal);
        }

        /// <summary>
        /// Remplit les cases vides avec des lettres aléatoires
        /// </summary>
        private static void RemplirCasesVides()
        {
            for (int i = 0; i < TailleGrille; i++)
            {
                for (int j = 0; j < TailleGrille; j++)
                {
                    if (Grille[i, j] == null)
                    {
                        // Lettre aléatoire A-Z
                        char lettre = (char)('A' + Rand.Next(26));
                        Grille[i, j] = lettre.ToString();
                    }
                }
            }
        }

        /// <summary>
        /// Obtient le mot placé contenant une position spécifique
        /// </summary>
        public static MotPlace ObtenirMotAPosition(int ligne, int colonne)
        {
            foreach (var mot in MotsPlaces)
            {
                foreach (var pos in mot.Positions)
                {
                    if (pos.Ligne == ligne && pos.Colonne == colonne)
                    {
                        return mot;
                    }
                }
            }
            return null;
        }
    }
}