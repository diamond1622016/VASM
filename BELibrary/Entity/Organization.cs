using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BELibrary.Entity
{
    [Table("Organization")]
    public partial class Organization
    {
        public Guid Id { get; set; }
        [StringLength(250)]
        public string Name { get; set; }
        [StringLength(250)]
        public string Founded_year { get; set; }
        public string Areas_of_AI { get; set; }
        [StringLength(250)]
        public string Business_type { get; set; }
        [StringLength(250)]
        public string Ownership_type { get; set; }
        [StringLength(250)]
        public string Scale { get; set; }
        [StringLength(250)]
        public string Number_of_Staff { get; set; }
        [StringLength(250)]
        public string Number_of_AI_Staff { get; set; }
        public string Headquarter_Address { get; set; }
        [StringLength(250)]
        public string Website { get; set; }
        [StringLength(250)]
        public string Logo { get; set; }
        [StringLength(250)]
        public string Telephone { get; set; }
        [StringLength(250)]
        public string Email { get; set; }
        [StringLength(250)]
        public string Country { get; set; }
        public string Organization_summary { get; set; }
        public string Selected_AI_products { get; set; }
        public string Customers_and_partners { get; set; }
        public string Development_plan  { get; set; }
        public string Interested_topics{ get; set; }
        [StringLength(5)]
        public string Type { get; set; }
    }
}