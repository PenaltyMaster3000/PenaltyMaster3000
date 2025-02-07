# PenaltyMaster3000 ⚽️

## Bonjour et bienvenue sur le dépôt du projet PenaltyMaster3000 ! 👋

*******

Sommaire 
 1. [Accessibilité](#acces)
 2. [Présentation du projet](#presentation)
 3. [Règles du jeu](#regles)
 3. [Solutions mises en place](#solutions)
 4. [Structure du projet](#structure)
 5. [Auteurs](#auteurs)

*******

<div id='acces'/>   

## :rotating_light: Branches
* La totalité des tps et le projet se trouvent dans la branche `master` (__sauf pour la partie 3 de l'exo 2__)
* La partie 3 de l'exercice 2 (__mapping__) se trouve dans la branche `ex2-part3-mapping-et-factory`
* Les branches crées au long de la période n'ont pas été supprimées afin de mettre a disposition un historique du travail effectuer.

## Accessibilité ↗

> :warning: **Warning**: Le déploiement n'a pas encore été fait. 

Pour lancer le projet **Penalty Master 3000**, vous devrez cloner le répertoire, ouvrir le projet dans Visual Studio et définir la solution PenaltyMaster3000 comme projet de démarrage.  

*******

<div id='presentation'/>

## **Présentation** 🎉

Votre simulateur de séance de penalty en réalité virtuelle !  
  
**PenaltyMaster3000** est un simulateur de séance de penalty multijoueur conçu pour la réalité virtuelle. Le jeu utilise la technologie **Kinect** pour suivre les mouvements des joueurs et les intégrer dans l'expérience de réalité virtuelle. Développé en **C#**, ce projet offre une immersion réaliste et interactive, permettant aux utilisateurs de vivre l'excitation d'une séance de penalty dans le confort de leur propre espace. En incarnant aussi bien le tireur que le gardien, vous pourrez défier vos amis dans un duel intense.  

*******

<div id='regles'/>  

## Règles du jeu 📖  

Une fois la partie lancée, son déroulement est assez linéaire en suivant une même boucle de jeu :   
- le tireur sélectionne de la zone de tir avec une certaine posture.  
- le tireur simule un tir pour passer au tour du gardien.
- le gardien sélectionne sa zone d'arrêt avec une posture spécifique.  

A la fin du tour, les joueurs changent de rôle et continue la partie.  

Au bout de 10 tirs, le joueur ayant inscrit le plus de buts remporte la partie. En cas d'égalité, les joueurs repartent sur 1 tir supplémentaire chacun jusqu'il y ait un résultat positif pour l'un des joueurs.  

Les 4 postures possibles sont : `main gauche en haut`, `main haut droite`, `main bas gauche` et `main bas droite`.  
La gesture de tir consiste à lever son pied vers l'avant comme si l'on tirait dans un ballon (le pied doit monter assez haut).  

*******

<div id='solutions'/>   

## Solutions mises en place 

**PenaltyMaster3000.csproj** : Ce projet est responsable de la logique du jeu PenaltyMaster3000. Il comprend les règles du jeu, le système de notation et les interactions des joueurs.

**MyGestureBank.csproj** : Ce projet contient une bibliothèque de gestes. Il est utilisé pour stocker, récupérer et gérer les gestes utilisés dans différentes applications.

**KinectUtils.csproj** : Ce projet est une bibliothèque d'utilitaires pour Kinect. Il fournit des fonctions d'aide et des classes pour simplifier le travail avec les données du capteur Kinect.

**PostureTester.csproj** : Ce projet est utilisé pour tester et valider les postures. Il utilise les données du capteur Kinect pour analyser et évaluer les postures des utilisateurs.

**KinectSensorStreams.csproj** : Ce projet gère le streaming de données du capteur Kinect. Il comprend des classes pour gérer différents types de flux de données, tels que la couleur, la profondeur et le corps.

**GestureTestApp.csproj** : Ce projet est une application pour tester les gestes. Il utilise la bibliothèque MyGestureBank pour tester et valider les gestes.

**KinectConnection.csproj** : Ce projet gère la connexion au capteur Kinect. Il comprend des classes pour initialiser le capteur, gérer les événements du capteur et gérer les données du capteur.

*******

<div id='structure'/>   

## Structure du projet 

La solution est divisée en de nombreux packages dont l'application WPF en .NET Framework composée des dossiers suivants :  
- `Helpers/` : contient les classes d'aide comme [`VisibilityManager.cs`](Helpers/VisibilityManager.cs)
- `Images/` : contient les images utilisées dans le projet
- `Navigation/` : contient les classes concernant la navigation, incluant [`INavigationService.cs`](Navigation/INavigationService.cs) et [`NavigationService.cs`](Navigation/NavigationService.cs)
- `View/` : contient les vues (UI) de l'application, incluant [`MainView.xaml`](View/MainView.xaml) et [`StartView.xaml`](View/StartView.xaml)
- `ViewModel/` : contient les ViewModels pour les vues, incluant [`MainVM.cs`](ViewModel/MainVM.cs) et [`StartVM.cs`](ViewModel/StartVM.cs)

*******

<div id='auteurs'/>

## **Auteurs** 👥

Étudiant 3ème Annnée - BUT Informatique - IUT Clermont Auvergne - 2023-2024   
`BRODA Lou` - `FRANCO Nicolas`
