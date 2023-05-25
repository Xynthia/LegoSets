namespace LegoSets.Dtos.LegoSets
{
    public class GetLegoSetDto
    {
        public int Id { get; set; }
        public int SetID { get; set; }
        public string Number { get; set; }
        public string Variant { get; set; }

        public string Theme { get; set; }
        public string SubTheme { get; set; }
        public string Year { get; set; }
        public string SetName { get; set; }

        public string Minifigs { get; set; }
        public int Pieces { get; set; }

        public string UKPrice { get; set; }
        public string USPrice { get; set; }
        public string CAPrice { get; set; }

        public string Image { get; set; }
        public string ImageFilename { get; set; }

        public bool InstructionsAvailable { get; set; }

        public string EAN { get; set; }
        public string UPC { get; set; }

        public bool Owned { get; set; }
    }
}
