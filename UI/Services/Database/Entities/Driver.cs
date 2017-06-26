using System;
using System.Collections.Generic;

namespace UI.services.database.entities
{
    public class Driver : inheritance.IIdEntity
    {
        public int Id { get; set; }
        public String Fullname { get; set; }
        public DateTime Birthday { get; set; }
    }
}
