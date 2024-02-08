using AutoMapper;
using BloodDonationAPI.Dto;
using BloodDonationAPI.Models;

namespace BloodDonationAPI.Helper
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<DonationOperation, DonationOperationDto>();
            CreateMap<DonationOperationDto, DonationOperation>();
            CreateMap<Client, ClientDto>();
            CreateMap<ClientDto, Client>();
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}
