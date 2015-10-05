using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Telerik.Web.UI;

using Quartz;
using Quartz.Portal;
using Quartz.Social;

public class CustomSchedulerProvider : DbSchedulerProviderBase
{
    public override void Delete(RadScheduler owner, Appointment appointmentToDelete)
    {
        throw new NotImplementedException();
    }

    public override IEnumerable<Appointment> GetAppointments(RadScheduler owner)
    {
        List<Appointment> appointments = new List<Appointment>();

        var events = qSoc_Event.GetEventsByType(owner.VisibleRangeStart, owner.VisibleRangeEnd, "Event");

        foreach (var e in events)
        {
            Appointment appointment = new Appointment();

            appointment.ID = e.EventID;
            appointment.AllowDelete = false;
            appointment.AllowEdit = false;
            appointment.Start = e.DateTime;
            appointment.End = e.DateTime;
            appointment.Subject = e.Name;
            appointment.Description = "Event: " + e.Summary;
            appointment.ToolTip = "Event: " + e.Summary;
            appointment.Resources.Add(new Resource("Calendar", 1, "Event"));

            appointments.Add(appointment);
        }

        var trainings = qSoc_Event.GetEventsByType(owner.VisibleRangeStart, owner.VisibleRangeEnd, "Training");

        foreach (var t in trainings)
        {
            Appointment appointment = new Appointment();

            appointment.ID = t.EventID;
            appointment.AllowDelete = false;
            appointment.AllowEdit = false;
            appointment.Start = t.DateTime;
            appointment.End = t.DateTime;
            appointment.Subject = t.Name;
            appointment.Description = "Training: " + t.Summary;
            appointment.ToolTip = "Training: " + t.Summary;
            appointment.Resources.Add(new Resource("Calendar", 2, "Training"));

            appointments.Add(appointment);
        }

        var meetings = qSoc_Event.GetEventsByType(owner.VisibleRangeStart, owner.VisibleRangeEnd, "Meeting");

        foreach (var m in meetings)
        {
            Appointment appointment = new Appointment();

            appointment.ID = m.EventID;
            appointment.AllowDelete = false;
            appointment.AllowEdit = false;
            appointment.Start = m.DateTime;
            appointment.End = m.DateTime;
            appointment.Subject = m.Name;
            appointment.Description = "Meeting: " + m.Summary;
            appointment.ToolTip = "Meeting: " + m.Summary;
            appointment.Resources.Add(new Resource("Calendar", 3, "Meeting"));
            appointment.Attributes.Add("Mode", "read");

            appointments.Add(appointment);
        }

        // turn off basic events, contests and tasks for St. Judes
        /*
         var events = qSoc_Event.GetEvents(owner.VisibleRangeStart, owner.VisibleRangeEnd);

        foreach (var e in events)
        {
            Appointment appointment = new Appointment();

            appointment.ID = e.EventID;
            appointment.AllowDelete = false;
            appointment.AllowEdit = false;
            appointment.Start = e.DateTime;
            appointment.End = e.DateTime;
            appointment.Subject = e.Name;
            appointment.Description = e.Summary;
            appointment.ToolTip = e.Summary;
            appointment.Resources.Add(new Resource("Calendar", 1, "Events"));
            appointment.Attributes.Add("NavigateURL", string.Format("events-view.aspx?EventID={0}", e.EventID));

            appointments.Add(appointment);
        }
         * 
        var contests = qSoc_Contest.GetContests(owner.VisibleRangeStart, owner.VisibleRangeEnd);

        foreach (var c in contests)
        {
            Appointment appointment = new Appointment();

            appointment.ID = c.ContestID;
            appointment.AllowDelete = false;
            appointment.AllowEdit = false;
            appointment.Start = c.StartDateTime;
            appointment.End = c.EndDateTime;
            appointment.Subject = c.Name;
            appointment.Description = c.Summary;
            appointment.ToolTip = c.Summary;
            appointment.Resources.Add(new Resource("Calendar", 2, "Contests"));
            appointment.Attributes.Add("NavigateURL", string.Format("contests-view.aspx?ContestID={0}", c.ContestID));

            appointments.Add(appointment);
        }

        {
            var tasks = qPtl_Task.GetTasks(owner.VisibleRangeStart, owner.VisibleRangeEnd);

            foreach (var c in tasks)
            {
                Appointment appointment = new Appointment();

                appointment.ID = c.TaskID;
                appointment.AllowDelete = false;
                appointment.AllowEdit = false;
                appointment.Start = c.StartDate.GetValueOrDefault();
                appointment.End = c.DueDate.GetValueOrDefault();
                appointment.Subject = c.Name;
                appointment.Description = c.Description;
                appointment.ToolTip = c.Name;
                appointment.Resources.Add(new Resource("Calendar", 3, "Tasks"));
                appointment.Attributes.Add("NavigateURL", string.Format("tasks-view.aspx?TaskID={0}", c.TaskID));

                appointments.Add(appointment);
            }
        }
        */

        return appointments;
    }

    public override IEnumerable<ResourceType> GetResourceTypes(RadScheduler owner)
    {
        //throw new NotImplementedException();
        return new List<ResourceType>();
    }

    public override IEnumerable<Resource> GetResourcesByType(RadScheduler owner, string resourceType)
    {
        throw new NotImplementedException();
    }

    public override void Insert(RadScheduler owner, Appointment appointmentToInsert)
    {
        throw new NotImplementedException();
    }

    public override void Update(RadScheduler owner, Appointment appointmentToUpdate)
    {
        throw new NotImplementedException();
    }
}

public partial class text_messages_calendar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        scheduler.Provider = new CustomSchedulerProvider();
    }
}