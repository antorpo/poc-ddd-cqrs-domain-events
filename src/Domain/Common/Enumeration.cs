using System.Reflection;

namespace Domain.Common
{
    // Learn more https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/enumeration-classes-over-enum-types
    public abstract class Enumeration : IComparable
    {
        public int Id { get; private set; }

        public string Name { get; private set; }

        public string Description { get; set; }

        protected Enumeration(int id, string name, string description) => (Id, Name, Description) = (id, name, description);

        public override string ToString() => Name;

        public override bool Equals(object obj)
        {
            if (obj is not Enumeration otherValue)
            {
                return false;
            }

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = Id.Equals(otherValue.Id);

            return typeMatches && valueMatches;
        }

        public override int GetHashCode() => Id.GetHashCode();

        public int CompareTo(object other) => Id.CompareTo(((Enumeration)other).Id);

        public string GetDescription(params object[] args) => string.Format(Description, args);

        #region Static
        public static IEnumerable<T> GetAll<T>() where T : Enumeration =>
          typeof(T).GetFields(BindingFlags.Public |
                              BindingFlags.Static |
                              BindingFlags.DeclaredOnly)
                      .Select(f => f.GetValue(null))
                      .Cast<T>();

        public static T FromValue<T>(int value) where T : Enumeration
        {
            var matchingItem = Parse<T, int>(value, "value", item => item.Id == value);
            return matchingItem;
        }

        public static T FromDisplayName<T>(string displayName) where T : Enumeration
        {
            var matchingItem = Parse<T, string>(displayName, "display name", item => item.Name == displayName);
            return matchingItem;
        }

        private static T Parse<T, K>(K value, string description, Func<T, bool> predicate) where T : Enumeration
        {
            var matchingItem = GetAll<T>().FirstOrDefault(predicate);

            if (matchingItem == null)
                throw new InvalidOperationException($"'{value}' is not a valid {description} in {typeof(T)}");

            return matchingItem;
        }

        #endregion Static
    }
}
