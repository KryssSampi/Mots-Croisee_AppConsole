using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace MotsCroises
{

    public static class WordLoader
    {
        private static readonly Random random = new Random();

        /// <summary>
        /// Charge un fichier texte et retourne une liste aléatoire de mots (30 ou plus).
        /// </summary>
        /// <param name="filePath">Chemin complet du fichier .txt</param>
        /// <param name="count">Nombre de mots à retourner (par défaut 30)</param>
        /// <returns>Liste aléatoire de mots uniques</returns>
        public static List<string> GetRandomWords(string? filePath = null, int count = 30)
        {
            // Si aucun chemin n’est fourni → on charge automatiquement "WordBank.txt" dans le dossier de l’exécutable
            if (string.IsNullOrEmpty(filePath))
            {
                string rootPath = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)?
                                      .Parent?.Parent?.Parent?.FullName
                                      ?? AppDomain.CurrentDomain.BaseDirectory;

                filePath = Path.Combine(rootPath, "WordBank.txt");
            }

            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Fichier introuvable : {filePath}");

            // Lecture du fichier complet
            string text = File.ReadAllText(filePath);

            // Extraction et nettoyage
            var allWords = text
                .Split(new[] { ' ', '\n', '\r', '\t', ',', ';', '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(w => w.Trim().ToLowerInvariant())
                .Where(w => w.Length > 1) // on ignore les mots d'une seule lettre
                .Distinct()
                .ToList();

            if (allWords.Count == 0)
                throw new InvalidOperationException("Aucun mot valide trouvé dans le fichier.");

            // Mélange aléatoire (Fisher-Yates)
            for (int i = allWords.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                (allWords[i], allWords[j]) = (allWords[j], allWords[i]);
            }

            // Si le fichier contient moins de mots que demandé
            int takeCount = Math.Min(count, allWords.Count);

            return allWords.Take(takeCount).ToList();
        }
    }
}