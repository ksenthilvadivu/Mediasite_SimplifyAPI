namespace DH.Curbside.Infrastructure.MediaSite.Mappings
{
    public class PresentationDefaultRepresentation
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Status { get; set; }

        public PresentationDefaultRepresentation(PresentationDefaultRepresentation obj)
        {
            Id = obj.Id;
            Title = obj.Title;
            Status = obj.Status;
        }

        public PresentationDefaultRepresentation() { }
    }
}
