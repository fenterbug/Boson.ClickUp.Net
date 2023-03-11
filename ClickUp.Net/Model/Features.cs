namespace ClickUp.Net.Model
{
	public class Features
	{
		public DueDates due_dates { get; set; }
		public TimeTracking time_tracking { get; set; }
		public Tags tags { get; set; }
		public TimeEstimates time_estimates { get; set; }
		public Checklists checklists { get; set; }
		public CustomFields custom_fields { get; set; }
		public RemapDependencies remap_dependencies { get; set; }
		public DependencyWarning dependency_warning { get; set; }
		public Portfolios portfolios { get; set; }
	}

	public class DueDates
	{
		public bool enabled { get; set; }
		public bool start_date { get; set; }
		public bool remap_due_dates { get; set; }
		public bool remap_closed_due_date { get; set; }
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