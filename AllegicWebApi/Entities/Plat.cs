namespace AllegicWebApi.Entities
{
    public class Plat
    {
        public int Id { get; set; }
        public string NamePlat { get; set; } = string.Empty;

        public bool gluten { get; set; }

        public bool shellfish { get; set; }
        public bool egg { get; set; }
        public bool fish { get; set; }
        public bool Peanuts { get; set; }
        public bool soy { get; set; }
        public bool milk { get; set; }
        public bool nuts { get; set; }
        public bool celery { get; set; }
        public bool mustard { get; set; }
        public bool sesame { get; set; }
        public bool sdands { get; set; }
        public bool lupin { get; set; }
        public bool molluscs { get; set; }


    }
}
