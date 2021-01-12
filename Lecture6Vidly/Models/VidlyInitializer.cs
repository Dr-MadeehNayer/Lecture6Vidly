using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Lecture6Vidly.Models
{
    public class VidlyInitializer:DropCreateDatabaseIfModelChanges<VidlyContext>
    {
        protected override void Seed(VidlyContext context)
        {
            
        }
    }
}