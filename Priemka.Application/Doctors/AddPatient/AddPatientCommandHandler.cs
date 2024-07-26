using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Priemka.Application.DataAccess;
using Priemka.Domain.Common;
using Priemka.Domain.Entities;
using Priemka.Domain.Interfaces;
using Priemka.Domain.ValueObjects;
using System.Data;
using System.Security.Claims;

namespace Priemka.Application.Doctors.AddPatient
{
    public class AddPatientCommandHandler : IRequestHandler<AddPatientCommand, Result<Guid>>
    {
        private readonly IDoctorsRepository _repository;
        private readonly IPatientRepository _patientRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AddPatientCommandHandler(IDoctorsRepository repository, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IPatientRepository patientRepository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _patientRepository = patientRepository;
        }
        public async Task<Result<Guid>> Handle(AddPatientCommand request, CancellationToken cancellationToken)
        {
            //var userId = _httpContextAccessor.HttpContext.User.FindFirst(Permissions.Patients.Create);
            var doctor = await _repository.GetById(request.DoctorId, cancellationToken);
            if (doctor.IsFailed) return doctor.ToResult();

            var fullName = FullName.Create(request.FirstName, request.LastName);
            if(fullName.IsFailed) return fullName.ToResult();

            var phone = Phone.Create(request.Phone);
            if(phone.IsFailed) return phone.ToResult(); 

            var email = Email.Create(request.Email);
            if(email.IsFailed) return email.ToResult();

            var address = Address.Create(request.Street, request.City);
            if(address.IsFailed) return address.ToResult();

            var patient = Patient.Create(     
                fullName.Value, 
                phone.Value, 
                email.Value, 
                address.Value, 
                request.Age,
                request.BirthDate, 
                request.Trouble, 
                request.Description);

            doctor.Value.AddPatient(patient.Value);
            
            await _patientRepository.AddAsync(patient.Value, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            //catch (DbUpdateConcurrencyException ex)
            //{
            //    throw new Exception(ex.Message);
            //}

            return doctor.Value.Id;
        }
    }
}
