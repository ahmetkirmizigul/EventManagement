using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EventManagement.Models;

public class EventParticipant
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Event")]
    public int EventId { get; set; }

    public Event Event { get; set; }

    [ForeignKey("Participant")]
    public int ParticipantId { get; set; }

    public Participant Participant { get; set; }
}
