﻿using AutoMapper;
using HtmlBuilder.API.CQRS.User.GetAll;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HtmlBuilder.API.CQRS.Page.GetAll
{
    public class GetAllPageQueryHandler : IRequestHandler<GetAllPageQueryRequest, GetAllPageQueryResponse>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetAllPageQueryHandler(AppDbContext dbContext, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<GetAllPageQueryResponse> Handle(GetAllPageQueryRequest request, CancellationToken cancellationToken)
        {
            var userName = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _dbContext.Pages.ToListAsync();

            var data = _mapper.Map<List<GetAllPageDto>>(result);

            return new GetAllPageQueryResponse() { Data = data };
        }
    }
}