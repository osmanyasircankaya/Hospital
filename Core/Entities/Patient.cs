using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Core.Entities
{
    public class Patient
    {
        public int Id { get; set; }
        public string CitizenNumber { get; set; }
        public DateTime AddedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
