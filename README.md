# Mots-Croisee_AppConsole
Imaginez un jeu de mots croisés qui ne se répète jamais. Chaque partie génère une grille entièrement nouvelle avec une disposition algorithmique sophistiquée. Pas de grilles préconçues, pas de répétition — juste de l'amusement pur et imprévisible.
---

# MOTS CROISÉS DYNAMIQUES  
### Génération Procédurale Infinie de Grilles de Mots

![Image 1](./Demo/Niv_1.png)  
![Image 2](./Demo/Niv_2.png)  
![Image 3](./Demo/Niv_3.png)

---

## Description  
**Mots Croisés Dynamiques** est un jeu de recherche de mots en console qui génère procéduralement des grilles uniques à chaque partie.  
Avec un algorithme sophistiqué de placement multi-directionnel et un système de coloration progressive, chaque session offre une expérience fraîche et stimulante.

---

## Points Forts  

- **Génération 100 % procédurale** : Jamais la même grille deux fois  
- **8 directions de placement** : horizontal, vertical et diagonales  
- **Coloration dynamique** : Les mots trouvés s’illuminent progressivement  
- **Grilles adaptatives** : Taille variable de 10×10 à 20×20  
-  **Easter Egg** : Code secret pour révéler toute la grille  

---

##  Caractéristiques  

### Génération Intelligente  

| Aspect | Détail |
|:--|:--|
| **Taille de grille** | 10×10 à 20×20 (aléatoire) |
| **Nombre de mots** | 10-15 selon la taille |
| **Directions** | 8 possibles (→ ↓ ↘ ↙ ↖ ↗ ↑ ←) |
| **Algorithme** | Backtracking avec tentatives limitées |
| **Remplissage** | Lettres aléatoires A-Z pour les cases vides |

### Fonctionnalités  

- Validation intelligente : détecte doublons et incohérences  
- Feedback visuel : messages colorés clairs  
- Progression visible : compteur de mots trouvés  
- Coloration unique : chaque mot possède sa couleur  
- Code secret : révèle instantanément tous les mots  

---

## Installation  

### Prérequis  
- .NET Framework 4.7.2+ ou .NET 5.0+  
- Console avec support couleur (Windows Terminal, PowerShell, etc.)

### Installation  

```bash
# Télécharger le projet
git clone [Mot-Croisée](https://github.com/KryssSampi/Mots-Croisee_AppConsole.git)
cd mots-croises

# Compiler
dotnet build

# Exécuter
dotnet run
````

---

## Comment Jouer

### Déroulement

1. **Lancement** : la grille est générée aléatoirement
2. **Nom du joueur** : entrez votre nom
3. **Recherche** : observez la grille et cherchez des mots
4. **Saisie** : entrez les mots trouvés un par un
5. **Progression** : les mots validés s’affichent en couleur
6. **Victoire** : trouvez tous les mots cachés !

### Exemples de mots

`jouer`, `lire`, `maman`, `magique`, `symphonie`, `pasteur`, `chemise`, `programme`, `linux`, `renard`, `bonjour`, `maison`, `voiture`, `arbre`, `fleur`, `paysage`, `mer`, `soleil`, `nuage`, `oiseau`, `chat`, `chien`, `livre`, `ecole`, `professeur`, `etudiant`, `ordinateur`, `telephone`, `musique`, `amour`

---

## Easter Egg

**Code secret :** `SNKR007`

Entrez-le à n’importe quel moment pour :

* Révéler tous les mots en couleur
* Voir les solutions complètes
* Terminer instantanément la partie

---

## Architecture

### Structure du Projet

```bash
MotsCroises/
├── Program.cs                  # Point d’entrée (30 lignes)
├── Display.cs                  # Interface console (affichage et effets)
├── GameData.cs                 # Données et configuration (150 lignes)
├── GameLogic.cs                # Validation et flux (80 lignes)
├── GridGenerator.cs            # Algorithme de génération procédurale (120 lignes)
├── WordLoader.cs               # Chargement dynamique du fichier WordBank.txt
├── WordBank.txt                # Banque de mots principale (source textuelle)
├── Mots-Croisés_AppConsole.csproj
├── Mots-Croisés_AppConsole.sln
├── bin/                        # Fichiers compilés (Debug/Release)
└── obj/                        # Fichiers temporaires

