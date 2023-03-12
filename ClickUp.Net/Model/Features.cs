namespace Boson.ClickUp.Net.Model
{
	public class Features
	{
		public DueDates due_dates { get; set; }
		public Sprints sprints { get; set; }
		public Points points { get; set; }
		public CustomItems custom_items { get; set; }
		public Tags tags { get; set; }
		public TimeEstimates time_estimates { get; set; }
		public TimeTracking time_tracking { get; set; }
		public Checklists checklists { get; set; }
		public Zoom zoom { get; set; }
		public Milestones milestones { get; set; }
		public CustomFields custom_fields { get; set; }
		public RemapDependencies remap_dependencies { get; set; }
		public DependencyWarning dependency_warning { get; set; }
		public MultipleAssignees multiple_assignees { get; set; }
		public Portfolios portfolios { get; set; }
		public Emails emails { get; set; }
	}

	public class Milestones
	{
		public bool enabled { get; set; }
	}

	public class Sprints
	{
		public bool enabled { get; set; }
	}

	public class MultipleAssignees
	{
		public bool enabled { get; set; }
	}

	public class Points
	{
		public bool enabled { get; set; }
	}

	public class Emails
	{
		public bool enabled { get; set; }
	}

	public class DueDates
	{
		public bool enabled { get; set; }
		public bool start_date { get; set; }
		public bool remap_due_dates { get; set; }
		public bool remap_closed_due_date { get; set; }
	}

	public class Zoom
	{
		public bool enabled { get; set; }
	}

	public class CustomItems
	{
		public bool enabled { get; set; }
	}

	public class CustomFields
	{
		public bool enabled { get; set; }
	}

	public class Checklists
	{
		public bool enabled { get; set; }
	}

	public class DependencyWarning
	{
		public bool enabled { get; set; }
	}

	public class Portfolios
	{
		public bool enabled { get; set; }
	}

	public class RemapDependencies
	{
		public bool enabled { get; set; }
	}

	public class Tags
	{
		public bool enabled { get; set; }
	}

	public class TimeEstimates
	{
		public bool enabled { get; set; }
	}

	public class TimeTracking
	{
		public bool enabled { get; set; }
	}
}