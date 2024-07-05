using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models.PetCareModel.Response
{
    public class PetCareResModel
    {
        public string Id { get; set; }

        public string Type { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
    }
}
