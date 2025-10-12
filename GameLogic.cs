using System;
using static MotsCroises.GameData;

namespace MotsCroises
{
    /// <summary>
    /// Gère la logique du jeu et les interactions
    /// </summary>
    public static class GameLogic
    {
        private const string CODE_TRICHE = "SNKR007";

        /// <summary>
        /// Traite la réponse du joueur
        /// </summary>
        public static ResultatReponse TraiterReponse(string reponse)
        {
            reponse = reponse.Trim().ToLower();

            // Easter egg - Code de triche
            if (reponse.ToUpper() == CODE_TRICHE)
            {
                return new ResultatReponse
                {
                    TypeResultat = TypeResultat.Triche,
                    Message = "Code de triche activé !"
                };
            }

            // Vérifier si le mot existe dans la liste
            if (!EstMotValide(reponse))
            {
                return new ResultatReponse
                {
                    TypeResultat = TypeResultat.MotInvalide,
                    Message = "Désolé mais le mot saisi n'est pas dans la grille"
                };
            }

            // Vérifier si le mot a déjà été trouvé
            if (MotsTrouves.Contains(reponse))
            {
                return new ResultatReponse
                {
                    TypeResultat = TypeResultat.DejaTrouve,
                    Message = "Vous avez déjà trouvé ce mot !"
                };
            }

            // Mot correct et nouveau
            AjouterMotTrouve(reponse);
            return new ResultatReponse
            {
                TypeResultat = TypeResultat.Succes,
                Message = "Bien joué !"
            };
        }

        /// <summary>
        /// Boucle principale du jeu
        /// </summary>
        public static void JouerPartie()
        {
            // Premier mot
            string mot = Display.DemanderMot("\n\n\t\t\t     Veuillez entrer le mot que vous avez trouvé : ");
            TraiterEtAfficher(mot);

            // Boucle jusqu'à trouver tous les mots
            while (!TousMotsTrouves)
            {
                mot = Display.DemanderMot("\n\n\t\t\t      Veuillez entrer le nouveau mot trouvé : ");
                TraiterEtAfficher(mot);
            }
        }

        /// <summary>
        /// Traite la réponse et affiche le résultat approprié
        /// </summary>
        private static void TraiterEtAfficher(string mot)
        {
            var resultat = TraiterReponse(mot);

            switch (resultat.TypeResultat)
            {
                case TypeResultat.Triche:
                    Display.AfficherSolution();
                    // Marquer tous les mots comme trouvés pour terminer le jeu
                    foreach (var m in MotsDisponibles)
                    {
                        AjouterMotTrouve(m);
                    }
                    break;

                case TypeResultat.Succes:
                    Display.AfficherEcranJeu(messageSucces: true);
                    break;

                case TypeResultat.MotInvalide:
                    Display.AfficherErreur(resultat.Message);
                    DemanderNouvelleReponse();
                    break;

                case TypeResultat.DejaTrouve:
                    Display.AfficherErreur(resultat.Message);
                    break;
            }
        }

        /// <summary>
        /// Demande une nouvelle réponse jusqu'à obtenir un mot valide
        /// </summary>
        private static void DemanderNouvelleReponse()
        {
            while (true)
            {
                string nouveauMot = Display.DemanderMot("\n\t\t\t\tVeuillez entrer une autre proposition svp : ");
                var resultat = TraiterReponse(nouveauMot);

                if (resultat.TypeResultat == TypeResultat.Succes)
                {
                    Display.AfficherEcranJeu(messageSucces: true);
                    break;
                }
                else if (resultat.TypeResultat == TypeResultat.Triche)
                {
                    Display.AfficherSolution();
                    foreach (var m in MotsDisponibles)
                    {
                        AjouterMotTrouve(m);
                    }
                    break;
                }
                else if (resultat.TypeResultat == TypeResultat.DejaTrouve)
                {
                    Display.AfficherErreur(resultat.Message);
                }
                else
                {
                    Display.AfficherErreur(resultat.Message);
                }
            }
        }
    }

    /// <summary>
    /// Type de résultat pour une réponse
    /// </summary>
    public enum TypeResultat
    {
        Succes,
        MotInvalide,
        DejaTrouve,
        Triche
    }

    /// <summary>
    /// Résultat du traitement d'une réponse
    /// </summary>
    public class ResultatReponse
    {
        public TypeResultat TypeResultat { get; set; }
        public string Message { get; set; }
    }
}