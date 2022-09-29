using System.Collections.Generic;
using APITestingTemplate.Models.Dtos;

namespace APITestingTemplate.Models.CombinedDtos;

public class AddCategoryData
{
    //book category information
    public ICollection<GetBookCategoryDto> BookCategoryData { get; set; } = new List<GetBookCategoryDto>();

}