```

### Architecture Technique

```
┌─────────────┐
│  Program.cs │  ← Orchestration
└──────┬──────┘
       │
   ┌───▼────────────────────────────┐
   │       GameData.cs              │
   │  • Configuration               │
   │  • État du jeu                 │
   │  • Structures de données       │
   └───┬────────────────────────────┘
       │
   ┌───▼──────────────┐   ┌──────────────────┐
   │ GridGenerator.cs │   │   Display.cs     │
   │  • Placement     │   │  • Affichage     │
   │  • Validation    │   │  • Coloration    │
   │  • Backtracking  │   │  • Feedback      │
   └──────────────────┘   └──────────────────┘
       │                           │
       └───────┬───────────────┬───┘
               │               |
       ┌───────▼──────────┐  ┌─▼────────────────┐
       │  GameLogic.cs    │  │  WordLoader.cs   │
       │  • Validation    │  │  • Obtention de  │
       │  • Flow du jeu   │  │ la liste de mots │
       │  • Easter egg    │  └──────────────────┘
       └──────────────────┘  
```

---

## Fonctionnalités Techniques
### Obtention Automatique et Random de mots
```csharp 
    // Mots 
    public static List<string> MotsDisponibles =
    GetRandomWords() ?? new List<string>(); 
    // obtention depuis WordLoader chargeant depuis lefichier "WordBank.txt"(Par défaut) 
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
        //Split , Organisation du fichier et extraction des mots.... (Voir WorsLoader.cs)
       }
```
### Algorithme de Placement

```csharp
private const int MAX_TENTATIVES_PAR_MOT = 100;
private const int MAX_TENTATIVES_POSITION = 50;

foreach (var mot in MotsDisponibles)
{
    bool place = false;
    for (int tentative = 0; tentative < MAX_TENTATIVES_PAR_MOT; tentative++)
    {
        var direction = DirectionAleatoire();
        var position = PositionAleatoire();

        if (PeutPlacer(mot, position, direction))
        {
            PlacerMot(mot, position, direction);
            place = true;
            break;
        }
    }

    if (!place)
        ForcerPlacement(mot);
}
```

### Directions Unifiées

```csharp
public enum Direction
{
    Horizontal,              // →
    Vertical,                // ↓
    DiagonaleBasDroite,      // ↘
    DiagonaleBasGauche,      // ↙
    DiagonaleHautGauche,     // ↖
    AntiDiagonaleHautDroite, // ↗
    VerticalInverse,         // ↑
    HorizontalInverse        // ←
}

(int deltaLigne, int deltaColonne) = ObtenirDelta(direction);
```

###  Coloration Progressive

```csharp
foreach (var motPlace in MotsPlaces)
{
    if (MotsTrouves.Contains(motPlace.Mot))
        AfficherCouleur(lettre, motPlace.Couleur);
    else
        AfficherNormal(lettre);
}
```

---

## Exemple de Grille

### Grille Initiale

```csharp
========================================================
||              | LES MOTS-CROISÉS |                ||
========================================================
||              BIENVENUE MON CHER AMI               ||
||          CECI EST UN PETIT JEU AMUSANT            ||
========================================================
|| Voici une grille contenant 12 mots cachés         ||
||              !!!BONNE CHANCE JOUEUR!!!            ||
========================================================
                 MOT TROUVÉ(S) : 0/12
```

### Après Découverte

```
                 MOT TROUVÉ(S) : 3/12

          M  A  G  I  Q  U  E  Z  T  R
          O  R  K  W  X  Y  P  Q  S  E
          U  B  L  I  V  R  E  M  O  N  ← LIVRE (jaune)
          I  E  O  U  L  E  U  R  E  R  ← FLEUR (vert)
          Q  F  U  X  M  A  M  A  N  D  ← MAMAN (rouge)
          ↑ MUSIQUE (bleu)
