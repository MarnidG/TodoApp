using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TodoApp.Models
{
    public class ToDo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Proszę dodać opis.")]
        public string Opis { get; set; }

        [Display(Name ="Data")]
        [Required(ErrorMessage = "Proszę podać datę.")]
        public DateTime DueDate { get; set; }

        [Display(Name = "Kategoria")]
        [Required(ErrorMessage = "Proszę wybierz kategorię.")]
        public int CategoryId { get; set; }

        [Display(Name = "Kategoria")]
        public virtual Category? Category { get; set; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = "Proszę wybierz status.")]
        public int StatusId {  get; set; }
        
        public virtual Status? Status { get; set; }

        [ValidateNever]
        public string userId { get; set; }
        
        // public bool Overdue => StatusId == "open" && DueDate < DateTime.Today;
    }
}
