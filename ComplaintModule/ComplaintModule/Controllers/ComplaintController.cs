using System;
using System.Security.Claims;
using ApiServices;
using Employee.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using User.Core.Entity;
using UserApi.Services;

namespace UserApi.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class ComplaintController : ControllerBase
  {
    private readonly ComplaintService _complaintService;
    public ComplaintController(UserDbContext context)
    {
      _complaintService = new ComplaintService(context);
    }

    /// <summary>
    /// Get all the complaints reported by the user.
    /// </summary>
    /// <param name="userId">userId of user</param>
    /// <returns>list of complaints reported by the user.</returns>
    [HttpGet("User/{userId}")]
    public IActionResult GetAllComplaintsByUser(int userId)
    {
      var id = string.Empty;
      var role = string.Empty;
      if (HttpContext.User.Identity is ClaimsIdentity identity)
      {
        id = identity.FindFirst(ClaimTypes.Name).Value;
        role = identity.FindFirst(ClaimTypes.Role).Value;
      }

      if (id == userId.ToString() || role == Roles.Admin)
      {
        return Ok(_complaintService.GetAllComplaintsForUser(userId));
      }
      return Unauthorized();
    }


    /// <summary>
    /// Get all the complaints for a responsible person.
    /// </summary>
    /// <param name="responsibleId">id of responsible.</param>
    /// <returns></returns>
    [Authorize(Roles = "Admin")]
    [HttpGet("Admin/{responsibleId}")]
    public IActionResult GetAllComplaintsForResolution(int responsibleId)
    {
      var userId = string.Empty;
      if (HttpContext.User.Identity is ClaimsIdentity identity)
      {
        userId = identity.FindFirst(ClaimTypes.Name).Value;
      }

      if (userId != responsibleId.ToString()) return Unauthorized();
      return Ok(_complaintService.GetAllComplaintsForResolution(responsibleId));
    }

    /// <summary>
    /// Add a new complaint in the system.
    /// </summary>
    /// <param name="complaint">complaint details.</param>
    /// <returns>return added complaint.</returns>
    [HttpPost]
    public IActionResult AddComplaint(ComplaintEntity complaint)
    {
      var userId = string.Empty;
      if (HttpContext.User.Identity is ClaimsIdentity identity)
      {
        userId = identity.FindFirst(ClaimTypes.Name).Value;
      }

      if (int.TryParse(userId,out int id))
      {
        complaint.ReporterId = id;
      }
      else
      {
        throw new Exception("Invalid user id in claims.");
      }
      
      complaint = _complaintService.AddComplaint(complaint);
      return Ok(complaint);
    }

    /// <summary>
    /// Update a particular complaint in system.
    /// </summary>
    /// <param name="complaintId">Id of the complaint.</param>
    /// <param name="complaint">complaint details.</param>
    /// <returns></returns>
    [Authorize(Roles = "Admin")]
    [HttpPut("{complaintId}")]
    public IActionResult UpdateComplaint(string complaintId,[FromBody]ComplaintEntity complaint)
    {
      var userId = string.Empty;
      if (HttpContext.User.Identity is ClaimsIdentity identity)
      {
        userId = identity.FindFirst(ClaimTypes.Name).Value;
      }

      if (int.TryParse(userId, out int id) && (id == complaint.ResponsibleId|| id==complaint.ReporterId))
      {
        _complaintService.UpdateComplaintStatus(complaintId, complaint);
        return Ok(complaint);
      }
      return Unauthorized();

    }
  }
}
