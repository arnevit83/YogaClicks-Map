namespace Clicks.Yoga.Portal.Models.Editors
{
    public class VenueSelectorModel
    {
        public VenueSelectorModel() : this(null) {}

        public VenueSelectorModel(bool? owned)
        {
            Owned = owned;
        }

        public int? Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool? Owned { get; set; }
    }
}