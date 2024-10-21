using Microsoft.EntityFrameworkCore;
using PatientManagement.Data;
using PatientManagement.Model;

namespace PatientManagement.Repository
{
    public class PatientRepository : IPatientRepository
    {
        private readonly PatientContext _context;

        public PatientRepository(PatientContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Patient>> GetAllPatientsAsync()
        {
            var patients = await _context.Patients.ToListAsync();
            return patients;
        }

        public async Task<Patient> GetPatientByIdAsync(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            return patient;
        }

        public async Task AddPatientAsync(Patient patient)
        {
            await _context.Patients.AddAsync(patient);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePatientAsync(Patient patient)
        {
            _context.Entry(patient).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeletePatientAsync(int id)
        {
            var patient = await _context.Patients.FindAsync(id);

            if (patient != null)
            {
                _context.Patients.Remove(patient);
                await _context.SaveChangesAsync();
            }
        }
    }
}
