using System.Text.Json.Serialization;

namespace Boson.ClickUp.Net.Model
{
    public class Space
    {
        public Space(string name, bool multiple_assignees, Features features = null)
        {
            this.name = name;
            this.multiple_assignees = multiple_assignees;
            this.features = features ?? new Features();
        }

        public bool @private { get; set; }

        public bool access { get; set; }
        public bool admin_can_manager { get; set; }

        public bool archived { get; set; }

        public string color { get; set; }

        public Features features { get; set; }
        public string id { get; set; }

        public bool multiple_assignees { get; set; }

        public string name { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<Status> statuses { get; set; }
    }
}