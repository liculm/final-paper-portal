using FinalPaper.Domain.ViewModels;
using FinalPaper.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FinalPaper.Query.QueryHandlers.GetAllMentors
{
    public sealed record GetAllMentorsQuery : IRequest<List<MentorViewModel>>;

    public sealed class GetAllMentorsQueryHandler : IRequestHandler<GetAllMentorsQuery, List<MentorViewModel>>
    {
        private readonly FinalPaperDBContext context;

        public GetAllMentorsQueryHandler(FinalPaperDBContext context)
        {
            this.context = context;
        }

        public async Task<List<MentorViewModel>> Handle(GetAllMentorsQuery request, CancellationToken cancellationToken)
        {
            var mentors = await context.Users
                .AsNoTracking()
                .Where(x => x.IsActive && x.Role.Id == 2)
                .Select(x => new MentorViewModel(x.Id.ToString(), x.FirstName, x.LastName, 5, null, null))
                .ToListAsync(cancellationToken);

            foreach (var mentor in mentors)
            {
                var courses = await context.Course
                    .AsNoTracking()
                    .Where(course => course.MentorId.ToString() == mentor.Id)
                    .ToListAsync(cancellationToken);

                if (courses.Any())
                {
                    mentor.CourseId = courses.First().Id.ToString();
                    mentor.CourseName = courses.First().Name;
                }
            }

            return mentors;
        }
    }
}