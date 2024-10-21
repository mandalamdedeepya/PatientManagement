using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatientManagement.Model;
using PatientManagement.Repository;

namespace PatientManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientRepository _repository;

        public PatientController(IPatientRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> GetAll()
        {
            var patients = await _repository.GetAllPatientsAsync();

            if (patients == null || !patients.Any())
            {
                return NotFound("No patient found.");
            }

            return Ok(patients);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetById(int id)
        {
            var patient = await _repository.GetPatientByIdAsync(id);

            if (patient == null)
            {
                return NotFound();
            }

            return Ok(patient);
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] Patient patient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingPatient = await _repository.GetPatientByIdAsync(patient.Id);
            if (existingPatient != null)
            {
                return Conflict("A patient with this ID already exists.");
            }

            await _repository.AddPatientAsync(patient);

            var createdResponse = CreatedAtAction(nameof(GetById), new { id = patient.Id }, patient);
            return createdResponse;
        }
    

        [HttpPut("{id}")]
        public async Task<ActionResult> Edit(int id, [FromBody] Patient patient)
        {
            if (id != patient.Id || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var existingPatient = await _repository.GetPatientByIdAsync(id);
            if (existingPatient == null)
            {
                return NotFound("Patient not found.");
            }

            await _repository.UpdatePatientAsync(patient);

            var noContentResponse = NoContent();
            return noContentResponse;
        }
    

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existingPatient = await _repository.GetPatientByIdAsync(id);
            if (existingPatient == null)
            {
                return NotFound("Patient not found.");
            }

            await _repository.DeletePatientAsync(id);

            var noContentResponse = NoContent();
            return noContentResponse;
        }
    }
}
