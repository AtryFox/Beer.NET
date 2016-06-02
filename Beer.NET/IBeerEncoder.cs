namespace DerAtrox.BeerNET {
    public interface IBeerEncoder {
        /// <summary>
        /// Serializes a string into Beer.
        /// </summary>
        /// <param name="input">The string to serialize.</param>
        /// <returns>Serialized string.</returns>
        string Encode(string input);

        /// <summary>
        /// Deserializes a string from Beer.
        /// </summary>
        /// <param name="input">The string to deserialize.</param>
        /// <returns>Deserialized string.</returns>
        string Decode(string input);
    }
}
