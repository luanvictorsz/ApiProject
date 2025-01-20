using System.ComponentModel.DataAnnotations;

namespace ApiProject.ViewModels
{
    public class CreateToDoViewModel
    {
        [Required]
        public string Title { get; set; }
    }
}
