using API.DTOs.FixQty;
using API.DTOs.User;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API{
    public class AutoMapperProfile :Profile{
        public AutoMapperProfile(){
            CreateMap<FixQtyClass, GetFixQtyDTO>();
            CreateMap<AddFixQtyDTO, FixQtyClass>();
            CreateMap<UpdateFixQtyDTO, FixQtyClass>();

            CreateMap<UserClass, GetUserDTO>();
            CreateMap<AddUserDTO, UserClass>();
            CreateMap<UpdateUserDTO, UserClass>();
        }
    }
}