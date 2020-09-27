using System;

namespace MyMemory.Api
{
    public struct ApiKey : IEquatable<ApiKey>
    {
        public static readonly ApiKey AnonymousAccess = default;

        public bool IsAnonymous => key == null;

        public bool Equals(ApiKey other)
        {
            return String.Equals(key, other.key);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is ApiKey other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (key != null ? key.GetHashCode() : 0);
        }

        public static bool operator ==(ApiKey left, ApiKey right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ApiKey left, ApiKey right)
        {
            return !left.Equals(right);
        }

        public override string ToString()
        {
            return key;
        }

        private readonly string key;

        public ApiKey(string key)
        {
            this.key = key;
        }
    }
}