using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs
{
    public class WeatherViewModel
    {
        [Required(ErrorMessage = "Latitude is required")]
        [Range(0, float.MaxValue, ErrorMessage = "Please enter valid float Number for Latitude")]
        [Display(Name = "Latitude")]
        public string Lat { get; set; }
        [Required(ErrorMessage = "Longitude is required")]
        [Range(0, float.MaxValue, ErrorMessage = "Please enter valid float Number for Longitude")]
        [Display(Name = "Longitude")]
        public string Lon { get; set; }
        [Display(Name = "StartedAt")]
        [Required(ErrorMessage = "StartedAt is required")]
        // [DataType(DataType.Date, ErrorMessage = "Please enter a correct date format dd/mm/yyyy hh:mm for StartedAt"), DisplayFormat( DataFormatString="{0:dd/MM/yyyy}", ApplyFormatInEditMode=true )]
        public DateTime StartedAt { get; set; }
    }
}