﻿using MediatR;

namespace HtmlBuilder.API.CQRS.Page.Add
{
    public class AddComponentCommandHandler : IRequestHandler<AddPageCommandRequest, AddPageCommandResponse>
    {
        private readonly AppDbContext _dbContext;

        public AddComponentCommandHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AddPageCommandResponse> Handle(AddPageCommandRequest request, CancellationToken cancellationToken)
        {
            var page = new Entities.Page();

            page.Id = request.Id;
            page.Name = request.Name;

            _dbContext.Pages.Add(page);

            await _dbContext.SaveChangesAsync();

            return new AddPageCommandResponse { };
        }
    }
}
