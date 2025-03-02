using System.ComponentModel.DataAnnotations;

namespace EventManagement.Models;

public class Participant
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Ad alanı zorunludur.")]
    [StringLength(50)]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Soyad alanı zorunludur.")]
    [StringLength(50)]
    public string LastName { get; set; }

    [Required(ErrorMessage = "E-posta alanı zorunludur.")]
    [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
    public string Email { get; set; }

    public List<EventParticipant> EventParticipants { get; set; } = new();
}