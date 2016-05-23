namespace DerAtrox.BeerNET {
    public interface IBeerEncoder {
        string Encode(string input);
        string Decode(string input);
    }
}
