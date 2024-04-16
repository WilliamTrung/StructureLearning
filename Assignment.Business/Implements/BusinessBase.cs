using Assignment.Business.Abstractions;
using Assignment.Data.UnitOfWork;
using Assignment.Shared.Provider.Abstractions;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Business.Implements
{
    public class BusinessBase : IBusiness
    {
        protected readonly IUnitOfWorkService _unitOfWorkService;
        protected readonly ICoreProvider _coreProvider;
        protected readonly IMapper _mapper;
        protected BusinessBase(IUnitOfWorkService unitOfWorkService,
          ICoreProvider coreProvider)
        {
            _unitOfWorkService = unitOfWorkService;
            _mapper = coreProvider.Mapper;
            _coreProvider = coreProvider;
        }
    }
}