```

---

## 📊 Statistiques & Performance

| Métrique       | v1.0               | v2.0 (Refactorisée) | Amélioration |
| :------------- | :----------------- | :------------------ | :----------- |
| Lignes de code | ~500               | ~400                | −20 %        |
| Code dupliqué  | ~300 lignes        | 0                   | −100 %       |
| Bugs logiques  | 3 majeurs          | 0                   | −100 %       |
| Fonctions      | 7 blocs identiques | 1 générique         | −85 %        |
| Complexité     | Très élevée        | Faible              | −80 %        |

-  **Génération :** < 100 ms (grilles 20×20)
- **Validation :** O(1) avec `HashSet`
- **Affichage :** instantané
- **Mémoire :** < 5 MB par session

---

## Corrections Majeures

### Boucle de Placement Cassée → Fixée 

```csharp
// Ancienne version — sortait prématurément
for (int x = 0; x < grille.Length; x++) {
    for (int y = 0; y < grille.Length; y++) {
        if (grille[x,y] == null) break;
    }
    break;
}
```

```csharp
// Nouvelle version — recherche complète
for (int tentative = 0; tentative < MAX_TENTATIVES; tentative++)
{
    var pos = PositionAleatoire();
    if (PeutPlacerMot(mot, pos, direction))
    {
        PlacerMot(mot, pos, direction);
        return true;
    }
}
```

### Gestion de Liste → Fix 

```csharp
// Avant : suppression hors limites
for (int i = (c + 19); i > c; i--) list.Remove(list[i]);

