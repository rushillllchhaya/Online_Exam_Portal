using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using API.Models;
using API.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class ExamController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExamController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: List all exams
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var exams = await _context.Exams.Include(e => e.SubjectExam).ToListAsync();
            return View(exams);
        }

        // GET: View Exam Details
        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var exam = await _context.Exams
                .Include(e => e.SubjectExam)
                .Include(e => e.Questions)
                .FirstOrDefaultAsync(e => e.ExamID == id);

            if (exam == null)
                return NotFound();

            return View(exam);
        }

        // GET: Start an exam
        [HttpGet]
        public async Task<IActionResult> Attempt(int id)
        {
            var exam = await _context.Exams
                .Include(e => e.Questions)
                .FirstOrDefaultAsync(e => e.ExamID == id);

            if (exam == null)
                return NotFound();

            return View(exam);
        }

        // POST: Submit answers
        [HttpPost]
        public async Task<IActionResult> Submit(int examId, List<SubmissionModel> submissions)
        {
            if (!ModelState.IsValid)
                return View("Attempt", await _context.Exams.FindAsync(examId));

            foreach (var submission in submissions)
            {
                _context.Submissions.Add(submission);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Results", new { examId });
        }

        // GET: View exam results
        [HttpGet]
        public async Task<IActionResult> Results(int examId)
        {
            var results = await _context.Submissions
                .Include(s => s.Exam)
                .Where(s => s.ExamID == examId)
                .ToListAsync();

            if (results == null || results.Count == 0)
                return NotFound();

            return View(results);
        }
    }
}
