﻿namespace TeamAPI.Models
{
    public class Player
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
