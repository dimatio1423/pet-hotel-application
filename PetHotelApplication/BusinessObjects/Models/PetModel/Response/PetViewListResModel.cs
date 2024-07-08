using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models.PetModel.Response
{
    public class PetViewListResModel
    {
        public string PetId { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }
        public DateOnly? Dob { get; set; }
    }
}
