# Rapport de Projet Agile - Équipe Z

## Sujet du Projet : Indicateurs de Santé

## Organisation et Méthodologie

### Méthodologie Adoptée : Scrum
- **Durée du projet** : 3 jours.
- **Sprints** : 6 sprints d'une demi-journée chacun.
- **Événements Scrum par sprint** :
    - **Sprint Planning** : Planification des tâches du sprint à venir.
    - **Daily Scrum** : Réunion rapide pour suivre l’avancement et résoudre les blocages.
    - **Sprint Review** : Démonstration des fonctionnalités développées et feedback des parties prenantes.
    - **Sprint Retrospective** : Analyse des points d'amélioration pour le prochain sprint.

### Cadre Agile
- **Board sur GitHub** :
    - Colonnes : *Identified*, *In Progress*, *Done*.
    - Chaque ticket est accompagné des critères d'acceptation, tests associés, et définition de fait (*Definition of Done*).
- **Backlog Priorisé** : Basé sur les besoins fonctionnels critiques du projet.
- **Définition de Fait (DoD)** :
    - Fonctionnalité testée et validée.
    - Code conforme aux standards qualité (linting, conventions, revues).
    - Documentation technique mise à jour.

---

## Livrables
- **Backlog Produit** :  
  Une liste priorisée des User Stories en fonction des besoins des utilisateurs.
- **Code Source** :  
  Répartition du code entre les fonctionnalités front-end et back-end. Tests unitaires intégrés.
- **Documentation** :
    - Documentation technique (API, structure des données).
    - Documentation fonctionnelle (scénarios de test).
- **Release Note** : Résumé des fonctionnalités livrées pour chaque sprint.

---

## Historique des Sprints
### Sprint 1
**Objectif** : Initialisation du projet.
- **Tâches réalisées** :
    - Choix des technologies (React.js pour le front, .NET Core pour l'API).
    - Initialisation du projet sur GitHub.
    - Mise en place des workflows CI/CD avec SonarCloud pour la qualité du code.
    - Création des tickets initialisés sur le board GitHub.
- **Problèmes rencontrés** :  
  Aucun problème majeur.
- **Résultat** : Une démo réussie montrant les premières étapes du projet.

### Sprint 2
**Objectif** : Authentification utilisateur et gestion des unités.
- **Tâches réalisées** :
    - Mise en place du système de création d’utilisateur.
    - Gestion des unités (kg/lb, km/miles) avec conversion.
    - Développement de la base de l'interface utilisateur.
- **Problèmes rencontrés** :
    - Manque de descriptions détaillées dans les tickets.
    - Couleurs de l’interface contestées par le client.
- **Solutions proposées** :
    - Documentation claire des tickets avec critères d'acceptation.
    - Consultation du client pour choisir une palette de couleurs commune.

### Sprint 3
**Objectif** : Finalisation des données utilisateur et visualisation des indicateurs de santé.
- **Tâches réalisées** :
    - Visualisation des données de la journée en cours.
    - Développement des graphes pour visualiser les données des indicateurs (Front-back).
    - Tests unitaires pour les fonctionnalités critiques.
- **Problèmes rencontrés** :
    - Retards dus à des conflits lors des fusions de branches.
    - Trop de tickets en cours, ralentissant le flux de travail.
- **Solutions proposées** :
    - Fusion fréquente des branches pour éviter les conflits.
    - Limiter le nombre de tâches en cours par développeur.

### Sprint 4 et suivants
**Objectif** : Sécurisation des données et optimisation finale.
- **Tâches réalisées** :
    - Sécurisation de l’API avec JWT pour l’authentification.
    - Finalisation de l’interface utilisateur (design responsive).
    - Ajout des tests end-to-end pour garantir la fiabilité du service.
---

## Rétrospectives
### Points positifs :
- Réalisation des fonctionnalités critiques.
- Équipe adaptable et collaborative.
- Mise en place efficace des outils de CI/CD.
- l'ésprit de travaillée en méthodologie Agile avec Scrum

### Points à améliorer :
- Anticiper les problèmes liés à la fusion de branches.
- Limiter les tâches assignées par sprint pour éviter les retards.
- Organiser des sessions de brainstorming plus régulières pour résoudre les désaccords rapidement.

---

## Conclusion et Résultats
-- A venir
