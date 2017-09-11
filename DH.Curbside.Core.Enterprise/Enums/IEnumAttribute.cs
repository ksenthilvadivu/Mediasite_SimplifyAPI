
namespace DH.Curbside.Core.Enterprise.Enums
{
    /// <summary>
    /// Custom attribute for all enums.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEnumAttribute<T>
    {
        /// <summary>
        /// Generic type Value.
        /// </summary>
        T Value { get; }
    }
}
