using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASMSEntityLayer.Models;
using ASMSEntityLayer.ViewModels;

namespace ASMSEntityLayer.Mappings
{
    //buraya Maps ctor gelecek 
    //içine creatMap metodu gelecek.
    public class Maps : Profile
    {
        public Maps()
        {
                       //userAddreses'ı UserAdressesVM'ye dönüştür.
            //CreateMap<UsersAddress, UsersAddressVM>(); //DAL --> BLL
                      //userAddressesVM'y, UserAdresses'a doönüştür.
            //CreateMap<UsersAddressVM, UsersAddress>(); //PL-->DAL

            //yukardakinin aynısını tek sefer de yapmak 
            //userAdress ve VM'yi birbirine dönüştür.

            CreateMap<UsersAddress, UsersAddressVM>().ReverseMap();


        }



    }
}