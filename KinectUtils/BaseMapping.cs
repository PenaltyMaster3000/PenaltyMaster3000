using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectUtils
{
    // Classe de base pour les mappages. T est le type de données renvoyé par le mappage.
    public abstract class BaseMapping<T>
    {
        // Indique si le mappage est en cours d'exécution.
        private bool running;

        // Événement déclenché lorsque le mappage est effectué.
        public EventHandler<T> OnMapping { get; set; }

        // Souscrit à un geste qui démarre le mappage.
        public void SubscribeToStartGesture(BaseGesture gesture)
        {
            // Lorsque le geste est reconnu, le mappage est démarré.
            gesture.GestureRecognized += (sender, args) => running = true;
        }

        // Souscrit à un geste qui arrête le mappage.
        public void SubscribeToEndGesture(BaseGesture gesture)
        {
            // Lorsque le geste est reconnu, le mappage est arrêté.
            gesture.GestureRecognized += (sender, args) => running = false;
        }

        // Souscrit à un geste qui bascule l'état du mappage.
        public void SubscribeToToggleGesture(BaseGesture gesture)
        {
            // Lorsque le geste est reconnu, l'état du mappage est inversé.
            gesture.GestureRecognized += (sender, args) => running = !running;
        }

        // Méthode abstraite pour effectuer le mappage. Doit être implémentée par les classes dérivées.
        protected abstract T Mapping(Body body);

        // Teste si le mappage peut être effectué.
        bool TestMapping(Body body)
        {
            // Le mappage peut être effectué si le mappage est en cours d'exécution.
            return running;
        }

        // Teste si le mappage peut être effectué et renvoie le résultat du mappage.
        protected bool TestMapping(Body body, out T output)
        {
            // Initialise la sortie à la valeur par défaut.
            output = default(T);

            // Si le mappage n'est pas en cours d'exécution, retourne false.
            if (!running)
            {
                return false;
            }

            // Appelle la méthode de mappage et assigne le résultat à la sortie.
            output = Mapping(body);

            // Retourne true si le mappage a réussi.
            return output != null;
        }
    }
}