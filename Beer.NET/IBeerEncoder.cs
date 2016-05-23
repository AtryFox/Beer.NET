namespace DerAtrox.BeerNET {
    public interface IBeerEncoder {
        string SerializeBeer(string input);
        string DeserializeBeer(string input);
    }
}
