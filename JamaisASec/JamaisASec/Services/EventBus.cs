
namespace JamaisASec.Services
{
    public static class EventBus
    {
        // Dictionnaire qui mappe le nom de l'événement à une liste de callbacks (méthodes à appeler)
        private static readonly Dictionary<string, List<Action>> _subscribers = new();

        // Méthode pour s'abonner à un événement
        public static void Subscribe(string eventName, Action callback)
        {
            // Si l'événement n'existe pas encore, on le crée avec une nouvelle liste
            if (!_subscribers.ContainsKey(eventName))
                _subscribers[eventName] = new List<Action>();

            // Ajout de la méthode (callback) à la liste des abonnés
            _subscribers[eventName].Add(callback);
        }

        // Méthode pour publier un événement (c'est-à-dire pour appeler toutes les méthodes abonnés)
        public static void Publish(string eventName)
        {
            // Vérifie si des abonnés existent pour cet événement
            if (_subscribers.ContainsKey(eventName))
            {
                // Appelle toutes les méthodes (callbacks) qui se sont abonnées à cet événement
                foreach (var callback in _subscribers[eventName])
                    callback?.Invoke();
            }
        }
    }
}
