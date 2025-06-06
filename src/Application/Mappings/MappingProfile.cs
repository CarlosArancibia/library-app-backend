using AutoMapper;
using Application.DTOs;
using Domain.Entities;
using Application.DTOs.Requests;

namespace Application.Mappings
{
    public class MappingProfile : Profile
    {
      public MappingProfile()
      {
          CreateMap<Book, BookDto>();
          CreateMap<CreateBookRequest, Book>().ForMember(dest => dest.Id, opt => opt.Ignore());
          CreateMap<UpdateBookRequest, Book>().ForMember(dest => dest.Id, opt => opt.Ignore());
          CreateMap<Book, UpdateBookRequest>();
      }
    }
}
