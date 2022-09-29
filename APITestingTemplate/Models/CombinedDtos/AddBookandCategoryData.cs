using System.Collections.Generic;
using APITestingTemplate.Models.Dtos;

namespace APITestingTemplate.Models.CombinedDtos;

public class AddBookandCategoryData
{
    //book category information
    public ICollection<GetBookCategoryDto> BookCategoryData { get; set; } = new List<GetBookCategoryDto>();

    //book information
    public ICollection<GetBookDto> BookData { get; set; } = new List<GetBookDto>();

}