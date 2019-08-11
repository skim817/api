using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace PlanYourDate.Model
{
    public partial class Places
    {
        public Places()
        {
            Reviews = new HashSet<Reviews>();
        }

        public int PlaceId { get; set; }
        [Required]
        [StringLength(255)]
        public string PlaceName { get; set; }
        public int RankBy { get; set; }
        [Required]
        [StringLength(510)]
        public string PhotoRef { get; set; }
        [Required]
        [StringLength(255)]
        public string PlaceAddress { get; set; }
        public bool IsFavourited { get; set; }
        public string Comment { get; set; }
        [Required]
        [StringLength(255)]
        public string PhoneNumber { get; set; }
        public bool OpenNow { get; set; }

        [InverseProperty("Place")]
        public virtual ICollection<Reviews> Reviews { get; set; }
    }

    [DataContract]
    public class PlaceDTO
    {
        [DataMember]
        public int PlaceId { get; set; }

        [DataMember]
        public string PlaceName { get; set; }

        [DataMember]
        public int RankBy { get; set; }

        [DataMember]
        public string PhotoRef { get; set; }

        [DataMember]
        public string PlaceAddress { get; set; }

        [DataMember]
        public bool IsFavourited { get; set; }

        [DataMember]
        public string Comment { get; set; }

        [DataMember]
        public string PhoneNumber { get; set; }

        [DataMember]
        public bool OpenNow { get; set; }

    }

}