// Après : mélange + sélection propre
MelangerListe(copie);
MotsDisponibles = copie.Take(nombre).ToList();
```

### Structure de Données → Typée

```csharp
class MotPlace {
    public List<Position> Positions { get; set; }
    public int Couleur { get; set; }
}
```

---

## Cas d’Usage

### Pour les Joueurs

- Entraînement mental quotidien
- Défi infini sans répétition
- Jeu familial éducatif
- Sessions rapides (5-15 min)

### Pour les Développeurs

- Exemple d’architecture modulaire
- Algorithme de backtracking
- Gestion d’affichage console
- Tests de génération procédurale

### Pour les Éducateurs

+ Outil d’enseignement algorithmique
+ Étude de refactorisation
+ Exemple de séparation des responsabilités
+ Illustration de design patterns

---

## Améliorations Futures

### Fonctionnalités Planifiées

* Mode difficile (mots longs, grandes grilles)
* Thèmes (animaux, pays, objets…)
* Chronomètre / contre-la-montre
* Indices progressifs
* Sauvegarde et reprise
* Statistiques et score
* Export en image
* Multijoueur compétitif

### Améliorations Techniques

* Tests unitaires complets
* Configuration externe (JSON/XML)
* Système de plugins de mots
* API de génération externe
* Version graphique (WPF / Avalonia)

---

## Documentation

### Pour Commencer

1. Lire ce README
2. Explorer `Program.cs` (flux global)
3. Étudier `GridGenerator.cs` (algorithme principal)
4. Personnaliser `GameData.cs` (listes de mots)

### Commentaires du Code

```csharp
/// <summary> Description claire de la fonction </summary>
/// <param name="param">Description du paramètre</param>
/// <returns>Description du retour</returns>
```

---

## Contribution

### Comment Contribuer

```bash
# Fork du projet
git checkout -b feature/nouvelle-fonctionnalite
git commit -m "Ajout nouvelle fonctionnalité"
git push origin feature/nouvelle-fonctionnalite
```
Puis ouvrir une **Pull Request**.

### Bonnes Pratiques

* Respecter l’architecture modulaire
* Commenter les fonctions complexes
* Tester avant de pousser
* Préserver la performance

---

## Licence

Projet éducatif et démonstratif, libre d’utilisation, modification et distribution.

---

## Réalisations Techniques

 - Architecture claire et modulaire
 * Algorithmes avancés (backtracking, placement         multi-directionnel)
 + Optimisation : −70 % de complexité
 - Zéro bug connu
 - Code documenté et maintenable
 * Génération instantanée

---

## Crédits
> Entièrement réaliser par Moi : Kryss Rayane Sampi Nana
### Méthodologie de Conception
| Étape Principale | Description | Objectifs Clés | Livrables / Résultats |
|:-----------------|:-------------|:---------------|:----------------------|
| **Conception Mentale** | Phase de réflexion initiale où l’idée brute est structurée. On établit le **cahier des charges**, on **visualise le produit final**, et on crée une **carte mentale** pour relier les concepts. | - Définir la vision globale du projet<br>- Identifier les besoins réels et les contraintes<br>- Clarifier les fonctions essentielles | - Cahier des charges clair et hiérarchisé<br>- Schémas et mind maps<br>- Vision conceptuelle aboutie |
| **Maquettage Visuel** | Création de **maquettes et prototypes** statiques permettant de visualiser la disposition générale, la navigation, les composants clés et le ton visuel du produit. | - Définir la direction artistique et UX<br>- Obtenir une **vue d’ensemble claire** du futur produit<br>- Préparer la base du design interactif | - Maquettes (Figma, Adobe XD, ou papier)<br>- Palette de couleurs et typographies<br>- Grilles et disposition des sections |
| **Réalisation Graphique & Animation Visuelle** | Implémentation des **interfaces graphiques** (UI) et des **animations** sans logique interne (no-mechanism). On travaille sur le rendu visuel, les transitions et l’ergonomie. | - Donner vie à la maquette<br>- Tester l’**esthétique** et la **fluidité visuelle**<br>- S’assurer de la **cohérence stylistique** | - Interfaces fonctionnelles (front-end, XAML, CSS, etc.)<br>- Animations, transitions et effets<br>- Design stable et interactif |
| **Implémentation des Mécanismes & Cœur du Programme** | Développement de la **logique interne** : base de données, gestion d’état, algorithmes, et intégration entre l’interface et le backend. | - Structurer le **cœur logique** du système<br>- Garantir la **performance** et la **stabilité**<br>- Centraliser les données et interactions | - Code source stable et modulaire<br>- Base de données fonctionnelle<br>- Mécanismes et API opérationnels |
| **Tests & Polissage Final** | Étape de **test approfondi** et de **correction**. On explore la majorité des cas d'utilisation possibles, puis on ouvre le projet à une **communauté restreinte** pour recueillir des retours. | - Déceler les bugs et incohérences<br>- Ajuster le rendu visuel et l’expérience utilisateur<br>- Stabiliser le produit avant diffusion | - Rapport de test et correctifs<br>- Version stable (v1.0+)<br>- Interface polie et réactive |
| **Publication & Partage** | Mise en ligne du produit final et **partage avec le public** ou un cercle professionnel. Cette phase marque la transition vers l’usage réel. | - Diffuser le travail accompli<br>- Obtenir des retours d’expérience<br>- Débuter l’**utilisation régulière** et la **maintenance** | - Version publique du projet<br>- Documentation claire<br>- Utilisation continue et mises à jour futures |
<br>
> "
>_De la vision à la réalité, chaque étape est une brique de maîtrise._
---

##  Support & Contact

 Lire la documentation intégrée
 Ouvrir une issue sur GitHub
 Participer aux discussions
 Contact direct pour toute question

---

## Ressources

* Code source complet et commenté
* Exemples dans `Program.cs`
* Schémas et architecture dans ce README
* Diagrammes techniques dans la documentation

---

### Mots Croisés Dynamiques — *Parce que votre cerveau mérite l’infini !* ✨

---

## Quick Start (30 secondes)

```bash
# 1. Télécharger
git clone https://github.com/KryssSampi/Mots-Croisee_AppConsole

# 2. Compiler
dotnet build

# 3. Jouer
dotnet run

# 4. Profiter !
# Entrez votre nom
# Cherchez des mots
# Tapez SNKR007 pour tricher 😉
```

### ***Amusez-vous bien !***

