using Karne.API.Data;
using Karne.API.DTOs;
using Karne.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Karne.API.Services
{
    public class EvaluationService : IEvaluationService
    {
        private readonly ApplicationDbContext _context;

        public EvaluationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task EvaluateExamAsync(int examId, List<ParsedResultDto> results)
        {
            var examQuestions = await _context.ExamQuestions
                .Where(q => q.ExamId == examId)
                .OrderBy(q => q.QuestionNo)
                .ToListAsync();

            if (!examQuestions.Any())
            {
                throw new InvalidOperationException("Exam content not found (Questions or Answer Key missing).");
            }

            foreach (var result in results)
            {
                if (!result.IsValid) continue; // Skip invalid lines or log them

                // Find or Create Student
                var student = await _context.Students.FirstOrDefaultAsync(s => s.StudentNumber == result.StudentNumber);
                if (student == null)
                {
                    student = new Student { StudentNumber = result.StudentNumber, FullName = "Unknown" };
                    _context.Students.Add(student);
                    await _context.SaveChangesAsync();
                }

                // CHECK FOR USER LINK (Auto-Distribution Logic)
                // If a User exists with this StudentNumber (assuming we store it, or via StudentId link)
                // For now, let's assume we can find a User who claims this StudentNumber or let's say the Student entity itself is the link.
                // In Phase 1.5, we linked User -> StudentId. So we can check if any User has this StudentId.
                
                var userLinked = await _context.Users.FirstOrDefaultAsync(u => u.StudentId == student.Id);
                
                var examResult = new StudentExamResult
                {
                    ExamId = examId,
                    StudentId = student.Id,
                    ProcessedDate = DateTime.Now
                };
                
                // If we had a Notification feature, we would send it here:
                // if (userLinked != null) NotificationService.Notify(userLinked.Id, "New Exam Result Available!");

                int correct = 0;
                int wrong = 0;
                int empty = 0;

                // Evaluate answers
                // result.GivenAnswers string index 0 corresponds to QuestionNo 1
                // Be careful with index boundaries
                
                for (int i = 0; i < examQuestions.Count; i++)
                {
                    var question = examQuestions[i];
                    // Assuming QuestionNo is 1-based and sequential, and matches list index logic.
                    // Ideally, match by QuestionNo.
                    
                    char givenAnswerChar = ' ';
                    int answerIndex = question.QuestionNo - 1;

                    if (answerIndex < result.GivenAnswers.Length)
                    {
                        givenAnswerChar = result.GivenAnswers[answerIndex];
                    }

                    bool isCorrect = false;
                    string givenAnswerStr = givenAnswerChar.ToString().ToUpper();
                    
                    if (string.IsNullOrWhiteSpace(givenAnswerStr))
                    {
                        empty++;
                    }
                    else if (givenAnswerStr == question.CorrectAnswer)
                    {
                        correct++;
                        isCorrect = true;
                    }
                    else
                    {
                        wrong++;
                    }

                    examResult.Answers.Add(new StudentAnswer
                    {
                        QuestionNo = question.QuestionNo,
                        GivenAnswer = givenAnswerStr.Trim(),
                        IsCorrect = isCorrect
                    });
                }

                examResult.TotalCorrect = correct;
                examResult.TotalWrong = wrong;
                examResult.TotalEmpty = empty;
                
                // Simple Net Calculation: Correct - (Wrong / 4). Make this configurable later.
                decimal net = correct - (wrong / 4.0m);
                examResult.NetScore = net;
                
                // Simple Score Calculation: (Net / TotalQuestions) * 100
                if (examQuestions.Count > 0)
                    examResult.Score = (net / examQuestions.Count) * 100;

                _context.StudentExamResults.Add(examResult);
            }

            await _context.SaveChangesAsync();
        }
    }
}
