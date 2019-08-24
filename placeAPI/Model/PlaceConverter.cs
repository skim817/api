using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace placeAPI.Model
{
    public partial class PlaceConverter
    {
        public int PlaceConverterId { get; set; }
        [Required]
        [StringLength(255)]
        public string PlaceTitle { get; set; }
        public string Photo { get; set; }
        public int? PlaceRank { get; set; }
        [Column("isFavourite")]
        public bool IsFavourite { get; set; }
        [Required]
        [Column("PlaceID")]
        [StringLength(255)]
        public string PlaceId { get; set; }
        public int? MainId { get; set; }

        [ForeignKey("MainId")]
        [InverseProperty("PlaceConverter")]
        public virtual Main Main { get; set; }
    }
}
