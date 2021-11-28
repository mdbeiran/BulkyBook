using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BulkyBook.DomainClass.Setting
{
    public class SiteSetting
    {
        #region Properties

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SettingId { get; set; }


        [Required]
        [MaxLength(100)]
        public string SiteName { get; set; }


        [Required]
        public string SiteDescription { get; set; }


        [Required]
        [EmailAddress]
        [MaxLength(256)]
        public string SiteEmail { get; set; }


        [Required]
        [MaxLength(250)]
        public string SiteTell { get; set; }


        [Required]
        [MaxLength(200)]
        public string SiteMobile { get; set; }


        [Required]
        [MaxLength(200)]
        public string SiteFax { get; set; }


        [Required]
        
        public string Address { get; set; }


        public string SiteRules { get; set; }
        public string CopyRight { get; set; }

        #endregion
    }
}
