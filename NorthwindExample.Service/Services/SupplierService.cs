using AutoMapper;
using NorthwindExample.Core.Models;
using NorthwindExample.Core.Repositories;
using NorthwindExample.Core.Services;
using NorthwindExample.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindExample.Service.Services
{
    public class SupplierService:Service<Supplier>,ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IMapper _mapper;

        public SupplierService(IGenericRepository<Supplier> repository, IUnitOfWork unitOfWork, ISupplierRepository supplierRepository,IMapper mapper ):base(repository,unitOfWork)
        {
            _supplierRepository = supplierRepository;
            _mapper = mapper;
        }
    }
}
