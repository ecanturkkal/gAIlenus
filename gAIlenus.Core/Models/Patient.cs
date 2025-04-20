﻿namespace gAIlenus.Core
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Surname { get; set; } = "";
        public DateTime Birthdate { get; set; }
        public ICollection<Diagnosis> Diagnoses { get; set; }
    }
}
