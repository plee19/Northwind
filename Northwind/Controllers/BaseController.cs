using AutoMapper;
using Northwind.Models;
using System.Web.Mvc;

namespace Northwind.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly IMapper Mapper;

        protected BaseController()
        {
            var config = new MapperConfiguration(x =>
            {
                x.CreateMap<Customer, CustomerEdit>();
            });
            Mapper = config.CreateMapper();
        }
    }
}