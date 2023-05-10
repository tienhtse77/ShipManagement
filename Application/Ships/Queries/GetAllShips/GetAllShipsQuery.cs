using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Application.Ships.Queries.GetAllShips
{
    public record GetAllShipsQuery : IRequest<List<ShipDto>>
    {
    }

    internal sealed class GetAllShipsQueryHandler : IRequestHandler<GetAllShipsQuery, List<ShipDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllShipsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ShipDto>> Handle(GetAllShipsQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<ShipDto>>(await _context.Ships.AsNoTracking().ToListAsync());
        }
    }
}
