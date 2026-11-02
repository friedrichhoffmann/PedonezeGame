namespace ResistanceFile {
    public enum ResistanceKind {
        Slashing,
        Piercing,
        Bludgeoning,
    }

    public class Resistance {
        public ResistanceKind Kind { get; set; }
        public int Degree { get; set; }
    }
}
