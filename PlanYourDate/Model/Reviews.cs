using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlanYourDate.Model
{
    public partial class Reviews
    {
        public int ReviewId { get; set; }
        public int? PlaceId { get; set; }
        [Required]
        [StringLength(255)]
        public string AuthorName { get; set; }
        public int Rating { get; set; }
        [Required]
        public string Comment { get; set; }

        [ForeignKey("PlaceId")]
        [InverseProperty("Reviews")]
        public virtual Places Place { get; set; }
    }
}
