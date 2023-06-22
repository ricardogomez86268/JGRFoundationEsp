using System.ComponentModel.DataAnnotations;

namespace JGRFoundation.API.Helpers.Adapter
{
    public interface IInvestorDto
    {
        public string Name { get; set; }
        public int RPower { get; set; }
    }
}
