namespace DH.Curbside.Core.Domain
{
    public class TermsOfService
    {
        public int TermsOfServiceId { get; set; }

        public string Description { get; set; }

        public virtual Provider Provider { get; set; }
    }
}
