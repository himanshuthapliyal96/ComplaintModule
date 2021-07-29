using System;
using System.Linq;
using Employee.Persistence;
using User.Core.Entity;

namespace UserApi.Services
{
  public class ComplaintService
  {
    private readonly UserDbContext _context;
    public ComplaintService(UserDbContext context)
    {
      _context = context;
    }

    public ComplaintEntity AddComplaint(ComplaintEntity complaint)
    {
      try
      {
        complaint.ComplaintId = Guid.NewGuid();
        _context.Complaints.Add(complaint);
        _context.SaveChanges();
        return complaint;
      }
      catch (Exception e)
      {
        throw new Exception($"Unable to add complaint due to an exception, Reason: {e.Message}");
      }
      
    }

    public void UpdateComplaintStatus(string complaintId,ComplaintEntity complaint)
    {
      try
      {
        _context.Complaints.Update(complaint);
        _context.SaveChanges();
      }
      catch (Exception e)
      {
        throw new Exception($"Unable to update complaint with Id {complaint.ComplaintId} due to an exception, Reason: {e.Message}");
      }

    }

    public object GetAllComplaintsForUser(int userId)
    {
      return _context.Complaints.Where(complaint => complaint.ReporterId == userId);
    }

    public object GetAllComplaintsForResolution(int userId)
    {
      return _context.Complaints.Where(complaint => complaint.ResponsibleId == userId);
    }
  }
}
