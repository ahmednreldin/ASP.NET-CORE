using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class TodoBindingModel
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(80)]
        [Display(Name="Enter Task Title")]
        public string Title { get; set; }

        [Required]
        [StringLength(150)]
        [Display(Name ="Enter Task Description")]
        public string Body { get; set; }

        [Required]
        public IFormFile Image { get; set; }

    }
}
