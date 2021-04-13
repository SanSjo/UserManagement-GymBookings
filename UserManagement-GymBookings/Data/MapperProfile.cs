using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement_GymBookings.Models;
using UserManagement_GymBookings.Models.ViewModel;


namespace UserManagement_GymBookings.Data
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            //CreateMap<GymClass, GymClassesViewModel>()
            //    .ForMember(dest => dest.Attending, opt => opt.Ignore());
            
            //CreateMap<GymClass, GymClassViewModel>()
            //    .ForMember(dest => dest.Attending, opt => opt.MapFrom((src, dest, _, context) => src.AttendingMembers.Any(a => a.ApplicationUserID == context.Items["Id"].ToString())));
            
            CreateMap<GymClass, GymClassViewModel>()
                .ForMember(dest => dest.Attending, opt => opt.MapFrom<AttendingResolver>());

            CreateMap<IEnumerable<GymClass>, IndexViewModel>()
                .ForMember(
                dest => dest.GymClasses,
                from => from.MapFrom(g => g.ToList()))
                .ForMember(

                dest => dest.ShowHistory, opt => opt.Ignore());
        }
    }

    //public class AttendingResolver : IValueResolver<GymClass, GymClassViewModel, bool>
    //{
    //    public bool Resolve(GymClass source, GymClassViewModel destination, bool destMember, ResolutionContext context)
    //    {
    //        if (source.AttendingMembers == null || context.Items.Count == 0) return false;
    //        return source.AttendingMembers.Any(a => a.ApplicationUserID == context.Items["Id"].ToString());
    //    }
    //}

    public class AttendingResolver : IValueResolver<GymClass, GymClassViewModel, bool>
    {
        public bool Resolve(GymClass source, GymClassViewModel destination, bool destMember, ResolutionContext context)
        {
            if (source.AttendingMembers == null || context.Items.Count == 0) return false;
            return source.AttendingMembers.Any(a => a.ApplicationUserID == context.Items["Id"].ToString());
        }
    }

    //public class AttendingResolver2 : IMemberResolver<GymClass, GymClassViewModel, bool>
    //{
    //    public bool Resolve(GymClass source, GymClassViewModel destination, bool destMember, ResolutionContext context)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}


}
