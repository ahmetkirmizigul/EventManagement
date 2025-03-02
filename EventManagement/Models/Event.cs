using System.ComponentModel.DataAnnotations;

namespace EventManagement.Models;

public class Event
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Başlık alanı zorunludur.")]
    [StringLength(100, ErrorMessage = "Başlık en fazla 100 karakter olabilir.")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Tarih alanı zorunludur.")]
    public DateTime Date { get; set; }

    [Required(ErrorMessage = "Yer alanı zorunludur.")]
    [StringLength(200)]
    public string Location { get; set; }

    [Required(ErrorMessage = "Ayrıntılar alanı zorunludur.")]
    public string Details { get; set; }

    public List<EventParticipant> EventParticipants { get; set; } = new();
}