using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PublicLibraryService.Application.Behaviors;
using PublicLibraryService.Application.Handlers;
using PublicLibraryService.Application.Queries;
using PublicLibraryService.Application.Validators;
using System.Reflection;

namespace PublicLibraryService.Application.Services
{
    public static class ApplicationServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(GetBookAvailabilityQuery).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetBookReadingRateQuery).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetBorrowedBooksByBorrowerIdQuery).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetHighValuedBorrowersQuery).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetMostBorrowedBooksQuery).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetRelatedBorrowedBooksQuery).Assembly);


                cfg.RegisterServicesFromAssembly(typeof(GetBookAvailabilityHandler).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetBookReadingRateHandler).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetBorrowedBooksByBorrowerIdHandler).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetHighValuedBorrowersHandler).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetMostBorrowedBooksHandler).Assembly);
                cfg.RegisterServicesFromAssembly(typeof(GetRelatedBorrowedBooksHandler).Assembly);


                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));


            services.AddValidatorsFromAssemblyContaining<GetBookAvailabilityQueryValidator>();
            services.AddValidatorsFromAssemblyContaining<GetBorrowedBooksByBorrowerIdQueryValidator>();
            services.AddValidatorsFromAssemblyContaining<GetHighValuedBorrowersQueryValidator>();
            services.AddValidatorsFromAssemblyContaining<GetMostBorrowedBooksQueryValidator>();
            services.AddValidatorsFromAssemblyContaining<GetRelatedBorrowedBooksQueryValidator>();


            return services;
        }
    }
}
