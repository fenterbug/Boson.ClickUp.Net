namespace Boson.ClickUp.Net.Model
{
	public class Checklists
	{
		public bool enabled { get; set; }
	}

	public class CustomFields
	{
		public bool enabled { get; set; }
	}

	public class CustomItems
	{
		public bool enabled { get; set; }
	}

	public class DependencyWarning
	{
		public bool enabled { get; set; }
	}

	public class DueDates
	{
		public bool enabled { get; set; }
		public bool remap_closed_due_date { get; set; }
		public bool remap_due_dates { get; set; }
		public bool start_date { get; set; }
	}

	public class Emails
	{
		public bool enabled { get; set; }
	}

	public class Features
	{
		public Checklists checklists => new();
		public CustomFields custom_fields => new();
		public CustomItems custom_items => new();
		public DependencyWarning dependency_warning => new();
		public DueDates due_dates => new();
		public Emails emails => new();
		public Milestones milestones => new();
		public MultipleAssignees multiple_assignees => new();
		public Points points => new();
		public Portfolios portfolios => new();
		public RemapDependencies remap_dependencies => new();
		public Sprints sprints => new();
		public Tags tags => new();
		public TimeEstimates time_estimates => new();
		public TimeTracking time_tracking => new();
		public Zoom zoom => new();
	}

	public class Milestones
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

	public class Portfolios
	{
		public bool enabled { get; set; }
	}

	public class RemapDependencies
	{
		public bool enabled { get; set; }
	}

	public class Sprints
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

	public class Zoom
	{
		public bool enabled { get; set; }
	}
}