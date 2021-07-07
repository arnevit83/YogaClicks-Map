namespace Clicks.Yoga.Portal.Models.Editors
{
    public class SignUpVenueSelectorModel
    {
        public SignUpVenueSelectorModel() : this(null) {}

        public SignUpVenueSelectorModel(bool? owned)
        {
            Owned = owned;
        }

        public int? Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool? Owned { get; set; }
    }
}