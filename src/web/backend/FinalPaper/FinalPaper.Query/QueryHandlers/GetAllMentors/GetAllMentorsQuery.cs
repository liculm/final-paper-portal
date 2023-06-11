using FinalPaper.Domain.ViewModels;
using FinalPaper.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FinalPaper.Domain.Enums;

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

        public async Task<List<MentorViewModel>> Handle(GetAllMentorsQuery request, CancellationToken cancellationToken) {
            var mentorCourses = await context.Course
                .AsNoTracking()
                .Where(x => x.IsActive)
                .Select(x => new MentorViewModel(x.MentorId.ToString(), x.User.FirstName, x.User.LastName, 5, x.Id, x.Name))
                .ToListAsync(cancellationToken);

            var approvedThesis = await context.Thesis
                .AsNoTracking()
                .Where(x => x.ThesisStatusTypeId == ThesisStatusTypes.Approved.Id)
                .ToListAsync(cancellationToken);

            foreach (var course in mentorCourses) {
                course.AvailableNumberOfStudents = 10 - approvedThesis.Count(x => x.CourseId == course.CourseId); 
            }
            
            return mentorCourses;
        }
    }
}