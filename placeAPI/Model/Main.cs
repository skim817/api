using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace placeAPI.Model
{
    public partial class Main
    {
        public Main()
        {
            PlaceConverter = new HashSet<PlaceConverter>();
        }

        public int MainId { get; set; }
        [Required]
        [StringLength(255)]
        public string PlaceName { get; set; }

        [InverseProperty("Main")]
        public virtual ICollection<PlaceConverter> PlaceConverter { get; set; }
    }
}
