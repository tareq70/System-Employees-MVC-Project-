﻿using AutoMapper;
using DAL.Models;
using Demo.PL.ViewModels;

namespace Demo.PL.MappingProfiles
{
    public class EmployeeProfile:Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeViewModel , Employee>().ReverseMap();
        }
    }
}
