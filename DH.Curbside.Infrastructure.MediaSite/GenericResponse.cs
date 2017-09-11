using System.Collections.Generic;

namespace DH.Curbside.Infrastructure.MediaSite
{
    public class GenericResponse<T>
    {
        public List<T> value { get; set; }
    }
}
