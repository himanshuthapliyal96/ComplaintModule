using System;

namespace User.Core.Entity
{
  public class ComplaintEntity
  {
    public Guid ComplaintId { get; set; }
    public int ReporterId { get; set; }

    public int ResponsibleId { get; set; }

    public string Complaint { get; set; }
    public string Type { get; set; }

    public string Status { get; set; }
  }
}
