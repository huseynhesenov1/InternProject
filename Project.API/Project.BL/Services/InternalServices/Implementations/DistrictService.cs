﻿using Project.BL.DTOs.DistrictDTOs;
using Project.BL.Services.InternalServices.Abstractions;
using Project.Core.Entities;
using Project.DAL.Repositories.Abstractions.District;

namespace Project.BL.Services.InternalServices.Implementations
{
    public class DistrictService : IDistrictService
    {
        private readonly IDistrictReadRepository _districtReadRepository;
        private readonly IDistrictWriteRepository _districtWriteRepository;

        public DistrictService(IDistrictWriteRepository districtWriteRepository, IDistrictReadRepository districtReadRepository)
        {
            _districtWriteRepository = districtWriteRepository;
            _districtReadRepository = districtReadRepository;
        }


        public async Task<District> CreateAsync(DistrictCreateDTO districtCreateDTO)
        {
            District district = new District()
            {
                Name = districtCreateDTO.Name,
            };
            district.CreatedAt = DateTime.UtcNow.AddHours(4);
            await _districtWriteRepository.CreateAsync(district);
            await _districtWriteRepository.SaveChangeAsync();
            return district;
        }


        public async Task<District> UpdateAsync(int Id, DistrictUpdateDTO districtUpdateDTO)
        {
            District district = await _districtReadRepository.GetByIdAsync(Id, false);
            if (district == null)
            {
                throw new Exception("Invalid ID");
            }
            District newDistrict = new District()
            {
                Name = districtUpdateDTO.Name,
            };
            newDistrict.Id = Id;
            newDistrict.UpdatedAt = DateTime.UtcNow.AddHours(4);
            _districtWriteRepository.Update(newDistrict);
            await _districtWriteRepository.SaveChangeAsync();
            return newDistrict;
        }



        public async Task<ICollection<DistrictReadDTO>> GetAllAsync()
        {
            ICollection<District> districts = await _districtReadRepository.GetAllAsync(false);
            List<DistrictReadDTO> districtReadDTOs = districts
                .Select(d => new DistrictReadDTO
                {
                    Id = d.Id,
                    Name = d.Name,
                })
                .ToList();
            return districtReadDTOs;
        }

        public async Task<District> GetByIdAsync(int id)
        {
            District district = await _districtReadRepository.GetByIdAsync(id, false);
            if (district is null)
            {
                throw new Exception("Bu Id-e uygun deyer tapilmadi");
            }
            return district;
        }

       

        public async Task<District> SoftDeleteAsync(int id)
        {
            District catagory = await _districtReadRepository.GetByIdAsync(id, true);
            if (catagory is null)
            {
                throw new Exception("Bu Id-e uygun deyer tapilmadi");
            }
            var res = _districtWriteRepository.SoftDelete(catagory);
            await _districtWriteRepository.SaveChangeAsync();
            return res;
        }
    }
}
